using System.Collections.Generic;
using System.Linq;
using Model.Card;

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
                        {
                            var listToAdd = new List<MyCard>(run);
                            var listToAddReversed = new List<MyCard>(run);
                            listToAddReversed.Reverse();
                            melds.Add(listToAdd);
                            melds.Add(listToAddReversed);

                        }
                    }
                }
            }

            return melds;
        }
    }
}