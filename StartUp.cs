using System;
using System.Linq;

namespace GreenVsRed
{
    public class StartUp
    {
        static void Main()
        {
            //get the initial dimension of the grid
            int[] dimensions = Console.ReadLine()
                                      .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(int.Parse)
                                      .ToArray();

            //initialize the grid
            object[,] grid = new object[dimensions[0], dimensions[1]];

            //fill in the matrix grid
            for (int rows = 0; rows < dimensions[0]; rows++)
            {
                int[] row = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int cols = 0; cols < dimensions[1]; cols++)
                {
                    grid[rows, cols] = new Cell(row[cols]);
                }
            }

            //get last input command
            string[] lastArguements = Console.ReadLine()
                                             .Split(new char[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);

            //set generation constraint limit
            int genConstraint = int.Parse(lastArguements[2]);

            //set the desired cell coordinations
            int[] desiredCellCoordinates = new int[] { int.Parse(lastArguements[1]), int.Parse(lastArguements[0]) };

            int changeOfStates = 0;

            //generations
            for (int gen = 0; gen < genConstraint; gen++)
            {
                //main algorithm
                for (int rows = 0; rows < grid.GetLength(0); rows++)
                {
                    for (int cols = 0; cols < grid.GetLength(1); cols++)
                    {
                        int result = 0;

                        object[] cellsToCheck;

                        Cell currentCell = (Cell)grid[rows, cols];

                        currentCell.SetNextState();

                        Cell topCell, bottomCell, leftCell, rightCell,
                             cornerTopLeftCell, cornerTopRightCell,
                             cornerBotLeftCell, cornerBotRightCell;

                        //top row
                        if (rows == 0)
                        {
                            bottomCell = (Cell)grid[rows + 1, cols];

                            if (cols == 0)
                            {
                                rightCell = (Cell)grid[rows, cols + 1];
                                cornerBotRightCell = (Cell)grid[rows + 1, cols + 1];
                                cellsToCheck = new object[] { rightCell, cornerBotRightCell, bottomCell };
                            }
                            else if (cols == grid.GetLength(1) - 1)
                            {
                                leftCell = (Cell)grid[rows, cols - 1];
                                cornerBotLeftCell = (Cell)grid[rows + 1, cols - 1];
                                cellsToCheck = new object[] { leftCell, cornerBotLeftCell, bottomCell };
                            }
                            else
                            {
                                leftCell = (Cell)grid[rows, cols - 1];
                                cornerBotLeftCell = (Cell)grid[rows + 1, cols - 1];
                                cornerBotRightCell = (Cell)grid[rows + 1, cols + 1];
                                rightCell = (Cell)grid[rows, cols + 1];
                                cellsToCheck = new object[] { leftCell, cornerBotLeftCell, cornerBotRightCell, rightCell, bottomCell };
                            }
                        }
                        // bottom row
                        else if (rows == grid.GetLength(0) - 1)
                        {
                            topCell = (Cell)grid[rows - 1, cols];

                            if (cols == 0)
                            {
                                rightCell = (Cell)grid[rows, cols + 1];
                                cornerTopRightCell = (Cell)grid[rows - 1, cols + 1];
                                cellsToCheck = new object[] { rightCell, cornerTopRightCell, topCell };
                            }
                            else if (cols == grid.GetLength(1) - 1)
                            {
                                leftCell = (Cell)grid[rows, cols - 1];
                                cornerTopLeftCell = (Cell)grid[rows - 1, cols - 1];
                                cellsToCheck = new object[] { leftCell, cornerTopLeftCell, topCell };
                            }
                            else
                            {
                                leftCell = (Cell)grid[rows, cols - 1];
                                cornerTopLeftCell = (Cell)grid[rows - 1, cols - 1];
                                cornerTopRightCell = (Cell)grid[rows - 1, cols + 1];
                                rightCell = (Cell)grid[rows, cols + 1];
                                cellsToCheck = new object[] { leftCell, cornerTopLeftCell, cornerTopRightCell, rightCell, topCell };
                            }
                        }
                        // in-between row
                        else
                        {
                            topCell = (Cell)grid[rows - 1, cols];
                            bottomCell = (Cell)grid[rows + 1, cols];

                            if (cols == 0)
                            {
                                rightCell = (Cell)grid[rows, cols + 1];
                                cornerBotRightCell = (Cell)grid[rows + 1, cols + 1];
                                cornerTopRightCell = (Cell)grid[rows - 1, cols + 1];
                                cellsToCheck = new object[] { rightCell, cornerBotRightCell, cornerTopRightCell, topCell, bottomCell };

                            }
                            else if (cols == grid.GetLength(1) - 1)
                            {
                                leftCell = (Cell)grid[rows, cols - 1];
                                cornerBotLeftCell = (Cell)grid[rows + 1, cols - 1];
                                cornerTopLeftCell = (Cell)grid[rows - 1, cols - 1];
                                cellsToCheck = new object[] { leftCell, cornerBotLeftCell, cornerTopLeftCell, topCell, bottomCell };
                            }
                            else
                            {
                                leftCell = (Cell)grid[rows, cols - 1];
                                rightCell = (Cell)grid[rows, cols + 1];
                                cornerTopRightCell = (Cell)grid[rows - 1, cols + 1];
                                cornerTopLeftCell = (Cell)grid[rows - 1, cols - 1];
                                cornerBotLeftCell = (Cell)grid[rows + 1, cols - 1];
                                cornerBotRightCell = (Cell)grid[rows + 1, cols + 1];
                                cellsToCheck = new object[] { leftCell, rightCell,
                                                          cornerTopRightCell, cornerTopLeftCell,
                                                          cornerBotLeftCell, cornerBotRightCell,
                                                          topCell, bottomCell };

                            }
                        } 

                        //check for the result
                        for (int i = 0; i < cellsToCheck.Length; i++)
                        {
                            result += currentCell.CompareTo((Cell)cellsToCheck[i]);
                        }

                        //change the next state of the cell
                        if (currentCell.CurrentState == 0)
                        {
                            if (result == 3 ||
                                result == 6)
                            {
                                currentCell.NextState = 1;
                            }
                            else
                            {
                                currentCell.NextState = 0;
                            }
                        }
                        else
                        {
                            if (result == 0 ||
                                result == 1 ||
                                result == 4 ||
                                result == 5 ||
                                result == 7 ||
                                result == 8)
                            {
                                currentCell.NextState = 0;
                            }
                            else
                            {
                                currentCell.NextState = 1;
                            }
                        }

                        //check the state for changes
                        if (rows == desiredCellCoordinates[0] &&
                            cols == desiredCellCoordinates[1] &&
                            currentCell.CurrentState == 1)
                        {
                            changeOfStates++;
                        }
                    }
                }

            }

            Console.WriteLine(changeOfStates);
        }
    }
}
