using Controller.Signal;
using UnityEngine;
using View.Factory;

namespace View.Card
{
    public class HandViewController : MonoBehaviour
    {
        [SerializeField] private HandCardHorizontalFitter _handCardHorizontalFitter;

        private void Awake()
        {
            SignalBus.Instance.Subscribe<CardsInitializedSignal>(InitializeCards);
        }
        
        private void InitializeCards(CardsInitializedSignal initializedSignal)
        {
            var factory = ServiceLocator.Resolve<CardViewFactory>();
            foreach (var myCard in initializedSignal.MyCards)
            {
                factory.GetCardView((myCard.Rank, myCard.Suit), _handCardHorizontalFitter.transform);
            }
        }
    }
}
