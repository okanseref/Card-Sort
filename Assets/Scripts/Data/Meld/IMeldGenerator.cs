using System.Collections.Generic;
using Data.Card;

namespace Data.Meld
{
    public interface IMeldGenerator
    {
        public List<List<MyCard>> GenerateMelds(List<MyCard> myCards);
    }
}