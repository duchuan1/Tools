using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CsCallString
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            var data = dt.Compute("(1 + 1) * 3 / 2", "");
        }
    }
}
