using System;
using System.Collections.Generic;
using System.IO;

namespace MagicSpell
{
    class Solution
    {
        static void Main(String[] args)
        {
            long numOfRows = Int64.Parse(Console.ReadLine());
            long finalValue = 1;
            long translatedHexToDecimalValue = 1;
            var translations = new Dictionary<string, string>(){
    {"xrtp", "0"},
    {"pmr", "1"},
    {"yep", "2"},
    {"yjtrr", "3"},
    {"gpit", "4"},
    {"gobr", "5"},
    {"doc", "6"},
    {"drbrm", "7"},
    {"rohjy", "8"},
    {"momr", "9"},
};
            for (long i = 0; i < numOfRows; i++)
            {
                string prev = String.Empty;
                string temp = String.Empty;
                String[] ArrayOfWords = Console.ReadLine().Split(' ');
                foreach(String word in ArrayOfWords)
                {
                    translations.TryGetValue(word, out temp);
                    temp = prev + temp;
                    prev = temp;
                }
                translatedHexToDecimalValue = Convert.ToInt64(temp, 16);
                finalValue = finalValue * translatedHexToDecimalValue;
            }
            Console.WriteLine(finalValue);
        }
    }
}