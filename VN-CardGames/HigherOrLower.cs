using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VN_CardGames
{
    class HigherOrLower() : ApplesOrOranges(name: "Higher Or Lower", instruction:"Similar to Clubs or Diamonds, draw one card and guess if the next card has a higher value or a lower value. Equal value counts as higher value", length: 52)
    {
        private bool bHigherOrLower; // Did the player decide higher or lower

        public new void StartGame()
        {
            InitializeGame();
            Console.ReadKey();
            Play();
        }
        public override void Play()
        {
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
                "1.) Higher",
                "2.) Lower",
                "3.) End Game\n",
                "Enter a number from these choices\n"
                };
                //
                //Write the Menu
                Console.Write($"You have drawn a ");
                Player.Hand[0].DisplayCardDetails();
                Console.WriteLine("\nDo you think the next card will be a Higher or Lower value?\n");
                foreach (string menu in Menu)
                {
                    Console.WriteLine(menu);
                }
                Console.WriteLine(Scoreboard);
                //
                //Get Player input
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 1:
                            bHigherOrLower = true;
                            break;
                        case 2:
                            bHigherOrLower = false;
                            break;
                        case 3:
                            EndGame();
                            return;
                        default:
                            Console.WriteLine("Pick a valid number");
                            break;
                    }
                    // Check input here
                    if (result <= 2 && result > 0)
                    {
                        DrawCard(Player);
                        CompareCards(Player.Hand[0], Player.Hand[1]);
                        Console.WriteLine("\n" + RevealAnswer(Player.Hand[0], Player.Hand[1]));
                        DiscardCard(Player.Hand[0], Player);
                    }
                }

                Console.ReadKey();
            }
            while (!bGameEnded);
            EndGame();
            //Play();
        }

        public new bool CompareCards(Card A, Card B)
        {
            A.DisplayCardDetails();
            Console.Write(" / ");
            B.DisplayCardDetails();
            if (A.Suit == B.Suit)
                return true;
            return false;
        }

        public override string RevealAnswer(Card A, Card B)
        {
            string output = $"Sorry {Player.Name}! No points awarded.";
            // Higher: grant a point if the player is right or if the value matches instead
            // Lower: grant a point if the player is correct but do not count the values matching
            if (bHigherOrLower && A.Value <= B.Value || !bHigherOrLower && A.Value > B.Value)
            {
                output = $"You're right, {Player.Name}! You have been awarded 1 point!";
                Player.Score++;
            }
            //Console.WriteLine(output);

            return output;
        }

    }
}
