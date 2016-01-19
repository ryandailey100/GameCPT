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
        
        
        public enum Ghost
        {
            Red,
            Pink,
            Green,
            Orange
        }

        public void UpdateAI(Ghost GhostType)
        {
            //===Find Location Point===
            #region Find Point Location

            //Point location var
            int Point_row = 0;
            int Point_col = 0;

            if (GhostType == Ghost.Red)
            {
                #region Red Ghost
                //Point will be on Pac-Man
                Point_row = Gameplay.PlayerClass.GetPlayerRow;
                Point_col = Gameplay.PlayerClass.GetPlayerCol;

                #endregion
            }
            else if (GhostType == Ghost.Pink)
            {
                #region Pink Ghost
                //Point will be 4 blocks ahead of Pac-Man
                if (Gameplay.PlayerClass.GetPlayerDir == Player.PlayerMovement.Up)
                {
                    Point_row = Gameplay.PlayerClass.GetPlayerRow - 4;
                    Point_col = Gameplay.PlayerClass.GetPlayerCol;

                    Point_row -= FindPinkPoint(Point_row, Point_col);



                    //for (int r = 0; r < Gameplay.GridOfMap.GetRows; r++)
                    //{
                    //    for (int c = 0; c < Gameplay.GridOfMap.GetCols; c++)
                    //    {
                    //        if (Gameplay.GridOfMap.GetMaze[r, c] )
                    //    }
                    //}

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

            
            //Find Path to Point


        }

        private int FindPinkPoint(int row, int col)
        {
            //Check if Point is not on a path or outside of bounds
            if (Gameplay.GridOfMap.GetMaze[row, col] == '#' || Gameplay.GridOfMap.GetMaze[row, col] == '=')
            {
                row++;//based it on direction, eiher row or col
                return 1 + FindPinkPoint(row, col);
            }
            else
            {
                return 0;
            }
           
        }


    }
}
