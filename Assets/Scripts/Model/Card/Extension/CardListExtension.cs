using System.Collections.Generic;
using Data.Card;
using UnityEngine;

namespace Model.Card
{
    public static class CardListExtension
    {
        public static List<(int fromIndex, int toIndex)> GetCardMoves(this List<MyCard> oldMyCards, List<MyCard> myCards)
        {
            var moves = new List<(int fromIndex, int toIndex)>();
            var seenPairs = new HashSet<(int, int)>();

            for (int newIndex = 0; newIndex < myCards.Count; newIndex++)
            {
                var card = myCards[newIndex];
                int oldIndex = oldMyCards.IndexOf(card);

                if (oldIndex != newIndex)
                {
                    var pair = (Mathf.Min(oldIndex, newIndex), Mathf.Max(oldIndex, newIndex));
                    if (!seenPairs.Contains(pair))
                    {
                        seenPairs.Add(pair);
                        moves.Add(pair);
                    }
                }
            }

            return moves;
        }
    }
}