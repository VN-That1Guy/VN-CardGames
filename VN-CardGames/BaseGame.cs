using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VN_CardGames
{
    public class BaseGame
    {
        public int Decks = 1; // How many Decks to play with by default
        public int Length = 52; 
        public int DefaultHand = 5;
        public string GameName = "Base Game";
        public string Instructions = "Instruction not found";
        public int PlayerCount = 1;
        public bool bGameEnded = false;
        public int Round = 0;
        public int MaxRounds = 0;
        public Deck drawpile = new Deck();
        public List<Card> DiscardPile = new List<Card>();
        public Player Player = new Player() { Name = "Player" };
        public List<Player> Players = new List<Player>();
        public Random rng = new Random();
        public bool bGoBack = false;

        public BaseGame() { }
        public BaseGame(string _name = "Game Name not found!", string _instructions = "Instruction not found!", int players = 1, int _decks = 1, int _decklength = 52, int Startamount = 5, int _MaxRounds = 0)
        {
            GameName = _name;
            Instructions = _instructions;
            PlayerCount = players;
            Decks = _decks;
            Length = _decklength;
            DefaultHand = Startamount;
            MaxRounds = _MaxRounds;
        }
        
        public void DisplayGameInfo()
        {
            Console.WriteLine($"{GameName}\n{Instructions}\nPlayers: {PlayerCount}\nDecks: {Decks}\nDeck Length:{Length}\nStarting Amount: {DefaultHand}");
            if (MaxRounds > 0)
                Console.WriteLine($"Rounds To Play: {MaxRounds}");
            Console.WriteLine("Press Any Key to continue");
        }

        public void DisplayGameName()
        {
            Console.WriteLine($"Game: {GameName}");
        }
        public void StartGame()
        {
            
        }
        public void InitializeGame()
        {
            Console.Clear();
            DisplayGameInfo();
            drawpile.DeckLength = Length * Decks;
            if (Decks < 0)
            {
                Console.WriteLine($"Hey! There are no cards available to play! Set the default decks to 1 or more (1 Deck = {drawpile.DeckLength} Cards).");
                return;
            }
            drawpile.InitializeDeck();
            ShuffleCards(drawpile.cards);
            for (int i = 0; i < PlayerCount; i++)
            {
                if (i == 0)
                    Players.Add(Player);
                else
                    Players.Add(new Player());

                for (int i1 = 0; i1 < DefaultHand; i1++)
                {
                    DrawCard(Players[i]);
                }
                /*foreach (Card card in Player.Hand)
                {
                    card.DisplayCard();
                }*/
            }
            /* Debug
             * Console.WriteLine($"Cards left in the stack: {drawpile.cards.Count}");
            Console.WriteLine("Remaining Cards: ");
            foreach (Card card in drawpile.cards)
            {
                Console.WriteLine(card.DisplayCard());
            }
            */
            // I was hoping that a child class that called upon this base function would call the child's version of the Play() function, it did not.
            //  Play();
        }
        public void EndGame()
        {
            Console.WriteLine("The Game has ended!");
            //Console.ReadKey();
        }
        public virtual void Play()
        {
            EndGame();
        }
        
        // Function to grab a card from the drawpile
        public bool DrawCard(Player Actor)
        {
            if (drawpile.cards.Count > 0)
            {
                Actor.Hand.Add(GetCard());
                return true;
            }

            Console.WriteLine("No available card!");
            return false;
        }
        // Get the top card from the drawpile
        public Card GetCard()
        {
            int i = drawpile.cards.Count - 1;
            Card Drawncard = new Card()
            {
                Value = drawpile.cards[i].Value,
                bFaceCard = drawpile.cards[i].bFaceCard,
                FaceCardName = drawpile.cards[i].FaceCardName,
                bAce = drawpile.cards[i].bAce,
                Suit = drawpile.cards[i].Suit
            };
            //Card Drawncard = drawpile.cards[drawpile.cards.Count-1]; // Commented out because it gets removed with the drawpile it's drawn from. Seems like all properties and functions are shared and synchronized if a different instance is assigned to an existing one


            drawpile.cards.RemoveAt(i);
            //Console.WriteLine(drawpile.cards.Count);

            //Actor.Hand.Add(Drawncard);

            return Drawncard;
        }

        // Discard a specified card from a specified player
        public Card DiscardCard(Card DrawnCard, Player Actor)
        {
            Card card = new Card()
            {
                Value = DrawnCard.Value,
                bFaceCard = DrawnCard.bFaceCard,
                FaceCardName = DrawnCard.FaceCardName,
                bAce = DrawnCard.bAce,
                Suit = DrawnCard.Suit
            };

            DiscardPile.Add(card);
            Actor.Hand.Remove(DrawnCard);

            return card;
        }
        
        //Modifed Fisher-Yates Shuffle from https://www.dotnetperls.com/fisher-yates-shuffle
        public void ShuffleCards(List<Card> array)
        {
            int n = array.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + rng.Next(n - i);
                (array[i], array[r]) = (array[r], array[i]);
            }
        }

    }
}