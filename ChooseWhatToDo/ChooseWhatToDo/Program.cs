using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChooseWhatToDo
{
    public class Program
    {
        public static bool again = true;
        public static async Task Main(string[] args)
        {
            while (again)
            {               
                PrintWelcome();

                int numberOfChoices = GetNumberOfChoices();

                PrintText("Please enter your options (enter after each):");

                string[] answers = new string[numberOfChoices];
                answers = GetUserChoices(numberOfChoices);

                PrintChoice();

                string userChoice = Console.ReadLine();

                while (!userChoice.All(char.IsDigit) || userChoice.Length == 0)
                {
                    PrintText("Please select a valid option.");
                    userChoice = Console.ReadLine();
                }

                if (Convert.ToInt32(userChoice) == 1)
                {
                    GenerateRankAsync(answers, numberOfChoices);
                }
                else if (Convert.ToInt32(userChoice) == 2)
                {
                    GenerateOptionNow(answers, numberOfChoices);                   
                }

                 again = CheckKey();
            }
        }

        private static int GetNumberOfChoices()
        {
            string choicesCount = Console.ReadLine();

            while (!choicesCount.All(char.IsDigit) || choicesCount.Length == 0)
            {
                PrintText("Please select a valid option.");
                choicesCount = Console.ReadLine();
            }

            return Convert.ToInt32(choicesCount);
        }

        public static void PrintText(string text)
        {
            Console.WriteLine(text);
        }

        public static void PrintWelcome()
        {
            PrintText("");
            PrintText("***WELCOME TO THE PROCRASTINATION STATION, NOT!***");
            PrintText("How many options do you have today?");
        }

        public static void PrintChoice()
        {
            PrintText("");
            PrintText("Thank you, please select your choice by typing the relevant number:");
            PrintText("1. Rank in order of doing first.");
            PrintText("2. Give my an option to do now!");
        }

        public static void PrintExit()
        {
            PrintText("Enter E to exit or press any other key to start again..");
        }

        public static void PrintGuessWhat()
        {
            PrintText("");
            PrintText("Guess what, no more procrastinating for you!");
        }

        public static bool CheckKey()
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.E)
                return false;
            else
                return true;
        }


        public static string[] GetUserChoices(int numberOfChoices)
        {
            string[] answer = new string[numberOfChoices];

            for (int i = 0; i < answer.Length; i++)
            {
                string userEntered = Console.ReadLine();

                while (userEntered == string.Empty)
                {
                    userEntered = Console.ReadLine();
                }
                answer[i] = userEntered;
            }

            return answer;
        }

        public static async void GenerateRankAsync(string[] answers, int numberOfChoices)
        {
            //randomise list
            Random randomSelected = new Random();
            int randomSelectedInt = -1;

            int[] randomRank = new int[numberOfChoices];
            for (int i = 0; i < randomRank.Length; i++)
                randomRank[i] = -1;

            for (int i = 0; i < randomRank.Length; i++)
            {
                do
                {
                    randomSelectedInt = randomSelected.Next(0, numberOfChoices);
                } while (randomRank.Contains(randomSelectedInt));

                for (int j = 0; j < 8; j++)
                {
                    Console.Write(".");
                    await Task.Delay(200);
                }

                randomRank[i] = randomSelectedInt;
            }

            //assign each one new rank
            string[] newAnswersRanked = new string[numberOfChoices];
            for (int i = 0; i < answers.Length; i++)
                newAnswersRanked[randomRank[i]] = answers[i];

            PrintGuessWhat();
            PrintText("Get going with these, in this order, starting with:");

            for (int i = 0; i < newAnswersRanked.Length; i++)
                PrintText((i + 1) + ". " + newAnswersRanked[i]);

            PrintText("");
            PrintExit();
        }

        public static async void GenerateOptionNow(string[] answers, int numberOfChoices)
        {
           
            Random randomSelected = new Random();
            int randomSelectedInt = randomSelected.Next(0, numberOfChoices);

            PrintGuessWhat();
            PrintText("Get up and get going with.." + Environment.NewLine);

            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                await Task.Delay(200);
            }

            PrintText("");
            PrintText(answers[randomSelectedInt] + Environment.NewLine);
            PrintExit();
        }
    }
}
