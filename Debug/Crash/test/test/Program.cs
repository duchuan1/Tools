using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static int add(int left, int right)
        {
            int v1 = left;
            int v2 = right;
            int rc = 0;

            for (int i = 0; i < 20; i++)
            {
                rc = v1 + v2;
                Task.Delay(100).GetAwaiter().GetResult();
                Console.WriteLine(rc);
            }

            throw new Exception();
            return rc / 0;
        }
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                add(1, 0);
            });
            Console.Read();
        }
    }
}
