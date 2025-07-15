using Managers;
using UnityEngine;
using View.Card;
using View.Pool;

namespace View.Factory
{
    public class CardViewFactory : MonoBehaviour
    {
        private CardView _cardViewItemPrefab;
        
        private MonoBehaviourObjectPool<CardView> _objectPool;

        private void Awake()
        {
            _cardViewItemPrefab = Resources.Load<CardView>("Prefabs/CardView");
            _objectPool = new MonoBehaviourObjectPool<CardView>(_cardViewItemPrefab, 11, gameObject.transform);
        }

        public CardView GetCardView((int, char) rankAndSuit, Transform customParent)
        {
            CardView viewInstance = _objectPool.Get();
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
            suit = char.ToLower(suit);
            return ServiceLocator.Resolve<AssetBundleManager>().LoadAssetFromBundle<Sprite>(CardNamer.GetCardName(rank,suit));
        }

        public void ReturnCardView(CardView view)
        {
            _objectPool.Return(view);
        }
    }
}