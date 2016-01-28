using System;
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

        bool InCage = true; //Starts in ghost cage
        public int TimeInCage;
        GhostMovement Prev_Dir = GhostMovement.Left;

        GhostMovement Dir_Ghost = GhostMovement.Left;

        Ghost GhostType;

        char GhostSymbol = '.'; // the '.' is a placeholder
        
        public int GetInitialRow
        {
            get { return Row_Initial; }
        }

        public int GetInitialCol
        {
            get { return Col_Initial; }
        }

        public GhostAI(Ghost TypeGhost)
        {
            GhostType = TypeGhost;

            //Get char of the Ghost type
            if (GhostType == Ghost.Red)
            {
                GhostSymbol = 'R';
                TimeInCage = 0; //Comes out right away //0 frames
            }
            else if (GhostType == Ghost.Pink)
            {
                GhostSymbol = 'P';
                TimeInCage = 25; //Comes out after around 4 seconds (1000 * 150 / 4), since the timer runs at 150ms) //25 frames
            }
            else if (GhostType == Ghost.Green)
            {
                GhostSymbol = 'G';
                TimeInCage = 50; //Comes out after around 4 seconds (1000 * 150 / 4), since the timer runs at 150ms) //50 frames
            }
            else if (GhostType == Ghost.Orange)
            {
                GhostSymbol = 'O';
                TimeInCage = 80; //Comes out after around 4 seconds (1000 * 150 / 4), since the timer runs at 150ms) //80 frames
            }
                

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
            if (InCage == true) //In Ghost house/cage
            {
                #region InCage Checks
                if (TimeInCage == 0)
                {
                    //Remove old Ghost in Cage
                    Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = '=';

                    //Find a way out of cage
                    row_Position = OutOfCage(row_Position, col_Position, true); //true returns the row
                    col_Position = OutOfCage(row_Position, col_Position, false); //false returns the col

                    //Set InCage to false
                    InCage = false;
                }
                else
                {
                    TimeInCage--;
                }
                #endregion
            }
            else
            {
                if (CheckForCollision() == true) return; //Exit Function

                //If ghost is eatable (runs away from you)
                if (Gameplay.GridOfMap.GetPowerUp == true && ((GhostType == Ghost.Red && Gameplay.GridOfMap.GetRedVul == true) || (GhostType == Ghost.Pink && Gameplay.GridOfMap.GetPinkVul == true) || (GhostType == Ghost.Green && Gameplay.GridOfMap.GetGreenVul == true) || (GhostType == Ghost.Orange && Gameplay.GridOfMap.GetOrangeVul == true))) //Run away from pacman
                {
                    #region Ghost Is Eatable
                    //Sets Prev_Dir
                    if (Dir_Ghost != GhostMovement.Nil)
                        Prev_Dir = Dir_Ghost;

                    if (Gameplay.GridOfMap.GetPowerTimer % 2 == 0) //Happens only every even number (every other frame). This slows down the ghost
                    {
                        //When ghosts hits an intersection
                        if (((Prev_Dir == GhostMovement.Up || Prev_Dir == GhostMovement.Down) && (Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == '.' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == '.' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == ',' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == ',' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'O' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'O')) || ((Prev_Dir == GhostMovement.Left || Prev_Dir == GhostMovement.Right) && (Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == '.' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == '.' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == ',' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == ',' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'O' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'O'))) //When up or down, check left and right. Or when left or right, check up and down. Then, find a new path to Pac-Man
                        {
                            if (Prev_Dir != GhostMovement.Down && (Gameplay.GridOfMap.GetOriginalMaze[row_Position - 1, col_Position] == '.' || Gameplay.GridOfMap.GetOriginalMaze[row_Position - 1, col_Position] == ',' || Gameplay.GridOfMap.GetOriginalMaze[row_Position - 1, col_Position] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[row_Position - 1, col_Position] == 'S'))
                                Dir_Ghost = GhostMovement.Up;
                            else if (Prev_Dir != GhostMovement.Up && (Gameplay.GridOfMap.GetOriginalMaze[row_Position + 1, col_Position] == '.' || Gameplay.GridOfMap.GetOriginalMaze[row_Position + 1, col_Position] == ',' || Gameplay.GridOfMap.GetOriginalMaze[row_Position + 1, col_Position] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[row_Position + 1, col_Position] == 'S'))
                                Dir_Ghost = GhostMovement.Down;
                            else if (Prev_Dir != GhostMovement.Right && (Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position - 1] == '.' || Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position - 1] == ',' || Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position - 1] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position - 1] == 'S'))
                                Dir_Ghost = GhostMovement.Left;
                            else if (Prev_Dir != GhostMovement.Left && (Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position + 1] == '.' || Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position + 1] == ',' || Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position + 1] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[row_Position, col_Position + 1] == 'S'))
                                Dir_Ghost = GhostMovement.Right;
                        }
                        else
                        {
                            Dir_Ghost = Prev_Dir;
                        }
                    }
                    else
                    {
                        Dir_Ghost = GhostMovement.Nil;
                    }
                    #endregion
                }
                else  //Regular Movements
                {
                    //Find Direction (if needed)
                    #region Find Direction
                    //If Ghost hits a intersection, then find path to pacman. Else, continue moving in direction (Dir_Ghost)
                    if (((Dir_Ghost == GhostMovement.Up || Dir_Ghost == GhostMovement.Down) && (Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == '.' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == '.' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == ',' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == ',' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == 'O' || Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == 'O')) || ((Dir_Ghost == GhostMovement.Left || Dir_Ghost == GhostMovement.Right) && (Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == '.' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == '.' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == ',' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == ',' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'o' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'S' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'R' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'P' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'G' || Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == 'O' || Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == 'O'))) //When up or down, check left and right. Or when left or right, check up and down. Then, find a new path to Pac-Man
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
                                    if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '&')
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
                                    if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '&')
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
                                    if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '&')
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
                                    if (Point_row >= 0 && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '#' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '=' && Gameplay.GridOfMap.GetMaze[Point_row, Point_col] != '&')
                                    {
                                        Point_col = Gameplay.PlayerClass.GetPlayerCol + i;
                                    }
                                }
                            }

                            #endregion
                        }
                        else if (GhostType == Ghost.Green)
                        {
                            #region Green Ghost : //To complex to explain, search on google

                            //rise over run
                            //rise = y2-y1
                            //run = x2-x1
                            //Point_row = 2 * rise
                            //Point_col = 2 * run

                            if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Up)
                            {
                                Point_row = Gameplay.GridOfMap.GetRedGhostrow + 2 * (Gameplay.GridOfMap.GetRedGhostrow - 2 - Gameplay.PlayerClass.GetPlayerRow);
                                Point_col = Gameplay.GridOfMap.GetRedGhostcol + 2 * (Gameplay.GridOfMap.GetRedGhostcol - Gameplay.PlayerClass.GetPlayerCol);

                                //Check for out of bounds
                                if (Point_row <= 0)
                                    Point_row = 1;
                                else if (Point_row > Gameplay.GridOfMap.GetRows)
                                    Point_row = Gameplay.GridOfMap.GetRows;

                                if (Point_col <= 0)
                                    Point_col = 1;
                                else if (Point_col > Gameplay.GridOfMap.GetCols)
                                    Point_col = Gameplay.GridOfMap.GetCols;

                            }
                            else if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Down)
                            {
                                Point_row = Gameplay.GridOfMap.GetRedGhostrow + 2 * (Gameplay.GridOfMap.GetRedGhostrow + 2 - Gameplay.PlayerClass.GetPlayerRow);
                                Point_col = Gameplay.GridOfMap.GetRedGhostcol + 2 * (Gameplay.GridOfMap.GetRedGhostcol - Gameplay.PlayerClass.GetPlayerCol);

                                //Check for out of bounds
                                if (Point_row <= 0)
                                    Point_row = 1;
                                else if (Point_row > Gameplay.GridOfMap.GetRows)
                                    Point_row = Gameplay.GridOfMap.GetRows;

                                if (Point_col <= 0)
                                    Point_col = 1;
                                else if (Point_col > Gameplay.GridOfMap.GetCols)
                                    Point_col = Gameplay.GridOfMap.GetCols;

                            }
                            else if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Left)
                            {
                                Point_row = Gameplay.GridOfMap.GetRedGhostrow + 2 * (Gameplay.GridOfMap.GetRedGhostrow - Gameplay.PlayerClass.GetPlayerRow);
                                Point_col = Gameplay.GridOfMap.GetRedGhostcol + 2 * (Gameplay.GridOfMap.GetRedGhostcol - 2 - Gameplay.PlayerClass.GetPlayerCol);

                                //Check for out of bounds
                                if (Point_row <= 0)
                                    Point_row = 1;
                                else if (Point_row > Gameplay.GridOfMap.GetRows)
                                    Point_row = Gameplay.GridOfMap.GetRows;

                                if (Point_col <= 0)
                                    Point_col = 1;
                                else if (Point_col > Gameplay.GridOfMap.GetCols)
                                    Point_col = Gameplay.GridOfMap.GetCols;

                            }
                            else if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Right)
                            {
                                Point_row = Gameplay.GridOfMap.GetRedGhostrow + 2 * (Gameplay.GridOfMap.GetRedGhostrow - Gameplay.PlayerClass.GetPlayerRow);
                                Point_col = Gameplay.GridOfMap.GetRedGhostcol + 2 * (Gameplay.GridOfMap.GetRedGhostcol + 2 - Gameplay.PlayerClass.GetPlayerCol);

                                //Check for out of bounds
                                if (Point_row <= 0)
                                    Point_row = 1;
                                else if (Point_row > Gameplay.GridOfMap.GetRows)
                                    Point_row = Gameplay.GridOfMap.GetRows;

                                if (Point_col <= 0)
                                    Point_col = 1;
                                else if (Point_col > Gameplay.GridOfMap.GetCols)
                                    Point_col = Gameplay.GridOfMap.GetCols;

                            }
                            

                            #endregion
                        }
                        else if (GhostType == Ghost.Orange)
                        {
                            #region Orange Ghost : //Target is the bottom left of the map when in a 8 block radius from Pac-Man

                            if (Math.Abs(Math.Sqrt(Math.Pow(row_Position - Gameplay.PlayerClass.GetPlayerRow, 2) + Math.Pow(col_Position - Gameplay.PlayerClass.GetPlayerCol, 2))) <= 8) //Point to Pac-Man
                            {
                                Point_row = Gameplay.PlayerClass.GetPlayerRow;
                                Point_col = Gameplay.PlayerClass.GetPlayerCol;
                            }
                            else //Point to bottom left of the map
                            {
                                //Find block in the path that is closest to the bottom left corner
                                for (int r = Gameplay.GridOfMap.GetRows - 1; r > 0; r--)
                                {
                                    for (int c = 0; c < Gameplay.GridOfMap.GetCols / 4; c++) //First quarter of the cols
                                    {
                                        if (Gameplay.GridOfMap.GetOriginalMaze[r, c] == '.' || Gameplay.GridOfMap.GetOriginalMaze[r, c] == ',')
                                        {
                                            Point_row = r;
                                            Point_col = c;

                                            //Break loop
                                            r = 0;
                                            break;
                                        }

                                    }

                                }

                                      
                            }


                            #endregion
                        }
                        #endregion


                        //Find Direction to Point (by finding a path)
                        //Dir_Ghost = FindPath(Point_row, Point_col, row_Position, col_Position, GhostMovement.Nil, GhostMovement.Nil);
                        Dir_Ghost = PathDir(Point_row, Point_col, row_Position, col_Position);

                    }
                    #endregion
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


                //Teleportation Check
                #region Teleport Check
                if (row_Position > 0 && Gameplay.GridOfMap.GetMaze[row_Position - 1, col_Position] == '&') //Up
                {
                    Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = ',';
                    //Find Other teleport symbol
                    for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                    {
                        for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                        {
                            if (r != row_Position - 1 || c != col_Position)
                            {
                                if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                {
                                    Gameplay.GridOfMap.GetMaze[r - 2, c] = GhostSymbol;
                                    row_Position = r - 2;

                                    //break loops
                                    r = Gameplay.GridOfMap.GetRows;
                                    break;
                                }

                            }
                        }
                    }
                }
                else if (row_Position < Gameplay.GridOfMap.GetRows - 1 && Gameplay.GridOfMap.GetMaze[row_Position + 1, col_Position] == '&') //Down
                {
                    Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = ',';
                    //Find Other teleport symbol
                    for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                    {
                        for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                        {
                            if (r != row_Position + 1 || c != col_Position)
                            {
                                if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                {
                                    Gameplay.GridOfMap.GetMaze[r + 2, c] = GhostSymbol;
                                    row_Position = r + 2;

                                    //break loops
                                    r = Gameplay.GridOfMap.GetRows;
                                    break;
                                }

                            }
                        }
                    }
                }
                else if (col_Position > 0 && Gameplay.GridOfMap.GetMaze[row_Position, col_Position - 1] == '&') //Left
                {
                    Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = ',';
                    //Find Other teleport symbol
                    for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                    {
                        for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                        {
                            if (r != row_Position || c != col_Position - 1)
                            {
                                if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                {
                                    Gameplay.GridOfMap.GetMaze[r, c - 2] = GhostSymbol;
                                    col_Position = c - 2;

                                    //break loops
                                    r = Gameplay.GridOfMap.GetRows;
                                    break;
                                }

                            }
                        }
                    }
                }
                else if (col_Position < Gameplay.GridOfMap.GetCols - 1 && Gameplay.GridOfMap.GetMaze[row_Position, col_Position + 1] == '&') //Down
                {
                    Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = ',';
                    //Find Other teleport symbol
                    for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                    {
                        for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                        {
                            if (r != row_Position || c != col_Position + 1)
                            {
                                if (Gameplay.GridOfMap.GetMaze[r, c] == '&')
                                {
                                    Gameplay.GridOfMap.GetMaze[r, c + 2] = GhostSymbol;
                                    col_Position = c + 2;

                                    //break loops
                                    r = Gameplay.GridOfMap.GetRows;
                                    break;
                                }

                            }
                        }
                    }
                }

                #endregion


                //Check for death
                CheckForCollision();
            }

            if (GhostType == Ghost.Red)
            {
                Gameplay.GridOfMap.GetRedGhostrow = row_Position;
                Gameplay.GridOfMap.GetRedGhostcol = col_Position;
            }
           

        }

        private GhostMovement PathDir(int RowPoint, int ColPoint, int GhostRow, int GhostCol)
        {
            //Going left or right and hits intersection with up or down being a option. Chooses better option 
            //(Dir_Ghost == GhostMovement.Left && FindPath(GhostRow, GhostCol, RowPoint - 1, ColPoint, FreshMaze()) == false || Dir_Ghost == GhostMovement.Right && FindPath(GhostRow, GhostCol, RowPoint + 1, ColPoint, FreshMaze()) == false) &&
            if ((Dir_Ghost != GhostMovement.Down && FindPath(GhostRow - 1, GhostCol, RowPoint, ColPoint, FreshMaze()) == true && Dir_Ghost != GhostMovement.Up && FindPath(GhostRow + 1, GhostCol, RowPoint, ColPoint, FreshMaze()) == true))
            {
                if (row_Position < RowPoint) 
                    return GhostMovement.Down;
                else if (row_Position > RowPoint)
                    return GhostMovement.Up;
                else
                {
                    if ((col_Position < ColPoint && Dir_Ghost == GhostMovement.Down) || (col_Position > ColPoint && Dir_Ghost == GhostMovement.Up))
                    {
                        return Dir_Ghost;
                    }
                    else
                    {
                        return GhostMovement.Up;
                    }
                    
                }
                    
            }
            else if ((Dir_Ghost != GhostMovement.Right && FindPath(GhostRow, GhostCol - 1, RowPoint, ColPoint, FreshMaze()) == true && Dir_Ghost != GhostMovement.Left && FindPath(GhostRow, GhostCol + 1, RowPoint, ColPoint, FreshMaze()) == true))
            {
                if (col_Position < ColPoint)
                    return GhostMovement.Right;
                else if (col_Position > ColPoint)
                    return GhostMovement.Left;
                else
                {
                    if ((row_Position < RowPoint && Dir_Ghost == GhostMovement.Right) || (row_Position > RowPoint && Dir_Ghost == GhostMovement.Right))
                    {
                        return Dir_Ghost;
                    }
                    else
                    {
                        //return GhostMovement.Left;
                    }
                }
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

            //Last resort (If Path Cannot be found)
            if (Dir_Ghost != GhostMovement.Down && (Gameplay.GridOfMap.GetOriginalMaze[GhostRow - 1, GhostCol] == '.' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow - 1, GhostCol] == ',' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow - 1, GhostCol] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow - 1, GhostCol] == 'S'))
                return GhostMovement.Up;
            else if(Dir_Ghost != GhostMovement.Up && (Gameplay.GridOfMap.GetOriginalMaze[GhostRow + 1, GhostCol] == '.' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow + 1, GhostCol] == ',' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow + 1, GhostCol] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow + 1, GhostCol] == 'S'))
                return GhostMovement.Down;
            else if (Dir_Ghost != GhostMovement.Right && (Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol - 1] == '.' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol - 1] == ',' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol - 1] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol - 1] == 'S'))
                return GhostMovement.Left;
            else if (Dir_Ghost != GhostMovement.Left && (Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol + 1] == '.' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol + 1] == ',' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol + 1] == 'o' || Gameplay.GridOfMap.GetOriginalMaze[GhostRow, GhostCol + 1] == 'S'))
                return GhostMovement.Right;

            //Extreme Last resort (if trapped in a corner or something terrible like that, reverse directions)
            if (Dir_Ghost == GhostMovement.Up)
                Dir_Ghost = GhostMovement.Down;
            else if(Dir_Ghost == GhostMovement.Down)
                Dir_Ghost = GhostMovement.Up;
            else if (Dir_Ghost == GhostMovement.Left)
                Dir_Ghost = GhostMovement.Right;
            else if (Dir_Ghost == GhostMovement.Right)
                Dir_Ghost = GhostMovement.Left;

            return Dir_Ghost;
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
            if (MazeArray[row, col] != '.' && MazeArray[row, col] != ',' && MazeArray[row, col] != 'o' && MazeArray[row, col] != 'R' && MazeArray[row, col] != 'P' && MazeArray[row, col] != 'G' && MazeArray[row, col] != 'O')
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
            //MazeArray[row, col] = 'j';
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

        private bool CheckForCollision()
        {
            
            if (row_Position == Gameplay.PlayerClass.GetPlayerRow && col_Position == Gameplay.PlayerClass.GetPlayerCol)
            {
                if (Gameplay.GridOfMap.GetPowerUp == true && (GhostType == Ghost.Red && Gameplay.GridOfMap.GetRedVul == true || (GhostType == Ghost.Pink && Gameplay.GridOfMap.GetPinkVul == true) || (GhostType == Ghost.Green && Gameplay.GridOfMap.GetGreenVul == true) || (GhostType == Ghost.Orange && Gameplay.GridOfMap.GetOrangeVul == true))) //Run away from pacman
                {
                    //Pac-Man eats ghost
                    Gameplay.GridOfMap.GetScore += 200;
                    Gameplay.GridOfMap.GetMaze[row_Position, col_Position] = '.'; ;
                    //Reset Ghost
                    if (GhostType == Ghost.Red)
                        Gameplay.RedGhostDeath();
                    else if (GhostType == Ghost.Pink)
                        Gameplay.PinkGhostDeath();
                    else if (GhostType == Ghost.Green)
                        Gameplay.GreenGhostDeath();
                    else if (GhostType == Ghost.Orange)
                        Gameplay.OrangeGhostDeath();

                    return true;
                }
                else
                {
                    //Ghost eats Pac-Man
                    if (Gameplay.GridOfMap.GetLives <= 0)
                    {
                        //Game Over
                        Gameplay.GridOfMap.GetGameOver = true;
                        
                        return true;
                    }
                    else
                    {
                        Gameplay.GridOfMap.GetDeath = true;

                        Gameplay.GridOfMap.SubtractLife();

                        Gameplay.GridOfMap.GetPowerUp = false;
                        Gameplay.GridOfMap.GetPowerTimer = 55;
                        Gameplay.GridOfMap.GetRedVul = true;
                        Gameplay.GridOfMap.GetPinkVul = true;
                        Gameplay.GridOfMap.GetGreenVul = true;
                        Gameplay.GridOfMap.GetOrangeVul = true;

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
                    return true;
                }
                
            }
            return false;
        }

        private int OutOfCage(int r, int c, bool RowOrCol) //true returns the Row, false returns the col
        {

            if (RowOrCol == true) //Return Row
            {
                if (r < 0 || r >= Gameplay.GridOfMap.GetRows)
                    return 0;

                if (Gameplay.GridOfMap.GetMaze[r, c] != '=' && Gameplay.GridOfMap.GetMaze[r, c] != '#' && Gameplay.GridOfMap.GetMaze[r, c] != 'R' && Gameplay.GridOfMap.GetMaze[r, c] != 'P' && Gameplay.GridOfMap.GetMaze[r, c] != 'G' && Gameplay.GridOfMap.GetMaze[r, c] != 'O')
                    return r;
                else
                {
                    if (OutOfCage(r - 1, c, true) != 0) //Up
                        return OutOfCage(r - 1, c, true);
                    if (OutOfCage(r + 1, c, true) != 0) //Down
                        return OutOfCage(r - 1, c, true);
                    if (OutOfCage(r, c - 1, true) != 0) //Left
                        return OutOfCage(r - 1, c, true);
                    if (OutOfCage(r, c + 1, true) != 0) //Right
                        return OutOfCage(r - 1, c, true);


                    return 0;
                }
               
            }
            else //Return Col
            {
                if (c < 0 || c >= Gameplay.GridOfMap.GetCols)
                    return 0;

                if (Gameplay.GridOfMap.GetMaze[r, c] != '=' && Gameplay.GridOfMap.GetMaze[r, c] != '#' && Gameplay.GridOfMap.GetMaze[r, c] != 'R' && Gameplay.GridOfMap.GetMaze[r, c] != 'P' && Gameplay.GridOfMap.GetMaze[r, c] != 'G' && Gameplay.GridOfMap.GetMaze[r, c] != 'O')
                    return c;
                else
                {
                    if (OutOfCage(r - 1, c, true) != 0) //Up
                        return OutOfCage(r - 1, c, true);
                    if (OutOfCage(r + 1, c, true) != 0) //Down
                        return OutOfCage(r - 1, c, true);
                    if (OutOfCage(r, c - 1, true) != 0) //Left
                        return OutOfCage(r - 1, c, true);
                    if (OutOfCage(r, c + 1, true) != 0) //Right
                        return OutOfCage(r - 1, c, true);


                    return 0;
                }
            }
            
        }


    }
}
