namespace HangMan;

public class SecretWordReader
{
    public static string ReadSecretWord()
    {
        var secretWord = string.Empty;
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(intercept: true);
            if (char.IsLetter(keyInfo.KeyChar))
            {
                secretWord += keyInfo.KeyChar;
                Console.Write("*");
            }
            else if (keyInfo.Key != ConsoleKey.Enter)
            {
                Console.Beep();
            }
        }
        while (keyInfo.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return secretWord;
    }
}


