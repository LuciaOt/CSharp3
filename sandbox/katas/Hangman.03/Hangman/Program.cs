using HangMan;
/*
Kedze si mozem zvolit slovo, tak pravdepodobne vzdy viem tu hru vyhrat, ak som teda nezabudla, co som zadala :D
Takze v tejto verzii ju jedine dat hrat niekomu inemu, aby hadal moje vymyslene slovo.

V tomto pripade, ked hram ako uzivatel s pocitacom, by som nechala pocitac vygenerovat nejake slovo, napr. nejaky druh ovocia, zeleniny, europsky stat atd.
Vybrat si teda s pouzitim Random z nejakeho zoznamu slovo a nechat uzivatela hadat (a samozrejme napisat mu, co ma skusit uhadnut).
Pocet nespravnych pokusov mu mozeme nechat zadat, alebo to moze byt nejaka nami dana hranica (napr. 10)
*/
Console.WriteLine("Type a secret word and press Enter:");
var inputWord = SecretWordReader.ReadSecretWord();
Console.WriteLine("Enter the number of incorrect guesses you would like to have:");
int maxGuesses;
while (!int.TryParse(Console.ReadLine(), out maxGuesses) || maxGuesses <= 0)
{
    Console.WriteLine("Please enter a valid positive number for guesses:");
}

var hangmanGame = new Hangman(inputWord, maxGuesses);
Console.Clear();

var guessResultMessages = new Dictionary<eGuessResult, string>
{
    { eGuessResult.Invalid, "\nInvalid input! Please enter a valid letter." },
    { eGuessResult.Duplicate, "\nYou've already guessed that letter!" },
    { eGuessResult.Correct, "\nCorrect guess!" },
    { eGuessResult.Incorrect, "\nIncorrect guess." }
};

var gameStatusMessages = new Dictionary<eGameStatus, string>
{
    { eGameStatus.Won, "Congratulations! You've won the game!"},
    { eGameStatus.Lost, "Game over! You've lost."}
};

while (hangmanGame.GetGameStatus() == eGameStatus.InProgress)
{
    hangmanGame.DisplayProgress();
    Console.Write("\nGuess a letter:");
    var guessedLetter = Console.ReadKey().KeyChar;
    var guessResult = hangmanGame.GuessLetter(guessedLetter);
    if (guessResultMessages.TryGetValue(guessResult, out var message))
    {
        Console.WriteLine(message);
    }

    var gameStatus = hangmanGame.GetGameStatus();
    if (gameStatusMessages.TryGetValue(gameStatus, out var statusMessage) && statusMessage != null)
    {
        Console.Clear();
        Console.WriteLine(statusMessage);
    }
}
