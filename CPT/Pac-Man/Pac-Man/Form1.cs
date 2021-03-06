﻿//By: Ryan Dailey & Andrew Cascone
//Grade 12 Computer Science CPT
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using GridMap;

namespace Pac_Man
{
    public partial class Form1 : Form
    {

        private Grid GridOfMap;
        bool ShowGrid = false;

        int rows = 0;
        int cols = 0;
        char[,] MapSelected;

        public Form1()
        {
            InitializeComponent();
        }

        //music
        SoundPlayer bgGameMusic = new SoundPlayer(Resource1._8bits);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ShowGrid == true)
            {
                base.OnPaint(e);
                if (GridOfMap != null)
                    GridOfMap.Draw(e.Graphics, 525, 260);
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //default home screen 
            timerArrow.Start();
            pic_Arrow.Location = new Point(294, 306);
            lblOn.Location = new Point(462, 325);
            lblOff.Location = new Point(550, 325);
            this.BackgroundImage = Resource1.Menu___Blank;
            btnStart.Visible = false;
            ListMaps.Location = new Point(200, 301);

            //play music
            bgGameMusic.PlayLooping();


            //create application folder structure in usr appdata, prevents problems
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Not-Pacman"))
            {
                //create map folders
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map1");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map2");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map3");
                
                //create highscore txt files
                using (File.Create((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map1\HighScore.txt")));
                using (File.Create((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map2\HighScore.txt")));
                using (File.Create((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map3\HighScore.txt")));

                //input map files into folders
                StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map1\PacMap1.txt");
                StreamWriter sw2 = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map2\PacMap2.txt");
                StreamWriter sw3 = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map3\PacMap3.txt");
                string txtMap = Resource1.PacMap1;
                string txtMap2 = Resource1.PacMap2;
                string txtMap3 = Resource1.PacMap3;

                sw.Write(txtMap);
                sw2.Write(txtMap2);
                sw3.Write(txtMap3);      
                sw.Close();
                sw2.Close();
                sw3.Close();
            }

            //load listview with map names
            DirectoryInfo Folder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps");

            DirectoryInfo[] MapFolders = Folder.GetDirectories();

            foreach (DirectoryInfo TheMapFolder in MapFolders)
            {
                FileInfo[] Maps = TheMapFolder.GetFiles("*.txt");

                foreach (FileInfo file in Maps)
                {
                    if (file.Name != "HighScore.txt")
                    {
                        ListMaps.Items.Add(file.Name);
                    }
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //up, down, arrow navigation
            if (e.KeyCode == Keys.Down && pic_Arrow.Location.Y == 306)
            {
                pic_Arrow.Location = new Point(294, 345);

            }

            else if (e.KeyCode == Keys.Up && pic_Arrow.Location.Y == 345)
            {
                pic_Arrow.Location = new Point(294, 306);

            }

            //map selection mnu
            else if (e.KeyCode == Keys.Enter && pic_Arrow.Location.Y == 306)
            {
                ShowGrid = true;
                this.Refresh();

                this.BackgroundImage = Resource1.Menu___Maps;
                ListMaps.Visible = true;
                ListMaps.Enabled = true;
                pic_Arrow.Location = new Point(268, 255);
                btnStart.Visible = true;
                lblBack.Visible = true;
                lblAbout.Visible = false;

                if (ListMaps.SelectedIndex >= 0)
                    pic_Arrow.Location = new Point(290, 520);
            }

            //back buttton
            else if (e.KeyCode == Keys.Escape)
            {
                ShowGrid = false;
                this.Refresh();

                ListMaps.Visible = false;
                lblOff.Visible = false;
                lblOn.Visible = false;
                btnStart.Visible = false;
                ListMaps.Enabled = false;
                lblAbouttxt.Visible = false;
                lblAbout.Visible = true;
                lblBack.Visible = false;

                pic_Arrow.Location = new Point(294, 306);

                this.BackgroundImage = Resource1.Menu___Blank;
            }

            //Options mnu
            else if (e.KeyCode == Keys.Enter && pic_Arrow.Location.Y == 345)
            {
                lblOff.Visible = true;
                lblOn.Visible = true;
                lblAbout.Visible = false;
                lblBack.Visible = true;

                pic_Arrow.Location = new Point(430, 324);

                this.BackgroundImage = Resource1.Menu___Options;
            }

            else if (e.KeyCode == Keys.Left & pic_Arrow.Location.X == 520)
            {
                pic_Arrow.Location = new Point(430, 324);
            }

            else if (e.KeyCode == Keys.Right && pic_Arrow.Location.X == 430)
            {
                pic_Arrow.Location = new Point(520, 324);
            }

            else if (e.KeyCode == Keys.Enter && pic_Arrow.Location.X == 520)
            {
                lblOff.ForeColor = Color.Maroon;
                lblOn.ForeColor = Color.White;
                bgGameMusic.Stop();

            }

            else if (e.KeyCode == Keys.Enter && pic_Arrow.Location.X == 430)
            {
                lblOff.ForeColor = Color.White;
                lblOn.ForeColor = Color.Maroon;
                bgGameMusic.PlayLooping();
            }

            else if (e.KeyCode == Keys.Escape && pic_Arrow.Location.X == 430 || pic_Arrow.Location.X == 520)
            {
                pic_Arrow.Location = new Point(294, 306);

                this.BackgroundImage = Resource1.Menu___Blank;

                lblOff.Visible = false;
                lblOn.Visible = false;
            }

        }

        private void timerArrow_Tick(object sender, EventArgs e)
        {
            pic_Arrow.Visible = !pic_Arrow.Visible;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string FileName = (string)ListMaps.SelectedItem;

            //load listview with map names
            DirectoryInfo Folder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps");

            DirectoryInfo[] MapFolders = Folder.GetDirectories();

            foreach (DirectoryInfo TheMapFolder in MapFolders)
            {
                FileInfo[] Maps = TheMapFolder.GetFiles("*.txt");

                foreach (FileInfo file in Maps)
                {
                    if (file.Name == FileName)
                    {
                        //---load the file---
                        StreamReader sr = new StreamReader(file.FullName);

                        rows = int.Parse(sr.ReadLine());
                        cols = int.Parse(sr.ReadLine());

                        MapSelected = new char[rows, cols];

                        //populate the Maze array
                        for (int r = 0; r < rows; r++)
                        {
                            string line = sr.ReadLine();
                            for (int c = 0; c < cols; c++)
                            {
                                MapSelected[r, c] = line[c];
                            }
                        }
                        sr.Close();
                        Gameplay GameplayForm = new Gameplay(MapSelected, file.DirectoryName);
                        GameplayForm.Show();

                        this.Close();
                    }
                }
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            
            ListMaps.Visible = false;
            lblOff.Visible = false;
            lblOn.Visible = false;
            btnStart.Visible = false;
            ListMaps.Enabled = false;
            lblBack.Visible = false;
            lblAbouttxt.Visible = false;
            lblAbout.Visible = true;

            pic_Arrow.Location = new Point(294, 306);

            this.BackgroundImage = Resource1.Menu___Blank;

            //Hide grid
            ShowGrid = false;
            this.Refresh();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resource1.Menu___About;
            lblAbouttxt.Visible = true;
            pic_Arrow.Location = new Point(105, 355);
            lblAbout.Visible = false;
            lblBack.Visible = true;

        }

        private void ListMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            pic_Arrow.Location = new Point(290, 520);

            string FileName = (string)ListMaps.SelectedItem;

            //load listview with map names
            DirectoryInfo Folder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps");

            DirectoryInfo[] MapFolders = Folder.GetDirectories();

            foreach (DirectoryInfo TheMapFolder in MapFolders)
            {
                FileInfo[] Maps = TheMapFolder.GetFiles("*.txt");

                foreach (FileInfo file in Maps)
                {
                    if (file.Name == FileName)
                    {
                        //---load the file---
                        StreamReader sr = new StreamReader(file.FullName);

                        rows = int.Parse(sr.ReadLine());
                        cols = int.Parse(sr.ReadLine());

                        GridOfMap = new Grid(rows, cols, 10);

                        MapSelected = new char[rows, cols];

                        //populate the Maze array
                        for (int r = 0; r < rows; r++)
                        {
                            string line = sr.ReadLine();
                            for (int c = 0; c < cols; c++)
                            {
                                MapSelected[r, c] = line[c];
                            }
                        }
                        sr.Close();

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
                                GridOfMap.GetMaze[r, c] = MapSelected[r, c];
                                GridOfMap.GetOriginalMaze[r, c] = MapSelected[r, c];
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


                        //Paint grid 
                        GridOfMap.PaintGrid();
                        this.Refresh();
                        
                    }
                }
            }

            


        }
    }
}