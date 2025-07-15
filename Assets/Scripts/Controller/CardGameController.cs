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
        
        void Start()
        {
            _myCards = new List<MyCard>
            {
                new MyCard(4, 'H'), new MyCard(5, 'H'), new MyCard(6, 'H'),
                new MyCard(7, 'C'), new MyCard(7, 'D'), new MyCard(7, 'S'),
                new MyCard(9, 'D'), new MyCard(10, 'D'), new MyCard(11, 'D'),
                new MyCard(13, 'C'), new MyCard(2, 'S')
            };
            
            SubscribeToSignals();
            
            SignalBus.Instance.Fire(new CardsInitializedSignal(new List<MyCard>(_myCards)));
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
        }

        private void SortRunsOnly()
        {
            SortInternal(new List<IMeldRule>()
            {
                _runMeldRule
            }, _myCards);
        }

        private void SortGroupsOnly()
        {
            SortInternal(new List<IMeldRule>()
            {
                _groupMeldRule
            }, _myCards);
        }

        private void SortSmart()
        {
            SortInternal(new List<IMeldRule>()
            {
                _runMeldRule,
                _groupMeldRule
            }, _myCards);
        }

        private void SortInternal(List<IMeldRule> meldRules, List<MyCard> _myCards)
        {
            var melds = meldRules.GenerateAllMelds(_myCards);

            var oldCardOrder = new List<MyCard>(_myCards);
            
            _cardSorter.SortCards(_myCards, melds);

            var cardMoves = oldCardOrder.GetCardMoves(_myCards);
            
            SignalBus.Instance.Fire(new CardsSortedSignal(cardMoves));
        }
    }
}
