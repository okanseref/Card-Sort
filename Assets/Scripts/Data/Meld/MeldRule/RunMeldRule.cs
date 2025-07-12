using System.Collections.Generic;
using System.Linq;
using Data.Card;

namespace Data.Meld
{
    // For 1-2-3 Melds
    public class RunMeldRule : IMeldRule
    {
        public List<List<MyCard>> GenerateMelds(List<MyCard> myCards)
        {
            var melds = new List<List<MyCard>>();

            foreach (var grp in myCards.GroupBy(c => c.Suit))
            {
                var sorted = grp.OrderBy(c => c.Rank).ToList();
                for (int i = 0; i <= sorted.Count - 3; i++)
                {
                    var run = new List<MyCard> { sorted[i] };
                    for (int j = i + 1; j < sorted.Count && sorted[j].Rank == run.Last().Rank + 1; j++)
                    {
                        run.Add(sorted[j]);
                        if (run.Count >= 3)
                            melds.Add(new List<MyCard>(run));
                    }
                }
            }

            return melds;
        }

        public bool IsCardApplicableToMeld(List<MyCard> meld, MyCard cardToAdd)
        {
            if (!meld.Any())
                return true;

            if (meld[0].Suit != cardToAdd.Suit)
                return false;

            if (meld.Last().Rank + 1 == cardToAdd.Rank)
                return true;

            return false;
        }
    }
}