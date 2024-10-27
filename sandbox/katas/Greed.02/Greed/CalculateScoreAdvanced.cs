using System;
using System.Linq;

namespace ScoringAdvanced
{
    public class CalculateScoreAdvanced
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
                    int tripleScore = (i == 1 ? 1000 : i * 100);
                    score += tripleScore;

                    if (counts[i] == 4)
                    {
                        score += tripleScore;
                    }
                    else if (counts[i] == 5)
                    {
                        score += tripleScore * 3;
                    }
                    else if (counts[i] == 6)
                    {
                        score += tripleScore * 7;
                    }
                    counts[i] = 0;
                }
            }


            score += counts[1] * 100;
            score += counts[5] * 50;



            int pairsCount = 0;
            for (int i = 1; i <= 6; i++)
            {
                if (counts[i] == 2)
                {
                    pairsCount++;
                }
            }

            if (pairsCount == 3)
            {
                score += 800;
            }

            if (counts[1] == 1 && counts[2] == 1 && counts[3] == 1 && counts[4] == 1 && counts[5] == 1 && counts[6] == 1)
            {
                score += 1200;
            }

            return score;
        }
    }
}
