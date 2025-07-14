using System.Collections.Generic;
using Data.Card;

namespace Model.Card
{
    public static class CardListExtension
    {
        public static List<(int fromIndex, int toIndex)> GetCardMoves(this List<MyCard> oldMyCards, List<MyCard> myCards)
        {
            var moves = new List<(int fromIndex, int toIndex)>();

            for (int newIndex = 0; newIndex < myCards.Count; newIndex++)
            {
                var card = myCards[newIndex];
                int oldIndex = oldMyCards.IndexOf(card);
                if (oldIndex != newIndex)
                {
                    moves.Add((oldIndex, newIndex));
                }
            }

            return moves;
        }
    }
}