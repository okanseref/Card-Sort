using System.Collections.Generic;

namespace Controller.Signal
{
    public class CardsSortedSignal
    {
        private List<(int fromIndex, int toIndex)> _cardMoves;

        public CardsSortedSignal(List<(int fromIndex, int toIndex)> cardMoves)
        {
            _cardMoves = cardMoves;
        }
    }
}