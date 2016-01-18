using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GridMap;
using System.IO;

namespace Pac_Man
{
    public partial class Gameplay : Form
    {

        char[,] Maze;
        int rows = 0;
        int cols = 0;
        Grid GridOfMap;

        PlayerMovement PlayerDir = PlayerMovement.Left; //Initally starts going left

        //Player(Pac-Man)
        bool UpSpace = false;
        bool DownSpace = false;
        bool LeftSpace = false;
        bool RightSpace = false;

        int PlayerRow = 0;
        int PlayerCol = 0;


        public enum PlayerMovement
        {
            Up,
            Down,
            Left,
            Right
        }
        

        public Gameplay()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GridOfMap != null)
                GridOfMap.Draw(e.Graphics, 10, 50);

        }

        


        private void Gameplay_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up && UpSpace == true)// && UpSpace == true
                PlayerDir = PlayerMovement.Up;
            else if (e.KeyCode == Keys.Down && DownSpace == true)
                PlayerDir = PlayerMovement.Down;
            else if (e.KeyCode == Keys.Left && LeftSpace == true)
                PlayerDir = PlayerMovement.Left;
            else if (e.KeyCode == Keys.Right && RightSpace == true)
                PlayerDir = PlayerMovement.Right;



        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateFrame();
        }

        private void Gameplay_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get file from Open Dialog box
            OpenFileDialog fd = new OpenFileDialog();

            //don't forget error checking

            if (fd.ShowDialog() == DialogResult.OK)
            {
                //load the file
                StreamReader sr = new StreamReader(fd.OpenFile());

                rows = int.Parse(sr.ReadLine());
                cols = int.Parse(sr.ReadLine());
                //initialize the Maze array
                Maze = new char[rows, cols];

                GridOfMap = new Grid(rows, cols, 20);
                GridOfMap.GetDots = new char[rows, cols];


                //populate the Maze array
                for (int r = 0; r < rows; r++)
                {
                    string line = sr.ReadLine();
                    for (int c = 0; c < cols; c++)
                    {
                        Maze[r, c] = line[c];
                    }
                }


                //Input all dots
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (Maze[r, c] == '.' || Maze[r, c] == '+')
                            GridOfMap.GetDots[r, c] = '*';
                        else
                            GridOfMap.GetDots[r, c] = Maze[r, c];
                    }

                }



                //configure grid so each cell is drawn properly
                UpdateFrame();

                //resize form to grid height and width
                this.Width = this.cols * 20 + 40;
                this.Height = this.rows * 20 + 100;

                //tell form to redraw
                this.Refresh();

                //Start Timer
                TimerUpdate.Start();

                button1.Enabled = false;

            }
        }

        private void UpdateFrame()
        {
            //Player Movement
            //Find if you are stuck to a wall in all directions
            #region Player Movement
            
            for (int r = 0; r < this.rows; r++)
            {
                for (int c = 0; c < this.cols; c++)
                {
                    if (Maze[r, c] == 'S')
                    {
                        ////---Check Directions---
                        ////Up
                        //if (Maze[r - 1, c] != '#' && Maze[r - 1, c] != '=')
                        //    UpSpace = true;
                        //else
                        //    UpSpace = false;

                        ////Down
                        //if (Maze[r + 1, c] != '#' && Maze[r + 1, c] != '=')
                        //    DownSpace = true;
                        //else
                        //    DownSpace = false;

                        ////Left
                        //if (Maze[r, c - 1] != '#' && Maze[r, c - 1] != '=')
                        //    LeftSpace = true;
                        //else
                        //    LeftSpace = false;

                        ////Right
                        //if (Maze[r, c + 1] != '#' && Maze[r, c + 1] != '=')
                        //    RightSpace = true;
                        //else
                        //    RightSpace = false;

                        //Store players position on grid
                        PlayerRow = r;
                        PlayerCol = c;

                        //Break both loops
                        r = this.rows;
                        break;
                    }
                }
            }



            if (PlayerDir == PlayerMovement.Up && UpSpace == true)
            {
                Maze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Maze[PlayerRow - 1, PlayerCol] = 'S'; //Move your pacman to the new location
                GridOfMap.GetDots[PlayerRow - 1, PlayerCol] = ','; //Eat a dot

                PlayerRow--;
                //---Add Score Increment Here---

            }
            else if (PlayerDir == PlayerMovement.Down && DownSpace == true)
            {
                Maze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Maze[PlayerRow + 1, PlayerCol] = 'S'; //Move your pacman to the new location
                GridOfMap.GetDots[PlayerRow + 1, PlayerCol] = ','; //Eat a dot

                PlayerRow++;
                //---Add Score Increment Here---

            }
            else if (PlayerDir == PlayerMovement.Left && LeftSpace == true)
            {
                Maze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Maze[PlayerRow, PlayerCol - 1] = 'S'; //Move your pacman to the new location
                GridOfMap.GetDots[PlayerRow, PlayerCol - 1] = ','; //Eat a dot

                PlayerCol--;
                //---Add Score Increment Here---

            }
            else if (PlayerDir == PlayerMovement.Right && RightSpace == true)
            {
                Maze[PlayerRow, PlayerCol] = '.'; //Put previous location back to a path
                Maze[PlayerRow, PlayerCol + 1] = 'S'; //Move your pacman to the new location
                GridOfMap.GetDots[PlayerRow, PlayerCol + 1] = ','; //Eat a dot

                PlayerCol++;
                //---Add Score Increment Here---

            }




            #endregion


            //---Check Directions---
            #region Check Directions
            //Up   //<--------------------------Add Up Portal '&'
            if (Maze[PlayerRow - 1, PlayerCol] != '#' && Maze[PlayerRow - 1, PlayerCol] != '=')
                UpSpace = true;
            else
                UpSpace = false;

            //Down   //<--------------------------Add Down Portal '&'
            if (Maze[PlayerRow + 1, PlayerCol] != '#' && Maze[PlayerRow + 1, PlayerCol] != '=')
                DownSpace = true;
            else
                DownSpace = false;

            //Left
            if (Maze[PlayerRow, PlayerCol - 1] != '&')
            {
                if (Maze[PlayerRow, PlayerCol - 1] != '#' && Maze[PlayerRow, PlayerCol - 1] != '=')
                    LeftSpace = true;
                else
                    LeftSpace = false;
            }
            else
            {
                Maze[PlayerRow, PlayerCol] = ',';
                //Find Other teleport symbol
                for (int r = 0; r < this.rows; r++)
                {
                    for (int c = 0; c < this.cols; c++)
                    {
                        if (r != PlayerRow || c != PlayerCol - 1)
                        {
                            if (Maze[r, c] == '&')
                                Maze[r, c - 2] = 'S';
                        }
                    }
                }
            }

            //Right
            if (Maze[PlayerRow, PlayerCol + 1] != '&')
            {
                if (Maze[PlayerRow, PlayerCol + 1] != '#' && Maze[PlayerRow, PlayerCol + 1] != '=')
                    RightSpace = true;
                else
                    RightSpace = false;
            }
            else
            {
                Maze[PlayerRow, PlayerCol] = ',';
                //Find Other teleport symbol
                for (int r = 0; r < this.rows; r++)
                {
                    for (int c = 0; c < this.cols; c++)
                    {
                        if (r != PlayerRow || c != PlayerCol + 1)
                        {
                            if (Maze[r, c] == '&')
                                Maze[r, c + 2] = 'S';
                        }
                    }
                }
            }

            #endregion


            //Paint grid
            #region Paint Grid
            for (int r = 0; r < this.rows; r++)
            {
                for (int c = 0; c < this.cols; c++)
                {
                    #region Map
                    //change colour of cell depending on what
                    //is in it
                    if (Maze[r, c] == '#')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Blue;
                    else if (Maze[r, c] == '.')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == ',')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == '+')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == 'x')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == '&')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == 'o')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == '=')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (Maze[r, c] == 'S')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Yellow;
                    else if (Maze[r, c] == 'R')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Red;
                    else if (Maze[r, c] == 'P')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Pink;
                    else if (Maze[r, c] == 'G')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Green;
                    else if (Maze[r, c] == 'O')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Orange;
                    #endregion

                }
            }

            this.Refresh();
            #endregion


        }

        private void Gameplay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                    e.IsInputKey = true;
                    break;
            }
        }
    }
}
