using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VN_CardGames
{
    public class Card
    {
        public CardSuit Suit;
        public int Value = 0;
        public bool bFaceCard = false;
        public string FaceCardName = "Not a Face Card";
        public bool bAce = false;

        public void Initialize()
        {
            if (Value == 1 || Value > 10)
            {
                bFaceCard = true;
                switch (Value)
                {
                    case 1:
                        FaceCardName = "Ace";
                        bAce = true;
                        break;
                    case 11:
                        FaceCardName = "Jack";
                        break;
                    case 12:
                        FaceCardName = "Queen";
                        break;
                    case 13:
                        FaceCardName = "King";
                        break;
                }
            }
        }
        public string DisplayCard()
        {
            string output = "";
            if (bFaceCard)
            {
                output += FaceCardName;
            }
            else
                output += Value;

            output += $" of {Suit}";
            return output;
        }

        // Stand-in "Visual" Presentations: setting the background color to the texts white and the text itself correspond to the color of their suit
        public void DisplayCardDetails()
        {
            Console.BackgroundColor = ConsoleColor.White;
            if (Suit == CardSuit.Clubs || Suit == CardSuit.Spades)
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (Suit == CardSuit.Diamonds || Suit == CardSuit.Hearts)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(DisplayCard());
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}