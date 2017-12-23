using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    class Program
    {
        static void Main(string[] args)
        {
            Pos pos = new Pos();
            pos.DefaultInit();
            pos.Begin();
        }
    }
}
