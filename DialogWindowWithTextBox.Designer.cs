namespace MyLittleMinion
{
    partial class DialogWindowForSetting
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
            this.textBoxOfPathForSaveFile = new System.Windows.Forms.TextBox();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.buttonChangePathForSaveFile = new System.Windows.Forms.Button();
            this.buttonCancelChanges = new System.Windows.Forms.Button();
            this.buttonSetDefaultSettings = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSetWhiteColorForFone = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxOfPathForSaveFile
            // 
            this.textBoxOfPathForSaveFile.Location = new System.Drawing.Point(17, 15);
            this.textBoxOfPathForSaveFile.Name = "textBoxOfPathForSaveFile";
            this.textBoxOfPathForSaveFile.Size = new System.Drawing.Size(646, 20);
            this.textBoxOfPathForSaveFile.TabIndex = 0;
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Location = new System.Drawing.Point(669, 130);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(123, 45);
            this.buttonSaveChanges.TabIndex = 1;
            this.buttonSaveChanges.Text = "Сохранить изменения";
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.ButtonSaveChanges_Click);
            // 
            // buttonChangePathForSaveFile
            // 
            this.buttonChangePathForSaveFile.Location = new System.Drawing.Point(669, 15);
            this.buttonChangePathForSaveFile.Name = "buttonChangePathForSaveFile";
            this.buttonChangePathForSaveFile.Size = new System.Drawing.Size(123, 23);
            this.buttonChangePathForSaveFile.TabIndex = 2;
            this.buttonChangePathForSaveFile.Text = "Изменить путь";
            this.buttonChangePathForSaveFile.UseVisualStyleBackColor = true;
            this.buttonChangePathForSaveFile.Click += new System.EventHandler(this.ButtonChangePathForSaveFile_Click);
            // 
            // buttonCancelChanges
            // 
            this.buttonCancelChanges.Location = new System.Drawing.Point(540, 130);
            this.buttonCancelChanges.Name = "buttonCancelChanges";
            this.buttonCancelChanges.Size = new System.Drawing.Size(123, 45);
            this.buttonCancelChanges.TabIndex = 3;
            this.buttonCancelChanges.Text = "Отменить изменения";
            this.buttonCancelChanges.UseVisualStyleBackColor = true;
            this.buttonCancelChanges.Click += new System.EventHandler(this.ButtonCancelChanges_Click);
            // 
            // buttonSetDefaultSettings
            // 
            this.buttonSetDefaultSettings.Location = new System.Drawing.Point(411, 130);
            this.buttonSetDefaultSettings.Name = "buttonSetDefaultSettings";
            this.buttonSetDefaultSettings.Size = new System.Drawing.Size(123, 45);
            this.buttonSetDefaultSettings.TabIndex = 4;
            this.buttonSetDefaultSettings.Text = "Вернуть настройки по умолчанию";
            this.buttonSetDefaultSettings.UseVisualStyleBackColor = true;
            this.buttonSetDefaultSettings.Click += new System.EventHandler(this.ButtonSetDefaultSettings_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Goldenrod;
            this.panel2.Controls.Add(this.buttonSetWhiteColorForFone);
            this.panel2.Controls.Add(this.buttonChangePathForSaveFile);
            this.panel2.Controls.Add(this.buttonSetDefaultSettings);
            this.panel2.Controls.Add(this.textBoxOfPathForSaveFile);
            this.panel2.Controls.Add(this.buttonCancelChanges);
            this.panel2.Controls.Add(this.buttonSaveChanges);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(811, 192);
            this.panel2.TabIndex = 5;
            // 
            // buttonSetWhiteColorForFone
            // 
            this.buttonSetWhiteColorForFone.Location = new System.Drawing.Point(17, 42);
            this.buttonSetWhiteColorForFone.Name = "buttonSetWhiteColorForFone";
            this.buttonSetWhiteColorForFone.Size = new System.Drawing.Size(130, 34);
            this.buttonSetWhiteColorForFone.TabIndex = 5;
            this.buttonSetWhiteColorForFone.Text = "Установить белый цвет на фон";
            this.buttonSetWhiteColorForFone.UseVisualStyleBackColor = true;
            this.buttonSetWhiteColorForFone.Click += new System.EventHandler(this.ButtonSetWhiteColorForFone_Click);
            // 
            // DialogWindowForSetting
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(835, 216);
            this.Controls.Add(this.panel2);
            this.Name = "DialogWindowForSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOfPathForSaveFile;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.Button buttonChangePathForSaveFile;
        private System.Windows.Forms.Button buttonCancelChanges;
        private System.Windows.Forms.Button buttonSetDefaultSettings;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSetWhiteColorForFone;
    }
}