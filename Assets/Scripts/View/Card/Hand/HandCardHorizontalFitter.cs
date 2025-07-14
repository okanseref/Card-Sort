using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace View.Card
{
    public class HandCardHorizontalFitter : MonoBehaviour
    {
        [SerializeField] private float _spacing = 1.0f;   // Space between items
        [SerializeField] private float _arcHeight = 1f;
        [SerializeField] private float _maxRotation = 15f; // degrees, for edges

        [ContextMenu("Apply Arched Fan Layout")]
        public void ApplyLayout(List<CardView> cardViews)
        {
            if (cardViews == null || cardViews.Count == 0) return;

            int count = cardViews.Count;
            float totalWidth = _spacing * (count - 1);
            float startX = -totalWidth / 2f;

            for (int i = 0; i < count; i++)
            {
                if (cardViews[i] == null) continue;

                float x = startX + i * _spacing;
                float normalizedX = x / (totalWidth / 2f); // -1 to 1

                // Position along a parabola
                float y = -_arcHeight * (normalizedX * normalizedX) + _arcHeight;
                cardViews[i].transform.localPosition = new Vector3(x, y, 0f);

                // Rotate slightly toward center (negative left, positive right)
                float rotationZ = -normalizedX * _maxRotation;
                cardViews[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ);

                cardViews[i].SpriteRenderer.sortingOrder = i;
            }
        }
    }
}