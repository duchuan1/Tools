using MsieJavaScriptEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CsCallJavaScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            using (var jsEngine = new MsieJsEngine())
            {
                //jsEngine.ExecuteFile(string.Format(@"{0}/Scripts/myscript.js", basePath));
                jsEngine.Execute("function add(left, right){ return (left + right) & 0xFFFFFF; }");
                string[] arr = new string[] { "1", "2"};
                var res = Convert.ToInt32(jsEngine.CallFunction("add", arr));
                Console.WriteLine(res);
                Console.ReadKey();
            }
        }
    }
}
