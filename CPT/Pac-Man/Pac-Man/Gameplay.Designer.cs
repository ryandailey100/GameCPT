namespace Pac_Man
{
    partial class Gameplay
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
            this.TimerUpdate = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Score = new System.Windows.Forms.Label();
            this.lbl_Lives = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_GameOver = new System.Windows.Forms.Label();
            this.lbl_PressPause = new System.Windows.Forms.Label();
            this.lbl_Highscore = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TimerUpdate
            // 
            this.TimerUpdate.Interval = 150;
            this.TimerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(303, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Score:";
            // 
            // lbl_Score
            // 
            this.lbl_Score.AutoSize = true;
            this.lbl_Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Score.ForeColor = System.Drawing.Color.White;
            this.lbl_Score.Location = new System.Drawing.Point(430, 43);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(40, 42);
            this.lbl_Score.TabIndex = 2;
            this.lbl_Score.Text = "0";
            // 
            // lbl_Lives
            // 
            this.lbl_Lives.AutoSize = true;
            this.lbl_Lives.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Lives.ForeColor = System.Drawing.Color.White;
            this.lbl_Lives.Location = new System.Drawing.Point(187, 43);
            this.lbl_Lives.Name = "lbl_Lives";
            this.lbl_Lives.Size = new System.Drawing.Size(40, 42);
            this.lbl_Lives.TabIndex = 4;
            this.lbl_Lives.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(70, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 42);
            this.label4.TabIndex = 3;
            this.label4.Text = "Lives:";
            // 
            // lbl_GameOver
            // 
            this.lbl_GameOver.AutoSize = true;
            this.lbl_GameOver.BackColor = System.Drawing.Color.Transparent;
            this.lbl_GameOver.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GameOver.ForeColor = System.Drawing.Color.Red;
            this.lbl_GameOver.Location = new System.Drawing.Point(245, 285);
            this.lbl_GameOver.Name = "lbl_GameOver";
            this.lbl_GameOver.Size = new System.Drawing.Size(504, 117);
            this.lbl_GameOver.TabIndex = 5;
            this.lbl_GameOver.Text = "Game Over!";
            this.lbl_GameOver.Visible = false;
            // 
            // lbl_PressPause
            // 
            this.lbl_PressPause.AutoSize = true;
            this.lbl_PressPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PressPause.ForeColor = System.Drawing.Color.White;
            this.lbl_PressPause.Location = new System.Drawing.Point(12, 9);
            this.lbl_PressPause.Name = "lbl_PressPause";
            this.lbl_PressPause.Size = new System.Drawing.Size(218, 25);
            this.lbl_PressPause.TabIndex = 6;
            this.lbl_PressPause.Text = "Press Esc to Pause";
            // 
            // lbl_Highscore
            // 
            this.lbl_Highscore.AutoSize = true;
            this.lbl_Highscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Highscore.ForeColor = System.Drawing.Color.White;
            this.lbl_Highscore.Location = new System.Drawing.Point(751, 43);
            this.lbl_Highscore.Name = "lbl_Highscore";
            this.lbl_Highscore.Size = new System.Drawing.Size(40, 42);
            this.lbl_Highscore.TabIndex = 8;
            this.lbl_Highscore.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(553, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 42);
            this.label3.TabIndex = 7;
            this.label3.Text = "Highscore:";
            // 
            // Gameplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(884, 626);
            this.Controls.Add(this.lbl_Highscore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_PressPause);
            this.Controls.Add(this.lbl_GameOver);
            this.Controls.Add(this.lbl_Lives);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_Score);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Gameplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NOT Pac-Man";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Gameplay_FormClosed);
            this.Load += new System.EventHandler(this.Gameplay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Gameplay_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer TimerUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Score;
        private System.Windows.Forms.Label lbl_Lives;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_GameOver;
        private System.Windows.Forms.Label lbl_PressPause;
        private System.Windows.Forms.Label lbl_Highscore;
        private System.Windows.Forms.Label label3;
    }
}