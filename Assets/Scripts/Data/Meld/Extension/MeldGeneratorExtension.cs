using System.Collections.Generic;
using Data.Card;

namespace Data.Meld.Extension
{
    public static class MeldGeneratorExtensions
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
    }

}