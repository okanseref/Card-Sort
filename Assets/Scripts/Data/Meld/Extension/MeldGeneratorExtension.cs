using System.Collections.Generic;
using System.Linq;
using Data.Card;
using UnityEngine;

namespace Data.Meld.Extension
{
    public static class MeldRuleExtensions
    {
        public static List<List<MyCard>> GenerateAllMelds(this List<IMeldRule> meldGenerators, List<MyCard> myCards)
        {
            var melds = new List<List<MyCard>>();
            
            foreach (var generator in meldGenerators)
            {
                melds.AddRange(generator.GenerateMelds(myCards));
            }

            return melds;
        }
        
        public static int CalculateDeadwoodSum(this List<List<MyCard>> melds, List<MyCard> myCards)
        {
            // Sort melds by descending length (longest first) otherwise might miss longer pattern
            var sortedMelds = melds.OrderByDescending(m => m.Count).ToList();

            int i = 0;
            int deadwoodSum = 0;

            while (i < myCards.Count)
            {
                bool matched = false;

                foreach (var meld in sortedMelds)
                {
                    if (i + meld.Count <= myCards.Count)
                    {
                        bool isMatch = true;
                        for (int j = 0; j < meld.Count; j++)
                        {
                            if (!myCards[i + j].Equals(meld[j]))
                            {
                                isMatch = false;
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            matched = true;
                            i += meld.Count; // skip matched meld
                            break;
                        }
                    }
                }

                if (!matched)
                {
                    deadwoodSum += myCards[i].Rank;
                    Debug.Log($"Deadwood: {myCards[i]} (value {myCards[i].Rank})");
                    i++;
                }
            }

            Debug.Log($"Total Deadwood Sum: {deadwoodSum}");
            return deadwoodSum;
        }
    }

}