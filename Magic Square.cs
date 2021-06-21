using System;
using System.Collections.Generic;
using System.Numerics;

namespace MagicSquare{
    class Solution
    {
        public static void printSums(BigInteger i_diagonalSum, BigInteger i_antiDiagonalSum, List<BigInteger> i_rowsSums, List<BigInteger> i_colsSums)
        {
            List<int> oddCols = new List<int>();
            List<int> oddRows = new List<int>();
            int differentSumsAmount = 0;

            for (int i = 0; i < i_colsSums.Count; i++)
            {
                if (i_colsSums[i] != i_diagonalSum)
                {
                    oddCols.Add(i + 1);
                }
            }

            for (int j = 0; j < i_rowsSums.Count; j++)
            {
                if(i_rowsSums[j] != i_diagonalSum)
                {
                    oddRows.Add(j + 1);
                }
            }

            differentSumsAmount = oddCols.Count + oddRows.Count;

            if (i_antiDiagonalSum != i_diagonalSum)
            {
                differentSumsAmount += 1;
            }

            Console.WriteLine(differentSumsAmount);

            for(int i = (oddCols.Count - 1); i > -1; i--)
            {
                Console.WriteLine("-" + oddCols[i]);
            }

            if (i_antiDiagonalSum != i_diagonalSum){
                Console.WriteLine("0");
            }
            
            foreach (int oddRowNum in oddRows)
            {
                Console.WriteLine(oddRowNum);
            }
        }

        public static void ParseInputAndCalcSums()
        {
            string firstLine = Console.ReadLine();
            int t = Int32.Parse(firstLine);
            bool isFirstDigit = true;
            BigInteger currentNum = 0;
            int progressCounter = 1;
            int rowsCounter = 1;
            int colsCounter = 1;
            bool isNegative = false;
            BigInteger diagonalSum = 0;
            BigInteger antiDiagonalSum = 0;
            List<BigInteger> RowsSums = new List<BigInteger>(new BigInteger[t]);
            List<BigInteger> ColsSums = new List<BigInteger>(new BigInteger[t]);

            while (progressCounter <= t*t)
            {
                isFirstDigit = true;
                string singleInputLine = Console.ReadLine();
                foreach(char singleInput in singleInputLine)
                {
                    if (singleInput == ' ')
                    {
                        //MAIN DIAGONAL CHECK
                        if (rowsCounter == colsCounter)
                        {
                            diagonalSum += currentNum;
                        }

                        //ANTI-DIAGONAL CHECK
                        if (rowsCounter + colsCounter == t + 1)
                        {
                            antiDiagonalSum += currentNum;
                        }

                        RowsSums[rowsCounter - 1] += currentNum;
                        ColsSums[colsCounter - 1] += currentNum;
                        progressCounter++;
                        colsCounter++;
                        isFirstDigit = true;
                        continue;
                    }

                    if(singleInput == '-')
                    {
                        isNegative = true;
                        continue;
                    }

                    if (isFirstDigit == true)
                    {
                        currentNum = (int)singleInput - 48;
                        if (isNegative)
                        {
                            currentNum *= -1;
                            isNegative = false;
                        }
                        isFirstDigit = false;
                    }
                    else
                    {
                        if (currentNum < 0)
                        {
                            currentNum = (currentNum * 10) - ((int)singleInput - 48);
                        }
                        else
                        {
                            currentNum = (currentNum * 10) + ((int)singleInput - 48);
                        }
                    }
                }
                //MAIN DIAGONAL CHECK
                if (rowsCounter == colsCounter)
                {
                    diagonalSum += currentNum;
                }
                //ANTI-DIAGONAL CHECK
                if (rowsCounter + colsCounter == t + 1)
                {
                    antiDiagonalSum += currentNum;
                }

                RowsSums[rowsCounter - 1] += currentNum;
                ColsSums[colsCounter - 1] += currentNum;
                progressCounter++;
                rowsCounter++;
                colsCounter = 1;
                isFirstDigit = true;
                isNegative = false;
            }
            printSums(diagonalSum, antiDiagonalSum, RowsSums, ColsSums);
        }
        static void Main(String[] args)
        {
            ParseInputAndCalcSums();
            //Console.ReadLine();
        }
    }
}