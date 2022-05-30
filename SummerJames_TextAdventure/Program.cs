using System;

namespace TextAdventure_SJ_SDG20
{
    class Program
    {
        static void Main(string[] args)
        {
            ModeSelection();
        }

        static void Run(bool ez)
        {
            //Future reference
            //GC.Collect();
            Game game = new Game(ez);

            //Game runs while game over is false
            while (!game.gameOver)
            {
                game.Update();
            }
            Console.WriteLine("\nDo you wanna try again? y/n\n");
            while (game.gameOver && game.isQuiting == false)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "y" || input == "yes")
                {
                    //Resetting all static variables except endings
                    Game.monsterDead = false;
                    Game.playerHealth = 20;
                    Game.playerDamage = 2;
                    Run(ez);
                    break;
                }
                else if (input == "n" || input == "no")
                {
                    Console.WriteLine("\nAight.\n");
                    game.gameOver = true;
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid input, try again.");
                }
            }
        }

        static void ModeSelection()
        {
            Console.WriteLine("Before we get started, do you want play on normal mode (reccomended) or easy mode?\n");
            Console.WriteLine("This game encourages self exploration and trial and error.\n" +
                "If this is not something you think you will enjoy, easy mode will give you a more guided experience.\n");
            Console.WriteLine("Type 'n' for normal mode and 'e' for easy mode");
            while (true)
            {
                string answer = Console.ReadLine().ToLower();
                if (answer == "e" || answer == "ez" || answer == "easy")
                {
                    Run(true);
                    break;
                }
                else if (answer == "n" || answer == "normal")
                {
                    Run(false);
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid input, try again.");
                }
            }
        }
    }
}
