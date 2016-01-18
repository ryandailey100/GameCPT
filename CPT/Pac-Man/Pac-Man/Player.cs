using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridMap;

namespace Pac_Man
{
    class Player
    {
        //Grid GridOfMap;
        

        private int Score = 0;
        private int Lives = 0;

        private int row_Initial = 0;
        private int col_Initial = 0;

        //Current Location
        int PlayerRow = 0;
        int PlayerCol = 0;

        bool UpSpace = false;
        bool DownSpace = false;
        bool LeftSpace = false;
        bool RightSpace = false;

        PlayerMovement PlayerDir = PlayerMovement.Left; //Initally starts going left

        public PlayerMovement GetPlayerDir
        {
            get { return PlayerDir; }
            set { PlayerDir = value; }
        }

        public enum PlayerMovement
        {
            Up,
            Down,
            Left,
            Right
        }

        public bool GetUpSpace
        {
            get { return UpSpace; }
        }

        public bool GetDownSpace
        {
            get { return DownSpace; }
        }

        public bool GetLeftSpace
        {
            get { return LeftSpace; }
        }

        public bool GetRightSpace
        {
            get { return RightSpace; }
        }



        public int GetScore
        {
            get { return Score; }
        }

        public int GetLives
        {
            get { return Lives; }
        }

        public void AddLife()
        {
            Lives++;
        }

        public void SubtractLife()
        {
            Lives--;
        }


        public void UpdatePlayer()
        {
            //---Movement & Score---
            #region Movement and Score
            for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
            {
                for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                {
                    if (Gameplay.GridOfMap.GetMaze[r, c] == 'S')
                    {
                        //Store players position on grid
                        PlayerRow = r;
                        PlayerCol = c;

                        //Break both loops
                        r = Gameplay.GridOfMap.GetRows;
                        break;
                    }
                }
            }



            if (PlayerDir == PlayerMovement.Up && UpSpace == true)
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[PlayerRow - 1, PlayerCol] = 'S'; //Move your pacman to the new location

                //---Add Score Increment Here---
                if (Gameplay.GridOfMap.GetDots[PlayerRow - 1, PlayerCol] == '*')
                {
                    Score += 10; //Adds 10
                }
                else if (Gameplay.GridOfMap.GetDots[PlayerRow - 1, PlayerCol] == 'o')
                {
                    Score += 50; //Adds 50
                }

                Gameplay.GridOfMap.GetDots[PlayerRow - 1, PlayerCol] = ','; //Eat a dot
                PlayerRow--;
            }
            else if (PlayerDir == PlayerMovement.Down && DownSpace == true)
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[PlayerRow + 1, PlayerCol] = 'S'; //Move your pacman to the new location


                //---Add Score Increment Here---
                //---Add Score Increment Here---
                if (Gameplay.GridOfMap.GetDots[PlayerRow + 1, PlayerCol] == '*')
                {
                    Score += 10; //Adds 10
                }
                else if (Gameplay.GridOfMap.GetDots[PlayerRow + 1, PlayerCol] == 'o')
                {
                    Score += 50; //Adds 50
                }


                Gameplay.GridOfMap.GetDots[PlayerRow + 1, PlayerCol] = ','; //Eat a dot
                PlayerRow++;
            }
            else if (PlayerDir == PlayerMovement.Left && LeftSpace == true)
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol - 1] = 'S'; //Move your pacman to the new location


                //---Add Score Increment Here---
                //---Add Score Increment Here---
                if (Gameplay.GridOfMap.GetDots[PlayerRow, PlayerCol - 1] == '*')
                {
                    Score += 10; //Adds 10
                }
                else if (Gameplay.GridOfMap.GetDots[PlayerRow, PlayerCol - 1] == 'o')
                {
                    Score += 50; //Adds 50
                }

                Gameplay.GridOfMap.GetDots[PlayerRow, PlayerCol - 1] = ','; //Eat a dot
                PlayerCol--;

            }
            else if (PlayerDir == PlayerMovement.Right && RightSpace == true)
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol + 1] = 'S'; //Move your pacman to the new location


                //---Add Score Increment Here---
                if (Gameplay.GridOfMap.GetDots[PlayerRow, PlayerCol + 1] == '*')
                {
                    Score += 10; //Adds 10
                }
                else if (Gameplay.GridOfMap.GetDots[PlayerRow, PlayerCol + 1] == 'o')
                {
                    Score += 50; //Adds 50
                }



                Gameplay.GridOfMap.GetDots[PlayerRow, PlayerCol + 1] = ','; //Eat a dot
                PlayerCol++;
            }
            #endregion
            
            //---Check Directions---
            #region Check Directions
            //Up 
            if (Gameplay.GridOfMap.GetMaze[PlayerRow - 1, PlayerCol] != '&')
            {
                if (Gameplay.GridOfMap.GetMaze[PlayerRow - 1, PlayerCol] != '#' && Gameplay.GridOfMap.GetMaze[PlayerRow - 1, PlayerCol] != '=')
                    UpSpace = true;
                else
                    UpSpace = false;
            }
            else
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = ',';
                //Find Other teleport symbol
                for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                {
                    for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                    {
                        if (r != PlayerRow - 1 || c != PlayerCol)
                        {
                            if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                Gameplay.GridOfMap.GetMaze[r - 2, c] = 'S';
                        }
                    }
                }
            }


            //Down  
            if (Gameplay.GridOfMap.GetMaze[PlayerRow + 1, PlayerCol] != '&')
            {
                if (Gameplay.GridOfMap.GetMaze[PlayerRow + 1, PlayerCol] != '#' && Gameplay.GridOfMap.GetMaze[PlayerRow + 1, PlayerCol] != '=')
                    DownSpace = true;
                else
                    DownSpace = false;
            }
            else
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = ',';
                //Find Other teleport symbol
                for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                {
                    for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                    {
                        if (r != PlayerRow + 1 || c != PlayerCol)
                        {
                            if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                Gameplay.GridOfMap.GetMaze[r + 2, c] = 'S';
                        }
                    }
                }
            }
            

            //Left
            if (Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol - 1] != '&')
            {
                if (Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol - 1] != '#' && Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol - 1] != '=')
                    LeftSpace = true;
                else
                    LeftSpace = false;
            }
            else
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = ',';
                //Find Other teleport symbol
                for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                {
                    for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                    {
                        if (r != PlayerRow || c != PlayerCol - 1)
                        {
                            if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                Gameplay.GridOfMap.GetMaze[r, c - 2] = 'S';
                        }
                    }
                }
            }

            //Right
            if (Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol + 1] != '&')
            {
                if (Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol + 1] != '#' && Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol + 1] != '=')
                    RightSpace = true;
                else
                    RightSpace = false;
            }
            else
            {
                Gameplay.GridOfMap.GetMaze[PlayerRow, PlayerCol] = ',';
                //Find Other teleport symbol
                for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                {
                    for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                    {
                        if (r != PlayerRow || c != PlayerCol + 1)
                        {
                            if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                Gameplay.GridOfMap.GetMaze[r, c + 2] = 'S';
                        }
                    }
                }
            }

            #endregion

        }



    }
}
