using System.Collections.Generic;
using Controller.Signal;
using UnityEngine;
using View.Factory;

namespace View.Card
{
    public class HandViewController : MonoBehaviour
    {
        [SerializeField] private HandCardHorizontalFitter _handCardHorizontalFitter;
        
        private List<CardView> _myCardViews = new();
        
        private void Awake()
        {
            SignalBus.Instance.Subscribe<CardsInitializedSignal>(InitializeCards);
            SignalBus.Instance.Subscribe<CardsSortedSignal>(OnCardsSorted);
        }
        
        private void InitializeCards(CardsInitializedSignal initializedSignal)
        {
            var factory = ServiceLocator.Resolve<CardViewFactory>();
            
            foreach (var myCard in initializedSignal.MyCards)
            {
                var cardView = factory.GetCardView((myCard.Rank, myCard.Suit), _handCardHorizontalFitter.transform);
                _myCardViews.Add(cardView);
            }
            
            _handCardHorizontalFitter.ApplyLayout(_myCardViews);
        }

        private void OnCardsSorted(CardsSortedSignal signal)
        {
            foreach (var cardMove in signal.CardMoves)
            {
                (_myCardViews[cardMove.fromIndex].transform.position,
                    _myCardViews[cardMove.toIndex].transform.position) = (
                    _myCardViews[cardMove.toIndex].transform.position,
                    _myCardViews[cardMove.fromIndex].transform.position);
                (_myCardViews[cardMove.fromIndex].transform.rotation,
                    _myCardViews[cardMove.toIndex].transform.rotation) = (
                    _myCardViews[cardMove.toIndex].transform.rotation,
                    _myCardViews[cardMove.fromIndex].transform.rotation);
                (_myCardViews[cardMove.fromIndex], _myCardViews[cardMove.toIndex]) = (_myCardViews[cardMove.toIndex],
                    _myCardViews[cardMove.fromIndex]);
            }
            
            _handCardHorizontalFitter.ApplyLayout(_myCardViews);
        }
    }
}
