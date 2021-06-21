using System;
using System.Collections.Generic;
using System.IO;

namespace MosaicDecoration2
{
    class MosaicDecoration2
    {
        public static string[] parseInput()
        {
            string input_String = Console.ReadLine();

            string[] strings_List = input_String.Split(' ');

            return strings_List;
        }

        public static void calculateCost(long i_Total_Amount_Of_Tiles_Used, long i_Price_For_Ten_Tiles, long i_Inches_Cut, long i_Price_Per_Inch_Cut)
        {
            long finalPrice = 0;
            long amountOfPiles = 0;

            if (i_Total_Amount_Of_Tiles_Used <= 10)
            {
                amountOfPiles = 1;
            }
            else
            {
                if (i_Total_Amount_Of_Tiles_Used % 10 != 0)
                {
                    amountOfPiles = (i_Total_Amount_Of_Tiles_Used / 10) + 1;
                }
                else
                {
                    amountOfPiles = (i_Total_Amount_Of_Tiles_Used / 10);
                }
            }

            finalPrice = (amountOfPiles * i_Price_For_Ten_Tiles) + (i_Inches_Cut * i_Price_Per_Inch_Cut);
            Console.WriteLine(finalPrice);
        }

        public static void cutFillers(long i_Wall_Width, long i_Wall_Height, long i_Tile_Width, long i_Tile_Height, ref long io_Total_Amount_Of_Tiles_Used, ref long io_Inches_Cut)
        {
            long area_One_Fillers_Height = 0;
            long area_Two_Fillers_Width = 0;
            long full_Tiles_Used_In_Width = i_Wall_Width / i_Tile_Width;
            long full_Tiles_Used_In_Height = i_Wall_Height / i_Tile_Height;
            long current_Cut_Tile_Height = i_Tile_Height;
            long current_Cut_Tile_Width = i_Tile_Width;
            bool areaOneCreated = false;
            bool areaTwoCreated = false;
            bool new_Tile_Used = true;

            if (i_Wall_Height % i_Tile_Height != 0) // If the remainder is 0, that means the height part of things fits perfectly, no need for cuts to create "areaOne"
            {
                areaOneCreated = true;
                area_One_Fillers_Height = i_Wall_Height % i_Tile_Height;
                long area_One_Fillers_Created = 0;
                while (area_One_Fillers_Created != full_Tiles_Used_In_Width)
                {
                    current_Cut_Tile_Height = current_Cut_Tile_Height - area_One_Fillers_Height;
                    if (current_Cut_Tile_Height < 0)
                    {
                        current_Cut_Tile_Height = i_Tile_Height;
                        new_Tile_Used = true;
                    }
                    else
                    {
                        if (new_Tile_Used)
                        {
                            io_Total_Amount_Of_Tiles_Used++;
                            new_Tile_Used = false;
                        }
                        io_Inches_Cut += i_Tile_Width;
                        area_One_Fillers_Created++;
                    }
                }
            }

            current_Cut_Tile_Height = i_Tile_Height;
            if (i_Wall_Width % i_Tile_Width != 0) // If the remainder is 0, that means the width part of things fits perfectly, no need for cuts to create "areaTwo"
            {
                areaTwoCreated = true;
                area_Two_Fillers_Width = i_Wall_Width % i_Tile_Width;
                /*if (!(areaOneCreated && current_Cut_Tile_Height >= i_Tile_Height && current_Cut_Tile_Width >= area_Two_Fillers_Width)) // checking to see if the previous tiles used on areaOne's fillers might still be usable for some areaTwo fillers without just throwing old leftovers
                {
                    io_Total_Amount_Of_Tiles_Used++;
                    new_Tile_Used = false;
                }*/
                long area_Two_Fillers_Created = 0;
                while (area_Two_Fillers_Created != full_Tiles_Used_In_Height)
                {
                    current_Cut_Tile_Width = current_Cut_Tile_Width - area_Two_Fillers_Width;
                    if (current_Cut_Tile_Width < 0)
                    {
                        current_Cut_Tile_Width = i_Tile_Width;
                        new_Tile_Used = true;
                    }
                    else
                    {
                        if (new_Tile_Used)
                        {
                            io_Total_Amount_Of_Tiles_Used++;
                            new_Tile_Used = false;
                        }
                        io_Inches_Cut += i_Tile_Height;
                        area_Two_Fillers_Created++;
                    }
                }
            }

            if (areaOneCreated && areaTwoCreated) // If both "areaOne" and "areaTwo" have been created, that means this third area must be created aswell
            {
                if (current_Cut_Tile_Width < area_Two_Fillers_Width || current_Cut_Tile_Height < area_One_Fillers_Height)
                {
                    io_Total_Amount_Of_Tiles_Used++;
                }

                io_Inches_Cut += area_One_Fillers_Height + area_Two_Fillers_Width;
            }

        }

        static void Main(String[] args)
        {
            string[] stringsList = parseInput();

            long wall_Width = Int64.Parse(stringsList[0]);
            long wall_Height = Int64.Parse(stringsList[1]);
            long tile_Width = Int64.Parse(stringsList[2]);
            long tile_Height = Int64.Parse(stringsList[3]);
            long price_For_Ten_Tiles = Int64.Parse(stringsList[4]);
            long price_Per_Inch_Cut = Int64.Parse(stringsList[5]);

            long inches_Cut = 0;
            long total_Amount_Of_Tiles_Used = 0;
            long full_Tiles_Used_In_Height = wall_Height / tile_Height;
            long full_Tiles_Used_In_Width = wall_Width / tile_Width;

            total_Amount_Of_Tiles_Used += full_Tiles_Used_In_Height * full_Tiles_Used_In_Width;

            if ((wall_Height % tile_Height == 0) && (wall_Width % tile_Width == 0)) // Done, just calculate the amount of tiles used, no cuts needed;
            {
                calculateCost(total_Amount_Of_Tiles_Used, price_For_Ten_Tiles, inches_Cut, price_Per_Inch_Cut);
            }
            else // Cuts needed, let's figure out how many and it what shapes, split into 3 potential areas
            {
                cutFillers(wall_Width, wall_Height, tile_Width, tile_Height, ref total_Amount_Of_Tiles_Used, ref inches_Cut);
                calculateCost(total_Amount_Of_Tiles_Used, price_For_Ten_Tiles, inches_Cut, price_Per_Inch_Cut);
            }

            //Console.Read();
        }
    }
}