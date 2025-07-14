using System;
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

        private void Start()
        {
            SortRunsButton.onClick.AddListener(OnSortRunsClicked);
            SortGroupButton.onClick.AddListener(OnSortGroupsClicked);
            SortSmartButton.onClick.AddListener(OnSortSmartClicked);
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
    }
}