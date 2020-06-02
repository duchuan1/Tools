using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace CsCallLuaScript
{
    class Program
    {
        static void Main(string[] args)
        {
            Lua state = new Lua();

            state.DoString(@"
	            function add (left, right)
	                return left + right;
	            end
	            ");
            var scriptFunc = state["add"] as LuaFunction;
           // var res = scriptFunc.Call(3, 5)[0];
            var res = (long)(scriptFunc.Call(3, 5).First());
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
