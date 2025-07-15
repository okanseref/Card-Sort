public static class CardNamer
{
    public static string GetCardName(int rank, char suit)
    {
        string suitName = suit switch
        {
            'c' or 'C' => "Clubs",
            'd' or 'D' => "Diamonds",
            'h' or 'H' => "Hearts",
            's' or 'S' => "Spades",
            _ => "Unknown"
        };

        string rankName = rank switch
        {
            1 => "Ace",
            11 => "Jack",
            12 => "Queen",
            13 => "King",
            _ => rank.ToString()
        };

        return suitName + rankName;
    }

}
