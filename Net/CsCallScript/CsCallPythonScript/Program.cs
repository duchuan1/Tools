using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace CsCallPythonScript
{
    class Program
    {
        static void Main(string[] args)
        {
            //调用字符串
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var source = engine.CreateScriptSourceFromString(
                "def add(left, right):\n" +
                "   return left + right\n" +
                "\n" +
                "class MyClass(object):\n" +
                "   def __init__(self, value):\n" +
                "       self.value = value\n");
            source.Execute(scope);
            var adder = scope.GetVariable<Func<object, object, object>>("add");
            Console.WriteLine(adder(2, 2));
            Console.WriteLine(adder(2.0, 2.5));

            var myClass = scope.GetVariable<Func<object, object>>("MyClass");
            var myInstance = myClass("hello");

            Console.WriteLine(engine.Operations.GetMember(myInstance, "value")); ;

            /*
            //直接调用脚本
            ScriptRuntime pyRunTime = Python.CreateRuntime();
            dynamic obj = pyRunTime.UseFile("hello.py");
            Console.Write(obj.welcome("Nick"));
            Console.ReadKey();
            */
            Console.ReadKey();
        }
    }
}
