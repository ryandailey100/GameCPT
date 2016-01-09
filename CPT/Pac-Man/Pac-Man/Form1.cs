using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


        private void Form1_Load(object sender, EventArgs e)
        {
            timerArrow.Start();
            pic_Arrow.Location = new Point(294, 306);
            this.BackgroundImage = Resource1.Menu___Blank;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //up, down, arrow navigation
            if (e.KeyCode == Keys.Down && pic_Arrow.Location.Y == 306)
            {
                pic_Arrow.Location = new Point(294, 345);

            }

            if (e.KeyCode == Keys.Up && pic_Arrow.Location.Y == 345)
            {
                pic_Arrow.Location = new Point(294, 306);

            }

            //Username mnu
            if (e.KeyCode == Keys.Enter && pic_Arrow.Location.Y == 306)
            {
                pic_Arrow.Location = new Point(248, 315);

                this.BackgroundImage = Resource1.Menu___Name;
            }

            if (e.KeyCode == Keys.Escape && pic_Arrow.Location.X == 248)
            {
                pic_Arrow.Location = new Point(294, 306);

                this.BackgroundImage = Resource1.Menu___Blank;
            }

            //Options mnu
            if (e.KeyCode == Keys.Enter && pic_Arrow.Location.Y == 345)
            {
                lblOff.Visible = true;
                lblOn.Visible = true;

                pic_Arrow.Location = new Point(430, 324);

                this.BackgroundImage = Resource1.Menu___Options;
            }

            if (e.KeyCode == Keys.Left & pic_Arrow.Location.X == 520)
            {
                pic_Arrow.Location = new Point(430, 324);
            }

            if (e.KeyCode == Keys.Right && pic_Arrow.Location.X == 430)
            {
                pic_Arrow.Location = new Point(520, 324);
            }

            if (e.KeyCode == Keys.Enter && pic_Arrow.Location.X == 520)
            {
                lblOff.ForeColor = Color.Maroon;
                lblOn.ForeColor = Color.White;
            }

            if (e.KeyCode == Keys.Enter && pic_Arrow.Location.X == 430)
            {
                lblOff.ForeColor = Color.White;
                lblOn.ForeColor = Color.Maroon;
            }

            if (e.KeyCode == Keys.Escape && pic_Arrow.Location.X == 430 || pic_Arrow.Location.X == 520)
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


    }
}
