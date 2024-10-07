/*
Tu by som pri vsetkych triedach zvolila len jeden namespace a nazvala ho napr. Greed
C# programs are organized using namespaces. Namespaces are used both as an “internal” organization system for a program, and as an “external” organization system—a way of presenting program elements that are exposed to other programs.
Vacsinou maju tie namespacey nazov podla zloziek, v ktorych sa nachadzaju, preto by som pouzila Greed.
Zvycajne to vsak vo velkych projektoch byva organizovane ako Folder1.Folder2.Folder3... atd, zalezi od zanorenia.
Pri Web API to mozno budeme robit podobne, a budeme mat namespace napr. ToDoList.Database (bude obsahovat logiku prace s databazou), ToDoList.Business (business vrstva). To este uvidime :)
*/
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
