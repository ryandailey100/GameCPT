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

namespace Pac_Man
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //---load the file---
        //StreamReader sr = new StreamReader(MapHere);  //<--load file from resources

        //    int rows = int.Parse(sr.ReadLine());
        //    int cols = int.Parse(sr.ReadLine());

              //char[,] MapSelected = new char[rows, cols];

        ////populate the Maze array
        //for (int r = 0; r < rows; r++)
        //{
        //    string line = sr.ReadLine();
        //    for (int c = 0; c < cols; c++)
        //    {
        //        MapSelected[r, c] = line[c];
        //    }
        //}

            //Gameplay GameplayForm = new Gameplay(MapSelected);
            //Gameplay.Show();

            //this.Close();

            //---------


        private void Form1_Load(object sender, EventArgs e)
        {
            //default home screen 
            timerArrow.Start();
            pic_Arrow.Location = new Point(294, 306);
            lblOn.Location = new Point(462, 325);
            lblOff.Location = new Point(550, 325);
            this.BackgroundImage = Resource1.Menu___Blank;
            btnStart.Visible = false;

            //create application folder structure
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Local\Not-Pacman"))
            {
                //create map folders
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map1");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map2");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map3");
                
                //create highscore txt files
                File.Create((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map1\HighScore.txt"));
                File.Create((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map2\HighScore.txt"));
                File.Create((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map3\HighScore.txt"));

                //input map files into folders
                StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NOT-Pacman\Maps\Map1\PacMap1.txt");
                string txtMap = Resource1.PacMap1;

                sw.Write(txtMap);
                sw.Close();
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
                this.BackgroundImage = Resource1.Menu___Maps;
                ListMaps.Visible = true;
                ListMaps.Enabled = true;
                pic_Arrow.Location = new Point(250, 296);
                btnStart.Visible = true;
                lblBack.Visible = true;
            }

            //back buttton
            else if (e.KeyCode == Keys.Escape && pic_Arrow.Location.X == 248)
            {
                ListMaps.Visible = false;
                lblOff.Visible = false;
                lblOn.Visible = false;
                btnStart.Visible = false;
                ListMaps.Enabled = false;

                pic_Arrow.Location = new Point(294, 306);

                this.BackgroundImage = Resource1.Menu___Blank;
            }

            //Options mnu
            else if (e.KeyCode == Keys.Enter && pic_Arrow.Location.Y == 345)
            {
                lblOff.Visible = true;
                lblOn.Visible = true;

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
            }

            else if (e.KeyCode == Keys.Enter && pic_Arrow.Location.X == 430)
            {
                lblOff.ForeColor = Color.White;
                lblOn.ForeColor = Color.Maroon;
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
                        StreamReader sr = new StreamReader(file.FullName);  //<--load file from resources

                        int rows = int.Parse(sr.ReadLine());
                        int cols = int.Parse(sr.ReadLine());

                        char[,] MapSelected = new char[rows, cols];

                        //populate the Maze array
                        for (int r = 0; r < rows; r++)
                        {
                            string line = sr.ReadLine();
                            for (int c = 0; c < cols; c++)
                            {
                                MapSelected[r, c] = line[c];
                            }
                        }

                        Gameplay GameplayForm = new Gameplay(MapSelected);
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

            pic_Arrow.Location = new Point(294, 306);

            this.BackgroundImage = Resource1.Menu___Blank;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}
