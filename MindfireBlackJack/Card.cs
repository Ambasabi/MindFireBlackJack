using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MindfireBlackJack
{
    public enum Suits
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }
    public class Card
    {

        public int faceValue { get; private set; }
        public Suits suit { get; private set; }
        public int cardWorth { get; set; }
        public Card(int newFaceValue, Suits newSuit)
        {
            try
            {
                faceValue = newFaceValue;
                suit = newSuit;

                // if card is a 2-10
                if (faceValue >= 2 && faceValue <= 10)
                {
                    cardWorth = faceValue;
                }
                // if card value is jack queen or king
                else if (faceValue > 10 && faceValue < 14)
                {
                    cardWorth = 10;
                }
                // if card value is Ace, default to 11 but we will use as a 1 if applicable
                else if (faceValue == 1)
                {
                    cardWorth = 11;
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
