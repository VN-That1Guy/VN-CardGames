using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VN_CardGames
{
    public class Player
    {
        public string Name = "Unnamed";
        public List<Card> Hand = new List<Card>();
        public int Score = 0;
        public int[] SuitValues = new int[4];
        public CardSuit HighValueSuit;

        public void ChangeName(string name)
        {
            Name = name;
        }

        public int GetScore()
        {
            return Score;
        }

        /* Highest Match functions */

        // Get the best matching total values from the best suit
        public int GetBestSuitValue()
        {
            int bestSuit = 0;
            
            for (int i = 0; i < SuitValues.Length; i++)
            {
                SuitValues[i] = 0;
                foreach (Card card in Hand)
                {
                    if (i == (int)card.Suit)
                        SuitValues[i] += card.Value;
                }
            }
            
            bestSuit = SuitValues.Max();
            return bestSuit;
        }

        // Find the Suit with the most total matching value
        public CardSuit GetBestSuit()
        {

            for (int Value = 0; Value < SuitValues.Length; Value++)
            {
                if (SuitValues[Value] == SuitValues.Max()) 
                {
                    HighValueSuit = (CardSuit)Enum.ToObject(typeof(CardSuit), Value);
                    break;
                }
            }
            return HighValueSuit;
        }
    }

    public class NPC : Player
    {
        public NPC() { Name = "NPC"; }
    }
}