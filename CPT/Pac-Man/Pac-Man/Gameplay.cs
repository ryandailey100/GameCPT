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
                        if (GridOfMap.GetMaze[r, c] == '.')
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
                GreenGhost = new GhostAI(GhostAI.Ghost.Green);
                OrangeGhost = new GhostAI(GhostAI.Ghost.Orange);



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
            GreenGhost.UpdateAI();
            //Orange Ghost
            OrangeGhost.UpdateAI();
            
            //Power Up Timer
            if (GridOfMap.GetPowerUp == true)
            {
                if (GridOfMap.GetPowerTimer > 0)
                    GridOfMap.GetPowerTimer--;
                else //End powerup
                {
                    //Reset
                    GridOfMap.GetPowerUp = false;
                    GridOfMap.GetRedVul = true;
                    GridOfMap.GetPinkVul = true;
                    GridOfMap.GetGreenVul = true;
                    GridOfMap.GetOrangeVul = true;
                }
                    
            }

            
            //Paint grid 
            GridOfMap.PaintGrid();
            this.Refresh();
            

            //Update label
            lbl_Score.Text = GridOfMap.GetScore.ToString();
            lbl_Lives.Text = GridOfMap.GetLives.ToString();

            //Check if all dots are gone
            #region Check Dots
            if (GridOfMap.CheckDotsComplete() == true)
            {
                
                //Input all dots
                for (int r = 0; r < GridOfMap.GetRows; r++)
                {
                    for (int c = 0; c < GridOfMap.GetCols; c++)
                    {
                        if (GridOfMap.GetOriginalMaze[r, c] == '.')
                            GridOfMap.GetDots[r, c] = '*';
                        else
                            GridOfMap.GetDots[r, c] = GridOfMap.GetOriginalMaze[r, c];
                    }
                }
                Gameplay.GridOfMap.GetPowerUp = false;
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

                //Reset game while keeping score and lives
                ResetObjects();

            }
            #endregion

            //Check for Game Over
            if (GridOfMap.GetGameOver == true)
                GameOver();

        }


        public static void ResetObjects()
        {
            //Player
            PlayerClass = new Player();

            //Ghosts
            RedGhost = new GhostAI(GhostAI.Ghost.Red);
            PinkGhost = new GhostAI(GhostAI.Ghost.Pink);
            GreenGhost = new GhostAI(GhostAI.Ghost.Green);
            OrangeGhost = new GhostAI(GhostAI.Ghost.Orange);

            
        }

        public static void RedGhostDeath()
        {
            GridOfMap.GetMaze[RedGhost.GetInitialRow, RedGhost.GetInitialCol] = 'R';
            Gameplay.GridOfMap.GetRedVul = false;
            RedGhost = new GhostAI(GhostAI.Ghost.Red);
            RedGhost.TimeInCage = 20;
        }

        public static void PinkGhostDeath()
        {
            GridOfMap.GetMaze[PinkGhost.GetInitialRow, PinkGhost.GetInitialCol] = 'P';
            Gameplay.GridOfMap.GetPinkVul = false;
            PinkGhost = new GhostAI(GhostAI.Ghost.Pink);
            PinkGhost.TimeInCage = 20;
        }

        public static void GreenGhostDeath()
        {
            GridOfMap.GetMaze[GreenGhost.GetInitialRow, GreenGhost.GetInitialCol] = 'G';
            Gameplay.GridOfMap.GetGreenVul = false;
            GreenGhost = new GhostAI(GhostAI.Ghost.Green);
            GreenGhost.TimeInCage = 20;
        }

        public static void OrangeGhostDeath()
        {
            GridOfMap.GetMaze[OrangeGhost.GetInitialRow, OrangeGhost.GetInitialCol] = 'O';
            Gameplay.GridOfMap.GetOrangeVul = false;
            OrangeGhost = new GhostAI(GhostAI.Ghost.Orange);
            OrangeGhost.TimeInCage = 20;
        }

        public void GameOver()
        {
            TimerUpdate.Stop();
            //Delete pac-man and ghosts from maze, so when the tiles gets painted, they are not there
            for (int r = 0; r < GridOfMap.GetRows; r++)
            {
                for (int c = 0; c < GridOfMap.GetCols; c++)
                {
                    if (GridOfMap.GetMaze[r, c] == 'S' || GridOfMap.GetMaze[r, c] == 'R' || GridOfMap.GetMaze[r, c] == 'P' || GridOfMap.GetMaze[r, c] == 'G' || GridOfMap.GetMaze[r, c] == 'O')
                        GridOfMap.GetMaze[r, c] = '.';
                }
            }


            //Paint grid 
            GridOfMap.PaintGrid();
            this.Refresh();

            //Show Game Over Label
            lbl_GameOver.Location = new Point(this.Height / 4, this.Width / 2);
            lbl_GameOver.Visible = true;
            this.Refresh();

            //Delay 3 seconds before returning to highscore menu
            System.Threading.Thread.Sleep(3000);

            //Opens up main menu form
            Form1 MainForm = new Form1();
            MainForm.Show();

            this.Close();

        }

        private void Gameplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}
