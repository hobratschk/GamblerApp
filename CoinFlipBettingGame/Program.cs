using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CoinFlipBettingGame
{
    public class Player
    {
        private string name;
        private int totalPoints;
        
        public Player(string name, int totalPoints)
        {
            this.name = name;
            this.totalPoints = totalPoints;
        }

        public string Name
        {
            get { return name; }
            set { name = value;  }
        }

        public int TotalPoints
        {
            get { return totalPoints;  }
            set { totalPoints = value; }
        }
    };

    public class Winner
    {
        private static int game = 0;
        private string name;
        private int totalPoints;
        private int gameNumber;

        public Winner(string name, int totalPoints)
        {
            gameNumber = ++game;
            this.name = name;
            this.totalPoints = totalPoints;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int TotalPoints
        {
            get { return totalPoints; }
            set { totalPoints = value; }
        }

        public int GameNumber
        {
            get { return gameNumber;  }
        }

    }

    class Program
    {
        public static List<Winner> WinnerList = new List<Winner>();
        static void Main(string[] args)
        {
        Gamble();
        }

        public static void Gamble()
        {
            Console.WriteLine("Let's gamble! How many players would you like to have?");
            string stringPlayers = Console.ReadLine();
            int intPlayers;
            bool isNumber = Int32.TryParse(stringPlayers, out intPlayers);
            if (isNumber && intPlayers > 0)
            {
                
                List<Player> PlayerList = new List<Player>();
                for (int i = 1; i <= intPlayers; i++)
                {
                    Console.WriteLine("Enter the name of player " + i + ".");
                    string name = Console.ReadLine();
                    PlayerList.Add(new Player(name, 100));
                }

                for (int i = 1; i < 4; i++) {
                    Console.WriteLine("Alright, it's round " + i + " of 3. How much would you like to bet on Round " + i + "?");
                    string stringBet = Console.ReadLine();
                    Console.WriteLine("Great. Would you like to bet on heads or tails? (h = heads, t = tails)");
                    string pickFlipValue = Console.ReadLine();
                    int intBet;
                    bool betIsNum = Int32.TryParse(stringBet, out intBet);
                    if (betIsNum)
                    {
                        Random randomFlip = new Random();
                        string[] headsOrTails = { "heads", "tails" };

                        foreach (Player player in PlayerList)
                        {
                            int flipValue = randomFlip.Next(0, headsOrTails.Length);
                            if ((headsOrTails[flipValue] == "heads" && pickFlipValue == "h") || (headsOrTails[flipValue] == "tails" && pickFlipValue == "t"))
                            {
                                player.TotalPoints += intBet;
                            }
                            else
                            {
                                player.TotalPoints -= intBet;
                            }
                        }
                        ScoreBoard(i, PlayerList);
                        
                    }
                    else {
                        Console.WriteLine("You have to enter a positive integer for the bet. Sorry, you have to start over.");
                        Gamble();
                    }
                }
                var winner = PlayerList.OrderByDescending(player => player.TotalPoints).First();
                WinnerList.Add(new Winner(winner.Name, winner.TotalPoints));
                // Console.WriteLine(WinnerList[0].Name);
                Console.WriteLine("Would you like to play again? Or view previous winners? (p for play, w for winners)");
                string answer = Console.ReadLine();
                if (answer == "p")
                {
                    Gamble();
                }
                else if (answer == "w")
                {
                    PrevWinners(WinnerList);
                }
                else
                {
                    Console.WriteLine("You didn't enter a valid answer. We'll start the game over in case you want to play again.");
                    Gamble();
                }
            } else
            {
                Console.WriteLine("You have to enter a positive integer for the number of players.");
                Gamble();
            }
        }

        public static string ScoreBoard( int i, List<Player> players)
        {
            foreach (Player player in players)
            {
                Console.WriteLine("After round " + i + ", " + player.Name + " has a score of " + player.TotalPoints + ".");
            };
            return null;
        }

        public static void PrevWinners(List<Winner> WinnerList)
        {
            Console.WriteLine("Would you like to view a list of all previous winners? Or would you like to view a winner from a specific game? ('a' for all previous winners, 's' for a specific game)");
            string answer = Console.ReadLine();
            if (answer == "a")
            {
                foreach (Winner winner in WinnerList) { Console.WriteLine("Game " + winner.GameNumber + " winner: " + winner.Name + " with a score of " + winner.TotalPoints + "."); }
                Gamble();
            }
            else if (answer == "s")
            {
                Console.WriteLine("For which game would you like to view a winner? Just enter a number.");
                string whichGame = Console.ReadLine();
                int intAnswer;
                bool isValidAnswer = Int32.TryParse(whichGame, out intAnswer);
                if (isValidAnswer && intAnswer <= WinnerList.Count())
                {
                    Console.WriteLine("The winner for game " + intAnswer + " was " + WinnerList[intAnswer - 1].Name + " with a score of " + WinnerList[intAnswer - 1].TotalPoints + ".");
                    Gamble();
                } else
                {
                    Console.WriteLine("You need to enter a valid number (a number less than the the total number of games played).");
                    PrevWinners(WinnerList);
                }
            }
            else
            {
                Console.WriteLine("You didn't enter a valid answer. We'll start the game over in case you want to play again.");
                Gamble();
            }
        }
    }
}
