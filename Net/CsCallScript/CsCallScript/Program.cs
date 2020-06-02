using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;

namespace CsCallScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = CSScript.CreateFunc<int>(@"int Product(int a, int b)
                                         {
                                             return a * b;
                                         }");
            int result = product(3, 4);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
