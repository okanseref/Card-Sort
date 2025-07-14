using System.Collections.Generic;
using Model.Card;

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