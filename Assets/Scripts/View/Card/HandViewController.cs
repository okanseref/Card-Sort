using System;
using System.Collections.Generic;
using Controller.Signal;
using UnityEngine;
using View.Card.Signal;
using View.Factory;
using View.UI.CardGame;

namespace View.Card
{
    public class HandViewController : MonoBehaviour
    {
        [SerializeField] private HandCardHorizontalFitter _handCardHorizontalFitter;
        [SerializeField] private CardDragPassDetector _cardDragPassDetector;
        [SerializeField] private CardDragController _cardDragController;
        [SerializeField] private Transform _packRoot;
        
        private List<CardView> _myCardViews = new();
        private int _handViewLock = 0;
        
        private void Awake()
        {
            SignalBus.Instance.Subscribe<CardsInitializedSignal>(InitializeCards);
            SignalBus.Instance.Subscribe<CardsSortedSignal>(OnCardsSorted);
        }

        private void Start()
        {
            _cardDragPassDetector.OnCardPassed += OnCardDragPassedAnotherOne;
            _cardDragController.DragEndedCallback += PlaceDraggedCardToPosition;

            CreatePackRoot();
        }

        private void CreatePackRoot()
        {
            var factory = ServiceLocator.Resolve<CardViewFactory>();
            var cardView = factory.GetCardView((default, default), _packRoot.transform, true);
            cardView.SpriteRenderer.sortingOrder = 500;
        }

        private void InitializeCards(CardsInitializedSignal initializedSignal)
        {
            var factory = ServiceLocator.Resolve<CardViewFactory>();
            
            foreach (var myCard in initializedSignal.MyCards)
            {
                var cardView = factory.GetCardView((myCard.Rank, myCard.Suit), _handCardHorizontalFitter.transform);
                cardView.transform.position = _packRoot.transform.position;
                _myCardViews.Add(cardView);
            }

            _cardDragController.SetDraggableCards(_myCardViews);
            
            _handCardHorizontalFitter.CalculateLayout(_myCardViews.Count);

            ChangeLock(1);
            _handCardHorizontalFitter.ApplyLayoutAnimated(_myCardViews, ()=>
            {
                ChangeLock(-1);
            });
        }

        private void OnCardsSorted(CardsSortedSignal signal)
        {
            foreach (var cardMove in signal.CardMoves)
            {
                ChangeLock(1);
                _handCardHorizontalFitter.MoveCardToPosition(_myCardViews[cardMove.fromIndex], cardMove.toIndex, () =>
                {
                    ChangeLock(-1);
                });
            }

            var newSortedList = new List<CardView>(_myCardViews);

            foreach (var cardMove in signal.CardMoves)
            {
                newSortedList[cardMove.toIndex] = _myCardViews[cardMove.fromIndex];
            }

            _myCardViews = newSortedList;
            
            _cardDragController.SetDraggableCards(_myCardViews);
        }

        private void OnCardDragPassedAnotherOne(int draggedIndex, int passedIndex)
        {
            _handCardHorizontalFitter.MoveCardToPosition(_myCardViews[passedIndex], draggedIndex);
            (_myCardViews[draggedIndex], _myCardViews[passedIndex]) =
                (_myCardViews[passedIndex], _myCardViews[draggedIndex]);
            SignalBus.Instance.Fire(new CardSwapSignal(draggedIndex, passedIndex));
        }

        private void PlaceDraggedCardToPosition(CardView draggingCard)
        {
            var indexOfCard = _myCardViews.IndexOf(draggingCard);
            ChangeLock(1);
            _handCardHorizontalFitter.MoveCardToPosition(_myCardViews[indexOfCard], indexOfCard, () =>
            {
                ChangeLock(-1);
            });
        }

        private void ChangeLock(int change)
        {
            _handViewLock += change;
            Debug.LogError(_handViewLock);
            SignalBus.Instance.Fire(new HandViewLockSignal(_handViewLock != 0));
        }

        private void OnDestroy()
        {
            _cardDragPassDetector.OnCardPassed -= OnCardDragPassedAnotherOne;
            _cardDragController.DragEndedCallback -= PlaceDraggedCardToPosition;
            SignalBus.Instance.Unsubscribe<CardsInitializedSignal>(InitializeCards);
            SignalBus.Instance.Unsubscribe<CardsSortedSignal>(OnCardsSorted);
        }
    }
}
