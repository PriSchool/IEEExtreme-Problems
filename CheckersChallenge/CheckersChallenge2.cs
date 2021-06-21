using System;
using System.Collections;
using System.Collections.Generic;

namespace CheckersChallenge
{
    public enum pieceType
    {
        Soldier,
        King
    }

    public enum direction
    {
        Left,
        Up,
        Right,
        Down,
        None
    }

    public class WhitePiece
    {
        public pieceType pieceType = pieceType.Soldier;
        public Location location;
        public direction cameFromDirection = direction.None;

        public WhitePiece(Location i_location)
        {
            location = i_location;
        }

        public static bool moveAsSoldier(ref Board i_currBoard, ref int io_resultAmount, Stack i_boardsStack)
        {
            //check left
            if (i_currBoard.soldierEatingCheck[(int)direction.Left] == false)
            {
                i_currBoard.soldierEatingCheck[(int)direction.Left] = true;
                if (i_currBoard.whitePiece.cameFromDirection != direction.Left)
                {
                    if (i_currBoard.whitePiece.location.col - 2 >= 0)
                    {
                        if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 1].data == 'x')
                        {
                            if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 2].data == '.')
                            {
                                i_boardsStack.Push(i_currBoard);
                                Location targetLocation = new Location('.', i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 2);
                                Board BoardToSend = ModifyBoard(i_currBoard, targetLocation, i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 1]);
                                BoardToSend.whitePiece.cameFromDirection = direction.Right;
                                i_currBoard = BoardToSend;
                                return true;
                            }
                        }
                    }
                }
            }

            //check up
            if (i_currBoard.soldierEatingCheck[(int)direction.Up] == false)
            {
                i_currBoard.soldierEatingCheck[(int)direction.Up] = true;
                if (i_currBoard.whitePiece.cameFromDirection != direction.Up)
                {
                    if (i_currBoard.whitePiece.location.row - 2 >= 0)
                    {
                        if (i_currBoard.contents[i_currBoard.whitePiece.location.row - 1, i_currBoard.whitePiece.location.col].data == 'x')
                        {
                            if (i_currBoard.contents[i_currBoard.whitePiece.location.row - 2, i_currBoard.whitePiece.location.col].data == '.')
                            {
                                i_boardsStack.Push(i_currBoard);
                                Location targetLocation = new Location('.', i_currBoard.whitePiece.location.row - 2, i_currBoard.whitePiece.location.col);
                                Board BoardToSend = ModifyBoard(i_currBoard, targetLocation, i_currBoard.contents[i_currBoard.whitePiece.location.row - 1, i_currBoard.whitePiece.location.col]);
                                BoardToSend.whitePiece.cameFromDirection = direction.Down;
                                i_currBoard = BoardToSend;
                                return true;
                            }
                        }
                    }
                }
            }

            //check right
            if (i_currBoard.soldierEatingCheck[(int)direction.Right] == false)
            {
                i_currBoard.soldierEatingCheck[(int)direction.Right] = true;
                if (i_currBoard.whitePiece.cameFromDirection != direction.Right)
                {
                    if (i_currBoard.whitePiece.location.col + 2 <= 7)
                    {
                        if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 1].data == 'x')
                        {
                            if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 2].data == '.')
                            {
                                i_boardsStack.Push(i_currBoard);
                                Location targetLocation = new Location('.', i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 2);
                                Board BoardToSend = ModifyBoard(i_currBoard, targetLocation, i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 1]);
                                BoardToSend.whitePiece.cameFromDirection = direction.Left;
                                i_currBoard = BoardToSend;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool moveAsKing(ref Board i_currBoard, ref int io_resultAmount, Stack i_boardsStack)
        {
            List<Location> possibleHorizontalRightLocations = new List<Location>();
            List<Location> possibleHorizontalLeftLocations = new List<Location>();
            List<Location> possibleVerticalAboveLocations = new List<Location>();
            List<Location> possibleVerticalBelowLocations = new List<Location>();
            bool firstX = false;
            bool secondX = false;
            Location xLocationToDispose = new Location('x', -1, -1);

            //white piece row-left side check
            if (i_currBoard.kingEatingDirectionCheck[(int)direction.Left] == false)
            {
                if (i_currBoard.whitePiece.cameFromDirection != direction.Left)
                {
                    firstX = false;
                    secondX = false;
                    xLocationToDispose = new Location('x', -1, -1);
                    for (int i = i_currBoard.whitePiece.location.col - 1; i > -1; i--)
                    {
                        if (firstX == false && i_currBoard.contents[i_currBoard.whitePiece.location.row, i].data == 'x')
                        {
                            firstX = true;
                            xLocationToDispose = i_currBoard.contents[i_currBoard.whitePiece.location.row, i];
                            continue;
                        }
                        if (firstX == true && i_currBoard.contents[i_currBoard.whitePiece.location.row, i].data == 'x')
                        {
                            break;
                        }
                        if (firstX == true && secondX == false)
                        {
                            possibleHorizontalLeftLocations.Add(i_currBoard.contents[i_currBoard.whitePiece.location.row, i]);
                        }
                    }

                    foreach (Location possibleHorizontalLeftLocation in possibleHorizontalLeftLocations)
                    {
                        bool found = false;
                        foreach (Location directionCheck in i_currBoard.kingEatingCheck[direction.Left])
                        {
                            if (directionCheck.row == possibleHorizontalLeftLocation.row && directionCheck.col == possibleHorizontalLeftLocation.col)
                            {
                                found = true;
                                break;
                            }

                        }
                        if (!found)
                        {
                            i_currBoard.kingEatingCheck[direction.Left].Add(possibleHorizontalLeftLocation);
                            if (i_currBoard.kingEatingCheck[direction.Left].Count == possibleHorizontalLeftLocations.Count)
                            {
                                i_currBoard.kingEatingDirectionCheck[(int)direction.Left] = true;
                            }
                        }
                        if (found)
                        {
                            continue;
                        }

                        i_boardsStack.Push(i_currBoard);
                        Board BoardToSend = ModifyBoard(i_currBoard, possibleHorizontalLeftLocation, xLocationToDispose);
                        BoardToSend.whitePiece.cameFromDirection = direction.Right;
                        i_currBoard = BoardToSend;
                        return true;
                    }
                    if (possibleHorizontalLeftLocations.Count == 0)
                    {
                        i_currBoard.kingEatingDirectionCheck[(int)direction.Left] = true;
                    }
                }
                else
                {
                    i_currBoard.kingEatingDirectionCheck[(int)direction.Left] = true;
                }
            }

            //white piece col-above check
            if (i_currBoard.kingEatingDirectionCheck[(int)direction.Up] == false)
            {
                if (i_currBoard.whitePiece.cameFromDirection != direction.Up)
                {
                    firstX = false;
                    secondX = false;
                    xLocationToDispose = new Location('x', -1, -1);
                    for (int i = i_currBoard.whitePiece.location.row - 1; i > -1; i--)
                    {
                        if (firstX == false && i_currBoard.contents[i, i_currBoard.whitePiece.location.col].data == 'x')
                        {
                            firstX = true;
                            xLocationToDispose = i_currBoard.contents[i, i_currBoard.whitePiece.location.col];
                            continue;
                        }
                        if (firstX == true && i_currBoard.contents[i, i_currBoard.whitePiece.location.col].data == 'x')
                        {
                            break;
                        }
                        if (firstX == true && secondX == false)
                        {
                            possibleVerticalAboveLocations.Add(i_currBoard.contents[i, i_currBoard.whitePiece.location.col]);
                        }
                    }

                    foreach (Location possibleVerticalAboveLocation in possibleVerticalAboveLocations)
                    {
                        bool found = false;
                        foreach (Location directionCheck in i_currBoard.kingEatingCheck[direction.Up])
                        {
                            if (directionCheck.row == possibleVerticalAboveLocation.row && directionCheck.col == possibleVerticalAboveLocation.col)
                            {
                                found = true;
                                break;
                            }

                        }
                        if (!found)
                        {
                            i_currBoard.kingEatingCheck[direction.Up].Add(possibleVerticalAboveLocation);
                            if (i_currBoard.kingEatingCheck[direction.Up].Count == possibleVerticalAboveLocations.Count)
                            {
                                i_currBoard.kingEatingDirectionCheck[(int)direction.Up] = true;
                            }
                        }
                        if (found)
                        {
                            continue;
                        }

                        i_boardsStack.Push(i_currBoard);
                        Board BoardToSend = ModifyBoard(i_currBoard, possibleVerticalAboveLocation, xLocationToDispose);
                        BoardToSend.whitePiece.cameFromDirection = direction.Down;
                        i_currBoard = BoardToSend;
                        return true;
                    }
                    if (possibleVerticalAboveLocations.Count == 0)
                    {
                        i_currBoard.kingEatingDirectionCheck[(int)direction.Up] = true;
                    }
                }
                else
                {
                    i_currBoard.kingEatingDirectionCheck[(int)direction.Up] = true;
                }
            }

            //white piece row-right side check
            if (i_currBoard.kingEatingDirectionCheck[(int)direction.Right] == false)
            {
                if (i_currBoard.whitePiece.cameFromDirection != direction.Right)
                {
                    firstX = false;
                    secondX = false;
                    xLocationToDispose = new Location('x', -1, -1);
                    for (int i = i_currBoard.whitePiece.location.col + 1; i < 8; i++)
                    {
                        if (firstX == false && i_currBoard.contents[i_currBoard.whitePiece.location.row, i].data == 'x')
                        {
                            firstX = true;
                            xLocationToDispose = i_currBoard.contents[i_currBoard.whitePiece.location.row, i];
                            continue;
                        }
                        if (firstX == true && i_currBoard.contents[i_currBoard.whitePiece.location.row, i].data == 'x')
                        {
                            break;
                        }
                        if (firstX == true && secondX == false)
                        {
                            possibleHorizontalRightLocations.Add(i_currBoard.contents[i_currBoard.whitePiece.location.row, i]);
                        }
                    }
                    foreach (Location possibleHorizontalRightLocation in possibleHorizontalRightLocations)
                    {
                        bool found = false;
                        foreach (Location directionCheck in i_currBoard.kingEatingCheck[direction.Right])
                        {
                            if (directionCheck.row == possibleHorizontalRightLocation.row && directionCheck.col == possibleHorizontalRightLocation.col)
                            {
                                found = true;
                                break;
                            }

                        }
                        if (!found)
                        {
                            i_currBoard.kingEatingCheck[direction.Right].Add(possibleHorizontalRightLocation);
                            if (i_currBoard.kingEatingCheck[direction.Right].Count == possibleHorizontalRightLocations.Count)
                            {
                                i_currBoard.kingEatingDirectionCheck[(int)direction.Right] = true;
                            }
                        }
                        if (found)
                        {
                            continue;
                        }

                        i_boardsStack.Push(i_currBoard);
                        Board BoardToSend = ModifyBoard(i_currBoard, possibleHorizontalRightLocation, xLocationToDispose);
                        BoardToSend.whitePiece.cameFromDirection = direction.Left;
                        i_currBoard = BoardToSend;
                        return true;
                    }
                    if (possibleHorizontalRightLocations.Count == 0)
                    {
                        i_currBoard.kingEatingDirectionCheck[(int)direction.Right] = true;
                    }
                }
                else
                {
                    i_currBoard.kingEatingDirectionCheck[(int)direction.Right] = true;
                }
            }

            //white piece col-below check
            if (i_currBoard.kingEatingDirectionCheck[(int)direction.Down] == false)
            {
                if (i_currBoard.whitePiece.cameFromDirection != direction.Down)
                {
                    firstX = false;
                    secondX = false;
                    xLocationToDispose = new Location('x', -1, -1);
                    for (int i = i_currBoard.whitePiece.location.row + 1; i < 8; i++)
                    {
                        if (firstX == false && i_currBoard.contents[i, i_currBoard.whitePiece.location.col].data == 'x')
                        {
                            firstX = true;
                            xLocationToDispose = i_currBoard.contents[i, i_currBoard.whitePiece.location.col];
                            continue;
                        }
                        if (firstX == true && i_currBoard.contents[i, i_currBoard.whitePiece.location.col].data == 'x')
                        {
                            break;
                        }
                        if (firstX == true && secondX == false)
                        {
                            possibleVerticalBelowLocations.Add(i_currBoard.contents[i, i_currBoard.whitePiece.location.col]);
                        }
                    }

                    foreach (Location possibleVerticalBelowLocation in possibleVerticalBelowLocations)
                    {
                        bool found = false;
                        foreach (Location directionCheck in i_currBoard.kingEatingCheck[direction.Down])
                        {
                            if (directionCheck.row == possibleVerticalBelowLocation.row && directionCheck.col == possibleVerticalBelowLocation.col)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            i_currBoard.kingEatingCheck[direction.Down].Add(possibleVerticalBelowLocation);
                            if (i_currBoard.kingEatingCheck[direction.Down].Count == possibleVerticalBelowLocations.Count)
                            {
                                i_currBoard.kingEatingDirectionCheck[(int)direction.Down] = true;
                            }
                        }
                        if (found)
                        {
                            continue;
                        }

                        i_boardsStack.Push(i_currBoard);
                        Board BoardToSend = ModifyBoard(i_currBoard, possibleVerticalBelowLocation, xLocationToDispose);
                        BoardToSend.whitePiece.cameFromDirection = direction.Up;
                        i_currBoard = BoardToSend;
                        return true;
                    }
                    if (possibleVerticalBelowLocations.Count == 0)
                    {
                        i_currBoard.kingEatingDirectionCheck[(int)direction.Down] = true;
                    }

                }
                else
                {
                    i_currBoard.kingEatingDirectionCheck[(int)direction.Down] = true;
                }
            }
            return false;
        }

        public static Board ModifyBoard(Board i_currBoard, Location i_whitePieceTargetLocation, Location i_xToDispose)
        {
            Board ChangingBoard = new Board(i_currBoard);
            foreach (Location locationToRemove in ChangingBoard.xLocations)
            {
                if (locationToRemove.row == i_xToDispose.row && locationToRemove.col == i_xToDispose.col)
                {
                    ChangingBoard.xLocations.Remove(locationToRemove);
                    break;
                }
            }
            ChangingBoard.contents[i_xToDispose.row, i_xToDispose.col].data = '.';
            ChangingBoard.contents[ChangingBoard.whitePiece.location.row, ChangingBoard.whitePiece.location.col].data = '.';
            ChangingBoard.whitePiece.location.row = i_whitePieceTargetLocation.row;
            ChangingBoard.whitePiece.location.col = i_whitePieceTargetLocation.col;
            ChangingBoard.whitePiece.location.data = 'o';
            ChangingBoard.whitePiece.pieceType = i_currBoard.whitePiece.pieceType;
            ChangingBoard.contents[i_whitePieceTargetLocation.row, i_whitePieceTargetLocation.col].data = 'o';
            return ChangingBoard;
        }
    }

    public class Location
    {
        public char data;
        public int row;
        public int col;

        public Location(char i_data, int i_row, int i_col)
        {
            data = i_data;
            row = i_row;
            col = i_col;
        }
    }

    public class Board
    {
        public Location[,] contents;
        public List<Location> xLocations;
        public List<bool> soldierEatingCheck = new List<bool> { false, false, false, false };
        public Dictionary<direction, List<Location>> kingEatingCheck = new Dictionary<direction, List<Location>> { { direction.Left, new List<Location>() }, { direction.Right, new List<Location>() }, { direction.Up, new List<Location>() }, { direction.Down, new List<Location>() } };
        public List<bool> kingEatingDirectionCheck = new List<bool> { false, false, false, false };
        public WhitePiece whitePiece;

        public Board(int i_height, int i_width)
        {
            contents = new Location[i_height, i_width];
            xLocations = new List<Location>();
        }

        public Board(Board i_Board)
        {
            contents = new Location[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    contents[i, j] = new Location(i_Board.contents[i, j].data, i_Board.contents[i, j].row, i_Board.contents[i, j].col);
                }
            }
            xLocations = new List<Location>(i_Board.xLocations);
            whitePiece = new WhitePiece(new Location(i_Board.whitePiece.location.data, i_Board.whitePiece.location.row, i_Board.whitePiece.location.col));

        }

        public static List<Board> ParseInput(ref int io_t_amount)
        {
            string firstLine = Console.ReadLine();
            int t = Int32.Parse(firstLine);
            List<Board> BoardsList = new List<Board>();

            //Looping 't' amount of times, placing into list of boards
            for (int i = 0; i < t; i++)
            {
                Board currBoard = new Board(8, 8);
                int singleInput = 0;

                //Parsing a single board
                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8;)
                    {
                        singleInput = Console.Read();
                        if ((char)singleInput != '\n' && (char)singleInput != '\r')
                        {
                            Location currLocation = new Location((char)singleInput, row, col);
                            currBoard.contents[row, col] = currLocation;
                            if ((char)singleInput == 'o')
                            {
                                currBoard.whitePiece = new WhitePiece(currLocation);
                                if (currBoard.whitePiece.location.row == 0)
                                {
                                    currBoard.whitePiece.pieceType = pieceType.King;
                                }
                            }
                            if ((char)singleInput == 'x')
                            {
                                currBoard.xLocations.Add(currLocation);
                            }
                            col++;
                        }
                    }
                }
                BoardsList.Add(currBoard);
                Console.ReadLine();
            }
            io_t_amount = t;
            return BoardsList;
        }

        public override string ToString()
        {
            string outputBoard = string.Empty;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    outputBoard += contents[row, col].data;
                }
                outputBoard += Environment.NewLine;
            }
            return outputBoard;
        }

        public static void PrintBoards(List<Board> i_boardsList, int t)
        {
            Console.WriteLine("# ------------------------------ #");
            foreach (Board board in i_boardsList)
            {
                Console.WriteLine(board.ToString());
                Console.WriteLine("White piece position: (" + board.whitePiece.location.row + ", " + board.whitePiece.location.col + ")");
                Console.WriteLine("White piece type: " + board.whitePiece.pieceType);
                Console.WriteLine("Amount of oponnent's pieces: " + board.xLocations.Count);
                Console.WriteLine();
            }
        }

        public static int returnAmountOfPossibleWinningMoves(Board i_board)
        {
            int resultAmount = 0;
            Stack boardsStack = new Stack();

            while (true)
            {
                if (i_board.whitePiece.location.row == 0)
                {
                    i_board.whitePiece.pieceType = pieceType.King;
                }
                if (boardsStack.Count == 0 && i_board.whitePiece.pieceType == pieceType.Soldier && i_board.soldierEatingCheck[0] == true && i_board.soldierEatingCheck[1] == true && i_board.soldierEatingCheck[2] == true)
                {
                    break;
                }
                if (boardsStack.Count == 0 && i_board.whitePiece.pieceType == pieceType.King && i_board.kingEatingDirectionCheck[0] == true && i_board.kingEatingDirectionCheck[1] == true && i_board.kingEatingDirectionCheck[2] == true && i_board.kingEatingDirectionCheck[3] == true)
                {
                    break;
                }

                if (i_board.whitePiece.pieceType == pieceType.Soldier)
                {
                    if (!WhitePiece.moveAsSoldier(ref i_board, ref resultAmount, boardsStack))
                    {
                        if (boardsStack.Count != 0)
                        {
                            i_board = (Board)boardsStack.Pop();
                        }
                    }
                }
                else
                {
                    if (!WhitePiece.moveAsKing(ref i_board, ref resultAmount, boardsStack))
                    {
                        if (boardsStack.Count != 0)
                        {
                            i_board = (Board)boardsStack.Pop();
                        }
                    }
                }

                if (i_board.xLocations.Count == 0)
                {
                    resultAmount++;
                }
                //Console.WriteLine(i_board.ToString());
            }
            return resultAmount;
        }
    }

    class Solution
    {
        static void Main(String[] args)
        {
            /* create a recursive structure that will map all possible outcomes of 
            "eating an opponent's piece by standing beyond,
            deleting it, and going again from there", and count in how many of those you leave a map totalling with 0 x's;
            loop that 't' times to get all testcases. */
            int t = 0;
            List<Board> BoardsList = Board.ParseInput(ref t);
            //int count = 1;
            foreach (Board board in BoardsList)
            {
                //Console.WriteLine("Amount of different winning paths for board number " + count + ": " + board.returnAmountOfPossibleWinningMoves());
                Console.WriteLine(Board.returnAmountOfPossibleWinningMoves(board));
                //count++;
            }

            //Board.PrintBoards(BoardsList, t);
            Console.ReadLine();
        }
    }
}