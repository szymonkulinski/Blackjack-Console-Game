using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack
{
    public class Karta
    {
        public int wartosc { get; set; }
        public char kolor { get; set; }
        public string oznaczenie { get; set; }

        public Karta(int wartosc_, char kolor_, string oznaczenie_)
        {
            wartosc = wartosc_;
            kolor = kolor_;
            oznaczenie = oznaczenie_;
        }
        public void narysujKarte()
        {
            Console.WriteLine("┌─────────┐");
            if (oznaczenie == "10")
                Console.WriteLine("|{0}{1}      |", oznaczenie, kolor);
            else
                Console.WriteLine("|{0}{1}       |", oznaczenie, kolor);
            Console.WriteLine("|         |");
            Console.WriteLine("|         |");
            Console.WriteLine("|    {0}    |", kolor);
            Console.WriteLine("|         |");
            Console.WriteLine("|         |");
            if (oznaczenie == "10")
                Console.WriteLine("|      {0}{1}|", oznaczenie, kolor);
            else
                Console.WriteLine("|       {0}{1}|", oznaczenie, kolor);
            Console.WriteLine("└─────────┘");
        }
    }
}
