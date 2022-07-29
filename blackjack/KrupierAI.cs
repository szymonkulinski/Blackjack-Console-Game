using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack
{
    public class KrupierAI : GraczArchetyp
    {
        public short doIluDobierac = 15;                //liczba podstawowa do ktorej krupier dopiera karte
        public short stopienRyzyka = 0;                 //liczba dodajaca sie do liczby do ktorej krupier dobiera liczbe, zwieksza sie gdy przegrywa sprawiajac ze 
                                                        //dobiera do większego progu, gdy wygrywa to próg sie zmniejsza. Prog dochodzi maksymalnie do 18.
        public KrupierAI()
        {
            sumaPunktow = 0;
            nazwa = "Krupier";
        }
    }
}
