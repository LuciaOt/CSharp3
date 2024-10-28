namespace HangMan;
public class Hangman(string secretWord, int maxAttempts)
{
    public string SecretWord { get; } = secretWord.ToUpperInvariant();
    private char[] GuessedLetters { get; } = new string('_', secretWord.Length).ToCharArray();
    private int RemainingAttempts { get; set; } = maxAttempts;
    private List<char> PreviousGuesses { get; } = [];

    public eGuessResult GuessLetter(char letter)
    {
        letter = char.ToUpperInvariant(letter);

        if (!char.IsLetter(letter))
        {
            Console.Clear();
            return eGuessResult.Invalid;
        }
        if (PreviousGuesses.Contains(letter))
        {
            Console.Clear();
            return eGuessResult.Duplicate;
        }
        PreviousGuesses.Add(letter);
        var correctGuess = false;

        for (var i = 0; i < SecretWord.Length; i++)
        {
            if (SecretWord[i] == letter && GuessedLetters[i] != letter)
            {
                Console.Clear();
                GuessedLetters[i] = letter;
                correctGuess = true;
                /*
                Tento Console.Clear() je tam zbytocny (medzi jednotlivymi clear nic nevypisujes, takze nie je treba).
                Celkovo by som ale Console.Clear() mozno aj dala prec, napr. pri incorrect guesses by som chcela vidiet, co som uz skusila za pismeno, aby som ho uz neskusala znova
                */
                //Console.Clear();
            }
        }

        if (!correctGuess)
        {
            Console.Clear();
            RemainingAttempts--;
            return eGuessResult.Incorrect;
        }
        return eGuessResult.Correct;
    }

    public void DisplayProgress()
    {
        Console.WriteLine("\nWord: " + new string(GuessedLetters));
        Console.WriteLine($"Remaining Attempts: {RemainingAttempts}");
    }

    public eGameStatus GetGameStatus()
    {
        if (GuessedLetters.SequenceEqual(SecretWord.ToCharArray()))
        {
            return eGameStatus.Won;
        }
        else if (RemainingAttempts <= 0)
        {
            return eGameStatus.Lost;
        }
        else
        {
            return eGameStatus.InProgress;
        }
    }
}
