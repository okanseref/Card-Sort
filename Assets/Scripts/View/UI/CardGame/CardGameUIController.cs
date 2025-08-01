﻿using System;
using System.Collections.Generic;
using Controller.Signal;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Card;
using View.Card.Signal;

namespace View.UI.CardGame
{
    public class CardGameUIController : MonoBehaviour
    {
        public CardDragController CardDragController;
        public Button SortRunsButton;
        public Button SortGroupButton;
        public Button SortSmartButton;
        public Button DefaultThemeButton;
        public Button CuteThemeButton;
        public TextMeshProUGUI DeadwoodCountText;

        private bool _lockDuringInput = false;
        
        private void Start()
        {
            SortRunsButton.onClick.AddListener(OnSortRunsClicked);
            SortGroupButton.onClick.AddListener(OnSortGroupsClicked);
            SortSmartButton.onClick.AddListener(OnSortSmartClicked);
            DefaultThemeButton.onClick.AddListener(OnDefaultThemeClicked);
            CuteThemeButton.onClick.AddListener(OnCuteThemeClicked);
            
            SignalBus.Instance.Subscribe<CardsSortedSignal>(OnCardsSorted);
            SignalBus.Instance.Subscribe<CardsInitializedSignal>(OnCardsInitialized);
            SignalBus.Instance.Subscribe<DeadwoodUpdatedSignal>(OnDeadwoodUpdated);
            SignalBus.Instance.Subscribe<HandViewLockSignal>(OnHandViewLocked);

            CardDragController.DragStartedCallback += OnDragStarted;
        }

        private void OnDragStarted(CardView arg1, List<CardView> arg2)
        {
            SetLockedButtons(true);
        }
        
        private void OnHandViewLocked(HandViewLockSignal signal)
        {
            SetLockedButtons(signal.IsLocked);
        }

        private void SetLockedButtons(bool isLocked)
        {
            SortRunsButton.interactable = !isLocked;
            SortGroupButton.interactable = !isLocked;
            SortSmartButton.interactable = !isLocked;
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

        private void OnDefaultThemeClicked()
        {
            ServiceLocator.Resolve<AssetBundleManager>().LoadAssetBundle(AssetBundleConstants.DefaultBundleName);
        }

        private void OnCuteThemeClicked()
        {
            ServiceLocator.Resolve<AssetBundleManager>().LoadAssetBundle(AssetBundleConstants.CuteCardsBundleName);
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
            SignalBus.Instance.Unsubscribe<HandViewLockSignal>(OnHandViewLocked);
            
            CardDragController.DragStartedCallback -= OnDragStarted;
        }
    }
}