using System.Collections.Generic;

namespace Controller.Signal
{
    public class CardsSortedSignal
    {
        public List<(int fromIndex, int toIndex)> CardMoves;

        public CardsSortedSignal(List<(int fromIndex, int toIndex)> cardMoves)
        {
            CardMoves = cardMoves;
        }
    }
}