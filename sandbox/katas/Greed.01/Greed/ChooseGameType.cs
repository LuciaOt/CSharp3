
using _Greed;
namespace GameTypes
{
    public class ChooseGameType
    {
        // nie je nutne nastavovat na null, lebo je to defaultna hodnota
        public eGameTypes? SelectedGameType { get; private set; }

        public void DisplayGameOptions()
        {
            Console.WriteLine("Available Game Types:");
            foreach (var gameType in Enum.GetValues(typeof(eGameTypes)))
            {
                Console.WriteLine($"{(int)gameType} {gameType}");

            }
        }
        public void SelectGameType(eGameTypes gameType)
        {
            Console.Clear();
            SelectedGameType = gameType;
            Console.WriteLine($"You have selected level: {SelectedGameType}.");
        }
    }
}




