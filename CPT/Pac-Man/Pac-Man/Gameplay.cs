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

        //Ghosts AI Class (make array of objects (eg. Tv shows program)) <<<<-----------
        static GhostAI RedGhost;
        static GhostAI PinkGhost;
        static GhostAI GreenGhost;
        static GhostAI OrangeGhost;
        

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
                GridOfMap.GetOriginalMaze = new char[rows, cols];

                //Set the rows and cols of map
                GridOfMap.GetRows = rows;
                GridOfMap.GetCols = cols;

                
                //populate the Maze array
                for (int r = 0; r < rows; r++)
                {
                    string line = sr.ReadLine();
                    for (int c = 0; c < cols; c++)
                    {
                        GridOfMap.GetMaze[r, c] = line[c];
                        GridOfMap.GetOriginalMaze[r, c] = line[c];
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

                //Create Player Class
                PlayerClass = new Player();
                
                //Create All 4 Ghost AI's
                RedGhost = new GhostAI(GhostAI.Ghost.Red);
                PinkGhost = new GhostAI(GhostAI.Ghost.Pink);
                //GreenGhost = new GhostAI(GhostAI.Ghost.Green);
                //OrangeGhost = new GhostAI(GhostAI.Ghost.Orange);



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
            RedGhost.UpdateAI();
            //Pink Ghost
            PinkGhost.UpdateAI();
            //Green Ghost
            //GreenGhost.UpdateAI();
            //Orange Ghost
            //OrangeGhost.UpdateAI();
            

            //Paint grid 
            GridOfMap.PaintGrid();
            this.Refresh();
            

            //Update label
            lbl_Score.Text = GridOfMap.GetScore.ToString();
            lbl_Lives.Text = GridOfMap.GetLives.ToString();

        }


        public static void ResetObjects()
        {
            //Player
            PlayerClass = new Player();

            //Ghosts
            RedGhost = new GhostAI(GhostAI.Ghost.Red);
            PinkGhost = new GhostAI(GhostAI.Ghost.Pink);
            //GreenGhost = new GhostAI(GhostAI.Ghost.Green);
            //OrangeGhost = new GhostAI(GhostAI.Ghost.Orange);

            
        }

      
        public static void GameOver()
        {
            
        }


    }
}
