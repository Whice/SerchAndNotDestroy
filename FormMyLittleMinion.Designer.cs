namespace MyLittleMinion
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
            this.buttonSaveListOfAction = new System.Windows.Forms.Button();
            this.buttonNextForColorsForIgnor = new System.Windows.Forms.Button();
            this.buttonPreviousForColorsForIgnor = new System.Windows.Forms.Button();
            this.buttonFindAndPerformAllAction = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.labelForNameForIgnorColor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelForStatus = new System.Windows.Forms.Label();
            this.pictureBoxForIgnorColor = new System.Windows.Forms.PictureBox();
            this.panelForColorsForIgnor = new System.Windows.Forms.Panel();
            this.numericUpDownCountSecondForAddIgnorColor = new System.Windows.Forms.NumericUpDown();
            this.labelCountSecondForAddIgnorColor = new System.Windows.Forms.Label();
            this.labelForNumberOfIgnorColor = new System.Windows.Forms.Label();
            this.buttonForDeleteSelectedColor = new System.Windows.Forms.Button();
            this.buttonAddManyColorsForIgnor = new System.Windows.Forms.Button();
            this.buttonAddSingleColorForIgnor = new System.Windows.Forms.Button();
            this.checkBoxForColorsForIgnor = new System.Windows.Forms.CheckBox();
            this.panelForModelForSearch = new System.Windows.Forms.Panel();
            this.checkBoxShowNotCorrectModel = new System.Windows.Forms.CheckBox();
            this.buttonCorrectModelForSearch = new System.Windows.Forms.Button();
            this.labelPercentageComplianceWithModel = new System.Windows.Forms.Label();
            this.numericUpDownPercentageComplianceWithModel = new System.Windows.Forms.NumericUpDown();
            this.textBoxCountOfThreads = new System.Windows.Forms.TextBox();
            this.checkBoxCountOfThreads = new System.Windows.Forms.CheckBox();
            this.checkBoxFirstFoundModelIsEnd = new System.Windows.Forms.CheckBox();
            this.checkBoxParallelSearch = new System.Windows.Forms.CheckBox();
            this.buttonForChangeSizeOferyBigModelForSearch = new System.Windows.Forms.Button();
            this.buttonAddModelForSearch = new System.Windows.Forms.Button();
            this.textBoxXBegin = new System.Windows.Forms.TextBox();
            this.textBoxYBegin = new System.Windows.Forms.TextBox();
            this.textBoxXEnd = new System.Windows.Forms.TextBox();
            this.textBoxYEnd = new System.Windows.Forms.TextBox();
            this.labelMousePosiotonView = new System.Windows.Forms.Label();
            this.checkBoxForPlaceOfSearch = new System.Windows.Forms.CheckBox();
            this.panelForPlaceOfSearch = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSelectActiveWindow = new System.Windows.Forms.CheckBox();
            this.panelConfigurationOfSearch = new System.Windows.Forms.Panel();
            this.buttonOpenListOfAction = new System.Windows.Forms.Button();
            this.comboBoxForSelectAction = new System.Windows.Forms.ComboBox();
            this.buttonFindAndPerformThisAction = new System.Windows.Forms.Button();
            this.buttonPrevAction = new System.Windows.Forms.Button();
            this.buttonNextAction = new System.Windows.Forms.Button();
            this.buttonAddAction = new System.Windows.Forms.Button();
            this.labelNumberOfSearchAndAction = new System.Windows.Forms.Label();
            this.labelWaitAfterThisAction = new System.Windows.Forms.Label();
            this.numericUpDownWaitAfterThisAction = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelNameOfLisActions = new System.Windows.Forms.Label();
            this.textBoxNameOfLisActions = new System.Windows.Forms.TextBox();
            this.buttonCloneThisAction = new System.Windows.Forms.Button();
            this.pictureBoxForCorrectModelForSearch = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForModelForSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIgnorColor)).BeginInit();
            this.panelForColorsForIgnor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountSecondForAddIgnorColor)).BeginInit();
            this.panelForModelForSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercentageComplianceWithModel)).BeginInit();
            this.panelForPlaceOfSearch.SuspendLayout();
            this.panelConfigurationOfSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitAfterThisAction)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForCorrectModelForSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(12, 11);
            this.FindButton.Margin = new System.Windows.Forms.Padding(2);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(86, 46);
            this.FindButton.TabIndex = 0;
            this.FindButton.Text = "Попробовать найти";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // pictureBoxForModelForSearch
            // 
            this.pictureBoxForModelForSearch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBoxForModelForSearch.Location = new System.Drawing.Point(191, 151);
            this.pictureBoxForModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxForModelForSearch.Name = "pictureBoxForModelForSearch";
            this.pictureBoxForModelForSearch.Size = new System.Drawing.Size(75, 41);
            this.pictureBoxForModelForSearch.TabIndex = 1;
            this.pictureBoxForModelForSearch.TabStop = false;
            this.pictureBoxForModelForSearch.Visible = false;
            this.pictureBoxForModelForSearch.Click += new System.EventHandler(this.PictureBoxForModelForSearch_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // buttonSaveListOfAction
            // 
            this.buttonSaveListOfAction.Enabled = false;
            this.buttonSaveListOfAction.Location = new System.Drawing.Point(8, 395);
            this.buttonSaveListOfAction.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveListOfAction.Name = "buttonSaveListOfAction";
            this.buttonSaveListOfAction.Size = new System.Drawing.Size(120, 39);
            this.buttonSaveListOfAction.TabIndex = 2;
            this.buttonSaveListOfAction.Text = "Сохранить список действий";
            this.buttonSaveListOfAction.UseVisualStyleBackColor = true;
            this.buttonSaveListOfAction.Click += new System.EventHandler(this.SaveImage_Click);
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
            // buttonFindAndPerformAllAction
            // 
            this.buttonFindAndPerformAllAction.Location = new System.Drawing.Point(5, 36);
            this.buttonFindAndPerformAllAction.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFindAndPerformAllAction.Name = "buttonFindAndPerformAllAction";
            this.buttonFindAndPerformAllAction.Size = new System.Drawing.Size(188, 47);
            this.buttonFindAndPerformAllAction.TabIndex = 5;
            this.buttonFindAndPerformAllAction.Text = "Найти и выполнить все действия";
            this.buttonFindAndPerformAllAction.UseVisualStyleBackColor = true;
            this.buttonFindAndPerformAllAction.Click += new System.EventHandler(this.FindNextButton_Click);
            // 
            // TestButton
            // 
            this.TestButton.Enabled = false;
            this.TestButton.Location = new System.Drawing.Point(8, 504);
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
            this.labelForNameForIgnorColor.Location = new System.Drawing.Point(-1, 7);
            this.labelForNameForIgnorColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelForNameForIgnorColor.Name = "labelForNameForIgnorColor";
            this.labelForNameForIgnorColor.Size = new System.Drawing.Size(142, 13);
            this.labelForNameForIgnorColor.TabIndex = 7;
            this.labelForNameForIgnorColor.Text = "Цвета для игнорирования:";
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
            this.labelForStatus.Location = new System.Drawing.Point(109, 11);
            this.labelForStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelForStatus.Name = "labelForStatus";
            this.labelForStatus.Size = new System.Drawing.Size(205, 22);
            this.labelForStatus.TabIndex = 10;
            this.labelForStatus.Text = "Поиск не выполняется";
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
            this.panelForColorsForIgnor.Controls.Add(this.numericUpDownCountSecondForAddIgnorColor);
            this.panelForColorsForIgnor.Controls.Add(this.labelCountSecondForAddIgnorColor);
            this.panelForColorsForIgnor.Controls.Add(this.labelForNumberOfIgnorColor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonForDeleteSelectedColor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonAddManyColorsForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonAddSingleColorForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonPreviousForColorsForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.pictureBoxForIgnorColor);
            this.panelForColorsForIgnor.Controls.Add(this.buttonNextForColorsForIgnor);
            this.panelForColorsForIgnor.Controls.Add(this.labelForNameForIgnorColor);
            this.panelForColorsForIgnor.Location = new System.Drawing.Point(490, 68);
            this.panelForColorsForIgnor.Margin = new System.Windows.Forms.Padding(2);
            this.panelForColorsForIgnor.Name = "panelForColorsForIgnor";
            this.panelForColorsForIgnor.Size = new System.Drawing.Size(340, 205);
            this.panelForColorsForIgnor.TabIndex = 14;
            this.panelForColorsForIgnor.Visible = false;
            // 
            // numericUpDownCountSecondForAddIgnorColor
            // 
            this.numericUpDownCountSecondForAddIgnorColor.Location = new System.Drawing.Point(280, 14);
            this.numericUpDownCountSecondForAddIgnorColor.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownCountSecondForAddIgnorColor.Name = "numericUpDownCountSecondForAddIgnorColor";
            this.numericUpDownCountSecondForAddIgnorColor.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownCountSecondForAddIgnorColor.TabIndex = 20;
            this.numericUpDownCountSecondForAddIgnorColor.UseWaitCursor = true;
            this.numericUpDownCountSecondForAddIgnorColor.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // labelCountSecondForAddIgnorColor
            // 
            this.labelCountSecondForAddIgnorColor.AutoSize = true;
            this.labelCountSecondForAddIgnorColor.Location = new System.Drawing.Point(153, 16);
            this.labelCountSecondForAddIgnorColor.Name = "labelCountSecondForAddIgnorColor";
            this.labelCountSecondForAddIgnorColor.Size = new System.Drawing.Size(121, 13);
            this.labelCountSecondForAddIgnorColor.TabIndex = 19;
            this.labelCountSecondForAddIgnorColor.Text = "Секунд до добавления";
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
            this.buttonForDeleteSelectedColor.Location = new System.Drawing.Point(131, 128);
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
            this.buttonAddManyColorsForIgnor.Location = new System.Drawing.Point(131, 84);
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
            this.buttonAddSingleColorForIgnor.Location = new System.Drawing.Point(131, 39);
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
            this.checkBoxForColorsForIgnor.Location = new System.Drawing.Point(490, 44);
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
            this.panelForModelForSearch.Controls.Add(this.pictureBoxForCorrectModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.checkBoxShowNotCorrectModel);
            this.panelForModelForSearch.Controls.Add(this.buttonCorrectModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.labelPercentageComplianceWithModel);
            this.panelForModelForSearch.Controls.Add(this.numericUpDownPercentageComplianceWithModel);
            this.panelForModelForSearch.Controls.Add(this.textBoxCountOfThreads);
            this.panelForModelForSearch.Controls.Add(this.checkBoxCountOfThreads);
            this.panelForModelForSearch.Controls.Add(this.checkBoxFirstFoundModelIsEnd);
            this.panelForModelForSearch.Controls.Add(this.checkBoxParallelSearch);
            this.panelForModelForSearch.Controls.Add(this.buttonForChangeSizeOferyBigModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.buttonAddModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.pictureBoxForModelForSearch);
            this.panelForModelForSearch.Controls.Add(this.label2);
            this.panelForModelForSearch.Location = new System.Drawing.Point(112, 207);
            this.panelForModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.panelForModelForSearch.Name = "panelForModelForSearch";
            this.panelForModelForSearch.Size = new System.Drawing.Size(375, 235);
            this.panelForModelForSearch.TabIndex = 16;
            // 
            // checkBoxShowNotCorrectModel
            // 
            this.checkBoxShowNotCorrectModel.AutoSize = true;
            this.checkBoxShowNotCorrectModel.Location = new System.Drawing.Point(137, 126);
            this.checkBoxShowNotCorrectModel.Name = "checkBoxShowNotCorrectModel";
            this.checkBoxShowNotCorrectModel.Size = new System.Drawing.Size(234, 17);
            this.checkBoxShowNotCorrectModel.TabIndex = 27;
            this.checkBoxShowNotCorrectModel.Text = "Показать не скорректированный эталон";
            this.checkBoxShowNotCorrectModel.UseVisualStyleBackColor = true;
            this.checkBoxShowNotCorrectModel.CheckedChanged += new System.EventHandler(this.CheckBoxShowCorrectModel_CheckedChanged);
            // 
            // buttonCorrectModelForSearch
            // 
            this.buttonCorrectModelForSearch.Location = new System.Drawing.Point(11, 113);
            this.buttonCorrectModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCorrectModelForSearch.Name = "buttonCorrectModelForSearch";
            this.buttonCorrectModelForSearch.Size = new System.Drawing.Size(120, 41);
            this.buttonCorrectModelForSearch.TabIndex = 26;
            this.buttonCorrectModelForSearch.Text = "Скорректировать эталон";
            this.buttonCorrectModelForSearch.UseVisualStyleBackColor = true;
            this.buttonCorrectModelForSearch.Click += new System.EventHandler(this.ButtonCorrectModelForSearch_Click);
            // 
            // labelPercentageComplianceWithModel
            // 
            this.labelPercentageComplianceWithModel.AutoSize = true;
            this.labelPercentageComplianceWithModel.Location = new System.Drawing.Point(9, 156);
            this.labelPercentageComplianceWithModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPercentageComplianceWithModel.Name = "labelPercentageComplianceWithModel";
            this.labelPercentageComplianceWithModel.Size = new System.Drawing.Size(183, 13);
            this.labelPercentageComplianceWithModel.TabIndex = 25;
            this.labelPercentageComplianceWithModel.Text = "Процентное соответствие эталону";
            // 
            // numericUpDownPercentageComplianceWithModel
            // 
            this.numericUpDownPercentageComplianceWithModel.Location = new System.Drawing.Point(11, 172);
            this.numericUpDownPercentageComplianceWithModel.Name = "numericUpDownPercentageComplianceWithModel";
            this.numericUpDownPercentageComplianceWithModel.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownPercentageComplianceWithModel.TabIndex = 24;
            this.numericUpDownPercentageComplianceWithModel.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPercentageComplianceWithModel.ValueChanged += new System.EventHandler(this.NumericUpDownPercentageComplianceWithModel_ValueChanged);
            // 
            // textBoxCountOfThreads
            // 
            this.textBoxCountOfThreads.Enabled = false;
            this.textBoxCountOfThreads.Location = new System.Drawing.Point(166, 64);
            this.textBoxCountOfThreads.Name = "textBoxCountOfThreads";
            this.textBoxCountOfThreads.Size = new System.Drawing.Size(100, 20);
            this.textBoxCountOfThreads.TabIndex = 14;
            this.textBoxCountOfThreads.Text = "1";
            // 
            // checkBoxCountOfThreads
            // 
            this.checkBoxCountOfThreads.AutoSize = true;
            this.checkBoxCountOfThreads.Enabled = false;
            this.checkBoxCountOfThreads.Location = new System.Drawing.Point(166, 47);
            this.checkBoxCountOfThreads.Name = "checkBoxCountOfThreads";
            this.checkBoxCountOfThreads.Size = new System.Drawing.Size(167, 17);
            this.checkBoxCountOfThreads.TabIndex = 13;
            this.checkBoxCountOfThreads.Text = "Задать количество потоков";
            this.checkBoxCountOfThreads.UseVisualStyleBackColor = true;
            this.checkBoxCountOfThreads.CheckedChanged += new System.EventHandler(this.CheckBoxCountOfThreads_CheckedChanged);
            // 
            // checkBoxFirstFoundModelIsEnd
            // 
            this.checkBoxFirstFoundModelIsEnd.AutoSize = true;
            this.checkBoxFirstFoundModelIsEnd.Location = new System.Drawing.Point(166, 88);
            this.checkBoxFirstFoundModelIsEnd.Name = "checkBoxFirstFoundModelIsEnd";
            this.checkBoxFirstFoundModelIsEnd.Size = new System.Drawing.Size(184, 17);
            this.checkBoxFirstFoundModelIsEnd.TabIndex = 12;
            this.checkBoxFirstFoundModelIsEnd.Text = "Иcкать до первого найденного";
            this.checkBoxFirstFoundModelIsEnd.UseVisualStyleBackColor = true;
            // 
            // checkBoxParallelSearch
            // 
            this.checkBoxParallelSearch.AutoSize = true;
            this.checkBoxParallelSearch.Location = new System.Drawing.Point(166, 27);
            this.checkBoxParallelSearch.Name = "checkBoxParallelSearch";
            this.checkBoxParallelSearch.Size = new System.Drawing.Size(184, 17);
            this.checkBoxParallelSearch.TabIndex = 11;
            this.checkBoxParallelSearch.Text = "Использовать многопоточноть";
            this.checkBoxParallelSearch.UseVisualStyleBackColor = true;
            this.checkBoxParallelSearch.CheckedChanged += new System.EventHandler(this.CheckBoxParallelSearch_CheckedChanged);
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
            this.buttonAddModelForSearch.Size = new System.Drawing.Size(120, 41);
            this.buttonAddModelForSearch.TabIndex = 9;
            this.buttonAddModelForSearch.Text = "Изменить эталон";
            this.buttonAddModelForSearch.UseVisualStyleBackColor = true;
            this.buttonAddModelForSearch.Click += new System.EventHandler(this.ButtonAddModelForSearch_Click);
            // 
            // textBoxXBegin
            // 
            this.textBoxXBegin.Location = new System.Drawing.Point(6, 39);
            this.textBoxXBegin.Name = "textBoxXBegin";
            this.textBoxXBegin.Size = new System.Drawing.Size(74, 20);
            this.textBoxXBegin.TabIndex = 17;
            this.textBoxXBegin.Text = "0";
            // 
            // textBoxYBegin
            // 
            this.textBoxYBegin.Location = new System.Drawing.Point(93, 39);
            this.textBoxYBegin.Name = "textBoxYBegin";
            this.textBoxYBegin.Size = new System.Drawing.Size(73, 20);
            this.textBoxYBegin.TabIndex = 18;
            this.textBoxYBegin.Text = "0";
            // 
            // textBoxXEnd
            // 
            this.textBoxXEnd.Location = new System.Drawing.Point(7, 78);
            this.textBoxXEnd.Name = "textBoxXEnd";
            this.textBoxXEnd.Size = new System.Drawing.Size(74, 20);
            this.textBoxXEnd.TabIndex = 19;
            this.textBoxXEnd.Text = "1920";
            // 
            // textBoxYEnd
            // 
            this.textBoxYEnd.Location = new System.Drawing.Point(94, 78);
            this.textBoxYEnd.Name = "textBoxYEnd";
            this.textBoxYEnd.Size = new System.Drawing.Size(73, 20);
            this.textBoxYEnd.TabIndex = 20;
            this.textBoxYEnd.Text = "1080";
            // 
            // labelMousePosiotonView
            // 
            this.labelMousePosiotonView.AutoSize = true;
            this.labelMousePosiotonView.Location = new System.Drawing.Point(187, 23);
            this.labelMousePosiotonView.Name = "labelMousePosiotonView";
            this.labelMousePosiotonView.Size = new System.Drawing.Size(125, 13);
            this.labelMousePosiotonView.TabIndex = 21;
            this.labelMousePosiotonView.Text = "labelMousePosiotonView";
            // 
            // checkBoxForPlaceOfSearch
            // 
            this.checkBoxForPlaceOfSearch.AutoSize = true;
            this.checkBoxForPlaceOfSearch.Location = new System.Drawing.Point(112, 45);
            this.checkBoxForPlaceOfSearch.Name = "checkBoxForPlaceOfSearch";
            this.checkBoxForPlaceOfSearch.Size = new System.Drawing.Size(167, 17);
            this.checkBoxForPlaceOfSearch.TabIndex = 22;
            this.checkBoxForPlaceOfSearch.Text = "Искать в заданной области";
            this.checkBoxForPlaceOfSearch.UseVisualStyleBackColor = true;
            this.checkBoxForPlaceOfSearch.CheckedChanged += new System.EventHandler(this.CheckBoxForPlaceOfSearch_CheckedChanged);
            // 
            // panelForPlaceOfSearch
            // 
            this.panelForPlaceOfSearch.BackColor = System.Drawing.Color.DarkSalmon;
            this.panelForPlaceOfSearch.Controls.Add(this.label3);
            this.panelForPlaceOfSearch.Controls.Add(this.label1);
            this.panelForPlaceOfSearch.Controls.Add(this.labelMousePosiotonView);
            this.panelForPlaceOfSearch.Controls.Add(this.checkBoxSelectActiveWindow);
            this.panelForPlaceOfSearch.Controls.Add(this.textBoxXBegin);
            this.panelForPlaceOfSearch.Controls.Add(this.textBoxYBegin);
            this.panelForPlaceOfSearch.Controls.Add(this.textBoxXEnd);
            this.panelForPlaceOfSearch.Controls.Add(this.textBoxYEnd);
            this.panelForPlaceOfSearch.Location = new System.Drawing.Point(112, 68);
            this.panelForPlaceOfSearch.Name = "panelForPlaceOfSearch";
            this.panelForPlaceOfSearch.Size = new System.Drawing.Size(372, 112);
            this.panelForPlaceOfSearch.TabIndex = 23;
            this.panelForPlaceOfSearch.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Координаты конца области";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Координаты отсчета области";
            // 
            // checkBoxSelectActiveWindow
            // 
            this.checkBoxSelectActiveWindow.AutoSize = true;
            this.checkBoxSelectActiveWindow.Location = new System.Drawing.Point(3, 3);
            this.checkBoxSelectActiveWindow.Name = "checkBoxSelectActiveWindow";
            this.checkBoxSelectActiveWindow.Size = new System.Drawing.Size(309, 17);
            this.checkBoxSelectActiveWindow.TabIndex = 21;
            this.checkBoxSelectActiveWindow.Text = "Выбрать активное окно в качестве области для поиска";
            this.checkBoxSelectActiveWindow.UseVisualStyleBackColor = true;
            this.checkBoxSelectActiveWindow.CheckedChanged += new System.EventHandler(this.CheckBoxSelectActiveWindow_CheckedChanged);
            // 
            // panelConfigurationOfSearch
            // 
            this.panelConfigurationOfSearch.BackColor = System.Drawing.Color.Moccasin;
            this.panelConfigurationOfSearch.Controls.Add(this.FindButton);
            this.panelConfigurationOfSearch.Controls.Add(this.panelForPlaceOfSearch);
            this.panelConfigurationOfSearch.Controls.Add(this.panelForModelForSearch);
            this.panelConfigurationOfSearch.Controls.Add(this.checkBoxForPlaceOfSearch);
            this.panelConfigurationOfSearch.Controls.Add(this.labelForStatus);
            this.panelConfigurationOfSearch.Controls.Add(this.panelForColorsForIgnor);
            this.panelConfigurationOfSearch.Controls.Add(this.checkBoxForColorsForIgnor);
            this.panelConfigurationOfSearch.Location = new System.Drawing.Point(198, 106);
            this.panelConfigurationOfSearch.Name = "panelConfigurationOfSearch";
            this.panelConfigurationOfSearch.Size = new System.Drawing.Size(861, 456);
            this.panelConfigurationOfSearch.TabIndex = 24;
            // 
            // buttonOpenListOfAction
            // 
            this.buttonOpenListOfAction.Enabled = false;
            this.buttonOpenListOfAction.Location = new System.Drawing.Point(8, 439);
            this.buttonOpenListOfAction.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOpenListOfAction.Name = "buttonOpenListOfAction";
            this.buttonOpenListOfAction.Size = new System.Drawing.Size(120, 39);
            this.buttonOpenListOfAction.TabIndex = 25;
            this.buttonOpenListOfAction.Text = "Открыть список действий";
            this.buttonOpenListOfAction.UseVisualStyleBackColor = true;
            // 
            // comboBoxForSelectAction
            // 
            this.comboBoxForSelectAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForSelectAction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxForSelectAction.FormattingEnabled = true;
            this.comboBoxForSelectAction.Location = new System.Drawing.Point(198, 37);
            this.comboBoxForSelectAction.Name = "comboBoxForSelectAction";
            this.comboBoxForSelectAction.Size = new System.Drawing.Size(861, 21);
            this.comboBoxForSelectAction.TabIndex = 26;
            // 
            // buttonFindAndPerformThisAction
            // 
            this.buttonFindAndPerformThisAction.Location = new System.Drawing.Point(5, 92);
            this.buttonFindAndPerformThisAction.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFindAndPerformThisAction.Name = "buttonFindAndPerformThisAction";
            this.buttonFindAndPerformThisAction.Size = new System.Drawing.Size(188, 47);
            this.buttonFindAndPerformThisAction.TabIndex = 27;
            this.buttonFindAndPerformThisAction.Text = "Найти и выполнить это действие";
            this.buttonFindAndPerformThisAction.UseVisualStyleBackColor = true;
            this.buttonFindAndPerformThisAction.Click += new System.EventHandler(this.ButtonFindAndPerformThisAction_Click);
            // 
            // buttonPrevAction
            // 
            this.buttonPrevAction.Location = new System.Drawing.Point(5, 170);
            this.buttonPrevAction.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPrevAction.Name = "buttonPrevAction";
            this.buttonPrevAction.Size = new System.Drawing.Size(89, 40);
            this.buttonPrevAction.TabIndex = 29;
            this.buttonPrevAction.Text = "Назад";
            this.buttonPrevAction.UseVisualStyleBackColor = true;
            this.buttonPrevAction.Click += new System.EventHandler(this.ButtonPrevAction_Click);
            // 
            // buttonNextAction
            // 
            this.buttonNextAction.Location = new System.Drawing.Point(98, 170);
            this.buttonNextAction.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNextAction.Name = "buttonNextAction";
            this.buttonNextAction.Size = new System.Drawing.Size(89, 40);
            this.buttonNextAction.TabIndex = 28;
            this.buttonNextAction.Text = "Вперед";
            this.buttonNextAction.UseVisualStyleBackColor = true;
            this.buttonNextAction.Click += new System.EventHandler(this.ButtonNextAction_Click);
            // 
            // buttonAddAction
            // 
            this.buttonAddAction.Location = new System.Drawing.Point(5, 232);
            this.buttonAddAction.Name = "buttonAddAction";
            this.buttonAddAction.Size = new System.Drawing.Size(89, 41);
            this.buttonAddAction.TabIndex = 30;
            this.buttonAddAction.Text = "Добавить действие";
            this.buttonAddAction.UseVisualStyleBackColor = true;
            this.buttonAddAction.Click += new System.EventHandler(this.ButtonAddAction_Click);
            // 
            // labelNumberOfSearchAndAction
            // 
            this.labelNumberOfSearchAndAction.AutoSize = true;
            this.labelNumberOfSearchAndAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNumberOfSearchAndAction.Location = new System.Drawing.Point(12, 148);
            this.labelNumberOfSearchAndAction.Name = "labelNumberOfSearchAndAction";
            this.labelNumberOfSearchAndAction.Size = new System.Drawing.Size(170, 20);
            this.labelNumberOfSearchAndAction.TabIndex = 31;
            this.labelNumberOfSearchAndAction.Text = "Номер действия: 0";
            // 
            // labelWaitAfterThisAction
            // 
            this.labelWaitAfterThisAction.AutoSize = true;
            this.labelWaitAfterThisAction.Location = new System.Drawing.Point(199, 65);
            this.labelWaitAfterThisAction.Name = "labelWaitAfterThisAction";
            this.labelWaitAfterThisAction.Size = new System.Drawing.Size(205, 13);
            this.labelWaitAfterThisAction.TabIndex = 32;
            this.labelWaitAfterThisAction.Text = "Подождать после этого действия(сек):";
            // 
            // numericUpDownWaitAfterThisAction
            // 
            this.numericUpDownWaitAfterThisAction.Location = new System.Drawing.Point(198, 81);
            this.numericUpDownWaitAfterThisAction.Name = "numericUpDownWaitAfterThisAction";
            this.numericUpDownWaitAfterThisAction.Size = new System.Drawing.Size(203, 20);
            this.numericUpDownWaitAfterThisAction.TabIndex = 26;
            this.numericUpDownWaitAfterThisAction.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1113, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.saveAsToolStripMenuItem.Text = "Сохранить как...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.exitToolStripMenuItem.Text = "Выйти. Уходи вон, неблагодарный!";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.settingToolStripMenuItem.Text = "Настройки";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // labelNameOfLisActions
            // 
            this.labelNameOfLisActions.AutoSize = true;
            this.labelNameOfLisActions.Location = new System.Drawing.Point(5, 351);
            this.labelNameOfLisActions.Name = "labelNameOfLisActions";
            this.labelNameOfLisActions.Size = new System.Drawing.Size(166, 13);
            this.labelNameOfLisActions.TabIndex = 25;
            this.labelNameOfLisActions.Text = "Название последовательности";
            // 
            // textBoxNameOfLisActions
            // 
            this.textBoxNameOfLisActions.Location = new System.Drawing.Point(9, 367);
            this.textBoxNameOfLisActions.Name = "textBoxNameOfLisActions";
            this.textBoxNameOfLisActions.Size = new System.Drawing.Size(162, 20);
            this.textBoxNameOfLisActions.TabIndex = 24;
            this.textBoxNameOfLisActions.Text = "Default list search and action.";
            // 
            // buttonCloneThisAction
            // 
            this.buttonCloneThisAction.Location = new System.Drawing.Point(100, 231);
            this.buttonCloneThisAction.Name = "buttonCloneThisAction";
            this.buttonCloneThisAction.Size = new System.Drawing.Size(89, 41);
            this.buttonCloneThisAction.TabIndex = 35;
            this.buttonCloneThisAction.Text = "Клонровать действие";
            this.buttonCloneThisAction.UseVisualStyleBackColor = true;
            this.buttonCloneThisAction.Click += new System.EventHandler(this.ButtonCloneThisAction_Click);
            // 
            // pictureBoxForCorrectModelForSearch
            // 
            this.pictureBoxForCorrectModelForSearch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBoxForCorrectModelForSearch.Location = new System.Drawing.Point(270, 151);
            this.pictureBoxForCorrectModelForSearch.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxForCorrectModelForSearch.Name = "pictureBoxForCorrectModelForSearch";
            this.pictureBoxForCorrectModelForSearch.Size = new System.Drawing.Size(75, 41);
            this.pictureBoxForCorrectModelForSearch.TabIndex = 28;
            this.pictureBoxForCorrectModelForSearch.TabStop = false;
            // 
            // MyLittleMonion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1113, 574);
            this.Controls.Add(this.buttonCloneThisAction);
            this.Controls.Add(this.labelNameOfLisActions);
            this.Controls.Add(this.textBoxNameOfLisActions);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.numericUpDownWaitAfterThisAction);
            this.Controls.Add(this.labelWaitAfterThisAction);
            this.Controls.Add(this.labelNumberOfSearchAndAction);
            this.Controls.Add(this.buttonAddAction);
            this.Controls.Add(this.buttonPrevAction);
            this.Controls.Add(this.buttonNextAction);
            this.Controls.Add(this.buttonFindAndPerformThisAction);
            this.Controls.Add(this.comboBoxForSelectAction);
            this.Controls.Add(this.buttonOpenListOfAction);
            this.Controls.Add(this.panelConfigurationOfSearch);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.buttonFindAndPerformAllAction);
            this.Controls.Add(this.buttonSaveListOfAction);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MyLittleMonion";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = " ";
            this.Move += new System.EventHandler(this.MyLittleMonion_Move);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForModelForSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIgnorColor)).EndInit();
            this.panelForColorsForIgnor.ResumeLayout(false);
            this.panelForColorsForIgnor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountSecondForAddIgnorColor)).EndInit();
            this.panelForModelForSearch.ResumeLayout(false);
            this.panelForModelForSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercentageComplianceWithModel)).EndInit();
            this.panelForPlaceOfSearch.ResumeLayout(false);
            this.panelForPlaceOfSearch.PerformLayout();
            this.panelConfigurationOfSearch.ResumeLayout(false);
            this.panelConfigurationOfSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitAfterThisAction)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForCorrectModelForSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.PictureBox pictureBoxForModelForSearch;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonSaveListOfAction;
        private System.Windows.Forms.Button buttonNextForColorsForIgnor;
        private System.Windows.Forms.Button buttonPreviousForColorsForIgnor;
        private System.Windows.Forms.Button buttonFindAndPerformAllAction;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Label labelForNameForIgnorColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelForStatus;
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
        private System.Windows.Forms.TextBox textBoxXBegin;
        private System.Windows.Forms.TextBox textBoxYBegin;
        private System.Windows.Forms.TextBox textBoxXEnd;
        private System.Windows.Forms.TextBox textBoxYEnd;
        private System.Windows.Forms.Label labelMousePosiotonView;
        private System.Windows.Forms.CheckBox checkBoxForPlaceOfSearch;
        private System.Windows.Forms.Panel panelForPlaceOfSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSelectActiveWindow;
        private System.Windows.Forms.CheckBox checkBoxParallelSearch;
        private System.Windows.Forms.CheckBox checkBoxFirstFoundModelIsEnd;
        private System.Windows.Forms.TextBox textBoxCountOfThreads;
        private System.Windows.Forms.CheckBox checkBoxCountOfThreads;
        private System.Windows.Forms.Label labelPercentageComplianceWithModel;
        private System.Windows.Forms.NumericUpDown numericUpDownPercentageComplianceWithModel;
        private System.Windows.Forms.Panel panelConfigurationOfSearch;
        private System.Windows.Forms.Button buttonOpenListOfAction;
        private System.Windows.Forms.ComboBox comboBoxForSelectAction;
        private System.Windows.Forms.Button buttonFindAndPerformThisAction;
        private System.Windows.Forms.Button buttonPrevAction;
        private System.Windows.Forms.Button buttonNextAction;
        private System.Windows.Forms.Button buttonAddAction;
        private System.Windows.Forms.Label labelNumberOfSearchAndAction;
        private System.Windows.Forms.Label labelWaitAfterThisAction;
        private System.Windows.Forms.NumericUpDown numericUpDownWaitAfterThisAction;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label labelNameOfLisActions;
        private System.Windows.Forms.TextBox textBoxNameOfLisActions;
        private System.Windows.Forms.Button buttonCloneThisAction;
        private System.Windows.Forms.NumericUpDown numericUpDownCountSecondForAddIgnorColor;
        private System.Windows.Forms.Label labelCountSecondForAddIgnorColor;
        private System.Windows.Forms.Button buttonCorrectModelForSearch;
        private System.Windows.Forms.CheckBox checkBoxShowNotCorrectModel;
        private System.Windows.Forms.PictureBox pictureBoxForCorrectModelForSearch;
    }
}

