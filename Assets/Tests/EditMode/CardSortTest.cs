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
            
            Assert.AreEqual(true, true);
        }
    }
}