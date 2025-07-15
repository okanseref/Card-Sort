using System;
using System.Collections.Generic;
using UnityEngine;

namespace View.Card
{
    public class CardDragPassDetector : MonoBehaviour
    {
        public CardDragController CardDragController;
        public Action<int, int> OnCardPassed;
        
        private CardView _draggedCard;                    
        private List<CardView> _targetCards;            
        private Dictionary<CardView, float> _lastRelativeX = new Dictionary<CardView, float>();        // Stores last known side of dragged card relative to each target

        private void Start()
        {
            CardDragController.DragStartedCallback += StartListening;
            CardDragController.DragEndedCallback += StopListening;
        }

        private void StartListening(CardView draggedCard, List<CardView> targetCards)
        {
            _draggedCard = draggedCard;
            _targetCards = targetCards;
        }

        private void StopListening(CardView cardView)
        {
            ResetDetection();
        }
    
        void Update()
        {
            if (_draggedCard == null) return;

            float draggedX = _draggedCard.transform.position.x;

            foreach (CardView target in _targetCards)
            {
                if(target == _draggedCard)
                    continue;
                
                float targetX = target.transform.position.x;
                float currentDiff = draggedX - targetX;

                if (!_lastRelativeX.ContainsKey(target))
                {
                    // Initialize
                    _lastRelativeX[target] = currentDiff;
                    continue;
                }

                float lastDiff = _lastRelativeX[target];

                // Crossing detection: sign change
                if (Mathf.Sign(lastDiff) != Mathf.Sign(currentDiff))
                {
                    OnPassed(target);
                    break;
                }

                // Update last known side
                _lastRelativeX[target] = currentDiff;
            }
        }

        void OnPassed(CardView target)
        {
            OnCardPassed?.Invoke(_targetCards.IndexOf(_draggedCard),_targetCards.IndexOf(target));
            _lastRelativeX[target] = Mathf.Sign(_draggedCard.transform.position.x -
                                               _targetCards[_targetCards.IndexOf(target)].transform.position.x) * 1;
        }

        public void ResetDetection()
        {
            _lastRelativeX.Clear();
            _draggedCard = null;
        }
    }
}