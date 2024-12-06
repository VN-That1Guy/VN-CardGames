using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VN_CardGames
{
    public class Deck
    {
        public int DeckLength = 0;
        public int CardsInSuit = 13;
        public int SuitIndex = 0;
        public List<Card> cards = new List<Card>();
        
        public void InitializeDeck() // Give and initialize the default amount of cards
        {
            SuitIndex = 0;
            if (DeckLength <= 0)
                return;
            for (int i = 1; i <= CardsInSuit; i++)
            {
                cards.Add(new Card()
                {
                    Value = i,
                    Suit = (CardSuit)SuitIndex
                });
                if (i >= CardsInSuit && cards.Count < DeckLength)
                {
                    i = 0;
                    if (SuitIndex < 3)
                    { 
                        SuitIndex++;
                        if (SuitIndex > 3)
                            SuitIndex = 0;
                    }
                }
            }
            foreach (Card card in cards)
            {
                card.Initialize();
            }
        }
    }
}
