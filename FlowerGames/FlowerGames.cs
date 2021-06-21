using System;
using System.Collections.Generic;
using System.IO;

namespace FlowerGames
{
    class FlowerGames
    {
        /*public static void numsWork()
        {
            string input = Console.ReadLine();
            int inputInt = Int32.Parse(input);
            int amountOfZeroes = 0;
            bool keep = true;
            List<int> ListOfNums = new List<int>();

            for (int i = 0; i < inputInt; i++) 
            {
                ListOfNums.Add(i+1);
                Console.Write(ListOfNums[i]);
                Console.Write(" ");
            }
            Console.WriteLine();

            while (amountOfZeroes != (ListOfNums.Count - 1))
            {
                printNumsList(ref ListOfNums, ref keep, ref amountOfZeroes);
            }
            Console.Read();
        }

        public static void printNumsList(ref List<int> io_List_Of_Nums, ref bool io_Keep, ref int i_Amount_Of_Zeroes)
        {
            for (int i = 0; i < io_List_Of_Nums.Count; i++)
            {
                if (io_List_Of_Nums[i] == 0)
                {
                    if (i > 8)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    continue;
                }

                if (!io_Keep)
                {
                    io_Keep = true;
                    io_List_Of_Nums[i] = 0;
                    if (i > 8)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    i_Amount_Of_Zeroes++;
                }

                else
                {
                    Console.Write(i + 1);
                    Console.Write(" ");
                    io_Keep = false;
                }
            }
            Console.WriteLine();
        }

        public static void bitsWork()
        {
            string input = Console.ReadLine();
            long inputLong = Int64.Parse(input);
            int amountOfZeroes = 0;
            bool keep = true;
            List<int> ListOfBits = new List<int>();
            for (int i = 0; i < inputLong; i++)
            {
                ListOfBits.Add(1);
                Console.Write("1 ");
            }
            Console.WriteLine();
            while (amountOfZeroes != (ListOfBits.Count - 1))
            {
                printBitsList(ref ListOfBits, ref keep, ref amountOfZeroes);
            }
            Console.Read();
        }

        public static void printBitsList(ref List<int> io_List_Of_Bits, ref bool io_Keep, ref int i_Amount_Of_Zeroes)
        {
            for (int i = 0; i < io_List_Of_Bits.Count; i++)
            {
                if (io_List_Of_Bits[i] == 0)
                {
                    Console.Write("0 ");
                    continue;
                }

                if (!io_Keep)
                {
                    io_Keep = true;
                    io_List_Of_Bits[i] = 0;
                    Console.Write("0 ");
                    i_Amount_Of_Zeroes++;
                }

                else
                {
                    Console.Write("1 ");
                    io_Keep = false;
                }
            }
            Console.WriteLine();
        }*/

        public static int checkTier(ulong i_InputNum)
        {
            int tier = 0;

            for (int i = 1; i < 100; i++)
            {
                if (myPow(i) <= i_InputNum)
                {
                    tier++;
                }
                else
                {
                    break;
                }
            }
            return tier;
        }

        public static ulong myPow(int exponent)
        {
            ulong answer = 1;

            for(int i = 0; i < exponent; i++)
            {
                answer = answer * 2;
            }
            return answer;
        }

        static void Main(String[] args)
        {
            //numsWork();
            //bitsWork();
            int amount_Of_Scenarios = Int32.Parse(Console.ReadLine());
            List<ulong> answersList = new List<ulong>();

            for (int i = 0; i < amount_Of_Scenarios; i++)
            {
                string input = Console.ReadLine();
                ulong inputULong = ulong.Parse(input);
                int tier = checkTier(inputULong);
                ulong distance = inputULong - myPow(tier);

                if (distance == 0)
                {
                    answersList.Add(1);
                }
                else
                {
                    answersList.Add((distance * 2) + 1);
                }
            }

            foreach (ulong answer in answersList)
            {
                Console.WriteLine(answer);
            }
        }
    }
}