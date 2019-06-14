using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tactical_Space_Cheese_Racer
{

    struct Player
    {
        public string Name;
        public int Pos;
    }

    class Program
    {
        const int EndSquare = 64;
        static int NumberOfPlayers;
        //used to detect gameover state.
        static bool gameOver = false;
        //Non RNG dice
        static int diceValuePos = 0;
        //1D array that holds the player information
        static Player[] players = new Player[4];
        // RNG not used currently
        static Random diceRandom = new Random();
        //pre set array for testing dice
        static int[] diceValues = new int[] { 4, 4, 2, 6 };

        static void PlayerTurn(int PlayerNo, int distance)
        {
            bool IsPosPowerSquare = false;

            //Adds distance (Dice throw amount) to the players position
            players[PlayerNo].Pos = players[PlayerNo].Pos + distance;
            Console.WriteLine("{0} rolled a {1} and is now at square {2}", players[PlayerNo].Name, distance, players[PlayerNo].Pos);

            //Checks if they player has reached the end of the board and therefore won.
            if (players[PlayerNo].Pos >= EndSquare)
            {
                Console.WriteLine("Player : {0} has reached square {1} and has won the game.", players[PlayerNo].Name, players[PlayerNo].Pos);
                Console.ReadLine();
                gameOver = true;
            }

            //Console.Write("Would you like");
            IsPosPowerSquare = CheesePowerSquare(players[PlayerNo].Pos, PlayerNo);

            if (IsPosPowerSquare == true)
            {
                Console.WriteLine("");
                TacticDice(PlayerNo);
            }
            else
            {
                Console.WriteLine("");
            }

        }

        // plays a turn for all the players in a game
        // stops moving players when someone has won
        static void GameTurn()
        {

            for (int i = 0; i < NumberOfPlayers; i = i + 1)
            {
                bool AcceptableInput = false;
                string TacticalDiceChoice;

                int distancechanged = DiceThrow();
                PlayerTurn(i, distancechanged);

                do
                {
                    Console.Write("{0} : Would you like to roll the tactical dice? (Y/N)", players[i].Name);
                    TacticalDiceChoice = Console.ReadLine();
                    //converts the input to lower case
                    string UpperTacticalDiceChoice = TacticalDiceChoice.ToUpper();

                    if (UpperTacticalDiceChoice  == "y" || UpperTacticalDiceChoice == "Y")
                    {
                        Console.WriteLine("");
                        TacticDice(i);
                        AcceptableInput = true;
                    }
                    else if (UpperTacticalDiceChoice == "n" || UpperTacticalDiceChoice == "N")
                    {
                        AcceptableInput = true;
                        Console.WriteLine("");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You must Enter Y/N");
                        AcceptableInput = false;
                    } 

                }
                while (AcceptableInput == false);

            }

        }

        //gets the number of players and their names
        //places them all at the start of the game
        static void ResetGame()
        {
            int counter = 0;
            int MaxPlayers = 4;
            int MinPlayers = 2;

            //Asks for the number of players and validates the input.
            do
            {

                NumberOfPlayers = 0;

                Console.Write("Enter the desired number of players : ");
                NumberOfPlayers = int.Parse(Console.ReadLine());
                Console.WriteLine("");

                if (NumberOfPlayers > MaxPlayers || NumberOfPlayers < MinPlayers)
                {
                    Console.WriteLine("You cannot have less than 2 players or more than 4");
                }    

            }
            while (NumberOfPlayers > MaxPlayers || NumberOfPlayers < 2);

            //Asks for the names of each player.
            for (int i = 0; i < NumberOfPlayers; i = i + 1)
            {
                counter = i + 1;
                Console.Write("Enter the name of player " + counter + ": ");
                players[i].Name = Console.ReadLine();
            }

            //Resets all players.Pos to 0 (Even if there are less than 4)
            for (int i = 0; i < 4; i = i + 1)
            {
                counter = i + 1;
                players[i].Pos = 0;
            }

            Console.ReadLine();
        }

        //Input the four Player names and re/set
        static void Main()
        {
            string RestartYN;
            bool AcceptableInput = false;
            ResetGame();

            // Calls gameTurn until game over state is achieved. Temporary
            do
            {
                GameTurn();
                ShowStatus();
            }
            while (gameOver == false);

            do
            {
                Console.Write("Would you like to restart the game Y/N: ");
                RestartYN = Console.ReadLine();
                //converts the input to lower case

                RestartYN = RestartYN.ToUpper();

                if (RestartYN == "y" || RestartYN == "Y")
                {
                    Console.WriteLine("");
                    gameOver = false;
                    AcceptableInput = true;
                    Main();

                }
                else if (RestartYN == "n" || RestartYN == "N")
                {
                    gameOver = true;
                    AcceptableInput = true;
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("You must Enter Y/N");
                    AcceptableInput = false;
                }
            } while (AcceptableInput == false);

            Console.WriteLine("End of Program");
            Console.ReadLine();
        }

        static void ShowStatus()
        {
            //TODO: Show the position of each player on the Board.
            Console.WriteLine("Tactical Space Cheese Racer Status Report");
            Console.WriteLine("=========================================");
            Console.WriteLine("");
            Console.WriteLine("There are {0} players in the game", NumberOfPlayers);

            for (int i = 0; i < NumberOfPlayers; i = i + 1)
            {
                Console.WriteLine("{0} Is on square {1}", players[i].Name, players[i].Pos);
            }
            Console.WriteLine("");
            Console.ReadLine();

        }

        //Random dice throw and returns to Main();
        static int DiceThrow()
        {
            int spots = diceRandom.Next(1, 7);
            //Console.WriteLine(spots);
            return spots;
        }

        static int PresetDiceThrow()
        {

            int spots = diceValues[diceValuePos];
            diceValuePos = diceValuePos + 1;
            if (diceValuePos == diceValues.Length) diceValuePos = 0;
            return spots;
            
        }

        static bool CheesePowerSquare(int pos, int PlayerNo)
        {
            //sets up an array with the cheese square positions.
            int[] CheeseSquares = new int[7] { 8, 15, 28, 33, 45, 55, 59 };

            //TODO: write a method that checks through the
            //cheese square positions and returns true if the square has cheese power
            for (int i = 0; i < 7; i = i + 1)
            {
                if (pos == CheeseSquares[i])
                {
                    Console.WriteLine("");
                    Console.WriteLine("Player {0} has landed on power cheese power square {1}", players[PlayerNo].Name, CheeseSquares[i]);
                    return true;
                }
            
            }
            return false;
        }

        static void TacticDice(int player)
        {

            int DiceValue = DiceThrow();

            //1 returns player to the start
            if (DiceValue == 1)
            {

                Console.WriteLine("You have rolled a 1: {0} your engine exploded sending you back to the start", players[player].Name);
                players[player].Pos = 0;
                Console.WriteLine("");

            }

            //2 sends everyone from that square back to the start
            if (DiceValue == 2)
            {

                Console.WriteLine("You have rolled a 2: {0} everyone on your square has had their engine", players[player].Name);
                Console.WriteLine("explode sending them all back to the start");
                Console.WriteLine("");
                for (int i = 0; i < NumberOfPlayers; i = i + 1)
                {
                    if (players[i].Pos == players[player].Pos)
                    {
                        if (i != player)
                        {
                            Console.WriteLine("Player {0} has been sent back to the start", players[i].Name);
                            players[i].Pos = 0;
                            players[player].Pos = 0;
                        }
                        
                    }
                }
                Console.WriteLine("");
            }

            //3 sends everyone from that square back to the start except for the person who rolled it
            if (DiceValue == 3)
            {

                int NotMovePlayersPos;
                Console.WriteLine("You have rolled a 3: {0} you have set off a gamma cheese chain reaction ", players[player].Name);
                Console.WriteLine("causing the engine of everyone on your square except you to explode sending to back to the start");
                for (int i = 0; i < NumberOfPlayers; i = i + 1)
                {
                    if (players[i].Pos == players[player].Pos)
                    {
                        NotMovePlayersPos = players[player].Pos;

                        if (i != player)
                        {
                            Console.WriteLine("Player {0} has been sent back to the start", players[i].Name);
                            players[i].Pos = 0;
                            players[player].Pos = NotMovePlayersPos;
                            Console.WriteLine("");
                        }   
                        
                    }
                }
                Console.WriteLine("");
            }

            //4 Player moves 6 places forward.
            if (DiceValue == 4)
            {

                Console.WriteLine("You have rolled a 4: {0} your engines have been given a chedder power boost ", players[player].Name);
                Console.WriteLine("this has caused you to move 6 places forward");
                players[player].Pos = players[player].Pos + 6;
                Console.WriteLine("");
                Console.WriteLine("{0} you are now at position {1}", players[player].Name ,players[player].Pos);
                Console.WriteLine("");

            }

            //5 allows them to move to any players position on the board.
            if (DiceValue == 5)
            {
                Console.WriteLine("You have rolled a 5: {0} you have been given the cheese transference power ", players[player].Name);
                Console.WriteLine("You can move to the same square as any player");

                for (int i = 0; i < NumberOfPlayers; i = i + 1)
                {
                    if (i != player)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Player {0}: {1} is at square {2} on the map", i + 1, players[i].Name, players[i].Pos);
                    }  

                }

                Console.WriteLine("");
                Console.Write("Enter the player number you wish to swap positions with ");
                int Chosenplayer = int.Parse(Console.ReadLine());
                players[player].Pos = players[Chosenplayer-1].Pos;
                Console.WriteLine("");
                Console.WriteLine("Your new position is " + players[player].Pos);
                Console.WriteLine("");
            }

            //6 allows them to swap position with any player on the board.
            if (DiceValue == 6)
            {

                Console.WriteLine("You have rolled a 6: {0} you have been given gamma cheese transference power ", players[player].Name);
                Console.WriteLine("you must swap positions with another player");

                for (int i = 0; i < NumberOfPlayers; i = i + 1)
                {
                    if (i != player)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Player {0}: {1} is at square {2} on the map ", i + 1, players[i].Name, players[i].Pos);
                    }   
                    
                }
                Console.WriteLine("");
                Console.Write("Enter the player number you wish to swap positions with: ");
                int ChosenPlayer = int.Parse(Console.ReadLine());

                int TempChosenPlayerPos = players[player].Pos;

                players[player].Pos = players[ChosenPlayer-1].Pos;
                players[ChosenPlayer-1].Pos = TempChosenPlayerPos;

                Console.WriteLine("");
                Console.WriteLine("Your new position is {0} and {1} has been move to your old position {2}", players[player].Pos, players[ChosenPlayer-1].Name, players[ChosenPlayer-1].Pos);
                Console.WriteLine("");
            }

         }
    }
 }
   
