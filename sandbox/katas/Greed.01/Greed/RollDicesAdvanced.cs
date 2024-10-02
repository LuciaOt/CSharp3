using System;

namespace RollingAdvanced
{
    public class RollDicesAdvanced
    {
        private static Random _random = new Random();
        public int[] DiceResults { get; private set; }

        public RollDicesAdvanced(int numberOfDice)
        {
            if (numberOfDice < 1 || numberOfDice > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfDice), "You must roll between 1 and 6 dice.");
            }

            DiceResults = new int[numberOfDice];
        }

        public void RollAllDice()
        {
            for (int i = 0; i < DiceResults.Length; i++)
            {
                DiceResults[i] = _random.Next(1, 7);
            }

            DisplayRollResults();
        }

        private void DisplayRollResults()
        {
            Console.WriteLine("You rolled the following numbers:");
            for (int i = 0; i < DiceResults.Length; i++)
            {
                Console.WriteLine($"Dice {i + 1}: {DiceResults[i]}");
            }
        }
    }
}
