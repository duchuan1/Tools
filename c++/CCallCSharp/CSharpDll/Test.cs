using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDll
{
    public class Test
    {
        public int DoTesting(int x, int y, string testing, out string error)
        {
            error = testing + " -> testing is ok.";
            return x + y;
        }
    }
}
