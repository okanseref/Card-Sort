using System;
using System.Collections.Generic;
using UnityEngine;
using View.Card.Signal;

namespace View.Card
{
    public class CardDragController : MonoBehaviour
    {
        private Camera _mainCam;
        private CardView _draggingCard;
        private Vector3 _offset;
        private int _draggingFingerId = -1;
        private bool _inputLocked = false;

        public Action<CardView , List<CardView>> DragStartedCallback;
        public Action<CardView> DragEndedCallback;

        private List<CardView> _draggableCardList = new();
        public void SetDraggableCards(List<CardView> draggableCards)
        {
            _draggableCardList = draggableCards;
        }

        private void Awake()
        {
            SignalBus.Instance.Subscribe<HandViewLockSignal>(OnInputLocked);
        }

        void Start()
        {
            _mainCam = Camera.main;
        }

        void Update()
        {
            if(_inputLocked)
                return;
            
            if (Application.isMobilePlatform)
            {
                HandleTouchInput();
            }
            else
            {
                HandleMouseInput();
            }
        }

        void OnInputLocked(HandViewLockSignal signal)
        {
            _inputLocked = signal.IsLocked;
        }

        void HandleMouseInput()
        {
            Vector3 worldPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 worldPoint2D = new Vector2(worldPos.x, worldPos.y);

            if (Input.GetMouseButtonDown(0))
            {
                TryStartDrag(worldPoint2D, -1, worldPos);
            }

            if (Input.GetMouseButton(0) && _draggingCard != null)
            {
                _draggingCard.transform.position = new Vector3(worldPos.x, worldPos.y, _draggingCard.transform.position.z) + _offset;
            }

            if (Input.GetMouseButtonUp(0) && _draggingCard != null)
            {
                EndDrag();
            }
        }

        void HandleTouchInput()
        {
            if (Input.touchCount == 0) return;

            foreach (Touch touch in Input.touches)
            {
                Vector3 worldPos = _mainCam.ScreenToWorldPoint(touch.position);
                Vector2 worldPoint2D = new Vector2(worldPos.x, worldPos.y);

                if (touch.phase == TouchPhase.Began && _draggingCard == null)
                {
                    TryStartDrag(worldPoint2D, touch.fingerId, worldPos);
                }

                if (touch.fingerId == _draggingFingerId)
                {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        _draggingCard.transform.position = new Vector3(worldPos.x, worldPos.y, _draggingCard.transform.position.z) + _offset;
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        EndDrag();
                    }
                }
            }
        }

        void TryStartDrag(Vector2 rayPos, int fingerId, Vector3 worldPos)
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(rayPos);

            CardView highestCard = null;
            int highestSortingOrder = int.MinValue;

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<CardView>(out var cardView) && _draggableCardList.Contains(cardView))
                {
                    var sr = cardView.GetComponent<SpriteRenderer>();
                    if (sr != null && sr.sortingOrder > highestSortingOrder)
                    {
                        highestSortingOrder = sr.sortingOrder;
                        highestCard = cardView;
                    }
                }
            }

            if (highestCard != null)
            {
                _draggingCard = highestCard;
                _offset = _draggingCard.transform.position - new Vector3(worldPos.x, worldPos.y, 0);
                _draggingFingerId = fingerId;
                highestCard.SpriteRenderer.sortingOrder += 100;
                DragStartedCallback?.Invoke(_draggingCard, _draggableCardList);
            }
        }

        void EndDrag()
        {
            DragEndedCallback?.Invoke(_draggingCard);
            _draggingCard = null;
            _draggingFingerId = -1;
        }
    }
}
