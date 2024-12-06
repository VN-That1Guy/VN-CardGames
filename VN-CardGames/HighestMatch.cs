using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VN_CardGames
{
    class HighestMatch(int playercount = 1) : BaseGame("Highest Match", "The player wth a Suit that has the highest total value wins! With only 4 cards, replace one of the cards in your hand with a new card or end the game here and find out who has the highest value from a suit.", playercount + 1, Startamount: 4, _MaxRounds: 9)
    {
        public new void StartGame()
        {
            InitializeGame();
            Console.ReadKey();
            Play();
        }
        public new void InitializeGame()
        {
            base.InitializeGame();
            if (Players.Count >= 2)
                Players[Players.Count - 1].ChangeName("Dealer");
        }
        public override void Play()
        {
            Random random = new Random();
            // The whole game and system is in a while loop
            // There are a few lines that are break; I'm not sure why the game doesn't stop at the end of the loop when the while condition has been met
            do
            {
                bGameEnded = Round >= MaxRounds;
                Console.Clear();

                Player.Score = Player.GetBestSuitValue();
                //string[] Menu = new string[Player.Hand.Count]; // Assuming it's always going to be 4
                int PlayerHandIndex = 0;
                //foreach (Card card in Player.Hand)
                //{
                //    Menu[PlayerHandIndex] = $"{PlayerHandIndex+1}.) {card.DisplayCard}";
                //    PlayerHandIndex++;
                //}

                // Menu Stuff here
                DisplayGameName();
                Console.WriteLine($"This is what you have, swap a card in your hand (1-4) or end the game (5)\n");
                //Player.Hand[0].DisplayCardDetails();
                //Console.Write(", Do you think the next card will be a Club or a Diamond?\n");
                foreach (Card card in Player.Hand)
                {
                    PlayerHandIndex++;
                    Console.Write($"{PlayerHandIndex}.) ");
                    card.DisplayCardDetails();
                    Console.WriteLine(); //New Line
                }
                Console.WriteLine($"{Player.Hand.Count+1}.) End Game\n");
                Console.WriteLine("Enter a number from these choices\n");
                Console.WriteLine($"Round: {Round}/{MaxRounds}\nCards Left: {drawpile.cards.Count}\nScore: {Player.Score} ({Player.GetBestSuit()})");
                //
                // Player Input stuff here
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    result--;
                    if (result >= 0 && result < 4)
                    {
                        if (drawpile.cards.Count > 0)
                        {
                            //DrawCard(Player);
                            DiscardCard(Player.Hand[result], Player);
                            Player.Hand.Insert(result, GetCard());
                            int DealerChance = random.Next(100);
                            if (DealerChance > ((Players[Players.Count - 1].Score / 52) * 100) && drawpile.cards.Count > 0) //Maximum Theoretical Score : 52 (4 Suit * 13 [King Value])
                            {
                                DiscardCard(Players[Players.Count - 1].Hand[random.Next(Players[Players.Count - 1].Hand.Count - 1)], Players[Players.Count - 1]);
                                DrawCard(Players[Players.Count - 1]);
                                Console.WriteLine("The Dealer has made a move!");
                            }
                        }
                        else
                            Console.WriteLine("No more cards left to draw!");
                        Round++;
                    }
                    else if (result == Player.Hand.Count)
                        bGameEnded = true;
                    else
                        Console.WriteLine("Pick a number within the options");
                }
                else
                {
                    Console.WriteLine("Pick a number");
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                
            }
            while (!bGameEnded);
            EndGame();
        }
        public new void EndGame()
        {
            //int[] Score = new int[Players.Count];
            int i = 0;
            Player winner = null;

            //Since there are more than one players, do a for loop to check the score of all players
             foreach (Player player in Players)
             {
                 player.Score = player.GetBestSuitValue();
             }
            for (i = 0; i < Players.Count; i++)
            {
                // Compare the score to the highest max value of the score given from the List
                // If the player's score matches the highest/max value, that is how the winner is found
                if (Players[i].Score == Players.Max(player => player.Score))
                {
                    if (winner == null)
                        winner = Players[i];
                    else
                    {
                        Console.WriteLine("A Tie!?!?!?");
                        winner = null;
                        break; // TODO: 2+ player support
                    }
                }
            }

            /* 
             if (Players[0].Score > Players[1].Score)
                winner = Players[0];
            else if (Players[0].Score < Players[1].Score)
                winner = Players[1];
            else
                winner = null;
            */

            if (winner != null)
            {
                Console.WriteLine($"The person who has the highest value is: {winner.Name}({winner.Score} [{winner.GetBestSuit()}])!");
                if (winner == Player)
                    Console.WriteLine("Congratulations, you won!");
                else
                    Console.WriteLine("You lost! Better luck next time!");
            }
            else
                Console.WriteLine("A Tie!\nEveryone is a winner!");
            
            // Display each player's hand at the end of the game (and their highest matching score value)
            foreach (Player player in Players)
            {
                Console.WriteLine($"---{player.Name}--- (Score:{player.Score} [{player.GetBestSuit()}])");
                foreach (Card card in player.Hand)
                {
                    Console.WriteLine(card.DisplayCard());
                }
                Console.WriteLine();// Empty space
            }

            base.EndGame();
        }
    }
}
