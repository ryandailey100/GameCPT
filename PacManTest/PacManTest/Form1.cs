using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GridMap;


namespace PacManTest
{
    public partial class Form1 : Form
    {
        
        char[,] Maze;
        int rows = 0;
        int cols = 0;
        Grid GridOfMap;

        PlayerMovement PlayerDir = PlayerMovement.Left; //Initally starts going left
        
        public enum PlayerMovement
        {
            Up,
            Down,
            Left,
            Right
        }

        public Form1()
        {
            InitializeComponent();
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
                TimerFrame.Start();
            }
        }

        private void UpdateFrame()
        {
            


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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GridOfMap != null)
                GridOfMap.Draw(e.Graphics, 10, 50);
           
        }

        private void TimerFrame_Tick(object sender, EventArgs e)
        {
            UpdateFrame();
        }

       

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                MessageBox.Show("Up");
            }
            else if (e.KeyCode == Keys.Down)
            {
                MessageBox.Show("Down");
            }
            else
            {
                MessageBox.Show("AA");
            }
        }
    }
}
