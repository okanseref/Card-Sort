using System.Collections.Generic;
using System.Linq;
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

            _handCardHorizontalFitter.CalculateLayout(_myCardViews.Count);
            

            _handCardHorizontalFitter.ApplyLayout(_myCardViews);
        }

        private void OnCardsSorted(CardsSortedSignal signal)
        {
            foreach (var cardMove in signal.CardMoves)
            {
                _handCardHorizontalFitter.MoveCardToPosition(_myCardViews[cardMove.fromIndex], cardMove.toIndex);
            }

            var newSortedList = new List<CardView>(_myCardViews);

            foreach (var cardMove in signal.CardMoves)
            {
                newSortedList[cardMove.toIndex] = _myCardViews[cardMove.fromIndex];
            }

            _myCardViews = newSortedList;
        }
    }
}
