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

        GhostMovement Dir_Ghost = GhostMovement.Up;

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
            Right
        }

        public void UpdateAI(Ghost GhostType)
        {
            //If Ghost hits a intersection, then find path to pacman. Else, continue moving in direction (Dir_Ghost)
            if (Gameplay.GridOfMap.GetMaze[row_Position, col_Position] == '+') //Change it so it checks all directions first
            {

            }
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
                    
                    for (int i = 1; i <= 4 ; i++)
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
            Dir_Ghost = FindPath(Point_row, Point_col);







            //Execute Movement
            #region Execute Movement

            //Get char of the Ghost type
            char GhostSymbol = '.'; //A random char, placeholder
            if (GhostType == Ghost.Red)
                GhostSymbol = 'R';
            else if (GhostType == Ghost.Pink)
                GhostSymbol = 'P';
            else if (GhostType == Ghost.Green)
                GhostSymbol = 'G';
            else if (GhostType == Ghost.Orange)
                GhostSymbol = 'O';

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
            
        }

       
        private GhostMovement FindPath(int row, int col)
        {


            
            return GhostMovement.Up;
        }
        


    }
}
