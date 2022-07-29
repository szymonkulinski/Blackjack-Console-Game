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
