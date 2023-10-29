namespace _01_delegates_test;

public class DelegatesTests
{
    private delegate int MyIntFunction(int x);

    private delegate void MyAction(ref bool value);

    private static int Square(int x)
    {
        return x * x;
    }

    private static int Add(int x)
    {
        return x + x;
    }

    private static int MyIntExecutor(MyIntFunction myIntFunction, int value)
    {
        return myIntFunction(value);
    }

    private static int MyIntInvoker(MyIntFunction myIntFunction, int value)
    {
        return myIntFunction.Invoke(value);
    }

    private static void Toggle(ref bool value)
    {
        value = !value;
    }

    [Test]
    public void MyIntFunctionTest()
    {
        int LocalSquare(int x)
        {
            return x * x;
        }

        MyIntFunction square1 = Square;

        // evolution of the lambda (fat arrow =>) function 
        MyIntFunction square2 = delegate(int x) { return x * x; };
        MyIntFunction square3 = x => { return x * x; };
        MyIntFunction square4 = x => x * x;

        MyIntFunction square5 = LocalSquare;

        Assert.That(Square(3), Is.EqualTo(9), $"{nameof(Square)} failed.");
        Assert.That(LocalSquare(3), Is.EqualTo(9), $"{nameof(LocalSquare)} failed.");
        Assert.That(square1(3), Is.EqualTo(9), $"{nameof(square1)} failed.");
        Assert.That(square2(3), Is.EqualTo(9), $"{nameof(square2)} failed.");
        Assert.That(square3(3), Is.EqualTo(9), $"{nameof(square3)} failed.");
        Assert.That(square4(3), Is.EqualTo(9), $"{nameof(square4)} failed.");
        Assert.That(square5(3), Is.EqualTo(9), $"{nameof(square5)} failed.");

        Assert.That(MyIntExecutor(square1, 3), Is.EqualTo(9), $"{nameof(MyIntExecutor)} failed.");
        Assert.That(MyIntInvoker(Add, 3), Is.EqualTo(6), $"{nameof(Add)} failed.");

        Assert.That(MyIntInvoker(i => i + i + i, 3), Is.EqualTo(9));
    }

    [Test]
    public void MyActionTest()
    {
        MyAction toggle = Toggle;

        var value = true;

        toggle(ref value);

        Assert.That(value, Is.False);
    }

    [Test]
    public void MyActionMultiCastTest()
    {
        var value = true; // true

        MyAction toggles = Toggle; // false
        toggles += Toggle; // true
        toggles += Toggle; // false
        toggles += (ref bool b) => b = !b; //true 

        toggles(ref value);

        Assert.That(value, Is.True); //actions are invoked in the order they were added
    }

    [Test]
    public void FuncTest()
    {
        int LocalFunc()
        {
            return 5;
        }

        Func<int> localFunc = LocalFunc;
        Func<int> func = () => 5;
        Func<int, int> func1 = i => i * 5;
        Func<int, int, int> func2 = (i, j) => i + j; // last type is the return type

        Assert.Multiple(() =>
        {
            Assert.That(localFunc(), Is.EqualTo(5));
            Assert.That(func(), Is.EqualTo(5));
            Assert.That(func1(5), Is.EqualTo(25));
            Assert.That(func2(5, 5), Is.EqualTo(10));
        });
    }

    [Test]
    public void ActionTest()
    {
        //custom assertions example
        Action<string> assertIsHelloWorld = s => Assert.That(s, Is.EqualTo("Hello World"));
        Action<string, int> assertHelloWorldSize = (s, i) => Assert.That(i, Is.EqualTo(s.Length));

        assertIsHelloWorld("Hello World");
        assertHelloWorldSize("Hello World", 11);
    }
}