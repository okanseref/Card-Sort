using System.Collections.Generic;
using System.Linq;
using Extensions;
using Model.Card;

namespace Data.Meld
{
    // For 7-7-7 Melds
    public class GroupMeldRule : IMeldRule
    {
        public List<List<MyCard>> GenerateMelds(List<MyCard> myCards)
        {
            var melds = new List<List<MyCard>>();

            // sets
            foreach (var grp in myCards.GroupBy(c => c.Rank))
            {
                var g = grp.ToList();
                if (g.Count >= 3)
                {
                    var allPermutations = g.GetAllFullAndSubsetPermutations();
                    foreach (var permutation in allPermutations)
                    {
                        melds.Add(permutation);
                    }
                }
            }

            return melds;
        }

    }
}