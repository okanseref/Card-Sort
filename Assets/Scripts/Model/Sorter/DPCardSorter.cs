using System;
using System.Collections.Generic;
using System.Linq;
using Data.Card;
using UnityEngine;

namespace Data.Sorter
{
    public class DPCardSorter
    {
        public void SortCards(List<MyCard> myCards, List<List<MyCard>> melds)
        {
            int n = myCards.Count;
            int maxState = 1 << n;
            int totalRankSum = myCards.Sum(c => c.Rank);

            int[] dp = Enumerable.Repeat(int.MaxValue, maxState).ToArray();
            var parent = new (int prevState, int meldMask)[maxState];

            dp[0] = totalRankSum;

            foreach (var meld in melds)
            {
                // compute bitmask and meld rank-sum
                int mask = 0, sum = 0;
                foreach (var myCard in meld)
                {
                    int idx = myCards.IndexOf(myCard);
                    mask |= 1 << idx;
                    sum += myCard.Rank;
                }

                LogToConsole($"Meld: {string.Join(", ", meld)}  Mask: {Convert.ToString(mask, 2).PadLeft(n, '0')}  Value: {sum}");

                // try to apply this meld on every existing state
                for (int state = 0; state < maxState; state++)
                {
                    if (dp[state] == int.MaxValue) continue;
                    if ((state & mask) != 0) continue; // overlap

                    int next = state | mask;
                    int newScore = dp[state] - sum;
                    if (newScore < dp[next])
                    {
                        dp[next] = newScore;
                        parent[next] = (state, mask);
                        LogToConsole($" - state {Convert.ToString(state,2).PadLeft(n,'0')} → {Convert.ToString(next,2).PadLeft(n,'0')}: score {dp[state]} → {newScore}");
                    }
                }

                LogToConsole("\n");
            }

            // find best end-state
            int bestState = 0, bestScore = int.MaxValue;
            for (int s = 0; s < maxState; s++)
            {
                if (dp[s] < bestScore)
                {
                    bestScore = dp[s];
                    bestState = s;
                }
            }
            
            LogToConsole($"\nMinimum deadwood score: {bestScore}");

            // backtrack melds
            var usedMelds = new List<List<MyCard>>();
            int cur = bestState;
            while (cur != 0)
            {
                var (prev, mask) = parent[cur];
                // reconstruct which myCards in this mask
                var meld = new List<MyCard>();
                for (int i = 0; i < n; i++)
                    if (((mask >> i) & 1) == 1)
                        meld.Add(myCards[i]);
                meld.Sort((x,y)=> x.Rank - y.Rank);
                usedMelds.Add(meld);

                cur = prev;
            }

            usedMelds.Sort((x,y)=> x.Sum((x1)=> x1.Rank) - y.Sum((y1)=> y1.Rank));
            
            // compute deadwood myCards
            var deadwood = new List<MyCard>();
            for (int i = 0; i < n; i++)
                if (((bestState >> i) & 1) == 0)
                    deadwood.Add(myCards[i]);
            
            deadwood.Sort((x,y)=> (x.Rank - y.Rank) * 100 + x.Suit - y.Suit);

            myCards.Clear();
            
            LogToConsole("\n▶Melds used:");
            foreach (var m in usedMelds)
            {
                LogToConsole("  • " + string.Join(", ", m));
                myCards.AddRange(m);
            }

            LogToConsole("\nDeadwood myCards:");
            foreach (var c in deadwood)
            {
                LogToConsole($"  • {c}  (value {c.Rank})");
                myCards.Add(c);
            }

            LogToConsole("\nSorted My Cards:");
            foreach (var myCard in myCards)
                LogToConsole($"  • {myCard}  (value {myCard.Rank})");
        }
        
        private void LogToConsole(string output)
        {
#if UNITY_EDITOR
            Debug.Log(output);
#endif
        }
    }
}