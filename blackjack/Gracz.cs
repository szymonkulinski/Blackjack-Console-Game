using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack
{
    public class Gracz : GraczArchetyp
    {
        public int zetony { get; set; }
        public int zaklad { get; set; }
        public Gracz()
        {
            zetony = 0;
            nazwa = "Gracz";
            zaklad = 0;
            sumaPunktow = 0;
        }
        public Gracz(int zetony_, string nazwa_)
        {
            zetony = zetony_;
            nazwa = nazwa_;
            zaklad = 0;
            sumaPunktow = 0;
        }
    }
}
