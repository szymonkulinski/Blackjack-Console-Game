using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack
{
    public class Gra
    {
        public Talia talia = new Talia();
        public Gracz gracz = new Gracz();
        public KrupierAI krupier = new KrupierAI();
        int ktoraDacKarte;
        public int postawionaSuma;
        public int stawka;
        public short numerWyboru;
        public bool trwanieRozgrywki = true;
        public Gra()
        {
            ktoraDacKarte = 0;
        }
        public void zakonczGre()
        {
            trwanieRozgrywki = false;
            if(czyWygrana())                             //sprawdzamy czy gra jest wygrana
            {
                if (krupier.stopienRyzyka > 0)          //stopien ryzyka nie moze byc ujemy -- minimalny prog dobierania wynosi 15,
                    krupier.stopienRyzyka--;
                Console.WriteLine("Brawo! Wygrales");
                gracz.zetony += stawka;
            }
            else if(czyRemis())                          //sprawdzamy czy remis                                              
            {
                Console.WriteLine("Remis!");
                gracz.zetony += postawionaSuma;
            }
            else                                        //w innym wypadku przegrana
            {
                if (krupier.stopienRyzyka < 3)         //jesli stipienRyzyka nie przekroczy 3 to dodajemy do niego 1. Zabezpieczenie aby nie przekroczylo 18
                    krupier.stopienRyzyka++;
                Console.WriteLine("Przegrales!");
            }
        }
        public bool czyWygrana()
        {
            if (gracz.powyzej21())
                return false;
            if ((krupier.sumaPunktow > gracz.sumaPunktow) && krupier.sumaPunktow <= 21)
                return false;
            if (krupier.sumaPunktow == gracz.sumaPunktow)
                return false;
            return true;
        }
        public bool czyRemis()
        {
            if ((krupier.sumaPunktow == gracz.sumaPunktow) || (gracz.powyzej21() && krupier.powyzej21()))
                return true;
            return false;
        }
        public void dodajGracza(Gracz gracz_)
        {
            gracz = gracz_;
        }
        public void dodawajKartyKrupierowi()
        {
            while (krupier.sumaPunktow < (krupier.doIluDobierac + krupier.stopienRyzyka))           //krupier przestaje grac w momencie gdy przekroczyl swoj prog dobierania
            {
                dodajKarteKrupierowi();
            }
        }
        public void dobierzKolejnaKarte()
        {
            gracz.dodajKarte(talia.karty[ktoraDacKarte]);       //dodajemy graczowi jedna karte
            ktoraDacKarte += 1;
            numerWyboru += 1;
        }
        public void dodajKarteKrupierowi()
        {
            krupier.dodajKarte(talia.karty[ktoraDacKarte]);
            ktoraDacKarte += 1;
        }
        public void podwojStawke()
        {
            dobierzKolejnaKarte();
            stawka += postawionaSuma;
            dodawajKartyKrupierowi();
        }
        public void przywrocUstawieniaPoczatkowe()
        {
            talia.potasujKarty();
            trwanieRozgrywki = true;
            gracz.posiadaneKarty.Clear();
            gracz.sumaPunktow = 0;
            krupier.posiadaneKarty.Clear();
            krupier.sumaPunktow = 0;
        }
        public void rozpocznijGre(int postawionaSuma_)
        {
            przywrocUstawieniaPoczatkowe();

            postawionaSuma = postawionaSuma_;
            gracz.zetony -= postawionaSuma;
            stawka = postawionaSuma * 2;
            krupier.dodajKarte(talia.karty[ktoraDacKarte]);      //dodajemy karte krupierowi
            ktoraDacKarte += 1;

            gracz.dodajKarte(talia.karty[ktoraDacKarte]);        //dodajemy dwie karty graczowi
            ktoraDacKarte += 1;
            gracz.dodajKarte(talia.karty[ktoraDacKarte]);        //dodajemy dwie karty graczowi
            ktoraDacKarte += 1;
        }
        public void pokazStanGry()
        {
            Console.Clear();
            Console.WriteLine("Zetony gracza: {0} ", gracz.zetony);
            Console.WriteLine("Stawka: {0}", stawka);
            Console.WriteLine("Karty {0}(Suma: {1}): ", krupier.nazwa, krupier.sumaPunktow);
            krupier.pokazKarty();
            Console.WriteLine("Karty {0}(Suma: {1}): ", gracz.nazwa, gracz.sumaPunktow);
            gracz.pokazKarty();
        }
    }
}


