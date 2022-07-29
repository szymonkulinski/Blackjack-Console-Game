using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack
{
    public class GraczArchetyp
    {
        public List<Karta> posiadaneKarty = new List<Karta>();
        public string nazwa { get; set; }
        public int sumaPunktow { get; set; }
        public void dodajKarte(Karta karta)
        {
            posiadaneKarty.Add(karta);
            sumaPunktow += karta.wartosc;
        }
        public void wyzerujKarty()
        {
            posiadaneKarty.Clear();
        }
        public void pokazKarty()
        {
            foreach (var item in posiadaneKarty)
            {
                item.narysujKarte();
            }
        }
        public bool powyzej21()
        {
            if (sumaPunktow > 21)
                return true;
            return false;
        }
    }
}
