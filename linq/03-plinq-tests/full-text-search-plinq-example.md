To enhance performance, I took advantage of LINQ's `AsParallel()` method for operations that were independent and read-only. This change significantly improved the speed; for example, the search time for patient JF1000 and the term "physician" dropped from 125 seconds to just 35 seconds.

Here is the internal class structure that was used:

```csharp
internal sealed class FullTextSearchService : IFullTextSearchService
{
    private readonly IFullTextEngine _fullTextEngine;
    private readonly IFullTextDataAccessProvider _fullTextDataAccessProvider;
    private readonly IFullTextRepository _fullTextRepository;
    private readonly IFullTextDocumentRenditionProvider _fullTextDocumentRenditionProvider;
    private readonly IFullTextDocumentDataProvider _fullTextDocumentDataProvider;
    private readonly ISession _session;

    private readonly ILegacyQueryFactory _legacyQueryFactory;

    private static readonly IEnumerable<FullTextPageData> EmptyFullTextPageData = ImmutableArray<FullTextPageData>.Empty;

    public FullTextSearchService(
        IFullTextDataAccessProvider fullTextDataAccessProvider,
        IFullTextEngine fullTextEngine,
        IFullTextRepository fullTextRepository,
        ILegacyQueryFactory legacyQueryFactory,
        IFullTextDocumentRenditionProvider fullTextDocumentRenditionProvider,
        IFullTextDocumentDataProvider fullTextDocumentDataProvider,
        ISession session)
    {
        _fullTextDataAccessProvider = fullTextDataAccessProvider ?? throw new ArgumentNullException(nameof(fullTextDataAccessProvider));
        _fullTextEngine = fullTextEngine ?? throw new ArgumentNullException(nameof(fullTextEngine));
        _fullTextRepository = fullTextRepository ?? throw new ArgumentNullException(nameof(fullTextRepository));
        _legacyQueryFactory = legacyQueryFactory ?? throw new ArgumentNullException(nameof(legacyQueryFactory));
        _fullTextDocumentRenditionProvider = fullTextDocumentRenditionProvider ?? throw new ArgumentNullException(nameof(fullTextDocumentRenditionProvider));
        _fullTextDocumentDataProvider = fullTextDocumentDataProvider ?? throw new ArgumentNullException(nameof(fullTextDocumentDataProvider));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public ImmutableArray<(long documentId, FullTextSearchResults)> ExecuteFTSQuerySearch(string query, IImmutableList<long> docIds)
    {
        using (_session.Security.OverrideKeywordSecurityChecks())
        using (_session.Security.OverrideDocumentQuerySecurityChecks())
        {
            var results = BuildLegacyQuery(docIds, query).Execute();
            return GetFullTextSearchResults(results, query);
        }
    }

    private ILegacyQuery BuildLegacyQuery(IImmutableList<long> docIds, string searchString)
    {
        var legacyQuery = _legacyQueryFactory.Create();

        legacyQuery.SetSearchTerm(searchString);

        if (docIds.Any())
        {
            legacyQuery.AddIDListConstraint(docIds);
        }

        return legacyQuery;
    }

    private ImmutableArray<(long documentId, FullTextSearchResults)> GetFullTextSearchResults(ServerResults results, string query)
    {
        return GetTopScoredResults(results)
            .AsParallel()
            .Select(serverResult =>
            {
                var contentIndexIdentifier = CreateContentIndexFrom(serverResult);
                return (
                    serverResult.ID,
                    new FullTextSearchResults
                    {
                        PaginatedDocumentIndexProperties = new PaginatedDocumentIndexProperties
                        {
                            VersionId = contentIndexIdentifier.VersionID,
                            FileTypeId = contentIndexIdentifier.FileTypeID,
                            RevisionId = contentIndexIdentifier.RevisionID
                        },
                        HitCount = serverResult.HitCount,
                        FirstHitSynopsis = serverResult.Summary,
                        FullTextPageDataItems = GetFullTextPageHitData(query, contentIndexIdentifier).ToImmutableArray()
                    });
            }).ToImmutableArray();
    }

    private static IEnumerable<ServerResult> GetTopScoredResults(ServerResults results) => results.Where(grp => grp.HighestScore != null).Select(grp => grp.HighestScore);

    private static ContentIndexIdentifier CreateContentIndexFrom(ServerResult serverResult)
    {
        return new ContentIndexIdentifier(
            serverResult.Flags == PDFPagination ? PaginatedContent : StandardContent,
            serverResult.ID,
            serverResult.DocumentTypeID)
        {
            VersionID = serverResult.VersionID ?? 0,
            FileTypeID = serverResult.RenditionID,
            RevisionID = serverResult.RevisionID
        };
    }

    private IEnumerable<IFullTextPageData> GetFullTextPageHitData(string query, ContentIndexIdentifier indexId)
    {
        if (indexId == null) throw new ArgumentNullException(nameof(indexId));
        if (indexId.IdentifierType != PaginatedContent) return EmptyFullTextPageData;

        var ftDocument = _fullTextDataAccessProvider.Get().GetFullTextObjectID(
            indexId.ID,
            indexId.RevisionID,
            indexId.FileTypeID);

        var index = _fullTextRepository.GetIndex(_session, ftDocument.CatalogID);

        return _fullTextEngine.GetPagesWithHits(index.Location, ftDocument.FtObjectID, query, indexId)
            .OrderBy(pageNum => pageNum)
            .AsParallel()
            .AsOrdered()
            .Select(pageNumber =>
            {
                var highlightCoordinates =
                    _fullTextEngine.GetMatchingPaginatedCoordinates(
                            index,
                            ftDocument.FtObjectID,
                            pageNumber,
                            query,
                            indexId)
                        .Select(rectangle =>
                            new FullTextHitHighlightCoordinates
                            {
                                X = rectangle.X,
                                Y = rectangle.Y,
                                Width = rectangle.Width,
                                Height = rectangle.Height
                            });

                return BuildFullTextPageData(indexId, pageNumber, highlightCoordinates);
            });
    }

    private FullTextPageData BuildFullTextPageData(
        ContentIndexIdentifier indexId,
        int pageNumber,
        IEnumerable<IFullTextHitHighlightCoordinates> highlightCoordinates)
    {
        var pageData = GetPageData(pageNumber, indexId);

        return new FullTextPageData
        {
            PageNumber = pageNumber + 1, // convert to 1-based
            HitHighlightCoordinates = highlightCoordinates.ToImmutableArray(),
            PageImageHeight = pageData.ImageProperties.ImageHeight,
            PageImageWidth = pageData.ImageProperties.ImageWidth,
            HorizontalDpi = pageData.ImageProperties.HorizontalDPI,
            VerticalDpi = pageData.ImageProperties.VerticalDPI
        };
    }

    private PageData GetPageData(int pageNumber, ContentIndexIdentifier contentIndexIdentifier)
    {
        var getPageProperties = new GetPageProperties()
        {
            GeneralProperties =
            {
                PageNumber = pageNumber,
                ContentType = ContentType.PngImage
            }
        };

        var rendition = _fullTextDocumentRenditionProvider.GetRendition(_session, contentIndexIdentifier);

        return _fullTextDocumentDataProvider.GetPage(_session, rendition, pageNumber, getPageProperties);
    }
}
```

![Full Text Search Results](./hcw-full-text-demo.png)
