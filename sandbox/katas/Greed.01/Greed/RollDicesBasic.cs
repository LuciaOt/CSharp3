namespace RollingBasic
{
    public class RollDicesBasic
    {
        private static Random _random = new Random();

        public int[] DiceResults { get; private set; }

        public RollDicesBasic()
        {
            DiceResults = new int[5];
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
