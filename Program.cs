using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace ProGrafica
{
    class Program
    {
        static void Main(string[] args)
        {
            using (game nw = new game(800, 700, "prueba"))
            {
                nw.Run(60.0);
            }
        }
    }
}
