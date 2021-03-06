﻿//By: Ryan Dailey & Andrew Cascone
//Grade 12 Computer Science CPT
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

        bool GamePaused = false;

        private string HighScorePath;

        public Gameplay(char[,] SelectedMaze, string MapDirectory)
        {
            InitializeComponent(); 

            int rows = SelectedMaze.GetLength(0);
            int cols = SelectedMaze.GetLength(1);

            GridOfMap = new Grid(rows, cols, 20);

            //initialize the Maze array
            GridOfMap.GetDots = new char[rows, cols];
            GridOfMap.GetMaze = new char[rows, cols];
            GridOfMap.GetOriginalMaze = new char[rows, cols];

            //Set the rows and cols of map
            GridOfMap.GetRows = rows;
            GridOfMap.GetCols = cols;

            //populate the Maze array
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridOfMap.GetMaze[r, c] = SelectedMaze[r,c];
                    GridOfMap.GetOriginalMaze[r, c] = SelectedMaze[r, c];
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

            HighScorePath = MapDirectory + @"\HighScore.txt";

            StreamReader sr = new StreamReader(HighScorePath);
            string SavedScore = sr.ReadLine();

            if (SavedScore != null) //Checks if it is null
                GridOfMap.GetHighScore = int.Parse(SavedScore);

            lbl_Highscore.Text = GridOfMap.GetHighScore.ToString();
            sr.Close();

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


            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GridOfMap != null)
                GridOfMap.Draw(e.Graphics, 10, 50);

        }

        private void Gameplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (GamePaused == true) //Unpause
                {
                    GamePaused = false;
                    TimerUpdate.Start();
                    lbl_GameOver.Visible = false;
                    lbl_PressPause.Visible = true;
                    this.Refresh();
                }
                else if (GamePaused == false && GridOfMap.GetGameOver == false) //Pause game
                {
                    GamePaused = true;
                    TimerUpdate.Stop();
                    lbl_GameOver.Location = new Point(this.Height / 4, this.Width / 2);
                    lbl_GameOver.Text = "Paused";
                    lbl_GameOver.Visible = true;
                    lbl_PressPause.Visible = false;
                    this.Refresh();
                }
            }
            else if (e.KeyCode == Keys.Up && PlayerClass.GetUpSpace == true)// && UpSpace == true
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
            
            if (GridOfMap.GetDeath == true)
            {
                GridOfMap.GetDeath = false;
                //Delay 1 second
                System.Threading.Thread.Sleep(1000);
            }

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
            lbl_GameOver.Text = "Game Over!";
            lbl_GameOver.Visible = true;
            lbl_PressPause.Visible = false;
            this.Refresh();

            
            //Highscore
            if (GridOfMap.GetHighScore < GridOfMap.GetScore)
            {
                //Overwrite Highscore
                StreamWriter sw = new StreamWriter(HighScorePath);
                sw.Write(GridOfMap.GetScore);
                sw.Close();
            }


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
