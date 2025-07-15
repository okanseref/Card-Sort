using System.Collections.Generic;

namespace Controller.Signal
{
    public class CardsSortedSignal
    {
        public List<(int fromIndex, int toIndex)> CardMoves;
        public int DeadwoodSum;

        public CardsSortedSignal(List<(int fromIndex, int toIndex)> cardMoves, int deadwoodSum)
        {
            CardMoves = cardMoves;
            DeadwoodSum = deadwoodSum;
        }
    }
}