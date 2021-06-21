using System;
using System.Collections.Generic;
using System.IO;

namespace Rumour
{
    class Rumour
    {
        public static long twoToThePowerOf(int i_Power)
        {
            long calculatedNum = 1;
            for(int i = 0; i< i_Power; i++)
            {
                calculatedNum *= 2;
            }
            return calculatedNum;
        }

        public static int equalLevelsDivider(long i_ComponentA, long i_ComponentB)
        {
            int distance = 0;
            while (i_ComponentA != i_ComponentB)
            {
                i_ComponentA = i_ComponentA / 2;
                i_ComponentB = i_ComponentB / 2;
                distance += 2;
            }
            return distance;
        }

        public static void calculateDistances(long i_Amount_Of_Queries)
        {
            int amountOfPows = 63;
            long[] calculatedPows = new long[amountOfPows];
            List<int> answersList = new List<int>();

            for (int i = 0; i < amountOfPows; i++)
            {
                calculatedPows[i] = twoToThePowerOf(i);
            }
            for (long i = 0; i < i_Amount_Of_Queries; i++)
            {
                int levelOfComponentA = -1;
                int levelOfComponentB = -1;
                string duoVerticesString = Console.ReadLine();
                string[] duoVertices = duoVerticesString.Split(' ');
                long componentA = Int64.Parse(duoVertices[0]);
                long componentB = Int64.Parse(duoVertices[1]);
                int j = 0;
                int distance = 0;
                int levelsDifference = 0;

                if (componentA == componentB) // on the off-chance they're the same number to begin with.
                {
                    answersList.Add(0);
                    continue;
                }

                while (levelOfComponentA == -1 || levelOfComponentB == -1) // finding out componentA's level
                {
                    if (levelOfComponentA == -1)
                    {
                        if (componentA < calculatedPows[j])
                        {
                            levelOfComponentA = j;
                        }
                    }

                    if (levelOfComponentB == -1)
                    {
                        if (componentB < calculatedPows[j])
                        {
                            levelOfComponentB = j;
                        }
                    }

                    j++;
                }

                if (levelOfComponentA == levelOfComponentB)
                {
                    distance = equalLevelsDivider(componentA, componentB);
                    answersList.Add(distance);
                }
                else
                {
                    if (levelOfComponentA > levelOfComponentB)
                    {
                        levelsDifference = levelOfComponentA - levelOfComponentB;
                        for (int z = 0; z < levelsDifference; z++)
                        {
                            componentA = componentA / 2;
                            distance++;
                        }

                        if (componentA == componentB) // after levels equation, check if perhaps they're equal now
                        {
                            answersList.Add(distance);
                            continue;
                        }
                        else
                        {
                            distance += equalLevelsDivider(componentA, componentB);
                            answersList.Add(distance);
                        }
                    }
                    else
                    {
                        levelsDifference = levelOfComponentB - levelOfComponentA;
                        for (int z = 0; z < levelsDifference; z++)
                        {
                            componentB = componentB / 2;
                            distance++;
                        }

                        if(componentA == componentB) // after levels-equation, check if perhaps they're equal now
                        {
                            answersList.Add(distance);
                            continue;
                        }
                        else
                        {
                            distance += equalLevelsDivider(componentA, componentB);
                            answersList.Add(distance);
                        }
                    }
                }
            }

            foreach(int answer in answersList)
            {
                Console.WriteLine(answer);
            }
        }


        static void Main(String[] args)
        {
            string amount_Of_Queries_String = Console.ReadLine();
            long amount_Of_Queries = Int64.Parse(amount_Of_Queries_String);

            calculateDistances(amount_Of_Queries);
            //Console.Read();
        }
    }
}