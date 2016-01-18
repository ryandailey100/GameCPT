using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GridMap
{
    public class Cell
    {
        //Fields
        private int mSize;
        private Color mBackgroundColour;

        //Constructors
        public Cell()
        {
            //default to white cell with a black border
            this.mSize = 20;
            this.mBackgroundColour = Color.White;
        }

        public Cell(int Size, Color BackgroundColour, Color BorderColour)
        {
            this.mSize = Size;
            this.mBackgroundColour = BackgroundColour;

        }

        //Properties
        public int Size
        {
            set { this.mSize = value; }
            get { return this.mSize; }
        }

        public Color BackgroundColor
        {
            set { this.mBackgroundColour = value; }
            get { return this.mBackgroundColour; }
        }




        //Methods
        public void Draw(Graphics g, int X, int Y)
        {
            //create a Pen and a Brush
            SolidBrush BackBrush = new SolidBrush(this.mBackgroundColour);
            //SolidBrush DotBrush = new SolidBrush(Color.Green);

            //draw cell
            g.FillRectangle(BackBrush, X, Y, this.mSize, this.mSize);
            //g.FillRectangle(DotBrush, X + ((this.mSize / 2) - (this.mSize / 8)), Y + ((this.mSize / 2) - (this.mSize / 8)), this.mSize / 4, this.mSize / 4);

            //dispose drawing tools
            BackBrush.Dispose();

        }

        public void DrawDots(Graphics g, int X, int Y)
        {
            //Create a Brush
            SolidBrush DotBrush = new SolidBrush(Color.Yellow);

            //Draw Dot
            g.FillRectangle(DotBrush, X + ((this.mSize / 2) - (this.mSize / 8)), Y + ((this.mSize / 2) - (this.mSize / 8)), this.mSize / 4, this.mSize / 4);

            //Dispose Brush
            DotBrush.Dispose();
        }

        public void DrawPowerDots(Graphics g, int X, int Y)
        {
            //Create a Brush
            SolidBrush DotBrush = new SolidBrush(Color.LightGoldenrodYellow);

            //Draw Dot
            g.FillRectangle(DotBrush, X + (this.mSize / 4), Y + (this.mSize / 4), this.mSize / 2, this.mSize / 2);

            //Dispose Brush
            DotBrush.Dispose();
        }
    }
}
