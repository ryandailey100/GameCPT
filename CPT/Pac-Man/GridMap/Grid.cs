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

        private int rows = 0;
        private int cols = 0;

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
                    mGrid[i, j] = new Cell();
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
