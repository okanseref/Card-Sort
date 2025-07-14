using System.Collections.Generic;
using Data.Card;

namespace Data.Meld
{
    public interface IMeldRule
    {
        public List<List<MyCard>> GenerateMelds(List<MyCard> myCards);
    }
}