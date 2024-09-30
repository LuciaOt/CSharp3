public class FizzBuzz
{
    private void DisplayMessage(int number, bool isFizz, bool isBuzz)
    {
        if (isFizz && isBuzz)
        {
            Console.WriteLine("FizzBuzz");
        }
        else if (isFizz)
        {
            Console.WriteLine("Fizz");
        }
        else if (isBuzz)
        {
            Console.WriteLine("Buzz");
        }
        else
        {
            Console.WriteLine(number);

        }


    }

    public void CountTo(int lastNumber)
    {
        for (int aktualniCislo = 1; aktualniCislo <= lastNumber; aktualniCislo++)
        {
            bool isFizz = aktualniCislo % 3 == 0;
            bool isBuzz = aktualniCislo % 5 == 0;
            DisplayMessage(aktualniCislo, isFizz, isBuzz);
        }
    }

    public void ExtraMethod(int lastNumber)
    {
        for (int aktualniCislo = 1; aktualniCislo <= lastNumber; aktualniCislo++)
        {
            string aktualniCisloString = aktualniCislo.ToString();
            bool containsThree = aktualniCisloString.Contains('3');
            bool containsFive = aktualniCisloString.Contains('5');

            DisplayMessage(aktualniCislo, containsThree, containsFive);


        }


    }
}
