using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace View.Card
{
    public class HandCardHorizontalFitter : MonoBehaviour
    {
        [SerializeField] private float _spacing = 1.0f;   // Space between cardViews
        [SerializeField] private float _arcHeight = 1f;
        [SerializeField] private float _maxRotation = 15f; // degrees, for edges
        [SerializeField] private float _animDuration = 0.3f; // degrees, for edges
        
        private List<Pose> _poses = new();
        
        public void CalculateLayout(int N)
        {
            _poses.Clear();
            
            if (N <= 0) return;

            float totalWidth = _spacing * (N - 1);
            float startX = -totalWidth / 2f;

            for (int i = 0; i < N; i++)
            {
                float x = startX + i * _spacing;
                float normalizedX = x / (totalWidth / 2f); // from -1 to 1

                float y = -_arcHeight * (normalizedX * normalizedX) + _arcHeight;
                Vector3 localPosition = new Vector3(x, y, 0f);

                float rotationZ = -normalizedX * _maxRotation;
                Quaternion localRotation = Quaternion.Euler(0f, 0f, rotationZ);

                _poses.Add(new Pose(localPosition, localRotation));
            }
        }

        public void MoveCardToPosition(CardView cardView, int positionIndex)
        {
            cardView.transform.DOMove(_poses[positionIndex].position, _animDuration);
            cardView.transform.DORotate(_poses[positionIndex].rotation.eulerAngles, _animDuration);
            cardView.SpriteRenderer.sortingOrder = positionIndex;
        }
        
        [ContextMenu("Apply Arched Fan Layout")]
        public void ApplyLayout(List<CardView> cardViews)
        {
            for (int i = 0; i < cardViews.Count && i < _poses.Count; i++)
            {
                MoveCardToPosition(cardViews[i], i);
            }
        }
    }
}