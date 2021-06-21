using System;
using System.Collections.Generic;

public enum pieceType
{
    Soldier,
    King
}

public enum direction
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class WhitePiece
{
    public pieceType pieceType = pieceType.Soldier;
    public Location location;
    public direction cameFromDirection = direction.None;

    public WhitePiece(Location i_location, pieceType i_pieceType)
    {
        location = i_location;
        pieceType = i_pieceType;
    }

    public static void checkWhitePieceType(ref WhitePiece io_whitePiece)
    {
        if (io_whitePiece.location.row == 0)
        {
            io_whitePiece.pieceType = pieceType.King;
        }
    }

    public static void checkAvailableMoves(Board i_currBoard, ref int io_resultAmount)
    {
        if (i_currBoard.whitePiece.pieceType == pieceType.Soldier)
        {
            //check up
            if (i_currBoard.whitePiece.cameFromDirection != direction.Up)
            {
                if (i_currBoard.whitePiece.location.row - 2 >= 0)
                {
                    if (i_currBoard.contents[i_currBoard.whitePiece.location.row - 1, i_currBoard.whitePiece.location.col].data == 'x')
                    {
                        if (i_currBoard.contents[i_currBoard.whitePiece.location.row - 2, i_currBoard.whitePiece.location.col].data == '.')
                        {
                            Location targetLocation = new Location('.', i_currBoard.whitePiece.location.row - 2, i_currBoard.whitePiece.location.col);
                            Board BoardToSend = PerformOpponentTake(i_currBoard, targetLocation, i_currBoard.contents[i_currBoard.whitePiece.location.row - 1, i_currBoard.whitePiece.location.col]);
                            BoardToSend.whitePiece.cameFromDirection = direction.Down;
                            makeTurn(BoardToSend, ref io_resultAmount);
                        }
                    }
                }
            }
                
            //check left
            if (i_currBoard.whitePiece.cameFromDirection != direction.Left)
            {
                if (i_currBoard.whitePiece.location.col - 2 >= 0)
                {
                    if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 1].data == 'x')
                    {
                        if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 2].data == '.')
                        {
                            Location targetLocation = new Location('.', i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 2);
                            Board BoardToSend = PerformOpponentTake(i_currBoard, targetLocation, i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col - 1]);
                            BoardToSend.whitePiece.cameFromDirection = direction.Right;
                            makeTurn(BoardToSend, ref io_resultAmount);
                        }
                    }
                }
            }
                
            //check right
            if (i_currBoard.whitePiece.cameFromDirection != direction.Right)
            {
                if (i_currBoard.whitePiece.location.col + 2 <= 7)
                {
                    if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 1].data == 'x')
                    {
                        if (i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 2].data == '.')
                        {
                            Location targetLocation = new Location('.', i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 2);
                            Board BoardToSend = PerformOpponentTake(i_currBoard, targetLocation, i_currBoard.contents[i_currBoard.whitePiece.location.row, i_currBoard.whitePiece.location.col + 1]);
                            BoardToSend.whitePiece.cameFromDirection = direction.Left;
                            makeTurn(BoardToSend, ref io_resultAmount);
                        }
                    }
                }
            } 
        }

        //king moves implementation
        else
        {
            List<Location> possibleHorizontalRightLocations = new List<Location>();
            List<Location> possibleHorizontalLeftLocations = new List<Location>();
            List<Location> possibleVerticalAboveLocations = new List<Location>();
            List<Location> possibleVerticalBelowLocations = new List<Location>();
            bool firstX = false;
            bool secondX = false;
            Location xLocationToDispose = new Location('x', -1, -1);

            //white piece row-right side check
            if (i_currBoard.whitePiece.cameFromDirection != direction.Right)
            {
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
                    Board BoardToSend = PerformOpponentTake(i_currBoard, possibleHorizontalRightLocation, xLocationToDispose);
                    //Console.WriteLine(BoardToSend.ToString());
                    BoardToSend.whitePiece.cameFromDirection = direction.Left;
                    makeTurn(BoardToSend, ref io_resultAmount);
                }
            }

            //white piece row-left side check
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
                    Board BoardToSend = PerformOpponentTake(i_currBoard, possibleHorizontalLeftLocation, xLocationToDispose);
                    //Console.WriteLine(BoardToSend.ToString());
                    BoardToSend.whitePiece.cameFromDirection = direction.Right;
                    makeTurn(BoardToSend, ref io_resultAmount);
                }
            }

            //white piece col-above check
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
                    Board BoardToSend = PerformOpponentTake(i_currBoard, possibleVerticalAboveLocation, xLocationToDispose);
                    //Console.WriteLine(BoardToSend.ToString());
                    BoardToSend.whitePiece.cameFromDirection = direction.Down;
                    makeTurn(BoardToSend, ref io_resultAmount);
                }
            }

            //white piece col-below check
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
                    Board BoardToSend = PerformOpponentTake(i_currBoard, possibleVerticalBelowLocation, xLocationToDispose);
                    //Console.WriteLine(BoardToSend.ToString());
                    BoardToSend.whitePiece.cameFromDirection = direction.Up;
                    makeTurn(BoardToSend, ref io_resultAmount);
                }
            }
        }
    }

    public static Board PerformOpponentTake(Board i_currBoard, Location i_whitePieceTargetLocation, Location i_xToDispose)
    {
        Board ChangingBoard = new Board(i_currBoard);
        foreach(Location locationToRemove in ChangingBoard.xLocations)
        {
            if(locationToRemove.row == i_xToDispose.row && locationToRemove.col == i_xToDispose.col)
            {
                ChangingBoard.xLocations.Remove(locationToRemove);
                break;
            }
        }
        ChangingBoard.contents[i_xToDispose.row, i_xToDispose.col].data = '.';
        ChangingBoard.contents[ChangingBoard.whitePiece.location.row, ChangingBoard.whitePiece.location.col].data = '.';
        ChangingBoard.whitePiece.location = i_whitePieceTargetLocation;
        ChangingBoard.contents[i_whitePieceTargetLocation.row, i_whitePieceTargetLocation.col].data = 'o';
        return ChangingBoard;
    }

    public static void makeTurn(Board i_currBoard, ref int io_resultAmount)
    {
        if (i_currBoard.xLocations.Count == 0)
        {
            io_resultAmount++;
            return;
        }
        if (i_currBoard.whitePiece.pieceType == pieceType.Soldier)
        {
            //check wether white piece currently needs to be changed from a soldier to a king 
            checkWhitePieceType(ref i_currBoard.whitePiece);
        }
        checkAvailableMoves(i_currBoard, ref io_resultAmount);
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
    public WhitePiece whitePiece;

    public Board(int i_height, int i_width)
    {
        contents = new Location[i_height, i_width];
        xLocations = new List<Location>();
    }

    public Board(Board i_Board)
    {
        contents = new Location[8,8];
        for(int i = 0; i< 8; i++)
        {
            for(int j = 0; j< 8; j++)
            {
                contents[i, j] = new Location(i_Board.contents[i, j].data, i_Board.contents[i, j].row, i_Board.contents[i, j].col);
            }   
        }
        xLocations = new List<Location>(i_Board.xLocations);
        whitePiece = new WhitePiece(new Location(i_Board.whitePiece.location.data, i_Board.whitePiece.location.row, i_Board.whitePiece.location.col), i_Board.whitePiece.pieceType);

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
                            pieceType Soldier = pieceType.Soldier;
                            currBoard.whitePiece = new WhitePiece(currLocation, Soldier);
                            WhitePiece.checkWhitePieceType(ref currBoard.whitePiece);
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

    public int returnAmountOfPossibleWinningMoves()
    {
        int resultAmount = 0;
        WhitePiece.makeTurn(this, ref resultAmount);
        return resultAmount;
    }
}

class Solution
{
    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

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
            Console.WriteLine(board.returnAmountOfPossibleWinningMoves());
            //count++;
        }

        //Board.PrintBoards(BoardsList, t);
        //Console.ReadLine();
    }
}
