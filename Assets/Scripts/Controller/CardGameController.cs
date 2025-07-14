using System.Collections.Generic;
using Data.Card;
using UnityEngine;

namespace Controller
{
    public class CardGameController : MonoBehaviour
    {
    
        void Start()
        {
            var myCards = new List<MyCard>
            {
                new MyCard(1, 'H'), new MyCard(2, 'S'), new MyCard(5, 'D'),
                new MyCard(4, 'H'), new MyCard(1, 'S'), new MyCard(3, 'D'),
                new MyCard(4, 'C'), new MyCard(4, 'S'), new MyCard(1, 'D'),
                new MyCard(3, 'S'), new MyCard(4, 'D')
            };
            
            
        }
    }
}
