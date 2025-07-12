using System.Collections.Generic;
using Data.Card;

namespace Data.Sorter
{
    public interface ICardSorter
    {
        public void SortCards(List<MyCard> myCards, List<List<MyCard>> melds);
    }
}