using System;
using System.Collections.Generic;

namespace Model.Card
{
    public static class CardExtension
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
        
        public static int GetDeadwoodValue(this MyCard myCard)
        {
            return Math.Clamp(myCard.Rank, 1, 10);
        }
    }
}