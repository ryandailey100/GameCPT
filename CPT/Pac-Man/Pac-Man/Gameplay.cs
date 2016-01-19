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

        public static Grid GridOfMap;


        //Player Class
        public static Player PlayerClass;

        //Ghosts AI Class
        GhostAI RedGhost;
        GhostAI PinkGhost;
        GhostAI GreenGhost;
        GhostAI OrangeGhost;
        


       
        

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

            if (e.KeyCode == Keys.Up && PlayerClass.GetUpSpace == true)// && UpSpace == true
                PlayerClass.GetPlayerDir = Player.PlayerMovement.Up;
            else if (e.KeyCode == Keys.Down && PlayerClass.GetDownSpace == true)
                PlayerClass.GetPlayerDir = Player.PlayerMovement.Down;
            else if (e.KeyCode == Keys.Left && PlayerClass.GetLeftSpace == true)
                PlayerClass.GetPlayerDir = Player.PlayerMovement.Left;
            else if (e.KeyCode == Keys.Right && PlayerClass.GetRightSpace == true)
                PlayerClass.GetPlayerDir = Player.PlayerMovement.Right;
            

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

                int rows = int.Parse(sr.ReadLine());
                int cols = int.Parse(sr.ReadLine());

                GridOfMap = new Grid(rows, cols, 20);
                GridOfMap.GetDots = new char[rows, cols];

                //initialize the Maze array
                GridOfMap.GetMaze = new char[rows, cols];

                //Set the rows and cols of map
                GridOfMap.GetRows = rows;
                GridOfMap.GetCols = cols;

                //Create Player Class
                PlayerClass = new Player();

                //Create All 4 Ghost AI's
                RedGhost = new GhostAI();
                PinkGhost = new GhostAI();
                GreenGhost = new GhostAI();
                OrangeGhost = new GhostAI();

                ////Set Ghosts
                //RedGhost.GhostType = GhostAI.Ghost.Red;
                //PinkGhost.GhostType = GhostAI.Ghost.Pink;
                //GreenGhost.GhostType = GhostAI.Ghost.Green;
                //OrangeGhost.GhostType = GhostAI.Ghost.Orange;



                //populate the Maze array
                for (int r = 0; r < rows; r++)
                {
                    string line = sr.ReadLine();
                    for (int c = 0; c < cols; c++)
                    {
                        GridOfMap.GetMaze[r, c] = line[c];
                    }
                }


                //Input all dots
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (GridOfMap.GetMaze[r, c] == '.' || GridOfMap.GetMaze[r, c] == '+')
                            GridOfMap.GetDots[r, c] = '*';
                        else
                            GridOfMap.GetDots[r, c] = GridOfMap.GetMaze[r, c];
                    }

                }



                //configure grid so each cell is drawn properly
                UpdateFrame();

                //resize form to grid height and width
                this.Width = GridOfMap.GetCols * 20 + 40;
                this.Height = GridOfMap.GetRows * 20 + 100;

                //tell form to redraw
                this.Refresh();

                //Start Timer
                TimerUpdate.Start();

                button1.Enabled = false;

            }
        }

        private void UpdateFrame()
        {
            //===Player Movement===
            PlayerClass.UpdatePlayer();


            //===Ghosts AI===
            //Red Ghost
            RedGhost.UpdateAI(GhostAI.Ghost.Red);
            //Pink Ghost
            PinkGhost.UpdateAI(GhostAI.Ghost.Pink);
            //Green Ghost
            GreenGhost.UpdateAI(GhostAI.Ghost.Green);
            //Orange Ghost
            OrangeGhost.UpdateAI(GhostAI.Ghost.Orange);
            


            //Paint grid <---Add this into the grid class------------<-----------<--------<--------<-----<<<<<<----<<<---
            #region Paint Grid
            for (int r = 0; r < GridOfMap.GetRows; r++)
            {
                for (int c = 0; c < GridOfMap.GetCols; c++)
                {
                    #region Map
                    //change colour of cell depending on what is in it
                    if (GridOfMap.GetMaze[r, c] == '#')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Blue;
                    else if (GridOfMap.GetMaze[r, c] == '.')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == ',')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == '+')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == 'x')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == '&')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == 'o')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == '=')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Black;
                    else if (GridOfMap.GetMaze[r, c] == 'S')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Yellow;
                    else if (GridOfMap.GetMaze[r, c] == 'R')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Red;
                    else if (GridOfMap.GetMaze[r, c] == 'P')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Pink;
                    else if (GridOfMap.GetMaze[r, c] == 'G')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Cyan;
                    else if (GridOfMap.GetMaze[r, c] == 'O')
                        GridOfMap.GetCell(r, c).BackgroundColor = Color.Orange;
                    #endregion

                }
            }

            this.Refresh();
            #endregion
            

            //Update label
            lbl_Score.Text = PlayerClass.GetScore.ToString();

        }




        
    }
}
