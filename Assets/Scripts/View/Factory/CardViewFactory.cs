using System;
using System.Collections.Generic;
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
        private List<CardView> _activeCardViews;
        private void Awake()
        {
            _cardViewItemPrefab = Resources.Load<CardView>("Prefabs/CardView");
            _objectPool = new MonoBehaviourObjectPool<CardView>(_cardViewItemPrefab, 11, gameObject.transform);
            _activeCardViews = new();
            
            SignalBus.Instance.Subscribe<BundleChangedSignal>(OnBundleChanged);
        }

        public CardView GetCardView((int, char) rankAndSuit, Transform customParent, bool isHidden = false)
        {
            CardView viewInstance = _objectPool.Get();
            Transform exchangeTransform;
            (exchangeTransform = viewInstance.transform).SetParent(customParent);
            exchangeTransform.localScale = Vector3.one;
            exchangeTransform.localPosition = Vector3.zero;
            exchangeTransform.localRotation = Quaternion.identity;
            if(isHidden)
                viewInstance.Init(GetCardBackgroundView(), rankAndSuit.Item1, rankAndSuit.Item2);
            else
            {
                viewInstance.Init(GetCardView(rankAndSuit.Item1, rankAndSuit.Item2), rankAndSuit.Item1, rankAndSuit.Item2);
            }
            
            _activeCardViews.Add(viewInstance);
            return viewInstance;
        }

        private void OnBundleChanged()
        {
            foreach (var cardView in _activeCardViews)
            {
                if (cardView != null)
                {
                    if(cardView.Suit == default && cardView.Rank == default)
                        cardView.SetSprite(GetCardBackgroundView());
                    else
                    {
                        cardView.SetSprite(GetCardView(cardView.Rank, cardView.Suit));
                    }
                }
            }
        }
        
        private Sprite GetCardView(int rank, char suit)
        {
            suit = char.ToLower(suit);
            return ServiceLocator.Resolve<AssetBundleManager>().LoadAssetFromBundle<Sprite>(CardNamer.GetCardName(rank,suit));
        }
        
        private Sprite GetCardBackgroundView()
        {
            return ServiceLocator.Resolve<AssetBundleManager>().LoadAssetFromBundle<Sprite>(AssetBundleConstants.CardBackAssetName);
        }

        public void ReturnCardView(CardView view)
        {
            _activeCardViews.Remove(view);
            _objectPool.Return(view);
        }

        private void OnDestroy()
        {
            SignalBus.Instance.Unsubscribe<BundleChangedSignal>(OnBundleChanged);
        }
    }
}