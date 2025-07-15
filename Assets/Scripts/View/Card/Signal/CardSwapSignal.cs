namespace View.Card.Signal
{
    public struct CardSwapSignal
    {
        public int OldIndex;
        public int NewIndex;

        public CardSwapSignal(int oldIndex, int newIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }
    }
}