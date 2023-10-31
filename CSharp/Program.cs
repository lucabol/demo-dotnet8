B("Interceptors");
var myClass = new MyClass();
myClass.SayHello(); // this prints "Hello from the interceptor!"
myClass.SayHello(); // this prints "Hello from MyClass"

B("Fixed size arrays");
Buffer<int> buffer = new();
for (int i = 0; i < 10; i++) buffer[i] = i;
foreach (var i in buffer) Console.Write($"{i} "); Console.WriteLine();

B("Primary Constructors");
Bigger b = new(2, 3, new ConsolePrinter());
b.PrintBigger();

B("Colletion expressions");
int[] row0                 = [1, 2, 3];
ReadOnlySpan<int> row1     = [4, 5, 6];
IEnumerable<int> row2      = [7, 8, 9];
List<int> single           = [..row0, ..row1, ..row2];
foreach(var s in single) Console.Write($"{s} ");
Console.WriteLine(Avg(single));
Console.WriteLine(Avg([1,2,3,4,5,6,7,8,9]));

B("Generic Math");
var iv = Avg([1,2,3]);
var dv = Avg([1.1,2.2,3.3]);
Console.WriteLine($"Int:{iv} Double:{dv}");

static T Avg<T>(IEnumerable<T> ns) where T : System.Numerics.INumber<T> {
        T sum   = T.Zero;
        T count = T.Zero;
        foreach(var n in ns) {sum = sum + n; count++;}
        return sum / count;
}

void B(string s) => Console.WriteLine($"\n{s.ToUpper()}");

interface IPrinter { void Print(int i); }
class ConsolePrinter: IPrinter { public void Print(int i) => Console.WriteLine($"Bigger: {i}");}

class Bigger(int x, int y, IPrinter p) {
    public void PrintBigger() => p.Print(x > y ? x : y);
}

[System.Runtime.CompilerServices.InlineArray(10)]
public struct Buffer<T> {
    private T _element0;
}

public class MyClass {
    public void SayHello() {
        Console.WriteLine("Hello from MyClass !");
    }
}


namespace MyInterceptors {
    public static class InterceptorClass {
        [System.Runtime.CompilerServices.InterceptsLocation("/home/lucabol/dev/demo-dotnet8/CSharp/Program.cs", line: 3, character: 9)]
        public static void InterceptorMethod(this MyClass myClass)
        {
            Console.WriteLine("Hello from the interceptor !");
        }
    }
}

#pragma warning disable 9113

namespace System.Runtime.CompilerServices {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    sealed class InterceptsLocationAttribute(string filePath, int line, int character) : Attribute
    {
    }
}

