using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MindfireBlackJack
{
    public class Deck
    {
        private Stack<Card> cards;

        private Card[] defaultCards;

        public Deck()
        {
            try
            {
                cards = new Stack<Card>();
                // Default deck that should not be used in play.
                defaultCards = new Card[52];
                for (int index = 0; index < 13; index++)
                {
                    Card newHeart = new Card(index + 1, Suits.Hearts);
                    Card newDiamond = new Card(index + 1, Suits.Diamonds);
                    Card newSpade = new Card(index + 1, Suits.Spades);
                    Card newClub = new Card(index + 1, Suits.Clubs);

                    // Add cards to the default deck
                    defaultCards[index] = newHeart;
                    defaultCards[index + 13] = newDiamond;
                    defaultCards[index + 26] = newSpade;
                    defaultCards[index + 39] = newClub;
                }

                shuffleDeck(4);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public Card dealCard()
        {
            // Deals card from top of deck (stack)
            return cards.Pop();
        }

        public void shuffleDeck(int numTimesToShuffle = 1)
        {
            try
            {
                Random rand = new Random();
                Card[] newDeck = defaultCards;
                for (int index = 0; index < numTimesToShuffle; index++)
                {
                    for (int deckIndex = newDeck.Length - 1; deckIndex > 0; --deckIndex)
                    {
                        int next = rand.Next(deckIndex + 1);
                        Card temp = newDeck[deckIndex];
                        newDeck[deckIndex] = newDeck[next];
                        newDeck[next] = temp;
                    }
                }
                cards.Clear();

                foreach (Card pushCard in newDeck)
                {
                    cards.Push(pushCard);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }        
    }
}
