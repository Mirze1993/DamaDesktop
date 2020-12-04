using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
   public class Kordinat
    {
        int x;int y;

        public Kordinat()
        {
        }

        public Kordinat(int x, int y)
        {
            X = x;
            Y = y;           
        }


        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }


        bool damka;

        public bool Damka { get => damka; set => damka = value; }

    }
}
