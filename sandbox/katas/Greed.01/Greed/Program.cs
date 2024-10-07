using System;
using _Greed;
using GameTypes;
using RollingBasic;
using ScoringBasic;
using RollingAdvanced;
using ScoringAdvanced;

/*
Ponukalo mi to prekonvertovat tento subor na top-level statements,
pri konzolovej aplikacii uz v novsich verziach C# a .NET nie je nutne pisat Main metodu, viac tu:
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements
*/
Console.WriteLine("Welcome to the Dice Rolling Game");

// Je standardom pouzivat implicitne typovanie pri lokalnych premennych, t.j. kompilator si pri deklaracii odvodi typ:
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables
// pri zvysku som to nechala tak
var gameChooser = new ChooseGameType();

while (true)
{
    bool validInput = false;

    while (!validInput)
    {
        gameChooser.DisplayGameOptions();
        Console.Write("Please enter the number of the game you want to play (or press 'e' to exit): ");
        string input = Console.ReadLine();
        if (input.Equals("e", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing. Goodbye!");
            return;
        }

        if (int.TryParse(input, out int selectedOption) && Enum.IsDefined(typeof(eGameTypes), selectedOption))
        {
            gameChooser.SelectGameType((eGameTypes)selectedOption);
            validInput = true;
            Console.Write("Press Enter to start the play!");
            Console.ReadLine();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    if (gameChooser.SelectedGameType == eGameTypes.Basic)
    {
        RollDicesBasic diceRoller = new RollDicesBasic();
        diceRoller.RollAllDice();
        CalculateScoreBasic scoreCalculator = new CalculateScoreBasic();
        int score = scoreCalculator.Calculate(diceRoller.DiceResults);
        Console.WriteLine($"Your score for this roll is: {score}");
    }

    if (gameChooser.SelectedGameType == eGameTypes.Advanced)
    {
        int diceCount;
        while (true)
        {
            Console.Write("How many dice would you like to roll (1 to 6)? ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out diceCount) && diceCount >= 1 && diceCount <= 6)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
            }
        }

        RollDicesAdvanced advancedDiceRoller = new RollDicesAdvanced(diceCount);
        advancedDiceRoller.RollAllDice();
        CalculateScoreAdvanced scoreCalculator = new CalculateScoreAdvanced();
        int score = scoreCalculator.Calculate(advancedDiceRoller.DiceResults);
        Console.WriteLine($"Your score for this roll is: {score}");
    }

    string playAgainInput;
    while (true)
    {
        Console.Write("Do you wish to play again? (Y/N): ");
        playAgainInput = Console.ReadLine();

        if (playAgainInput.Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            break;
        }
        else if (playAgainInput.Equals("N", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing. Goodbye!");
            return;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Please enter 'Y' to continue or 'N' to exit.");
        }
    }


}
