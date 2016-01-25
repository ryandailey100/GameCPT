namespace Pac_Man
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pic_Arrow = new System.Windows.Forms.PictureBox();
            this.timerArrow = new System.Windows.Forms.Timer(this.components);
            this.lblOn = new System.Windows.Forms.Label();
            this.lblOff = new System.Windows.Forms.Label();
            this.ListMaps = new System.Windows.Forms.ListBox();
            this.btnPlay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Arrow)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Arrow
            // 
            this.pic_Arrow.Image = global::Pac_Man.Resource1.Menu___Arrow;
            this.pic_Arrow.Location = new System.Drawing.Point(777, 78);
            this.pic_Arrow.Name = "pic_Arrow";
            this.pic_Arrow.Size = new System.Drawing.Size(26, 33);
            this.pic_Arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Arrow.TabIndex = 0;
            this.pic_Arrow.TabStop = false;
            // 
            // timerArrow
            // 
            this.timerArrow.Interval = 300;
            this.timerArrow.Tick += new System.EventHandler(this.timerArrow_Tick);
            // 
            // lblOn
            // 
            this.lblOn.AutoSize = true;
            this.lblOn.BackColor = System.Drawing.Color.Transparent;
            this.lblOn.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOn.ForeColor = System.Drawing.Color.Maroon;
            this.lblOn.Location = new System.Drawing.Point(754, 42);
            this.lblOn.Name = "lblOn";
            this.lblOn.Size = new System.Drawing.Size(49, 33);
            this.lblOn.TabIndex = 1;
            this.lblOn.Text = "On";
            this.lblOn.Visible = false;
            // 
            // lblOff
            // 
            this.lblOff.AutoSize = true;
            this.lblOff.BackColor = System.Drawing.Color.Transparent;
            this.lblOff.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOff.ForeColor = System.Drawing.Color.White;
            this.lblOff.Location = new System.Drawing.Point(737, 9);
            this.lblOff.Name = "lblOff";
            this.lblOff.Size = new System.Drawing.Size(66, 33);
            this.lblOff.TabIndex = 2;
            this.lblOff.Text = "Off";
            this.lblOff.Visible = false;
            // 
            // ListMaps
            // 
            this.ListMaps.BackColor = System.Drawing.SystemColors.InfoText;
            this.ListMaps.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListMaps.ForeColor = System.Drawing.Color.White;
            this.ListMaps.FormattingEnabled = true;
            this.ListMaps.ItemHeight = 21;
            this.ListMaps.Location = new System.Drawing.Point(282, 301);
            this.ListMaps.Name = "ListMaps";
            this.ListMaps.Size = new System.Drawing.Size(259, 193);
            this.ListMaps.TabIndex = 3;
            this.ListMaps.Visible = false;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(361, 509);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(101, 41);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Begin Game";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Pac_Man.Resource1.Menu___Maps;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(815, 588);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.pic_Arrow);
            this.Controls.Add(this.ListMaps);
            this.Controls.Add(this.lblOff);
            this.Controls.Add(this.lblOn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NOT Pac-Man";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Arrow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Arrow;
        private System.Windows.Forms.Timer timerArrow;
        private System.Windows.Forms.Label lblOn;
        private System.Windows.Forms.Label lblOff;
        private System.Windows.Forms.ListBox ListMaps;
        private System.Windows.Forms.Button btnPlay;
    }
}

