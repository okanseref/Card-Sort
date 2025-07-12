namespace Data.Card
{
    public class MyCard
    {
        public int Rank;  // 1–13
        public char Suit; // 'Hearts', 'Diamonds', 'Clubs', 'Spades'

        public MyCard(int rank, char suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString() => $"{Rank}{Suit}";
    }
}