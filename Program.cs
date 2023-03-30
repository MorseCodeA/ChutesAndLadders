using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ChutesAndLadders
{
    class Program
    {
        public class Slot
        {
            public int JumpDestination { get; set; }
            public Slot(int jumpDestination)
            {
                JumpDestination = jumpDestination;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chutes and Ladders!");
            Console.WriteLine("Enter how many games to run:");

            // get game number
            string? gameCount_string = Console.ReadLine();
            int gameCount = 0;
            try
            {
                // number of games played
                gameCount = Int32.Parse(gameCount_string);
            }
            catch(FormatException)
            {
                Console.WriteLine($"Unable to parse '{gameCount_string}'");
            }

            //--- Initalize variables ---//
            // create array list
            List<Slot> slotList = new List<Slot>();
            // populate with 100 slot objects
            slotList.Add(new Slot(38)); slotList.Add(new Slot(2)); slotList.Add(new Slot(3)); slotList.Add(new Slot(14)); slotList.Add(new Slot(5));
            slotList.Add(new Slot(6)); slotList.Add(new Slot(7)); slotList.Add(new Slot(8)); slotList.Add(new Slot(31)); slotList.Add(new Slot(10));
            slotList.Add(new Slot(11)); slotList.Add(new Slot(12)); slotList.Add(new Slot(13)); slotList.Add(new Slot(14)); slotList.Add(new Slot(15));
            slotList.Add(new Slot(6)); slotList.Add(new Slot(17)); slotList.Add(new Slot(18)); slotList.Add(new Slot(19)); slotList.Add(new Slot(20));
            slotList.Add(new Slot(42)); slotList.Add(new Slot(22)); slotList.Add(new Slot(23)); slotList.Add(new Slot(24)); slotList.Add(new Slot(25));
            slotList.Add(new Slot(26)); slotList.Add(new Slot(27)); slotList.Add(new Slot(84)); slotList.Add(new Slot(29)); slotList.Add(new Slot(30));
            slotList.Add(new Slot(31)); slotList.Add(new Slot(32)); slotList.Add(new Slot(33)); slotList.Add(new Slot(34)); slotList.Add(new Slot(35));
            slotList.Add(new Slot(44)); slotList.Add(new Slot(37)); slotList.Add(new Slot(38)); slotList.Add(new Slot(39)); slotList.Add(new Slot(40));
            slotList.Add(new Slot(41)); slotList.Add(new Slot(42)); slotList.Add(new Slot(43)); slotList.Add(new Slot(44)); slotList.Add(new Slot(45));
            slotList.Add(new Slot(46)); slotList.Add(new Slot(26)); slotList.Add(new Slot(48)); slotList.Add(new Slot(11)); slotList.Add(new Slot(50));
            slotList.Add(new Slot(67)); slotList.Add(new Slot(52)); slotList.Add(new Slot(53)); slotList.Add(new Slot(54)); slotList.Add(new Slot(55));
            slotList.Add(new Slot(53)); slotList.Add(new Slot(57)); slotList.Add(new Slot(58)); slotList.Add(new Slot(59)); slotList.Add(new Slot(60));
            slotList.Add(new Slot(61)); slotList.Add(new Slot(19)); slotList.Add(new Slot(63)); slotList.Add(new Slot(60)); slotList.Add(new Slot(65));
            slotList.Add(new Slot(66)); slotList.Add(new Slot(67)); slotList.Add(new Slot(68)); slotList.Add(new Slot(69)); slotList.Add(new Slot(70));
            slotList.Add(new Slot(91)); slotList.Add(new Slot(72)); slotList.Add(new Slot(73)); slotList.Add(new Slot(74)); slotList.Add(new Slot(75));
            slotList.Add(new Slot(76)); slotList.Add(new Slot(77)); slotList.Add(new Slot(78)); slotList.Add(new Slot(79)); slotList.Add(new Slot(100));
            slotList.Add(new Slot(81)); slotList.Add(new Slot(82)); slotList.Add(new Slot(83)); slotList.Add(new Slot(84)); slotList.Add(new Slot(85));
            slotList.Add(new Slot(86)); slotList.Add(new Slot(24)); slotList.Add(new Slot(88)); slotList.Add(new Slot(89)); slotList.Add(new Slot(90));
            slotList.Add(new Slot(91)); slotList.Add(new Slot(92)); slotList.Add(new Slot(73)); slotList.Add(new Slot(94)); slotList.Add(new Slot(75));
            slotList.Add(new Slot(96)); slotList.Add(new Slot(97)); slotList.Add(new Slot(78)); slotList.Add(new Slot(99)); slotList.Add(new Slot(100));

            // number of turns in 1 game
            int turns = 0;
            // current position of player
            int currentPosition = 0;
            // previous position before step (incase of overflow)
            int positionBeforeStep = 0;
            // create a "pseudo-random" seed
            Random rnd = new Random();
            // the random step taken during a turn
            int step = 0;
            // --------------------------//

            // Create new file to record
            string filePath = "ChutesAndLadders_" + string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now) + ".csv";
            StringBuilder csv = new StringBuilder();
            // write headers
            string newLine = string.Format("{0},{1}", "Game #", "Turns");
            csv.AppendLine(newLine);

            //---- Playing the game ----//
            for (int i = 1; i <= gameCount; i++)
            {
                //Console.WriteLine("Start Game " + i + "!");
                // reset current position and turns to 0
                currentPosition = 0;
                turns = 0;
                // Take turns until 100 exactly is reached
                while (currentPosition != 100)
                {
                    // take turn
                    // roll dice
                    step = rnd.Next(1, 7);
                    // move player
                    positionBeforeStep = currentPosition;
                    currentPosition += step;
                    // check if overflow
                    if (currentPosition > 100)
                    {
                        currentPosition = positionBeforeStep;
                    }
                    currentPosition = slotList[currentPosition - 1].JumpDestination;
                    // increment turns
                    ++turns;
                    //Console.WriteLine("Roll: " + step);
                    //Console.WriteLine("Current Position: " + currentPosition);
                    //Console.WriteLine("Turns: " + turns);
                }
                // game finished, record game #, turns
                newLine = string.Format("{0},{1}", i, turns);
                csv.AppendLine(newLine);

                //Console.WriteLine("Finished " + i + "!");
            }
            Console.WriteLine("Finished playing " + gameCount + " games!");
            // write all data to csv file
            File.WriteAllText(filePath, csv.ToString());
        }
    }
}