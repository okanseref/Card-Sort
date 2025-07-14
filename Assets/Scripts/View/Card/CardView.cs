using UnityEngine;
using UnityEngine.Serialization;

namespace View.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer SpriteRenderer;
        public void Init(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
        }
    }
}