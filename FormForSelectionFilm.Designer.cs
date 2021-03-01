namespace SerchAndNotDestroy
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
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectionFilm)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSelectionFilm
            // 
            this.pbSelectionFilm.BackColor = System.Drawing.Color.White;
            this.pbSelectionFilm.Location = new System.Drawing.Point(70, 84);
            this.pbSelectionFilm.Name = "pbSelectionFilm";
            this.pbSelectionFilm.Size = new System.Drawing.Size(100, 50);
            this.pbSelectionFilm.TabIndex = 0;
            this.pbSelectionFilm.TabStop = false;
            this.pbSelectionFilm.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // FormForSelectionFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbSelectionFilm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormForSelectionFilm";
            this.Opacity = 0.3D;
            this.Text = "FormForSelectionFilm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectionFilm_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectionFilm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectionFilm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSelectionFilm;
        private System.Windows.Forms.Timer timer1;
    }
}