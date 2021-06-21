using System;
using System.IO;
using System.Collections.Generic;

namespace Painters_Dilemma
{
    class Solution
    {
        public static List<int> parseScenarioInput()
        {
            List<int> colorSequence = new List<int>();
            int color = 0;
            while (true)
            {
                int currentChar = Console.Read();
                if (currentChar == 32 || currentChar == 13)
                {
                    colorSequence.Add(color);
                    color = 0;
                    continue;
                }
                if (currentChar == 10)
                {
                    break;
                }
                color = (color * 10) + (currentChar - 48);
            }
            return colorSequence;
        }

        public static void switchBrushPaints(ref int io_MinimalAmount, List<int> i_ColorSequence, ref int io_FirstBrush, ref int io_SecondBrush, int i_CurrentIndex)
        {
            int movingIndex = i_CurrentIndex;
            bool foundAnyBrush = false;
            for (; movingIndex < i_ColorSequence.Count; movingIndex++)
            {
                if (i_ColorSequence[movingIndex] == io_FirstBrush)
                {
                    io_SecondBrush = i_ColorSequence[i_CurrentIndex];
                    io_MinimalAmount++;
                    foundAnyBrush = true;
                    break;
                }
                if (i_ColorSequence[movingIndex] == io_SecondBrush)
                {
                    io_FirstBrush = i_ColorSequence[i_CurrentIndex];
                    io_MinimalAmount++;
                    foundAnyBrush = true;
                    break;
                }
            }
            if (!foundAnyBrush)
            {
                io_FirstBrush = i_ColorSequence[i_CurrentIndex];
                io_MinimalAmount++;
            }
        }

        public static int computeMinimalAmount(List<int> i_ColorSequence, int i_AmountOfPaintActions)
        {
            int minimalAmount = 1;
            int firstBrush = i_ColorSequence[0];
            int secondBrush = 0;
            int i = 1;

            //deciding secondBrush's paint
            for (; i < i_AmountOfPaintActions; i++)
            {
                if (i_ColorSequence[i] != firstBrush)
                {
                    secondBrush = i_ColorSequence[i];
                    minimalAmount++;
                    break;
                }
            }

            i++;
            //checking for every other possible paint brush switch
            for (; i < i_AmountOfPaintActions; i++)
            {
                if (i_ColorSequence[i] == firstBrush || i_ColorSequence[i] == secondBrush)
                {
                    continue;
                }
                else
                {
                    switchBrushPaints(ref minimalAmount, i_ColorSequence, ref firstBrush, ref secondBrush, i);
                }
            }

            return minimalAmount;
        }

        public static void Main(String[] args)
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192), Console.InputEncoding, false, 8192));
            int amountOfScenarios = Convert.ToInt32(Console.ReadLine());
            List<List<int>> ListOfColorSequences = new List<List<int>>();

            for (int i = 0; i < amountOfScenarios; i++)
            {
                Console.ReadLine();
                List<int> colorSequence = parseScenarioInput();
                ListOfColorSequences.Add(colorSequence);
            }

            for (int i = 0; i < amountOfScenarios; i++)
            {
                int minimalSwitchesAmount = computeMinimalAmount(ListOfColorSequences[i], ListOfColorSequences[i].Count);
                Console.WriteLine(minimalSwitchesAmount);
            }
            //Console.Read();
        }
    }
}