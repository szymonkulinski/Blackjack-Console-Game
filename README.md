# Blackjack Console Game
Gra blackjack napisana w C#. Użytkownik rozpoczyna z 2000 żetonami. Użytkownik ma możliwość wyboru, jaką ilość żetonów postawić. Następnie zwizualizowane zostają karty zarówno gracza jak i kroupiera, a użytkownik wybiera czy chce dobrać karte, podwoić stawke i dobrać kartę, czy spasowac.

<p align="center">
  <img src="https://user-images.githubusercontent.com/56955430/181792562-012a3ccd-ad01-4ae0-a687-b46a1f8ccbbf.png" width="700">
</p>


# Kod

## Karta.cs
Klasa ta reprezentuje jedną kartę. Każda z kart jest reprezentowana przez atrybuty takie jak wartość karty, kolor oraz oznaczenie. Każda karta posiada również metodę do rysowania graficznej reprezentacji karty.
```cs
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
```
## Talia.cs
Klasa Talia reprezentuje zbiór 52 kart. Talia posiada metodę, która wypełnia ją wszystkimi rodzajami kart:
```cs
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
                            karty[idKarty] = new Karta(11, kolory[idKolor], wartosci[idOznaczenie]);    //jezeli karta to As -- wartosc = 11;
                        }
                        else if (idOznaczenie > 8)
                        {
                            karty[idKarty] = new Karta(10, kolory[idKolor], wartosci[idOznaczenie]);    //jezeli karta to Jopek, Dama, Krol -- wartosc = 10;
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
```
Oraz metodę służącą do tasowania Talii, w której każda z kart w talii zostaje zamieniona z inna kartą stojącą na losowym miejscu z przedziału 0-51:
```cs
        public void potasujKarty()
        {
            for (int i = 0; i < 52; i++)
            {
                int randomInt = random.Next(52);
                (karty[randomInt], karty[i]) = (karty[i], karty[randomInt]);        //zamieniamy wartość stojącą na miejscu i z karta na miejscu losowym od 0 do 51
            }
        }
```
## GraczArchetyp.cs
Jest to klasa z której dziedziczy zarówno gracz, jak i kroupier, ponieważ korzystają oni z podobnych metod.
Każdy gracz definiowany jest poprzez nazwę, sumePunktów oraz posiadaneKarty:
```cs
        public List<Karta> posiadaneKarty = new List<Karta>();
        public string nazwa { get; set; }
        public int sumaPunktow { get; set; }
```
Oraz ma możliwość dobrania nowej karty:
```cs
        public void dodajKarte(Karta karta)
        {
            posiadaneKarty.Add(karta);
            sumaPunktow += karta.wartosc;
        }
```
Wyzerowania swoich kart
```cs
        public void wyzerujKarty()
        {
            posiadaneKarty.Clear();
        }
```
Rozrysowania wszyskich swoich kart korzystają z metody narysujKarte() klasy Karta:
```cs
        public void pokazKarty()
        {
            foreach (var item in posiadaneKarty)
            {
                item.narysujKarte();
            }
        }
```
Oraz metody bool sprawdzającej, czy gracz ma powyżej 21 punktów. Służy ona do sprawdzania, czy gracz przegrał.
```cs
        public bool powyzej21()
        {
            if (sumaPunktow > 21)
                return true;
            return false;
        }
```
## Gracz.cs
Jest to klasa, z której korzysta gracz. W odróznieniu od kroupiera gracz posiada zmienna przechowującą liczbę żetonów, oraz zakład który gracz postawił w danym rozdaniu.
```cs
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
```
## KroupierAI.cs
Kroupier posiada zmienną definiującą do jakiego progu punktów domyślnie dobierać, oraz stopień ryzyka(Maksymalnie 3) który zostaje dodany do progu. Przykładowo, kroupier ze stopniem ryzyka 0 będzie dobierał karty do liczby 15 punktów, a kroupier ze stopniem ryzyka 3 będzie dobierał do 18 punktów.
Stopień ryzyka zależny jest od tego, czy kroupier wygrywa. Wygrywający kroupier będzie zachowywał się mniej ryzykownie, niż przegrywający.
```cs
        public short doIluDobierac;                //liczba podstawowa do ktorej krupier dopiera karte
        public short stopienRyzyka;                 //liczba dodajaca sie do liczby do ktorej krupier dobiera liczbe, zwieksza sie gdy przegrywa sprawiajac ze 
                                                        //dobiera do większego progu, gdy wygrywa to próg sie zmniejsza. Prog dochodzi maksymalnie do 18.
        public KrupierAI()
        {
            doIluDobierac = 15;
            stopienRyzyka = 0;
            sumaPunktow = 0;
            nazwa = "Krupier";
        }
```

## Gra.cs
Klasa gra służy do kontrolowania przebiegu rozdań. Znajdują się tam metody min. Sprawdzające kto wygrał, rozdające karty. Metody kontrolujące zachowanie gracza takie jak np. podwajanie stawki.

```cs
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
 ```
 ## Program.cs
Klasa służąca do przyjmowania od użytkownika wyborów oraz dostarczająca mu menu. 
```cs
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
```
