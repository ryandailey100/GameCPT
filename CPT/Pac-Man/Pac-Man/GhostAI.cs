﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class GhostAI
    {
        //Initial Location
        int Row_Initial = 0;
        int Col_Initial = 0;

        //Location X Y
        int row_Position = 0;
        int col_Position = 0;

        GhostMovement Dir_Ghost = GhostMovement.Left;

        Ghost GhostType;

        char GhostSymbol = '.'; // the '.' is a placeholder
        

        public GhostAI(Ghost TypeGhost)
        {
            GhostType = TypeGhost;

            //Get char of the Ghost type
            if (GhostType == Ghost.Red)
                GhostSymbol = 'R';
            else if (GhostType == Ghost.Pink)
                GhostSymbol = 'P';
            else if (GhostType == Ghost.Green)
                GhostSymbol = 'G';
            else if (GhostType == Ghost.Orange)
                GhostSymbol = 'O';


            for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
            {
                for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                {
                    if (Gameplay.GridOfMap.GetMaze[r, c] == GhostSymbol)
                    {
                        //Store players position on grid
                        Row_Initial = r;
                        Col_Initial = c;

                        row_Position = r;
                        col_Position = c;

                        //Break both loops
                        r = Gameplay.GridOfMap.GetRows;
                        break;
                    }
                }
            }
        }

        public enum Ghost
        {
            Red,
            Pink,
            Green,
            Orange
        }

        private enum GhostMovement
        {
            Up,
            Down,
            Left,
            Right,
            Nil
        }

        public void UpdateAI()
        {
            //Check for death
            CheckForDeath();

            
            //If Ghost hits a intersection, then find path to pacman. Else, continue moving in direction (Dir_Ghost)
            if (((Dir_Ghost == GhostMovement.Up || Dir_Ghost == GhostMovement.Down) && (Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == '.' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == '.' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == ',' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == ',')) || ((Dir_Ghost == GhostMovement.Left || Dir_Ghost == GhostMovement.Right) && (Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == '.' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == '.' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == ',' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == ','))) //When up or down, check left and right. Or when left or right, check up and down. Then, find a new path to Pac-Man
            {
                //===Find Location Point===
                #region Find Point Location

                //Point location var
                int Point_row = 0;
                int Point_col = 0;

                if (GhostType == Ghost.Red)
                {
                    #region Red Ghost : //Point will be on Pac-Man

                    Point_row = Gameplay.PlayerClass.GetPlayerRow;
                    Point_col = Gameplay.PlayerClass.GetPlayerCol;

                    #endregion
                }
                else if (GhostType == Ghost.Pink)
                {
                    #region Pink Ghost : //Point will be 4 blocks ahead of Pac-Man

                    if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Up)
                    {
                        Point_row = Gameplay.PlayerClass.GetPlayerRow;
                        Point_col = Gameplay.PlayerClass.GetPlayerCol;

                        for (int i = 1; i <= 4; i++)
                        {
                            if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=')
                            {
                                Point_row = Gameplay.PlayerClass.GetPlayerRow - i;
                            }
                        }
                    }
                    else if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Down)
                    {
                        Point_row = Gameplay.PlayerClass.GetPlayerRow;
                        Point_col = Gameplay.PlayerClass.GetPlayerCol;

                        for (int i = 1; i <= 4; i++)
                        {
                            if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=')
                            {
                                Point_row = Gameplay.PlayerClass.GetPlayerRow + i;
                            }
                        }
                    }
                    else if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Left)
                    {
                        Point_row = Gameplay.PlayerClass.GetPlayerRow;
                        Point_col = Gameplay.PlayerClass.GetPlayerCol;

                        for (int i = 1; i <= 4; i++)
                        {
                            if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=')
                            {
                                Point_col = Gameplay.PlayerClass.GetPlayerCol - i;
                            }
                        }
                    }
                    else if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Right)
                    {
                        Point_row = Gameplay.PlayerClass.GetPlayerRow;
                        Point_col = Gameplay.PlayerClass.GetPlayerCol;

                        for (int i = 1; i <= 4; i++)
                        {
                            if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=')
                            {
                                Point_col = Gameplay.PlayerClass.GetPlayerCol + i;
                            }
                        }
                    }

                    #endregion
                }
                else if (GhostType == Ghost.Green)
                {
                    #region Green Ghost



                    #endregion
                }
                else if (GhostType == Ghost.Orange)
                {
                    #region Orange Ghost




                    #endregion
                }
                #endregion


                //Find Direction to Point (by finding a path)
                //Dir_Ghost = FindPath(Point_row, Point_col, row_Position, col_Position, GhostMovement.Nil, GhostMovement.Nil);
                Dir_Ghost = PathDir(Point_row, Point_col, row_Position, col_Position);
            }
            else //Dont find path, continue moving in direction (inertia)
            {

            }

            //Execute Movement
            #region Execute Movement
            
            //Update Maze according to the direction set
            if (Dir_Ghost == GhostMovement.Up)
            {
                //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = '.'; 
                
                //Move ghost to the new location
                row_Position--;
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = GhostSymbol; 
            }
            else if (Dir_Ghost == GhostMovement.Down)
            {
                //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = '.';

                //Move ghost to the new location
                row_Position++;
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = GhostSymbol;
            }
            else if (Dir_Ghost == GhostMovement.Left)
            {
                //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = '.';

                //Move ghost to the new location
                col_Position--;
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = GhostSymbol;
            }
            else if (Dir_Ghost == GhostMovement.Right)
            {
                //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = '.';

                //Move ghost to the new location
                col_Position++;
                Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = GhostSymbol;
            }
            #endregion


            //Check for death
            CheckForDeath();

        }

        private GhostMovement PathDir(int RowPoint, int ColPoint, int GhostRow, int GhostCol)
        {
            //char[,] MazeCopy = new char[Gameplay.GridOfMap.GetRows, Gameplay.GridOfMap.GetCols];
            //for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
            //{
            //    for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
            //    {
            //        MazeCopy[r, c] = Gameplay.GridOfMap.GetMaze[r, c];
            //    }
            //}

            //Going left or right and hits intersection with up or down being a option. Chooses better option
            if (Dir_Ghost != GhostMovement.Down && FindPath(GhostRow - 1, GhostCol, RowPoint, ColPoint, FreshMaze()) == true && Dir_Ghost != GhostMovement.Up && FindPath(GhostRow + 1, GhostCol, RowPoint, ColPoint, FreshMaze()) == true)
            {
                if (row_Position < RowPoint)
                    return GhostMovement.Down;
                else
                    return GhostMovement.Up;
            }
            else if (Dir_Ghost != GhostMovement.Right && FindPath(GhostRow, GhostCol - 1, RowPoint, ColPoint, FreshMaze()) == true && Dir_Ghost != GhostMovement.Left && FindPath(GhostRow, GhostCol + 1, RowPoint, ColPoint, FreshMaze()) == true)
            {
                if (col_Position < ColPoint)
                    return GhostMovement.Right;
                else
                    return GhostMovement.Left;
            }
            else
            {
                if (Dir_Ghost != GhostMovement.Down && FindPath(GhostRow - 1, GhostCol, RowPoint, ColPoint, FreshMaze()) == true) //Up
                {
                    return GhostMovement.Up;
                }
                else if (Dir_Ghost != GhostMovement.Up && FindPath(GhostRow + 1, GhostCol, RowPoint, ColPoint, FreshMaze()) == true) //Down
                {
                    return GhostMovement.Down;
                }
                else if (Dir_Ghost != GhostMovement.Right && FindPath(GhostRow, GhostCol - 1, RowPoint, ColPoint, FreshMaze()) == true) //Left
                {
                    return GhostMovement.Left;
                }
                else if (Dir_Ghost != GhostMovement.Left && FindPath(GhostRow, GhostCol + 1, RowPoint, ColPoint, FreshMaze()) == true) //Right
                {
                    return GhostMovement.Right;
                }
            }
            

            return GhostMovement.Nil;
        }

        private bool FindPath(int row, int col, int PointRow, int PointCol, char[,] MazeArray)
        {
            //Check if out of bounds
            if (row < 0 || row >= Gameplay.GridOfMap.GetRows || col < 0 || col >= Gameplay.GridOfMap.GetCols)
                return false;

            //If it found the point
            if (row == PointRow && col == PointCol)
                return true;

            //If it is not on the path
            if (MazeArray[row, col] != '.' && MazeArray[row, col] != ',' && MazeArray[row, col] != 'o' && MazeArray[row, col] != 'S')
                return false;

            MazeArray[row, col] = 'k';

            //Check all directions
            if (FindPath(row - 1, col, PointRow, PointCol, MazeArray) == true) //Up
                return true;
            if (FindPath(row, col - 1, PointRow, PointCol, MazeArray) == true) //Left
                return true;
            if (FindPath(row + 1, col, PointRow, PointCol, MazeArray) == true) //Down
                return true;
            if (FindPath(row, col + 1, PointRow, PointCol, MazeArray) == true) //Right
                return true;


            //Exit recursion if all else fails
            MazeArray[row, col] = 'j';
            return false;
        }

        private char[,] FreshMaze()
        {
            char[,] MazeCopy = new char[Gameplay.GridOfMap.GetRows, Gameplay.GridOfMap.GetCols];
            for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
            {
                for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                {
                    MazeCopy[r, c] = Gameplay.GridOfMap.GetMaze[r, c];
                }
            }

            return MazeCopy;
        }

        private void CheckForDeath()
        {
            if (row_Position == Gameplay.PlayerClass.GetPlayerRow && col_Position == Gameplay.PlayerClass.GetPlayerCol)
            {
                if (Gameplay.GridOfMap.GetScore <= 0)
                {
                    //Game Over
                    Gameplay.GameOver();
                }
                else
                {
                    Gameplay.GridOfMap.SubtractLife();

                    //Spawn everything back to normal
                    for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                    {
                        for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                        {
                            Gameplay.GridOfMap.GetMaze[r, c] = Gameplay.GridOfMap.GetOriginalMaze[r, c];
                        }
                    }

                    //reset all ghosts and player classes
                    Gameplay.ResetObjects();
                }
                

            }
        }


    }
}
