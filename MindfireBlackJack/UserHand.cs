using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfireBlackJack
{
    public class UserHand
    {
        public List<Card> cards { get; private set; }
        public int handWorth { get; set; }

        public UserHand(Card firstCard, Card secondCard)
        {
            cards = new List<Card>();
            // set default hand worth to 0
            handWorth = 0;

            // add first 2 cards to players hand
            addCardToHand(firstCard);
            addCardToHand(secondCard);
        }

        public void addCardToHand(Card cardToAdd)
        {
            cards.Add(cardToAdd);
            handWorth += cardToAdd.cardWorth;
        }
    }
}

