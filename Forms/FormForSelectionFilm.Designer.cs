﻿namespace MyLittleMinion
{
    partial class FormForSelectionFilm
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
            this.pbSelectionFilm = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelBackGroundImage = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectionFilm)).BeginInit();
            this.panelBackGroundImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbSelectionFilm
            // 
            this.pbSelectionFilm.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pbSelectionFilm.Location = new System.Drawing.Point(40, 43);
            this.pbSelectionFilm.Margin = new System.Windows.Forms.Padding(2);
            this.pbSelectionFilm.Name = "pbSelectionFilm";
            this.pbSelectionFilm.Size = new System.Drawing.Size(180, 112);
            this.pbSelectionFilm.TabIndex = 0;
            this.pbSelectionFilm.TabStop = false;
            this.pbSelectionFilm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbSelectionFilm_MouseDown);
            this.pbSelectionFilm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PbSelectionFilm_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panelBackGroundImage
            // 
            this.panelBackGroundImage.Controls.Add(this.pbSelectionFilm);
            this.panelBackGroundImage.Location = new System.Drawing.Point(96, 66);
            this.panelBackGroundImage.Name = "panelBackGroundImage";
            this.panelBackGroundImage.Size = new System.Drawing.Size(312, 246);
            this.panelBackGroundImage.TabIndex = 1;
            // 
            // FormForSelectionFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.panelBackGroundImage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormForSelectionFilm";
            this.Opacity = 0.3D;
            this.Text = "FormForSelectionFilm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormForSelectionFilm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectionFilm)).EndInit();
            this.panelBackGroundImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSelectionFilm;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelBackGroundImage;
    }
}