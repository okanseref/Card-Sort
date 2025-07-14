using UnityEngine;

namespace View.CardView
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public void Init(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}