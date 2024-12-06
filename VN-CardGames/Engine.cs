using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VN_CardGames
{
    internal class Engine
    {
        // For some reason, calling start on game does not call the right start functions in assigned Child classes
        //BaseGame game;

        public void Start()
        {
            string[] MenuChoices = 
            {
                "Welcome, select the game you want to play:",
                "1.) Clubs or Diamonds?",
                "2.) Higher or Lower?",
                "3.) Highest Match",
                "4.) Exit"
            };
            foreach (string menu in MenuChoices)
            {
                Console.WriteLine(menu);
            }
            if (int.TryParse(Console.ReadLine(), out int i))
            {
                switch (i)
                {
                    case 1:
                        //game = new ApplesOrOranges();
                        new ApplesOrOranges().StartGame();
                        break;
                    case 2:
                        new HigherOrLower().StartGame();
                        break;
                    case 3:
                        new HighestMatch().StartGame();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
                if (i >= MenuChoices.Length || i <= 0)
                    Console.WriteLine("Pick a a valid number specified in the option");
            }
            else
                Console.WriteLine("Please pick a number");

            /*if (game == null)
                Console.WriteLine("Please pick a valid number specified in the options");
            else
                Console.WriteLine("Starting up game...");

            Console.ReadKey();
            if (game != null)
                game.StartGame();*/

            Console.ReadKey();
            Console.Clear();
            Start();
        }
    }
}
