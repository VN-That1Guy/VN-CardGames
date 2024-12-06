using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VN_CardGames
{
    public class ApplesOrOranges(string name= "Clubs or Diamonds?", string instruction = "Earn a point if you guess right on if your card's Suit will match the next card's Suit or not!", int players = 1, int length = 26, int start = 1) : BaseGame(name, instruction, players:players,_decklength:length,Startamount:start)
    {
        public new void StartGame()
        {
            InitializeGame();
            Console.ReadKey();
            Play();
        }

        public override void Play()
        {
            CardSuit Choice = 0;
            // The whole game and system is in a while loop
            // There are a few lines that are break; I'm not sure why the game doesn't stop at the end of the loop when the while condition has been met
            do
            {
                bGameEnded = this.drawpile.cards.Count <= 0;
                Console.Clear();
                if (bGameEnded)
                    break;
                // Menu Data
                DisplayGameName();
                string Scoreboard = $"Cards Left: {drawpile.cards.Count}\nScore: {Player.Score}";
                string[] Menu =
                {
                "1.) Club",
                "2.) Diamond",
                "3.) End Game\n",
                "Enter a number from these choices\n"
                };
                // Write the data here
                Console.Write($"You have: ");
                Player.Hand[0].DisplayCardDetails();
                Console.WriteLine("\nDo you think the next card will be a Club or a Diamond?\n");
                foreach (string menu in Menu)
                {
                    Console.WriteLine(menu);
                }
                Console.WriteLine(Scoreboard);
                // Get the player input
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 1:
                            Choice = CardSuit.Clubs;
                            break;
                        case 2:
                            Choice = CardSuit.Diamonds;
                            break;
                    }
                    if (result <= 2) // If the input is convertable and is within the given numbers, progress the game
                    {
                        DrawCard(Player);
                        CompareCards(Player.Hand[0], Player.Hand[1]);
                        Console.WriteLine(RevealAnswer(Choice, Player.Hand[1]));
                        DiscardCard(Player.Hand[0], Player);
                    }
                    else if (result == 3)
                    {
                        bGameEnded = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pick a valid number");
                    }
                }
                else
                    Console.WriteLine("Pick a Number");

                Console.ReadKey(); 
            }
            while(!bGameEnded);
            EndGame();
            //Play();
        }

        public new void EndGame()
        {
            Console.Clear();
            Console.WriteLine($"Your Score: {Player.Score}");
            base.EndGame();
        }

        public virtual bool CompareCards(Card A, Card B)
        {
            A.DisplayCardDetails();
            Console.Write(" / ");
            B.DisplayCardDetails();
            if (A.Suit == B.Suit)
            {
                Console.WriteLine("\nThe suits match");
                return true;
            }
            else
                Console.WriteLine("\nThe suits did not match");
            return false;
        }

        public virtual string RevealAnswer(CardSuit Answer, Card B)
        {
            string output = $"Sorry {Player.Name}! No points awarded.";

            if (Answer == B.Suit) 
            {
                output = $"You're right, {Player.Name}! You have been awarded 1 point!";
                Player.Score++;
            }

            return output;
        }

        public virtual string RevealAnswer(Card A, Card B)
        {
            return "Hey! The Wrong version of this method is being called!";
        }
    }
}
