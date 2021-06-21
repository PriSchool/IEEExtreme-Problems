using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    class Solution
    {
        public static void rulesParse(ref int[] o_DeadCellsRules, ref int[] o_LivingCellsRules)
        {
            int currentInput = -1;
            for (int i = 0; i < 5; i++)
            {
                currentInput = Console.Read();
                o_DeadCellsRules[i] = (currentInput - 48);
            }

            Console.Read();

            for (int i = 0; i < 5; i++)
            {
                currentInput = Console.Read();
                o_LivingCellsRules[i] = (currentInput - 48);
            }

            Console.ReadLine();
        }

        public static int[,] boardParse(ref int o_amountOfTurns)
        {
            string stringInput = Console.ReadLine();
            string[] splittedInput = stringInput.Split(' ');
            int size = Int32.Parse(splittedInput[0]);
            o_amountOfTurns = Int32.Parse(splittedInput[1]);
            int[,] starterBoard = new int[size, size];

            int cellInput = -1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellInput = Console.Read();
                    cellInput = cellInput - 48;
                    starterBoard[i, j] = cellInput;
                }
                Console.ReadLine();
            }

            return starterBoard;
        }

        public static void printBoard(int[,] i_BoardToPrint, int i_BoardSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    Console.Write(i_BoardToPrint[i, j]);
                }
                Console.Write("\n");
            }
        }

        public static int[,] performSingleBoardTurn(int[,] currentBoard, int i_BoardSize, int[] deadCellsRules, int[] livingCellsRules)
        {
            int[,] newBoard = (int[,])currentBoard.Clone();
            int[] neighborsData = new int[4];
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    int LivingNeighborsAmount = 0;

                    if (currentBoard[i, (j - 1 + i_BoardSize) % i_BoardSize] == 1)
                    {
                        LivingNeighborsAmount++;
                    }
                    if (currentBoard[(i - 1 + i_BoardSize) % i_BoardSize, j] == 1)
                    {
                        LivingNeighborsAmount++;
                    }
                    if (currentBoard[i, (j + 1 + i_BoardSize) % i_BoardSize] == 1)
                    {
                        LivingNeighborsAmount++;
                    }
                    if (currentBoard[(i + 1 + i_BoardSize) % i_BoardSize, j] == 1)
                    {
                        LivingNeighborsAmount++;
                    }
                    int currentCell = currentBoard[i, j];
                    newBoard[i, j] = applyGameRules(LivingNeighborsAmount, deadCellsRules, livingCellsRules, currentCell);
                }
            }

            return newBoard;
        }

        public static int applyGameRules(int i_LivingNeighborsAmount, int[] i_DeadCellsRules, int[] i_LivingCellsRules, int i_CurrentCell)
        {
            if (i_CurrentCell == 0)
            {
                for(int i = 0; i < 5; i++)
                {
                    if (i_DeadCellsRules[i_LivingNeighborsAmount] == 1)
                    {
                        i_CurrentCell = 1;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i_LivingCellsRules[i_LivingNeighborsAmount] != 1)
                    {
                        i_CurrentCell = 0;
                        break;
                    }
                }
            }
            
            return i_CurrentCell;
        }

        public static int[,] computeGameOfLife(int[,] starterBoard, int i_BoardSize, int[] i_DeadCellsRules, int[] i_LivingCellsRules, int i_AmountOfTurns)
        {
            int[,] currentBoard = (int[,])starterBoard.Clone();

            //printBoard(currentBoard, i_BoardSize);
            for (int i = 0; i< i_AmountOfTurns; i++)
            {
                int[,] newBoard = performSingleBoardTurn(currentBoard, i_BoardSize, i_DeadCellsRules, i_LivingCellsRules);
                currentBoard = (int[,])newBoard.Clone();
            }
            printBoard(currentBoard, i_BoardSize);
            return currentBoard;
        }

        public static void Main()
        {
            int[] deadCellsRules = new int[5];
            int[] livingCellsRules = new int[5];
            rulesParse(ref deadCellsRules, ref livingCellsRules);
            int amountOfTurns = 0;
            int[,] starterBoard = boardParse(ref amountOfTurns);
            int boardSize = starterBoard.GetLength(0);
            computeGameOfLife(starterBoard, boardSize, deadCellsRules, livingCellsRules, amountOfTurns);
            Console.Read();
        }
    }
}