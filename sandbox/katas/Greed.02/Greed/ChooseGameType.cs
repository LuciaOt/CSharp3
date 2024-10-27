
using _Greed;
namespace GameTypes
{
    public class ChooseGameType
    {
        public eGameTypes? SelectedGameType { get; private set; } = null;

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




