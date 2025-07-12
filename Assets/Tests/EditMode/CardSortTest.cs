using System.Collections.Generic;
using Data.Card;
using Data.Meld;
using Data.Meld.Extension;
using Data.Sorter;
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
            
            Assert.AreEqual(deadwoodSum, 15);
        }
        
        [Test]
        public void CardSortTestOverlap()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(3, 'H'), new MyCard(4, 'H'), new MyCard(5, 'H'), // Run
                new MyCard(5, 'D'), new MyCard(5, 'S'), new MyCard(6, 'H'), // 3x5 + continuation
                new MyCard(6, 'C'), new MyCard(7, 'H'),                     // Another run possible
                new MyCard(10, 'S'), new MyCard(11, 'S'), new MyCard(12, 'S') // Run
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var cardSorter = new DPCardSorter();
            cardSorter.SortCards(myCards, melds);
            
            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(deadwoodSum, 16);
        }
    }
}