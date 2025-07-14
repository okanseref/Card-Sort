using System.Collections.Generic;
using UnityEngine;
using View.Pool;

namespace View.Factory
{
    public class CardViewFactory : Singleton<CardViewFactory>
    {
        [SerializeField] private CardView.CardView cardViewItemPrefab;
        [SerializeField] private List<Sprite> spriteList;

        private MonoBehaviourObjectPool<CardView.CardView> _objectPool;

        private void Awake()
        {
            _objectPool = new MonoBehaviourObjectPool<CardView.CardView>(cardViewItemPrefab, 8);
        }

        public CardView.CardView GetCardView(int i, Transform customParent)
        {
            CardView.CardView viewInstance = _objectPool.Get();
            Transform exchangeTransform;
            (exchangeTransform = viewInstance.transform).SetParent(customParent);
            exchangeTransform.localScale = Vector3.one;
            exchangeTransform.localPosition = Vector3.zero;
            exchangeTransform.localRotation = Quaternion.identity;
            viewInstance.Init(GetDropSprite(i));
            return viewInstance;
        }

        public Sprite GetDropSprite(int i)
        {
            return spriteList[i % spriteList.Count];
        }

        public void ReturnCardView(CardView.CardView view)
        {
            _objectPool.Return(view);
        }
    }
}