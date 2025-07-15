using System.Collections.Generic;
using Data.Meld;
using Data.Sorter;
using Model.Card;
using Model.Meld.Extension;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class DeadwoodSumTest
    {
        [Test]
        public void DeadwoodTestEasy()
        {
            var myCards = new List<MyCard>
            { 
                new MyCard(1, 'A'), new MyCard(2, 'A'), new MyCard(3, 'A'), new MyCard(4, 'A'), new MyCard(5, 'A')
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(0, deadwoodSum);
        }
        
        [Test]
        public void DeadwoodTestNotOrdered()
        {
            var myCards = new List<MyCard>
            { 
                new MyCard(2, 'A'), new MyCard(1, 'A'), new MyCard(3, 'A') // out of order
            };

            var meldGenerators = new List<IMeldRule>();
            meldGenerators.Add(new GroupMeldRule());
            meldGenerators.Add(new RunMeldRule());

            var melds = meldGenerators.GenerateAllMelds(myCards);
            
            var deadwoodSum = melds.CalculateDeadwoodSum(myCards);
            
            Assert.AreEqual(6, deadwoodSum);
        }
    }
}