using System;

namespace blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;     //ustawienie kodowania które zawiera symbole kart(tzn serce pik etc)
            Gra gra = new Gra();
            int poczatkowaLiczbaZetonow = 2000;
            int stawianaKwota;
            char wybor;
            char grajWyjdz;
            gra.talia.wypelnij();
            gra.talia.wypelnij();
            gra.talia.potasujKarty();
            gra.dodajGracza(new Gracz(poczatkowaLiczbaZetonow, "Gracz"));                   //dodajemy do gry gracza z poczatkowa liczba zetonow(2000)
            for(; ; )
            {
                Console.Clear();
                if (gra.gracz.zetony == 0)
                {
                    Console.WriteLine("Masz 0 zetonow, przegrales. Kliknij dowolny przycisk zaby zaczac od nowa");
                    gra.gracz.zetony = poczatkowaLiczbaZetonow;
                    Console.ReadKey();
                }
                Console.Clear();
                Console.Write("Posiadane zetony: {0}\n", gra.gracz.zetony);
                Console.WriteLine("[1] Graj");
                Console.WriteLine("[2] Wyjdz");
                grajWyjdz = Console.ReadKey().KeyChar;

                if (grajWyjdz == '2')                                                       //jesli gracz wybiera opcje wyjdz to wychodzimy z programu
                    return;

                gra.przywrocUstawieniaPoczatkowe();                                         //przed rozpoczeciem kazdej gry musimy przywrocic ustawienia poczatkowe, tzn posiadane karty, sume punktow etc
                do
                {
                    Console.Clear();
                    Console.Write("Posiadane zetony: {0}\n", gra.gracz.zetony);
                    Console.Write("Ile zetonow chcesz postawic: ");
                    stawianaKwota = int.Parse(Console.ReadLine());
                } while (stawianaKwota > gra.gracz.zetony);

                gra.rozpocznijGre(stawianaKwota);
                gra.pokazStanGry();
                if (gra.gracz.sumaPunktow == 21)
                {
                    gra.dodawajKartyKrupierowi();
                    gra.pokazStanGry();
                    gra.zakonczGre();
                }
                    

                while (gra.trwanieRozgrywki)                                    //Gdy dana runda trwa, stan trwanie rozgrywki zmienia sie na false w przypadku wywolania funkcji gra.zakonczGre()
                {
                    Console.WriteLine("[d] Dobierz karte");
                    if (gra.numerWyboru == 0)
                        Console.WriteLine("[p] Podwoj stawke i dobierz karte");
                    Console.WriteLine("[s] Spasuj");
                    wybor = Console.ReadKey().KeyChar;
                    switch (wybor)
                    {
                        case 'd':
                            gra.dobierzKolejnaKarte();
                            gra.pokazStanGry();
                            if (gra.gracz.powyzej21() || gra.gracz.sumaPunktow == 21)
                            {
                                gra.dodawajKartyKrupierowi();
                                gra.zakonczGre();
                            }
                            break;
                        case 'p':
                            if (gra.numerWyboru == 0)                        //funkcja podwojenia stawki mozlia jest tylko przy pierwszym wyborze, tzn niemozliwa jest gdy dobierzemy wiecej niz 2 karty
                            {
                                gra.podwojStawke();
                            }
                            gra.dodawajKartyKrupierowi();
                            gra.pokazStanGry();
                            gra.zakonczGre();
                            break;
                        case 's':
                            gra.dodawajKartyKrupierowi();
                            gra.pokazStanGry();
                            gra.zakonczGre();
                            break;
                        default:
                            gra.pokazStanGry();
                            break;
                    }
                }
                Console.WriteLine("Wcisnij dowolny przycisk aby kontynuowac...");
                Console.ReadKey();
            }
        }
    }
}
