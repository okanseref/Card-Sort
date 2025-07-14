using System;
using UnityEngine;

namespace View.Card
{
    [Serializable]
    public class CardViewInfo
    {
        public Sprite Asset;
        public int Rank;  // 1–13
        public char Suit; // 'Hearts', 'Diamonds', 'Clubs', 'Spades'
    }
}