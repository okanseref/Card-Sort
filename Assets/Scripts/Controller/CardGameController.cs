using System;
using System.Collections.Generic;
using Controller.Signal;
using Data.Meld;
using Data.Sorter;
using Model.Card;
using Model.Meld.Extension;
using UnityEngine;
using View.Card.Signal;

namespace Controller
{
    public class CardGameController : MonoBehaviour
    {
        private DPCardSorter _cardSorter = new();
        private RunMeldRule _runMeldRule = new();
        private GroupMeldRule _groupMeldRule = new();
        
        private List<MyCard> _myCards;
        private List<IMeldRule> _runMeldRules;
        private List<IMeldRule> _groupMeldRules;
        private List<IMeldRule> _smartMeldRules;

        void Start()
        {
            _runMeldRules = new List<IMeldRule>() { _runMeldRule };
            _groupMeldRules = new List<IMeldRule>() { _groupMeldRule };
            _smartMeldRules = new List<IMeldRule>() { _runMeldRule, _groupMeldRule };
            
            _myCards = new List<MyCard>
            {
                new MyCard(4, 'H'), new MyCard(5, 'H'), new MyCard(6, 'H'),
                new MyCard(7, 'C'), new MyCard(7, 'D'), new MyCard(7, 'S'),
                new MyCard(9, 'D'), new MyCard(10, 'D'), new MyCard(11, 'D'),
                new MyCard(13, 'C'), new MyCard(2, 'S')
            };
            
            SubscribeToSignals();

            var melds = _smartMeldRules.GenerateAllMelds(_myCards);
            var initialDeadwoodSum = melds.CalculateDeadwoodSum(_myCards);
            
            SignalBus.Instance.Fire(new CardsInitializedSignal(new List<MyCard>(_myCards), initialDeadwoodSum));
        }

        private void SubscribeToSignals()
        {
            SignalBus.Instance.Subscribe<SortRunsOnlySignal>(SortRunsOnly);
            SignalBus.Instance.Subscribe<SortGroupsOnlySignal>(SortGroupsOnly);
            SignalBus.Instance.Subscribe<SortSmartSignal>(SortSmart);
            SignalBus.Instance.Subscribe<CardSwapSignal>(OnCardSwapped);
        }

        private void OnCardSwapped(CardSwapSignal signal)
        {
            (_myCards[signal.OldIndex], _myCards[signal.NewIndex]) =
                (_myCards[signal.NewIndex], _myCards[signal.OldIndex]);
            SignalBus.Instance.Fire(new DeadwoodUpdatedSignal(CalculateSmartMeldsDeadwoodSum(_myCards)));
        }

        private void SortRunsOnly()
        {
            SortInternal(_runMeldRules, _myCards);
        }

        private void SortGroupsOnly()
        {
            SortInternal(_groupMeldRules, _myCards);
        }

        private void SortSmart()
        {
            SortInternal(_smartMeldRules, _myCards);
        }

        private void SortInternal(List<IMeldRule> meldRules, List<MyCard> _myCards)
        {
            var melds = meldRules.GenerateAllMelds(_myCards);

            var oldCardOrder = new List<MyCard>(_myCards);
            
            _cardSorter.SortCards(_myCards, melds);

            var cardMoves = oldCardOrder.GetCardMoves(_myCards);
            
            var deadwoodSum = CalculateSmartMeldsDeadwoodSum(_myCards);
            
            SignalBus.Instance.Fire(new CardsSortedSignal(cardMoves, deadwoodSum));
        }

        private int CalculateSmartMeldsDeadwoodSum(List<MyCard> _myCards)
        {
            var melds = _smartMeldRules.GenerateAllMelds(_myCards);
            return melds.CalculateDeadwoodSum(_myCards);
        }

        private void OnDestroy()
        {
            SignalBus.Instance.Unsubscribe<SortRunsOnlySignal>(SortRunsOnly);
            SignalBus.Instance.Unsubscribe<SortGroupsOnlySignal>(SortGroupsOnly);
            SignalBus.Instance.Unsubscribe<SortSmartSignal>(SortSmart);
            SignalBus.Instance.Unsubscribe<CardSwapSignal>(OnCardSwapped);
        }
    }
}
