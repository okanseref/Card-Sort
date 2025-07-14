using System.Collections.Generic;
using UnityEngine;
using View.Card;
using View.Pool;

namespace View.Factory
{
    public class CardViewFactory : MonoBehaviour
    {
        private CardView.CardView _cardViewItemPrefab;
        private CardViewInfoCollection _cardViewInfoCollection;

        private Dictionary<(int,char), CardViewInfo> _cardViewInfos = new();
        
        private MonoBehaviourObjectPool<CardView.CardView> _objectPool;

        private void Awake()
        {
            _cardViewItemPrefab = Resources.Load<CardView.CardView>("Prefabs/CardView");
            _cardViewInfoCollection = Resources.Load<CardViewInfoCollection>("Info/CardViewInfoCollection");
            _objectPool = new MonoBehaviourObjectPool<CardView.CardView>(_cardViewItemPrefab, 11, gameObject.transform);

            foreach (var cardViewInfo in _cardViewInfoCollection.CardViewInfos)
            {
                _cardViewInfos.Add((cardViewInfo.Rank, cardViewInfo.Suit), cardViewInfo);
            }
        }

        public CardView.CardView GetCardView((int, char) rankAndSuit, Transform customParent)
        {
            CardView.CardView viewInstance = _objectPool.Get();
            Transform exchangeTransform;
            (exchangeTransform = viewInstance.transform).SetParent(customParent);
            exchangeTransform.localScale = Vector3.one;
            exchangeTransform.localPosition = Vector3.zero;
            exchangeTransform.localRotation = Quaternion.identity;
            viewInstance.Init(GetCardView(rankAndSuit.Item1, rankAndSuit.Item2));
            return viewInstance;
        }

        private Sprite GetCardView(int rank, char suit)
        {
            if (_cardViewInfos.TryGetValue((rank, suit), out var cardViewInfo))
            {
                return cardViewInfo.Asset;
            }

            Debug.LogError("Card not found:" + rank + suit);
            return null;
        }

        public void ReturnCardView(CardView.CardView view)
        {
            _objectPool.Return(view);
        }
    }
}