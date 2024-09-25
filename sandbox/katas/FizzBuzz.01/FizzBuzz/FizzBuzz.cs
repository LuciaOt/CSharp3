using System;

public class FizzBuzz
{
    public void CountTo(int lastNumber)
    {
        for (int aktualniCislo = 1; aktualniCislo <= lastNumber; aktualniCislo++)
        {
            if (aktualniCislo % 3 == 0 && aktualniCislo % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
                //return;
            }
            else if (aktualniCislo % 3 == 0)
            {
                Console.WriteLine("Fizz");
                //return;
            }
            else if (aktualniCislo % 5 == 0)
            {
                Console.WriteLine("Buzz");
                //return;
            }
            else
            {
                Console.WriteLine(aktualniCislo);

            }
        }
    }
}
