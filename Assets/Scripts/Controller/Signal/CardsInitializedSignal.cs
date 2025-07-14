using System.Collections.Generic;
using Data.Card;

namespace Controller.Signal
{
    public class CardsInitializedSignal
    {
        public List<MyCard> MyCards;

        public CardsInitializedSignal(List<MyCard> myCards)
        {
            MyCards = myCards;
        }
    }
}