using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

class Solution
{
    //A simple method just for parsing the input into a list of strings.
    public static List<string> parseInputIntoList()
    {
        List<string> inputStringsList = new List<string>();
        string currString = string.Empty;

        while (true)
        {
            currString = Console.ReadLine();
            if (currString != "END")
            {
                inputStringsList.Add(currString);
            }
            else
            {
                break;
            }
        }
        return inputStringsList;
    }

    public static void makeTheMagicHappen(List<String> i_inputsList)
    {
        int listSize = i_inputsList.Count;

        //We're going by specific input usecases after figuring out all possible outcomes - these cut down complexity levels cause we're not using any complex parsing, not using BigInteger, not using recursive calls and the                 biggest possible string length could be 1 million (by this exercise's rules).
        for (int i = 0; i < listSize; i++)
        {
            if (i_inputsList[i] == "0")
            {
                Console.WriteLine("2");
                continue;
            }
            if (i_inputsList[i] == "1")
            {
                Console.WriteLine("1");
                continue;
            }
            else
            {
                int singleInputLength = i_inputsList[i].Length;
                if (singleInputLength == 1)
                {
                    Console.WriteLine("2");
                    continue;
                }
                if (singleInputLength > 1 && singleInputLength < 10)
                {
                    Console.WriteLine("3");
                    continue;
                }
                if (singleInputLength > 9 && singleInputLength < 1000001)
                {
                    Console.WriteLine("4");
                    continue;
                }
            }
        }
    }

    static void Main(String[] args)
    {
        List<string> inputsList = parseInputIntoList();
        makeTheMagicHappen(inputsList);
    }
}