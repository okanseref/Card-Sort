using UnityEngine;

namespace View.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer SpriteRenderer;
        public int Rank;
        public char Suit;

        public void Init(Sprite sprite, int rank, char suit)
        {
            SpriteRenderer.sprite = sprite;
            Rank = rank;
            Suit = suit;
        }

        public void SetSprite(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
        }
    }
}