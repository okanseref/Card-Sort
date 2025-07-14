using System.Collections.Generic;
using UnityEngine;

namespace View.Card
{
    [CreateAssetMenu(fileName = "CardViewInfoCollection", menuName = "InfoSO/CardViewInfoCollection")]
    public class CardViewInfoCollection : ScriptableObject
    {
        public List<CardViewInfo> CardViewInfos;
    }
}