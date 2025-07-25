﻿using System;
using System.Collections.Generic;
using Data.Meld;
using Data.Sorter;
using Extensions;
using Model.Card;
using Model.Meld.Extension;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class CardSortTest
    {
        [Test]
        public void CardSortTestEasy()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(4, 'H'), new MyCard(5, 'H'), new MyCard(6, 'H'),
                new MyCard(7, 'C'), new MyCard(7, 'D'), new MyCard(7, 'S'),
                new MyCard(9, 'D'), new MyCard(10, 'D'), new MyCard(11, 'D'),
                new MyCard(13, 'C'), new MyCard(2, 'S')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);

            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(12, deadwoodSum);
        }
        
        [Test]
        public void CardSortTestOverlap()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(3, 'H'), new MyCard(4, 'H'), new MyCard(5, 'H'),
                new MyCard(5, 'D'), new MyCard(5, 'S'), new MyCard(6, 'H'),
                new MyCard(6, 'C'), new MyCard(7, 'H'),
                new MyCard(10, 'S'), new MyCard(11, 'S'), new MyCard(12, 'S')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);
            
            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(16, deadwoodSum);
        }
        
        [Test]
        public void CardSortTestMultipleSets()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(8, 'H'), new MyCard(8, 'D'), new MyCard(8, 'C'), // Set of 8s
                new MyCard(9, 'H'), new MyCard(10, 'H'), new MyCard(11, 'H'), // Run
                new MyCard(9, 'D'), new MyCard(10, 'D'), new MyCard(11, 'D'), // Another run
                new MyCard(3, 'S'), new MyCard(12, 'C') // Deadwood cards
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);
            
            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(13, deadwoodSum);
        }
        
        [Test]
        public void AllDeadwood()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(2, 'H'), new MyCard(4, 'C'), new MyCard(5, 'S'),
                new MyCard(7, 'D'), new MyCard(8, 'S'), new MyCard(9, 'C'),
                new MyCard(11, 'H'), new MyCard(12, 'D'), new MyCard(13, 'S'),
                new MyCard(6, 'C'), new MyCard(3, 'H')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);
            
            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(74, deadwoodSum);
        }
        
        [Test]
        public void CardSortTwiceTest()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(4, 'H'), new MyCard(5, 'H'), new MyCard(6, 'H'),
                new MyCard(7, 'C'), new MyCard(7, 'D'), new MyCard(7, 'S'),
                new MyCard(9, 'D'), new MyCard(10, 'D'), new MyCard(11, 'D'),
                new MyCard(13, 'C'), new MyCard(2, 'S')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);
            var firstSortResult = new List<MyCard>(myCards);
            myCards.ShuffleList();
            cardSorter.SortCards(myCards, melds);

            var diffAmount = 0;
            for (int i = 0; i < myCards.Count; i++)
            {
                if (myCards[i] != firstSortResult[i])
                    diffAmount++;
            }
            
            Assert.AreEqual(0, diffAmount);
        }
    }
}