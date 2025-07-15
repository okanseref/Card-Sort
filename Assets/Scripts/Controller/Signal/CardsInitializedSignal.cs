using System.Collections.Generic;
using Model.Card;

namespace Controller.Signal
{
    public class CardsInitializedSignal
    {
        public List<MyCard> MyCards;
        public int DeadwoodSum;

        public CardsInitializedSignal(List<MyCard> myCards, int deadwoodSum)
        {
            MyCards = myCards;
            DeadwoodSum = deadwoodSum;
        }
    }
}