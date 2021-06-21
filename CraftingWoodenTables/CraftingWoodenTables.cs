using System;

namespace CraftingWoodenTables
{
    class Solution
    {
        public static string[] parseInput()
        {
            string[] inputsArray = Console.ReadLine().Split(' ');

            return inputsArray;
        }
        
        public static bool createNewTable(long i_Maximal_Planks_Amount_In_A_Slot, long i_Single_Table_Cost, ref long io_InteractiveSlot, ref long io_Slots_Depleted, ref long io_Empty_Slots_Amount, long i_Full_Slots_Amount)
        {
            bool tableIsCreated = false;
            long currentTableBuildCount = 0;

            while (io_Slots_Depleted < i_Full_Slots_Amount)
            {
                long currentAmountMissing = i_Single_Table_Cost - currentTableBuildCount;

                if (io_InteractiveSlot < currentAmountMissing) // check if what's in the slot is equal or lower to the amount I'm still missing for the table build
                {
                    currentTableBuildCount += io_InteractiveSlot;
                    io_InteractiveSlot = i_Maximal_Planks_Amount_In_A_Slot;
                    io_Empty_Slots_Amount++;
                    io_Slots_Depleted++;
                    continue;
                }
                if (io_InteractiveSlot == currentAmountMissing)
                {
                    io_InteractiveSlot = i_Maximal_Planks_Amount_In_A_Slot;
                    io_Slots_Depleted++;
                    tableIsCreated = true;
                    break;
                }
                if (io_InteractiveSlot > currentAmountMissing)
                {
                    io_InteractiveSlot = io_InteractiveSlot - currentAmountMissing;
                    if (io_Empty_Slots_Amount > 0) // is what I'm holding enough to build a table right now + is there empty room for the table to be stored at?
                    {
                        io_Empty_Slots_Amount--;
                        tableIsCreated = true;
                    }
                    break;
                }
            }
            return tableIsCreated;
        }

        public static long calculateAmountOfTables(string[] i_InputsArray)
        {
            long single_Table_Cost = long.Parse(i_InputsArray[0]);
            long amount_Of_Slots = long.Parse(i_InputsArray[1]);
            long maximal_Planks_Amount_In_A_Slot = long.Parse(i_InputsArray[2]);
            long total_Planks_Amount = long.Parse(i_InputsArray[3]);
            long full_Slots_Amount = total_Planks_Amount / maximal_Planks_Amount_In_A_Slot;
            long amount_Of_Ready_Tables = 0;
            long empty_Slots_Amount = amount_Of_Slots - full_Slots_Amount;
            long potential_Tables_Amount_To_Create = total_Planks_Amount / single_Table_Cost;

            if (single_Table_Cost > total_Planks_Amount) // if a table costs more than the given amount of planks to start with
            {
                return amount_Of_Ready_Tables;
            }

            if (total_Planks_Amount % maximal_Planks_Amount_In_A_Slot != 0)
            {
                full_Slots_Amount++;
                empty_Slots_Amount--;
            }

            long interactiveSlot = total_Planks_Amount % maximal_Planks_Amount_In_A_Slot;
            if (interactiveSlot == 0)
            {
                interactiveSlot = maximal_Planks_Amount_In_A_Slot;
            }

            long slots_Depleted = 0;

            if ((amount_Of_Slots >= potential_Tables_Amount_To_Create) && (maximal_Planks_Amount_In_A_Slot <= single_Table_Cost)) // if the amount of tables possible to create from the current amount of planks is able to fit the current amount of slots given, while also performing "slot cleanups" in each table creation
            {
                return potential_Tables_Amount_To_Create;
            }
            else
            {
                bool tableIsCreated = false;
                do
                {
                    tableIsCreated = createNewTable(maximal_Planks_Amount_In_A_Slot, single_Table_Cost, ref interactiveSlot, ref slots_Depleted, ref empty_Slots_Amount, full_Slots_Amount);
                    if (tableIsCreated)
                    {
                        amount_Of_Ready_Tables++;
                    }
                }
                while (tableIsCreated);
            }
            return amount_Of_Ready_Tables;
        }


        static void Main(String[] args)
        {
            string[] inputsArray = parseInput();
            Console.WriteLine(calculateAmountOfTables(inputsArray));
            //Console.Read();
        }
    }
}