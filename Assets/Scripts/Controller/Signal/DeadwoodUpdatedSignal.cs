namespace Controller.Signal
{
    public struct DeadwoodUpdatedSignal
    {
        public int DeadwoodSum;

        public DeadwoodUpdatedSignal(int deadwoodSum)
        {
            DeadwoodSum = deadwoodSum;
        }
    }
}