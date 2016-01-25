using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GridMap
{
    public class Grid
    {

        //fields
        private Cell[,] mGrid;
        private int mRows, mColumns;
        private int mCellSize;

        char[,] Maze;
        char[,] Dots;
        char[,] OriginalMaze;

        private int rows = 0;
        private int cols = 0;

        private int Score = 0;
        private int Lives = 3;

        //Special Dot Power Up
        bool PowerUp = false;
        int PowerTimer = 0;

        //Ghosts Power up vulnerability
        bool RedVulnerable = true;
        bool PinkVulnerable = true;
        bool GreenVulnerable = true;
        bool OrangeVulnerable = true;

        //Red Ghost Position (for the green ghost)
        int RedGhostRow = 0;
        int RedGhostCol = 0;

        bool GameOver = false; //if its true, game ends

        
        public bool GetGameOver
        {
            get { return GameOver; }
            set { GameOver = value; }
        }

        public int GetRedGhostrow
        {
            get { return RedGhostRow; }
            set { RedGhostRow = value; }
        }

        public int GetRedGhostcol
        {
            get { return RedGhostCol; }
            set { RedGhostCol = value; }
        }

        public bool GetRedVul
        {
            set { RedVulnerable = value; }
            get { return RedVulnerable; }
        }

        public bool GetPinkVul
        {
            set { PinkVulnerable = value; }
            get { return PinkVulnerable; }
        }

        public bool GetGreenVul
        {
            set { GreenVulnerable = value; }
            get { return GreenVulnerable; }
        }

        public bool GetOrangeVul
        {
            set { OrangeVulnerable = value; }
            get { return OrangeVulnerable; }
        }

        public bool GetPowerUp
        {
            get { return PowerUp; }
            set { PowerUp = value; }
        }

        public int GetPowerTimer
        {
            get { return PowerTimer; }
            set { PowerTimer = value; }
        }

        public int GetScore
        {
            get { return Score; }
            set { Score = value; }
        }

        public int GetLives
        {
            get { return Lives; }
            set { Lives = value; }
        }

        public void AddLife()
        {
            Lives++;
        }

        public void SubtractLife()
        {
            Lives--;
        }

        public char[,] GetMaze
        {
            get { return Maze; }
            set { Maze = value; }
        }

        public char[,] GetDots
        {
            set { Dots = value; }
            get { return Dots; }
        }

        public char[,] GetOriginalMaze
        {
            get { return OriginalMaze; }
            set { OriginalMaze = value; }
        }

        public int GetRows
        {
            get { return rows; }
            set { rows = value; }
        }

        public int GetCols
        {
            get { return cols; }
            set { cols = value; }
        }

        public void PaintGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    #region Map
                    //change colour of cell depending on what is in it
                    if (Maze[r, c] == '#')
                        GetCell(r, c).BackgroundColor = Color.Navy;
                    else if (Maze[r, c] == '.')
                        GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == ',')
                        GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == '&')
                        GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == 'o')
                        GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == '=')
                        GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == 'S')
                        GetCell(r, c).BackgroundColor = Color.Yellow;
                    else if (Maze[r, c] == 'R')
                    {
                        #region Red
                        if (PowerUp == true && RedVulnerable == true)
                        {
                            if (PowerTimer < 15) //Start to blink blue and white (after 6 seconds passed, or down to the last 15 frames(approx last 2 seconds))
                            {
                                if (PowerTimer % 2 == 0)
                                    GetCell(r, c).BackgroundColor = Color.Blue;
                                else
                                    GetCell(r, c).BackgroundColor = Color.White;
                            }
                            else
                            {
                                GetCell(r, c).BackgroundColor = Color.Blue;
                            }
                        }
                        else
                        {
                            GetCell(r, c).BackgroundColor = Color.Red;
                        }
                        #endregion
                    }
                    else if (Maze[r, c] == 'P')
                    {
                        #region Pink
                        if (PowerUp == true && PinkVulnerable == true)
                        {
                            if (PowerTimer < 15) //Start to blink blue and white (after 6 seconds passed, or down to the last 15 frames(approx last 2 seconds))
                            {
                                if (PowerTimer % 2 == 0)
                                    GetCell(r, c).BackgroundColor = Color.Blue;
                                else
                                    GetCell(r, c).BackgroundColor = Color.White;
                            }
                            else
                            {
                                GetCell(r, c).BackgroundColor = Color.Blue;
                            }
                        }
                        else
                        {
                            GetCell(r, c).BackgroundColor = Color.Pink;
                        }
                        #endregion
                    }
                    else if (Maze[r, c] == 'G')
                    {
                        #region Green
                        if (PowerUp == true && GreenVulnerable == true)
                        {
                            if (PowerTimer < 15) //Start to blink blue and white (after 6 seconds passed, or down to the last 15 frames(approx last 2 seconds))
                            {
                                if (PowerTimer % 2 == 0)
                                    GetCell(r, c).BackgroundColor = Color.Blue;
                                else
                                    GetCell(r, c).BackgroundColor = Color.White;
                            }
                            else
                            {
                                GetCell(r, c).BackgroundColor = Color.Blue;
                            }
                        }
                        else
                        {
                            GetCell(r, c).BackgroundColor = Color.Cyan;
                        }
                        #endregion
                    }
                    else if (Maze[r, c] == 'O')
                    {
                        #region Orange
                        if (PowerUp == true && OrangeVulnerable == true)
                        {
                            if (PowerTimer < 15) //Start to blink blue and white (after 6 seconds passed, or down to the last 15 frames(approx last 2 seconds))
                            {
                                if (PowerTimer % 2 == 0)
                                    GetCell(r, c).BackgroundColor = Color.Blue;
                                else
                                    GetCell(r, c).BackgroundColor = Color.White;
                            }
                            else
                            {
                                GetCell(r, c).BackgroundColor = Color.Blue;
                            }
                        }
                        else
                        {
                            GetCell(r, c).BackgroundColor = Color.Orange;
                        }
                        #endregion
                    }
                    #endregion

                }
            }
        }

        public bool CheckDotsComplete()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (Dots[r, c] == '*')
                        return false;
                }
            }

            return true;
        }

        //constructor
        public Grid(int Rows, int Columns, int CellSize)
        {
            //store rows and columns
            this.mRows = Rows;
            this.mColumns = Columns;
            this.mCellSize = CellSize;

            //create the grid array
            mGrid = new Cell[this.mRows, this.mColumns];

            //create each cell in the array
            for (int i = 0; i < this.mRows; i++)
            {
                for (int j = 0; j < this.mColumns; j++)
                {
                    mGrid[i, j] = new Cell(CellSize);
                }
            }


        }


        //methods
        public void Draw(Graphics g, int X, int Y)
        {
            //draw the grid starting form (X,Y) top left corner

            //co-ordinate values for looping
            int pX = X;
            int pY = Y;

            //loop through each row and column and draw the cell
            for (int i = 0; i < this.mRows; i++)
            {
                pY = Y + (i * this.mCellSize);
                for (int j = 0; j < this.mColumns; j++)
                {
                    pX = X + (j * this.mCellSize);
                    mGrid[i, j].Draw(g, pX, pY);


                    if (Dots[i, j] == '*')
                        mGrid[i, j].DrawDots(g, pX, pY);
                    else if (Dots[i, j] == 'o')
                        mGrid[i, j].DrawPowerDots(g, pX, pY);


                }
            }


        }

        public Cell GetCell(int Row, int Column)
        {
            //get a cell from the grid

            return mGrid[Row, Column];

        }

    }
}
