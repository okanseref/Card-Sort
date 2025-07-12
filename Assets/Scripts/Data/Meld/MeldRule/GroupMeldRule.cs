using System.Collections.Generic;
using System.Linq;
using Data.Card;

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
                if (g.Count >= 3) melds.Add(g.Take(3).ToList());
                if (g.Count == 4) melds.Add(g.ToList());
            }

            return melds;
        }

    }
}