namespace SerchAndNotDestroy
{
    partial class MyLittleMonion
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FindButton = new System.Windows.Forms.Button();
            this.pictureBoxForModelForSearch = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SaveImage = new System.Windows.Forms.Button();
            this.buttonNextForColorsForIgnor = new System.Windows.Forms.Button();
            this.buttonPreviousForColorsForIgnor = new System.Windows.Forms.Button();
            this.FindNextButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.labelForNameForIgnorColor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelForStatus = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxForIgnorColor = new System.Windows.Forms.PictureBox();
            this.panelForColorsForIgnor = new System.Windows.Forms.Panel();
            this.labelForNumberOfIgnorColor = new System.Windows.Forms.Label();
            this.buttonForDeleteSelectedColor = new System.Windows.Forms.Button();
            this.buttonAddManyColorsForIgnor = new System.Windows.Forms.Button();
            this.buttonAddSingleColorForIgnor = new System.Windows.Forms.Button();
            this.checkBoxForColorsForIgnor = new System.Windows.Forms.CheckBox();
            this.panelForModelForSearch = new System.Windows.Forms.Panel();
            this.buttonForChangeSizeOferyBigModelForSearch = new System.Windows.Forms.Button();
            this.buttonAddModelForSearch = new System.Windows.Forms.Button();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelMousePosiotonView = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForModelForSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIgnorColor)).BeginInit();
            this.panelForColorsForIgnor.SuspendLayout();
            this.panelForModelForSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(9, 10);
            this.FindButton.Margin = new System.Windows.Forms.Padding(2);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(68, 46);
            this.FindButton.TabIndex = 0;
            this.FindButton.Text = "Найти";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // pictureBoxForModelForSearch
            // 
            this.pictureBoxForModelForSearch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBoxForModelForSearch.Location = new System.Drawing.Point(166, 95);
            this.pictureBoxForModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxForModelForSearch.Name = "pictureBoxForModelForSearch";
            this.pictureBoxForModelForSearch.Size = new System.Drawing.Size(75, 41);
            this.pictureBoxForModelForSearch.TabIndex = 1;
            this.pictureBoxForModelForSearch.TabStop = false;
            this.pictureBoxForModelForSearch.Click += new System.EventHandler(this.PictureBoxForModelForSearch_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // SaveImage
            // 
            this.SaveImage.Enabled = false;
            this.SaveImage.Location = new System.Drawing.Point(8, 117);
            this.SaveImage.Margin = new System.Windows.Forms.Padding(2);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(120, 34);
            this.SaveImage.TabIndex = 2;
            this.SaveImage.Text = "Сохранить";
            this.SaveImage.UseVisualStyleBackColor = true;
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // buttonNextForColorsForIgnor
            // 
            this.buttonNextForColorsForIgnor.Enabled = false;
            this.buttonNextForColorsForIgnor.Location = new System.Drawing.Point(70, 97);
            this.buttonNextForColorsForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNextForColorsForIgnor.Name = "buttonNextForColorsForIgnor";
            this.buttonNextForColorsForIgnor.Size = new System.Drawing.Size(56, 40);
            this.buttonNextForColorsForIgnor.TabIndex = 3;
            this.buttonNextForColorsForIgnor.Text = "Вперед";
            this.buttonNextForColorsForIgnor.UseVisualStyleBackColor = true;
            this.buttonNextForColorsForIgnor.Click += new System.EventHandler(this.Next_Click);
            // 
            // buttonPreviousForColorsForIgnor
            // 
            this.buttonPreviousForColorsForIgnor.Enabled = false;
            this.buttonPreviousForColorsForIgnor.Location = new System.Drawing.Point(10, 97);
            this.buttonPreviousForColorsForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPreviousForColorsForIgnor.Name = "buttonPreviousForColorsForIgnor";
            this.buttonPreviousForColorsForIgnor.Size = new System.Drawing.Size(56, 40);
            this.buttonPreviousForColorsForIgnor.TabIndex = 4;
            this.buttonPreviousForColorsForIgnor.Text = "Назад";
            this.buttonPreviousForColorsForIgnor.UseVisualStyleBackColor = true;
            this.buttonPreviousForColorsForIgnor.Click += new System.EventHandler(this.Prev_Click);
            // 
            // FindNextButton
            // 
            this.FindNextButton.Enabled = false;
            this.FindNextButton.Location = new System.Drawing.Point(9, 65);
            this.FindNextButton.Margin = new System.Windows.Forms.Padding(2);
            this.FindNextButton.Name = "FindNextButton";
            this.FindNextButton.Size = new System.Drawing.Size(120, 47);
            this.FindNextButton.TabIndex = 5;
            this.FindNextButton.Text = "Найти по очереди";
            this.FindNextButton.UseVisualStyleBackColor = true;
            this.FindNextButton.Click += new System.EventHandler(this.FindNextButton_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(8, 292);
            this.TestButton.Margin = new System.Windows.Forms.Padding(2);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 58);
            this.TestButton.TabIndex = 6;
            this.TestButton.Text = "TestButton";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // labelForNameForIgnorColor
            // 
            this.labelForNameForIgnorColor.AutoSize = true;
            this.labelForNameForIgnorColor.Location = new System.Drawing.Point(8, 10);
            this.labelForNameForIgnorColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelForNameForIgnorColor.Name = "labelForNameForIgnorColor";
            this.labelForNameForIgnorColor.Size = new System.Drawing.Size(142, 13);
            this.labelForNameForIgnorColor.TabIndex = 7;
            this.labelForNameForIgnorColor.Text = "Цвета для игнорирования:";
            this.labelForNameForIgnorColor.Click += new System.EventHandler(this.Label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Эталон(картинка) для поиска";
            // 
            // labelForStatus
            // 
            this.labelForStatus.AutoSize = true;
            this.labelForStatus.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForStatus.Location = new System.Drawing.Point(138, 10);
            this.labelForStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelForStatus.Name = "labelForStatus";
            this.labelForStatus.Size = new System.Drawing.Size(205, 22);
            this.labelForStatus.TabIndex = 10;
            this.labelForStatus.Text = "Поиск не выполняется";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(116, 292);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(75, 41);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.PictureBox2_Click);
            // 
            // pictureBoxForIgnorColor
            // 
            this.pictureBoxForIgnorColor.BackColor = System.Drawing.Color.DarkSalmon;
            this.pictureBoxForIgnorColor.Location = new System.Drawing.Point(10, 26);
            this.pictureBoxForIgnorColor.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxForIgnorColor.Name = "pictureBoxForIgnorColor";
            this.pictureBoxForIgnorColor.Size = new System.Drawing.Size(117, 41);
            this.pictureBoxForIgnorColor.TabIndex = 13;
            this.pictureBoxForIgnorColor.TabStop = false;
            // 
            // panelForColorsForIgnor
            // 
            this.panelForColorsForIgnor.BackColor = System.Drawing.Color.DarkSalmon;
            this.panelForColorsForIgnor.Controls.Add(this.labelForNumberOfIgnorColor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonForDeleteSelectedColor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonAddManyColorsForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonAddSingleColorForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonPreviousForColorsForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.pictureBoxForIgnorColor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonNextForColorsForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.labelForNameForIgnorColor);
            this.panelForColorsForIgnor.Location = new System.Drawing.Point(520, 46);
            this.panelForColorsForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.panelForColorsForIgnor.Name = "panelForColorsForIgnor";
            this.panelForColorsForIgnor.Size = new System.Drawing.Size(340, 181);
            this.panelForColorsForIgnor.TabIndex = 14;
            this.panelForColorsForIgnor.Visible = false;
            // 
            // labelForNumberOfIgnorColor
            // 
            this.labelForNumberOfIgnorColor.AutoSize = true;
            this.labelForNumberOfIgnorColor.Location = new System.Drawing.Point(10, 72);
            this.labelForNumberOfIgnorColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelForNumberOfIgnorColor.Name = "labelForNumberOfIgnorColor";
            this.labelForNumberOfIgnorColor.Size = new System.Drawing.Size(67, 13);
            this.labelForNumberOfIgnorColor.TabIndex = 17;
            this.labelForNumberOfIgnorColor.Text = "Нет цветов.";
            // 
            // buttonForDeleteSelectedColor
            // 
            this.buttonForDeleteSelectedColor.Enabled = false;
            this.buttonForDeleteSelectedColor.Location = new System.Drawing.Point(132, 116);
            this.buttonForDeleteSelectedColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonForDeleteSelectedColor.Name = "buttonForDeleteSelectedColor";
            this.buttonForDeleteSelectedColor.Size = new System.Drawing.Size(194, 45);
            this.buttonForDeleteSelectedColor.TabIndex = 16;
            this.buttonForDeleteSelectedColor.Text = "Удалить выбраный цвет";
            this.buttonForDeleteSelectedColor.UseVisualStyleBackColor = true;
            this.buttonForDeleteSelectedColor.Click += new System.EventHandler(this.ButtonForDeleteSelectedColor_Click);
            // 
            // buttonAddManyColorsForIgnor
            // 
            this.buttonAddManyColorsForIgnor.Location = new System.Drawing.Point(132, 72);
            this.buttonAddManyColorsForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddManyColorsForIgnor.Name = "buttonAddManyColorsForIgnor";
            this.buttonAddManyColorsForIgnor.Size = new System.Drawing.Size(194, 41);
            this.buttonAddManyColorsForIgnor.TabIndex = 15;
            this.buttonAddManyColorsForIgnor.Text = "Добавить много цветов";
            this.buttonAddManyColorsForIgnor.UseVisualStyleBackColor = true;
            this.buttonAddManyColorsForIgnor.Click += new System.EventHandler(this.ButtonAddManyColorsForIgnor_Click);
            // 
            // buttonAddSingleColorForIgnor
            // 
            this.buttonAddSingleColorForIgnor.Location = new System.Drawing.Point(132, 27);
            this.buttonAddSingleColorForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddSingleColorForIgnor.Name = "buttonAddSingleColorForIgnor";
            this.buttonAddSingleColorForIgnor.Size = new System.Drawing.Size(194, 41);
            this.buttonAddSingleColorForIgnor.TabIndex = 14;
            this.buttonAddSingleColorForIgnor.Text = "Добавить один цвет";
            this.buttonAddSingleColorForIgnor.UseVisualStyleBackColor = true;
            this.buttonAddSingleColorForIgnor.Click += new System.EventHandler(this.ButtonAddSingleColorForIgnor_Click);
            // 
            // checkBoxForColorsForIgnor
            // 
            this.checkBoxForColorsForIgnor.AutoSize = true;
            this.checkBoxForColorsForIgnor.Location = new System.Drawing.Point(520, 22);
            this.checkBoxForColorsForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxForColorsForIgnor.Name = "checkBoxForColorsForIgnor";
            this.checkBoxForColorsForIgnor.Size = new System.Drawing.Size(208, 17);
            this.checkBoxForColorsForIgnor.TabIndex = 15;
            this.checkBoxForColorsForIgnor.Text = "Включить цвета для игнорирования";
            this.checkBoxForColorsForIgnor.UseVisualStyleBackColor = true;
            this.checkBoxForColorsForIgnor.CheckedChanged += new System.EventHandler(this.CheckBoxForColorsForIgnor_CheckedChanged);
            // 
            // panelForModelForSearch
            // 
            this.panelForModelForSearch.BackColor = System.Drawing.Color.LightCoral;
            this.panelForModelForSearch.Controls.Add(this.buttonForChangeSizeOferyBigModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.buttonAddModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.pictureBoxForModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.label2);
            this.panelForModelForSearch.Location = new System.Drawing.Point(140, 46);
            this.panelForModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.panelForModelForSearch.Name = "panelForModelForSearch";
            this.panelForModelForSearch.Size = new System.Drawing.Size(375, 205);
            this.panelForModelForSearch.TabIndex = 16;
            // 
            // buttonForChangeSizeOferyBigModelForSearch
            // 
            this.buttonForChangeSizeOferyBigModelForSearch.Location = new System.Drawing.Point(11, 24);
            this.buttonForChangeSizeOferyBigModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonForChangeSizeOferyBigModelForSearch.Name = "buttonForChangeSizeOferyBigModelForSearch";
            this.buttonForChangeSizeOferyBigModelForSearch.Size = new System.Drawing.Size(152, 40);
            this.buttonForChangeSizeOferyBigModelForSearch.TabIndex = 10;
            this.buttonForChangeSizeOferyBigModelForSearch.Text = "Растянуть панель для большого эталона";
            this.buttonForChangeSizeOferyBigModelForSearch.UseVisualStyleBackColor = true;
            this.buttonForChangeSizeOferyBigModelForSearch.Click += new System.EventHandler(this.ButtonForChangeSizeOferyBigModelForSearch_Click);
            // 
            // buttonAddModelForSearch
            // 
            this.buttonAddModelForSearch.Location = new System.Drawing.Point(11, 68);
            this.buttonAddModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddModelForSearch.Name = "buttonAddModelForSearch";
            this.buttonAddModelForSearch.Size = new System.Drawing.Size(72, 41);
            this.buttonAddModelForSearch.TabIndex = 9;
            this.buttonAddModelForSearch.Text = "Изменить эталон";
            this.buttonAddModelForSearch.UseVisualStyleBackColor = true;
            this.buttonAddModelForSearch.Click += new System.EventHandler(this.ButtonAddModelForSearch_Click);
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(8, 224);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(49, 20);
            this.textBoxX.TabIndex = 17;
            this.textBoxX.Text = "0";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(63, 224);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(49, 20);
            this.textBoxY.TabIndex = 18;
            this.textBoxY.Text = "0";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(8, 250);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(49, 20);
            this.textBoxWidth.TabIndex = 19;
            this.textBoxWidth.Text = "0";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(63, 250);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(49, 20);
            this.textBoxHeight.TabIndex = 20;
            this.textBoxHeight.Text = "0";
            // 
            // labelMousePosiotonView
            // 
            this.labelMousePosiotonView.AutoSize = true;
            this.labelMousePosiotonView.Location = new System.Drawing.Point(8, 193);
            this.labelMousePosiotonView.Name = "labelMousePosiotonView";
            this.labelMousePosiotonView.Size = new System.Drawing.Size(125, 13);
            this.labelMousePosiotonView.TabIndex = 21;
            this.labelMousePosiotonView.Text = "labelMousePosiotonView";
            // 
            // MyLittleMonion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(881, 504);
            this.Controls.Add(this.labelMousePosiotonView);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.panelForModelForSearch);
            this.Controls.Add(this.checkBoxForColorsForIgnor);
            this.Controls.Add(this.panelForColorsForIgnor);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelForStatus);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.FindNextButton);
            this.Controls.Add(this.SaveImage);
            this.Controls.Add(this.FindButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MyLittleMonion";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Мой маленький помощник";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForModelForSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIgnorColor)).EndInit();
            this.panelForColorsForIgnor.ResumeLayout(false);
            this.panelForColorsForIgnor.PerformLayout();
            this.panelForModelForSearch.ResumeLayout(false);
            this.panelForModelForSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.PictureBox pictureBoxForModelForSearch;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button SaveImage;
        private System.Windows.Forms.Button buttonNextForColorsForIgnor;
        private System.Windows.Forms.Button buttonPreviousForColorsForIgnor;
        private System.Windows.Forms.Button FindNextButton;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Label labelForNameForIgnorColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelForStatus;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBoxForIgnorColor;
        private System.Windows.Forms.Panel panelForColorsForIgnor;
        private System.Windows.Forms.CheckBox checkBoxForColorsForIgnor;
        private System.Windows.Forms.Button buttonForDeleteSelectedColor;
        private System.Windows.Forms.Button buttonAddManyColorsForIgnor;
        private System.Windows.Forms.Button buttonAddSingleColorForIgnor;
        private System.Windows.Forms.Label labelForNumberOfIgnorColor;
        private System.Windows.Forms.Panel panelForModelForSearch;
        private System.Windows.Forms.Button buttonAddModelForSearch;
        private System.Windows.Forms.Button buttonForChangeSizeOferyBigModelForSearch;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelMousePosiotonView;
    }
}

