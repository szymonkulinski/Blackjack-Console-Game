using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack
{
    public class Talia
    {
        private Random random = new Random();
        public Karta[] karty = new Karta[52];
        private char[] kolory = { '\u2660', '\u2665', '\u2666', '\u2663' };     // wartosci ascii kolorow
        private String[] wartosci = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public void wypelnij()                              
        {
            int idKarty = 0;
            while (idKarty < 52)                        // musimy mieć 52 karty
            {
                for (int idKolor = 0; idKolor < 4; idKolor++)                         // dostepne sa 4 kolory
                {
                    for (int idOznaczenie = 0; idOznaczenie < 13; idOznaczenie++)                    // dostepne jest 13 wartosci
                    {
                        if (idOznaczenie == 12)
                        {
                            karty[idKarty] = new Karta(11, kolory[idKolor], wartosci[idOznaczenie]);    //jezeli karta to ass -- wartosc = 11;
                        }
                        else if (idOznaczenie > 8)
                        {
                            karty[idKarty] = new Karta(10, kolory[idKolor], wartosci[idOznaczenie]);    //jezeli karta to jopek, dama, krol -- wartosc = 10;
                        }
                        else
                        {
                            karty[idKarty] = new Karta(idOznaczenie + 2, kolory[idKolor], wartosci[idOznaczenie]); //w innym wypadku -- wartosc = oznaczenie
                        }
                        idKarty++;
                    }
                }
            }
        }
        public void potasujKarty()
        {
            for (int i = 0; i < 52; i++)
            {
                int randomInt = random.Next(52);
                (karty[randomInt], karty[i]) = (karty[i], karty[randomInt]);        //zamieniamy wartość stojącą na miejscu i z karta na miejscu losowym od 0 do 51
            }
        }
    }
}
