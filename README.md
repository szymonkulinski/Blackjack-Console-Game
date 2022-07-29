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
