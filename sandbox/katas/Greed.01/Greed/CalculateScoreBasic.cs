using RollingBasic;
namespace ScoringBasic
{
    public class CalculateScoreBasic
    {
        public int Calculate(int[] diceResults)
        {
            int score = 0;
            int[] counts = new int[7];

            foreach (int die in diceResults)
            {
                counts[die]++;
            }
            for (int i = 1; i <= 6; i++)
            {
                if (counts[i] >= 3)
                {
                    if (i == 1)
                        score += 1000;
                    else
                        score += i * 100;

                    counts[i] -= 3;
                }
            }
            score += counts[1] * 100;
            score += counts[5] * 50;
            return score;
        }
    }
}
