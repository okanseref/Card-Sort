﻿using System.Collections.Generic;
using Data.Meld;
using Data.Sorter;
using Model.Card;
using Model.Meld.Extension;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class CardTestDocumentationCase
    {
        [Test]
        public void CardSortRunMeldOnlyTest()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(1, 'H'), new MyCard(2, 'S'), new MyCard(5, 'D'),
                new MyCard(4, 'H'), new MyCard(1, 'S'), new MyCard(3, 'D'),
                new MyCard(4, 'C'), new MyCard(4, 'S'), new MyCard(1, 'D'),
                new MyCard(3, 'S'), new MyCard(4, 'D')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);

            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(10, deadwoodSum);
        }
        
        [Test]
        public void CardSortGroupMeldOnlyTest()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(1, 'H'), new MyCard(2, 'S'), new MyCard(5, 'D'),
                new MyCard(4, 'H'), new MyCard(1, 'S'), new MyCard(3, 'D'),
                new MyCard(4, 'C'), new MyCard(4, 'S'), new MyCard(1, 'D'),
                new MyCard(3, 'S'), new MyCard(4, 'D')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);

            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(13, deadwoodSum);
        }
        
        [Test]
        public void CardSortSmartMeldOnlyTest()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(1, 'H'), new MyCard(2, 'S'), new MyCard(5, 'D'),
                new MyCard(4, 'H'), new MyCard(1, 'S'), new MyCard(3, 'D'),
                new MyCard(4, 'C'), new MyCard(4, 'S'), new MyCard(1, 'D'),
                new MyCard(3, 'S'), new MyCard(4, 'D')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);

            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(2, deadwoodSum);
        }
    }
}