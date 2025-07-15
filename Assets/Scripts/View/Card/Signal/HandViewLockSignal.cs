namespace View.Card.Signal
{
    public struct HandViewLockSignal
    {
        public bool IsLocked;

        public HandViewLockSignal(bool isLocked)
        {
            IsLocked = isLocked;
        }
    }
}