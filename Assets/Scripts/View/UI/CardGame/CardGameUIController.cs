using System;
using Controller.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Card.Signal;

namespace View.UI.CardGame
{
    public class CardGameUIController : MonoBehaviour
    {
        public Button SortRunsButton;
        public Button SortGroupButton;
        public Button SortSmartButton;
        public TextMeshProUGUI DeadwoodCountText;

        private void Start()
        {
            SortRunsButton.onClick.AddListener(OnSortRunsClicked);
            SortGroupButton.onClick.AddListener(OnSortGroupsClicked);
            SortSmartButton.onClick.AddListener(OnSortSmartClicked);
            
            SignalBus.Instance.Subscribe<CardsSortedSignal>(OnCardsSorted);
            SignalBus.Instance.Subscribe<CardsInitializedSignal>(OnCardsInitialized);
            SignalBus.Instance.Subscribe<DeadwoodUpdatedSignal>(OnDeadwoodUpdated);
        }

        private void SetDeadwoodText(int deadwoodCount)
        {
            DeadwoodCountText.text = "Deadwood: " + deadwoodCount;
        }

        private void OnCardsSorted(CardsSortedSignal signal)
        {
            SetDeadwoodText(signal.DeadwoodSum);
        }

        private void OnDeadwoodUpdated(DeadwoodUpdatedSignal signal)
        {
            SetDeadwoodText(signal.DeadwoodSum);
        }

        private void OnCardsInitialized(CardsInitializedSignal signal)
        {
            SetDeadwoodText(signal.DeadwoodSum);
        }

        private void OnSortRunsClicked()
        {
            SignalBus.Instance.Fire<SortRunsOnlySignal>();
        }

        private void OnSortGroupsClicked()
        {
            SignalBus.Instance.Fire<SortGroupsOnlySignal>();
        }

        private void OnSortSmartClicked()
        {
            SignalBus.Instance.Fire<SortSmartSignal>();
        }

        private void OnDestroy()
        {
            SignalBus.Instance.Unsubscribe<CardsSortedSignal>(OnCardsSorted);
            SignalBus.Instance.Unsubscribe<CardsInitializedSignal>(OnCardsInitialized);
            SignalBus.Instance.Unsubscribe<DeadwoodUpdatedSignal>(OnDeadwoodUpdated);
        }
    }
}