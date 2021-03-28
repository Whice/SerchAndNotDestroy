﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace MyLittleMinion
{
    public partial class MyLittleMonion : Form
    {
        /// <summary>
        /// Хранит настройки помощника.
        /// </summary>
        SettingOfMinion settingOfMinion;
        /// <summary>
        /// Экземпляр окна настроек. Будет создан в случае надобности поменять настройки.
        /// </summary>
        DialogWindowForSetting dialogWindowForSetting;

        /// <summary>
        /// Список действий.(Вообще список списка, но пока только тут только один список.)
        /// </summary>
        List<ListOfActionsOfMinion> exemplarsOfLAM = new List<ListOfActionsOfMinion>();
        /// <summary>
        /// Номер списка действий в общем списке. Пока что все время 0, т.к. список действий один.
        /// </summary>
        int numberLOEOLAM = 0;

        /// <summary>
        /// Локальная переменная ссылка на класс поиска для взаимодействия с интерфесом. По сути укороченыое имя, псевдоним.
        /// </summary>
        Search exemplarOfSearch;
        /// <summary>
        /// Локальная переменная ссылка на класс действия помощника для взаимодействия с интерфесом. По сути укороченыое имя, псевдоним.
        /// </summary>
        ListActionOfMinion exemplarOfListActionsOfMinion;
        /// <summary>
        /// Сообщает была ил загружена форма.
        /// </summary>
        bool FormIsLoad = false;


        public MyLittleMonion()
        {
            InitializeComponent();
            settingOfMinion = new SettingOfMinion();
            dialogWindowForSetting = new DialogWindowForSetting(ref settingOfMinion);
            dialogWindowForSetting.Close();


            exemplarsOfLAM.Add(new ListOfActionsOfMinion());

            FillUINewDataFromListSearchAndAction();

            SetImageModelConfig();


            FillVariantsOfTypeForComboBoxForTyepOfAction();
        }
        private void MyLittleMonion_Load(object sender, EventArgs e)
        {
            FormIsLoad = true;
            timer1.Enabled = true;
        }


        #region/// Вспомогательные функции НАЧАЛО
        /// <summary>
        ///Счетчик, который после достижения определенной велечины позволит изменить цвет фона.
        /// </summary>
        int countTimerTickForChangeBackColor = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            labelMousePosiotonView.Text = "Mouse position: " + Convert.ToString(Cursor.Position.X) + "; " + Convert.ToString(Cursor.Position.Y) + ";";

        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(Convert.ToString(comboBoxForSelectAction.SelectedIndex));
            /*Thread.Sleep(2000);
            ActionOfMinion aMinion = new ActionOfMinion();
            aMinion.cursorPosition = Cursor.Position;
                aMinion.MouseDoubleClickLeftButton();*/
        }
        /// <summary>
        /// Заполняет интерфейс данными из списка поисков и действий.
        /// </summary>
        void FillUINewDataFromListSearchAndAction()
        {

            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();

            textBoxNameOfLisActions.Text = exemplarsOfLAM[numberLOEOLAM].nameOfListOfSearchingAndActions;


            numericUpDownCountOfThreads.Enabled = false;
            if (exemplarOfSearch.multyThreadSearch == 0)
                checkBoxParallelSearch.Checked = true;
            else if (exemplarOfSearch.multyThreadSearch > 0)
            {
                checkBoxCountOfThreads.Checked = true;
                numericUpDownCountOfThreads.Text = Convert.ToString(exemplarOfSearch.multyThreadSearch);
                numericUpDownCountOfThreads.Enabled = true;
            }
            else
                checkBoxParallelSearch.Checked = false;

            checkBoxFirstFoundModelIsEnd.Checked = exemplarOfSearch.stopSearchingAfterFirstPointFound;
            numericUpDownPercentageComplianceWithModel.Value = exemplarOfSearch.percentageComplianceWithModel;
            SetImageModelConfig();


            checkBoxForPlaceOfSearch.Checked = exemplarOfSearch.UsePlaceForSearch;
            checkBoxSelectActiveWindow.Checked = exemplarOfSearch.UseActiveWindow;
            numericUpDownXBegin.Value = exemplarOfSearch.GetLocationOfPlaceForSearch().X;
            numericUpDownYBegin.Value = exemplarOfSearch.GetLocationOfPlaceForSearch().Y;
            numericUpDownXEnd.Value = exemplarOfSearch.GetLocationOfPlaceForSearch().X + exemplarOfSearch.SearchAreaSize.Width;
            numericUpDownYEnd.Value = exemplarOfSearch.GetLocationOfPlaceForSearch().Y + exemplarOfSearch.SearchAreaSize.Height;

            checkBoxForColorsForIgnor.Checked = exemplarOfSearch.UseIgnorColors;
            UpdateContentPanelOfColorsForIgnor();

            listBoxForListOfActions.Items.Clear();
            //Копия списка этого экземпляра, чтобы по нему можно было перемещаться.
            for (int i = 0; i < exemplarOfListActionsOfMinion.count; i++)
                listBoxForListOfActions.Items.Add("Действие №" + (i + 1).ToString());

            if (comboBoxTypeOfAction.SelectedIndex >= 0)//призагрузке формы имеет значение -1 и отказывается принимать другие. В работе уже все нормально.
                comboBoxTypeOfAction.SelectedIndex = exemplarOfListActionsOfMinion.GetAction().typeOfAction;
            if (comboBoxForSelectAction.SelectedIndex >= 0)//призагрузке формы имеет значение -1 и отказывается принимать другие. В работе уже все нормально.
                FillVariantsOfActionsForComboBoxForSelectAction();
            numericUpDownWaitAfterThisAction.Value = exemplarOfListActionsOfMinion.GetAction().timeOfWaitingAfterActionInSecond;
            richTextBoxForExemplarOfAction.Text = exemplarOfListActionsOfMinion.GetAction().textForAction;
        }
        /// <summary>
        /// Считывает из интерфейса инормацию в список поисков и действий.
        /// </summary>
        void FillExemplarsOfListOfSearchAndActionDataFromUI()
        {
            exemplarsOfLAM[numberLOEOLAM].nameOfListOfSearchingAndActions = textBoxNameOfLisActions.Text;

            exemplarOfSearch.multyThreadSearch = -1;
            if (checkBoxParallelSearch.Checked)
                exemplarOfSearch.multyThreadSearch = 0;
            if (checkBoxCountOfThreads.Checked)
                exemplarOfSearch.multyThreadSearch = Convert.ToInt32(numericUpDownCountOfThreads.Text);

            exemplarOfSearch.percentageComplianceWithModel = Convert.ToByte(numericUpDownPercentageComplianceWithModel.Value);
            exemplarOfSearch.stopSearchingAfterFirstPointFound = checkBoxFirstFoundModelIsEnd.Checked;

            exemplarOfSearch.UsePlaceForSearch = checkBoxForPlaceOfSearch.Checked;
            exemplarOfSearch.UseActiveWindow = checkBoxSelectActiveWindow.Checked;
            if (checkBoxForPlaceOfSearch.Checked)
            {
                if (checkBoxSelectActiveWindow.Checked)
                    exemplarOfSearch.SetActiveWindowForPlaceForSearching();
                else
                    exemplarOfSearch.SetPlaceForSearching(Convert.ToInt32(numericUpDownXBegin.Value), Convert.ToInt32(numericUpDownYBegin.Value),
                        Convert.ToInt32(numericUpDownXEnd.Value), Convert.ToInt32(numericUpDownYEnd.Value));
            }
            else
            {
                exemplarOfSearch.SetPlaceForSearchForFullMonitor();
            }

            exemplarOfSearch.UseIgnorColors = checkBoxForColorsForIgnor.Checked;

            exemplarOfListActionsOfMinion.GetAction().typeOfAction = (byte)comboBoxTypeOfAction.SelectedIndex;
            exemplarOfListActionsOfMinion.GetAction().numberOfAction = (ushort)comboBoxForSelectAction.SelectedIndex;
            exemplarOfListActionsOfMinion.GetAction().timeOfWaitingAfterActionInSecond = (int)numericUpDownWaitAfterThisAction.Value;
        }
        private void MyLittleMonion_Move(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            FillUINewDataFromListSearchAndAction();
        }
        private void MyLittleMonion_SizeChanged(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            FillUINewDataFromListSearchAndAction();
            ResizeUI();
        }
        private void ResizeUI()
        {
            //Изменение размера с учетом изменения родительского контейнера. Точка отсчета не должна меняться.
            Control parenControl = generalTabControlOfConfiguration.Parent;
            generalTabControlOfConfiguration.Size = new Size(
                (parenControl.Width - 15) - generalTabControlOfConfiguration.Location.X,
                (parenControl.Height - 38) - generalTabControlOfConfiguration.Location.Y);

            parenControl = tabControlConfigurationOfSearch.Parent;
            tabControlConfigurationOfSearch.Size = new Size(
                (parenControl.Width - 5) - tabControlConfigurationOfSearch.Location.X,
                (parenControl.Height - 5) - tabControlConfigurationOfSearch.Location.Y);

            parenControl = comboBoxForSelectAction.Parent;
            comboBoxForSelectAction.Size = new Size(
                (parenControl.Width - 5) - comboBoxForSelectAction.Location.X,
                comboBoxForSelectAction.Height);

            parenControl = richTextBoxForExemplarOfAction.Parent;
            richTextBoxForExemplarOfAction.Size = new Size(
                (parenControl.Width - 5) - richTextBoxForExemplarOfAction.Location.X,
                richTextBoxForExemplarOfAction.Height);

        }

        #endregion/// Вспомогательные функции КОНЕЦ


        #region/// Поиск НАЧАЛО

        private void FindButton_Click(object sender, EventArgs e)
        {
            SearchingModelOnScreen();
        }
        /// <summary>
        /// Нужен для выбора запуска поиска. Последовательный или многопоточный.
        /// </summary>
        /// <returns></returns>
        delegate bool srPerSearchModelInArea();
        /// <summary>
        /// Выполнение поиска.
        /// </summary>
        void SearchingModelOnScreen()
        {
            pictureBoxForModelForSearch.Visible = false;
            DateTime testTimeOfSearch = DateTime.Now;
            labelForStatus.Text = "Выполняется поиск...";

            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarOfSearch.CreateScreenShot();

            srPerSearchModelInArea srPerSearchModelInArea1;
            if (checkBoxCountOfThreads.Checked)
                srPerSearchModelInArea1 = (() => exemplarOfSearch.SearchModelInAreaInMultyThreads(Convert.ToInt32(numericUpDownCountOfThreads.Text)));
            else if (checkBoxParallelSearch.Checked)
                srPerSearchModelInArea1 = (() => exemplarOfSearch.SearchModelInAreaInFourThreads());
            else
                srPerSearchModelInArea1 = (() => exemplarOfSearch.SearchModelInArea());

            if (srPerSearchModelInArea1())
            {
                //MessageBox.Show("Нашел!");
                Cursor.Position = exemplarOfSearch.foundPoints[0];
                //MouseClickLeftButton(RememberCursorPoisition);
                labelForStatus.Text = "Поиск завершен за " + Convert.ToString(DateTime.Now - testTimeOfSearch);
            }
            else
            {
                labelForStatus.Text = "Поиск завершен за " + Convert.ToString(DateTime.Now - testTimeOfSearch);
                MessageBox.Show("No!");
            }
            pictureBoxForModelForSearch.Visible = true;
        }

        #endregion/// Поиск КОНЕЦ


        #region///Панель игнорирования цветов НАЧАЛО
        private void CheckBoxForColorsForIgnor_CheckedChanged(object sender, EventArgs e)
        {
            panelForColorsForIgnor.Enabled = checkBoxForColorsForIgnor.Checked;
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            UpdateContentPanelOfColorsForIgnor();
        }
        private void Next_Click(object sender, EventArgs e)
        {
            exemplarOfSearch.numberIgnorColorInList++;
            pictureBoxForIgnorColor.Image = AdditionalFunctions.FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                exemplarOfSearch.ShowIgnorColor());
            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(exemplarOfSearch.numberIgnorColorInList + 1);
        }
        private void Prev_Click(object sender, EventArgs e)
        {
            exemplarOfSearch.numberIgnorColorInList--;
            pictureBoxForIgnorColor.Image = AdditionalFunctions.FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                exemplarOfSearch.ShowIgnorColor());
            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(exemplarOfSearch.numberIgnorColorInList + 1);
        }
        private void ButtonAddSingleColorForIgnor_Click(object sender, EventArgs e)
        {
            string savedText = buttonAddSingleColorForIgnor.Text;
            for (int i = 0; i < (int)numericUpDownCountSecondForAddIgnorColor.Value; i++)
            {
                buttonAddSingleColorForIgnor.Text = "Наведите курсор на цвет \nдо истечения времени: " + Convert.ToString((int)numericUpDownCountSecondForAddIgnorColor.Value - i);

                Task waitTask = Task.Run(() => { Thread.Sleep(1000); });
                waitTask.Wait();
            }

            exemplarOfSearch.AddIgnorColorInPoint(new Point((int)(Cursor.Position.X * Search.getScalingFactor()), (int)(Cursor.Position.Y * Search.getScalingFactor())));
            buttonAddSingleColorForIgnor.Text = savedText;

            UpdateContentPanelOfColorsForIgnor();
            pictureBoxForIgnorColor.Visible = true;
        }
        private void ButtonAddManyColorsForIgnor_Click(object sender, EventArgs e)
        {
            Bitmap image; //Bitmap для открываемого изображения

            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 
                    exemplarOfSearch.AddIgnorColorsInPicture(image);
                    DisableButtonsForEmptyList(true);
                    pictureBoxForIgnorColor.Visible = true;
                    UpdateContentPanelOfColorsForIgnor();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonForDeleteSelectedColor_Click(object sender, EventArgs e)
        {

            if (exemplarOfSearch.ShowIgnorColor().A == 0)
            {
                DisableButtonsForEmptyList(false);
                labelForNumberOfIgnorColor.Text = "Нет цветов";
            }
            else
            {
                exemplarOfSearch.RemoveIgnorColor();
                UpdateContentPanelOfColorsForIgnor();
                if (exemplarOfSearch.ShowIgnorColor().A == 0)
                {
                    DisableButtonsForEmptyList(false);
                    labelForNumberOfIgnorColor.Text = "Нет цветов";
                }
            }
        }
        /// <summary>
        /// Отключает кнопки, если нет цветов.
        /// </summary>
        /// <param name="isNotDisable"></param>
        void DisableButtonsForEmptyList(bool isNotDisable)
        {
            buttonNextForColorsForIgnor.Enabled = isNotDisable;
            buttonPreviousForColorsForIgnor.Enabled = isNotDisable;
            buttonForDeleteSelectedColor.Enabled = isNotDisable;
            pictureBoxForIgnorColor.Visible = isNotDisable;
        }
        /// <summary>
        /// Обновляет данные в панели. Берет их из exemplarOfSearch.
        /// </summary>
        void UpdateContentPanelOfColorsForIgnor()
        {
            if (exemplarOfSearch.ShowIgnorColor().A == 0)
            {
                DisableButtonsForEmptyList(false);
                labelForNumberOfIgnorColor.Text = "Нет цветов";
            }
            else
            {
                pictureBoxForIgnorColor.Image = AdditionalFunctions.FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                    exemplarOfSearch.ShowIgnorColor());
                DisableButtonsForEmptyList(true);
                labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(exemplarOfSearch.numberIgnorColorInList + 1);
            }
        }

        #endregion///Панель игнорирования цветов КОНЕЦ


        #region///Панель для действий с эталоном НАЧАЛО
        private void ButtonAddModelForSearchUploadFromHard_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open_dialog = new OpenFileDialog())
            {//создание диалогового окна для выбора файла
                open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
                if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
                {
                    try
                    {
                        Bitmap image;

                        //копирование битмапа в стриме позволяет создать полностью независимую копию битмапа
                        MemoryStream ms = new MemoryStream();
                        using (FileStream stream = new FileStream(open_dialog.FileName, FileMode.Open))
                        {
                            stream.CopyTo(ms);
                            image = (Bitmap)Bitmap.FromStream(ms);
                        }
                        //укажите pictureBox, в который нужно загрузить изображение 

                        exemplarOfSearch.pictureModelForSearch = (Bitmap)image.Clone();
                        image = null;
                        GC.Collect();
                        this.pictureBoxForModelForSearch.Invalidate();
                    }
                    catch
                    {
                        DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            SetImageModelConfig();
        }
        private void ButtonAddModelForSearchSelectOnScreen_Click(object sender, EventArgs e)
        {
            FormForSelectionFilm formForSelectionFilm = new FormForSelectionFilm();
            formForSelectionFilm.Show();
            formForSelectionFilm.SendRectangle += exemplarOfSearch.AddModelFromAreaOnScreen;
            formForSelectionFilm.IndicateUpdateFromSender += FillUINewDataFromListSearchAndAction;
        }
        private void ButtonCorrectModelForSearch_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open_dialog = new OpenFileDialog())
            {//создание диалогового окна для выбора файла
                open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
                open_dialog.Multiselect = true;
                if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
                {
                    try
                    {
                        //копирование битмапа в стриме позволяет создать полностью независимую копию битмапа
                        foreach (var file in open_dialog.FileNames)
                        {
                            using (FileStream stream = new FileStream(file, FileMode.Open))
                            {
                                Bitmap image;
                                MemoryStream ms = new MemoryStream();
                                stream.CopyTo(ms);
                                image = (Bitmap)Bitmap.FromStream(ms);
                                //укажите pictureBox, в который нужно загрузить изображение 
                                exemplarOfSearch.CorrectionModel((Bitmap)image.Clone());
                            }
                        }
                    }
                    catch
                    {
                        DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.pictureBoxForModelForSearch.Invalidate();
                    GC.Collect();
                }
            }
            SetImageModelConfig();
        }
        private void ButtonCorrectModelForSearchSelectOnScreen_Click(object sender, EventArgs e)
        {
            FormForSelectionFilm formForSelectionFilm = new FormForSelectionFilm();
            formForSelectionFilm.Show();
            formForSelectionFilm.SendRectangle += exemplarOfSearch.CorrectModelFromAreaOnScreen;
            formForSelectionFilm.IndicateUpdateFromSender += FillUINewDataFromListSearchAndAction;
        }
        private void CheckBoxShowCorrectModel_CheckedChanged(object sender, EventArgs e)
        {
            SetImageModelConfig();
        }
        private void PictureBoxForModelForSearch_Click(object sender, EventArgs e)
        {
            //Поиск местположения pictureBox в глобальных координатах.
            //Сперва смотрю всех его родительских окон до формы, а потом и координаты самой формы добавляю. Точнее наоброт.
            Point locationPBForModel = new Point(this.Location.X + pictureBoxForModelForSearch.Location.X, this.Location.Y + pictureBoxForModelForSearch.Location.Y);
            Control pbParent = pictureBoxForModelForSearch.Parent;
            while (pbParent != this)
            {
                locationPBForModel = new Point(locationPBForModel.X + pbParent.Location.X, locationPBForModel.Y + pbParent.Location.Y);
                pbParent = pbParent.Parent;
            }

            //Для точности, почему-то сбивается локация нажатия
            Point correctionPoint = new Point(-8, -31);
            //Прицел в эталоне: разница между положениями курсора и началом pictureBox.
            this.exemplarOfSearch.aimModel = new Point(
                Cursor.Position.X - locationPBForModel.X + correctionPoint.X,
                Cursor.Position.Y - locationPBForModel.Y + correctionPoint.Y
                );
            SetImageModelConfig();
        }


        /// <summary>
        /// Рисует прицел на эталоне согласно заданной в экземпляре точке.
        /// </summary>
        Bitmap DrawAimOnModel(Bitmap pictureBoxImage)
        {
            Graphics paintAim = Graphics.FromImage(pictureBoxImage);// pictureBoxForModelForSearch.CreateGraphics();
            PaintingAim(paintAim, this.exemplarOfSearch.aimModel);
            return pictureBoxImage;

        }
        /// <summary>
        /// Рисует четырехкрылый прицел, разными цветами, да потолще, чтобы видно было.
        /// </summary>
        /// <param name="paintAim"></param>
        /// <param name="centerAim"></param>
        void PaintingAim(Graphics paintAim, Point centerAim)
        {
            int radiusLine = 15;
            Pen redPen = new Pen(Color.Red);
            Pen bluePen = new Pen(Color.Blue);
            Pen greenPen = new Pen(Color.Green);

            //Левое крыло
            int centerAimY = centerAim.Y;
            paintAim.DrawLine(redPen, centerAim.X - 5 - radiusLine, centerAimY++, centerAim.X - 5, centerAimY);
            paintAim.DrawLine(redPen, centerAim.X - 5 - radiusLine, centerAimY++, centerAim.X - 5, centerAimY);
            paintAim.DrawLine(bluePen, centerAim.X - 5 - radiusLine, centerAimY++, centerAim.X - 5, centerAimY);
            paintAim.DrawLine(bluePen, centerAim.X - 5 - radiusLine, centerAimY++, centerAim.X - 5, centerAimY);
            paintAim.DrawLine(greenPen, centerAim.X - 5 - radiusLine, centerAimY++, centerAim.X - 5, centerAimY);
            paintAim.DrawLine(greenPen, centerAim.X - 5 - radiusLine, centerAimY++, centerAim.X - 5, centerAimY);
            //Правое крыло
            centerAimY = centerAim.Y;
            paintAim.DrawLine(redPen, centerAim.X + 5 + radiusLine, centerAimY--, centerAim.X + 5, centerAimY);
            paintAim.DrawLine(redPen, centerAim.X + 5 + radiusLine, centerAimY--, centerAim.X + 5, centerAimY);
            paintAim.DrawLine(bluePen, centerAim.X + 5 + radiusLine, centerAimY--, centerAim.X + 5, centerAimY);
            paintAim.DrawLine(bluePen, centerAim.X + 5 + radiusLine, centerAimY--, centerAim.X + 5, centerAimY);
            paintAim.DrawLine(greenPen, centerAim.X + 5 + radiusLine, centerAimY--, centerAim.X + 5, centerAimY);
            paintAim.DrawLine(greenPen, centerAim.X + 5 + radiusLine, centerAimY--, centerAim.X + 5, centerAimY);
            //Верхнее крыло
            int centerAimX = centerAim.X;
            paintAim.DrawLine(redPen, centerAimX--, centerAim.Y - 5 - radiusLine, centerAimX, centerAim.Y - 5);
            paintAim.DrawLine(redPen, centerAimX--, centerAim.Y - 5 - radiusLine, centerAimX, centerAim.Y - 5);
            paintAim.DrawLine(bluePen, centerAimX--, centerAim.Y - 5 - radiusLine, centerAimX, centerAim.Y - 5);
            paintAim.DrawLine(bluePen, centerAimX--, centerAim.Y - 5 - radiusLine, centerAimX, centerAim.Y - 5);
            paintAim.DrawLine(greenPen, centerAimX--, centerAim.Y - 5 - radiusLine, centerAimX, centerAim.Y - 5);
            paintAim.DrawLine(greenPen, centerAimX--, centerAim.Y - 5 - radiusLine, centerAimX, centerAim.Y - 5);
            //Нижнее крыло
            centerAimX = centerAim.X;
            paintAim.DrawLine(redPen, centerAimX++, centerAim.Y + 5 + radiusLine, centerAimX, centerAim.Y + 5);
            paintAim.DrawLine(redPen, centerAimX++, centerAim.Y + 5 + radiusLine, centerAimX, centerAim.Y + 5);
            paintAim.DrawLine(bluePen, centerAimX++, centerAim.Y + 5 + radiusLine, centerAimX, centerAim.Y + 5);
            paintAim.DrawLine(bluePen, centerAimX++, centerAim.Y + 5 + radiusLine, centerAimX, centerAim.Y + 5);
            paintAim.DrawLine(greenPen, centerAimX++, centerAim.Y + 5 + radiusLine, centerAimX, centerAim.Y + 5);
            paintAim.DrawLine(greenPen, centerAimX++, centerAim.Y + 5 + radiusLine, centerAimX, centerAim.Y + 5);

        }

        private void NumericUpDownPercentageComplianceWithModel_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPercentageComplianceWithModel.Value < 1)
                numericUpDownPercentageComplianceWithModel.Value = 1;
            if (numericUpDownPercentageComplianceWithModel.Value > 100)
                numericUpDownPercentageComplianceWithModel.Value = 100;

            exemplarOfSearch.percentageComplianceWithModel = (byte)numericUpDownPercentageComplianceWithModel.Value;
        }
        /// <summary>
        /// Картинка, которая отпраавляется в pictureBox эталона либо с начальным эталоном, либо с корректированным.
        /// На ней рисуется прицел.
        /// </summary>
        Bitmap imageForUpdateModelPicture;
        /// <summary>
        /// В зависимости от установки checkBoxShowNotCorrectModel помещает нужную картинку эталона и рисует прицел.
        /// </summary>
        void SetImageModelConfig()
        {
            if (checkBoxShowNotCorrectModel.Checked)
            {
                pictureBoxForModelForSearch.Image = (Image)exemplarOfSearch.pictureModelForSearch;
                pictureBoxForModelForSearch.Size = exemplarOfSearch.pictureModelForSearch.Size;
            }
            else
            {
                pictureBoxForModelForSearch.Image = (Image)exemplarOfSearch.correctModel;
                pictureBoxForModelForSearch.Size = exemplarOfSearch.correctModel.Size;
            }
            imageForUpdateModelPicture = (Bitmap)pictureBoxForModelForSearch.Image;
            imageForUpdateModelPicture = DrawAimOnModel((Bitmap)imageForUpdateModelPicture.Clone());
            pictureBoxForModelForSearch.Image = (Image)imageForUpdateModelPicture;
        }
        private void CheckBoxParallelSearch_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCountOfThreads.Enabled = checkBoxParallelSearch.Checked;
            if (!checkBoxParallelSearch.Checked)
                checkBoxCountOfThreads.Checked = false;
        }
        private void CheckBoxCountOfThreads_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownCountOfThreads.Enabled = checkBoxCountOfThreads.Checked;
        }

        #endregion///Панель для действий с эталоном КОНЕЦ


        #region///Панель с настройками области поиска НАЧАЛО
        private void CheckBoxSelectActiveWindow_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownXBegin.Enabled = !checkBoxSelectActiveWindow.Checked;
            numericUpDownXEnd.Enabled = !checkBoxSelectActiveWindow.Checked;
            numericUpDownYBegin.Enabled = !checkBoxSelectActiveWindow.Checked;
            numericUpDownYEnd.Enabled = !checkBoxSelectActiveWindow.Checked;
            buttonSelectSearchAreaOnScreen.Enabled = !checkBoxSelectActiveWindow.Checked;
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            FillUINewDataFromListSearchAndAction();
        }

        private void CheckBoxForPlaceOfSearch_CheckedChanged(object sender, EventArgs e)
        {
            panelForPlaceOfSearch.Enabled = checkBoxForPlaceOfSearch.Checked;
        }
        private void ButtonSelectSearchAreaOnScreen_Click(object sender, EventArgs e)
        {
            FormForSelectionFilm formForSelectionFilm = new FormForSelectionFilm();
            formForSelectionFilm.Show();
            formForSelectionFilm.SendTwoPointsOfRectangle += ReceiveRectangle;
            FillExemplarsOfListOfSearchAndActionDataFromUI();
        }
        void ReceiveRectangle(int xBegin, int yBegin, int xEnd, int yEnd)
        {
            numericUpDownXBegin.Value = xBegin;
            numericUpDownYBegin.Value = yBegin;
            numericUpDownXEnd.Value = xEnd;
            numericUpDownYEnd.Value = yEnd;
        }
        private void NumericUpDownYBegin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownYBegin.Value >= numericUpDownYEnd.Value)
                numericUpDownYBegin.Value = numericUpDownYEnd.Value - 1;
        }

        private void NumericUpDownYEnd_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownYBegin.Value >= numericUpDownYEnd.Value)
                numericUpDownYEnd.Value = numericUpDownYBegin.Value + 1;
        }

        private void NumericUpDownXEnd_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownXBegin.Value >= numericUpDownXEnd.Value)
                numericUpDownXEnd.Value = numericUpDownXBegin.Value + 1;
        }

        private void NumericUpDownXBegin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownXBegin.Value >= numericUpDownXEnd.Value)
                numericUpDownXBegin.Value = numericUpDownXEnd.Value - 1;
        }

        #endregion///Панель с настройками области поиска КОНЕЦ


        #region///Конфигурация действий НАЧАЛО

        private void FillVariantsOfTypeForComboBoxForTyepOfAction()
        {
            comboBoxTypeOfAction.Items.Add("Мышь");
            comboBoxTypeOfAction.Items.Add("Клавиатура");
            comboBoxTypeOfAction.SelectedIndex = 0;

        }
        private void ComboBoxTypeOfAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            FillVariantsOfActionsForComboBoxForSelectAction();
        }
        private void FillVariantsOfActionsForComboBoxForSelectAction()
        {
            //Заполняется каждый раз заново в соответствии с указанным типом действия
            comboBoxForSelectAction.Items.Clear();
            foreach (string nameOfAction in ActionOfMinion.listNameOfMauseAction[exemplarOfListActionsOfMinion.GetAction().typeOfAction])
                comboBoxForSelectAction.Items.Add(nameOfAction);

            comboBoxForSelectAction.SelectedIndex = exemplarOfListActionsOfMinion.GetAction().numberOfAction;
        }
        private void ComboBoxForSelectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
        }
        private void RichTextBoxForExemplarOfAction_TextChanged(object sender, EventArgs e)
        {
            exemplarOfListActionsOfMinion.GetAction().textForAction = richTextBoxForExemplarOfAction.Text;
        }
        private void ButtonAddAction_Click_1(object sender, EventArgs e)
        {
            exemplarOfListActionsOfMinion.Add(new ActionOfMinion());
            FillUINewDataFromListSearchAndAction();
        }
        private void ButtonDeleteAction_Click_1(object sender, EventArgs e)
        {
            exemplarOfListActionsOfMinion.Remove();
            FillUINewDataFromListSearchAndAction();
        }
        private void ListBoxForListOfActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            exemplarOfListActionsOfMinion.numberActionInList = (ushort)listBoxForListOfActions.SelectedIndex;
            FillUINewDataFromListSearchAndAction();
        }
        private void ListBoxForListOfActions_DrawItem(object sender, DrawItemEventArgs e)
        {
           /* e.DrawBackground();
            string text = ((ListBox)sender).Items[exemplarOfListActionsOfMinion.numberActionInList].ToString();
            Color color = Color.White;
            if (text == "Действие №1")
                color = Color.Green;
            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);
            e.Graphics.DrawString(text,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();*/
        }


        #endregion///Конфигурация действий КОНЕЦ


        #region///Верхнее меню НАЧАЛО

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Помощник - хороший мальчик!");
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogWindowForSetting = new DialogWindowForSetting(ref settingOfMinion);
            dialogWindowForSetting.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarsOfLAM[numberLOEOLAM].SaveAs(settingOfMinion);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarsOfLAM[numberLOEOLAM].Save(settingOfMinion);

        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exemplarsOfLAM[numberLOEOLAM].Open(settingOfMinion);
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();
            FillUINewDataFromListSearchAndAction();

            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
        }
        /// <summary>
        /// Обновляет данные в конфигарации при перемещении окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ИнструкцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstructionFrom InsForm = new InstructionFrom();
            InsForm.Show();
        }

        #endregion///Верхнее меню КОНЕЦ


        #region///Информация о списке, с которым сейчас идет работа НАЧАЛО
        private void TextBoxNameOfLisActions_TextChanged(object sender, EventArgs e)
        {
            exemplarsOfLAM[numberLOEOLAM].nameOfListOfSearchingAndActions = textBoxNameOfLisActions.Text;
        }

        private void ButtonAddAction_Click(object sender, EventArgs e)
        {

            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarsOfLAM[numberLOEOLAM].Add(new Search(), new ListActionOfMinion());
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();
            FillUINewDataFromListSearchAndAction();


            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
        }

        private void ButtonPrevAction_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            if (exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList > 0)//если есть куда назад
            {
                exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList--;
                exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
                exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();
                FillUINewDataFromListSearchAndAction();

                labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
            }
        }

        private void ButtonNextAction_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            //Если есть куда вперед
            if (exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList < exemplarsOfLAM[numberLOEOLAM].GetSizeOfListOfSearchAndActionsOfMinion() - 1)
            {
                exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList++;
                exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
                exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();
                FillUINewDataFromListSearchAndAction();

                labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
            }
        }

        private void ButtonFindAndPerformThisAction_Click(object sender, EventArgs e)
        {
            FindAndPerformThisAction();
        }
        private void FindAndPerformThisActionButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < exemplarsOfLAM[numberLOEOLAM].GetSizeOfListOfSearchAndActionsOfMinion(); i++)
            {
                exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList = i;
                FillUINewDataFromListSearchAndAction();
                FindAndPerformThisAction();

                //Каждый пятый раз запускать очистку памяти, чтобы не кушать много.
                if (i % 5 == 0)
                    GC.Collect();
            }
        }
        /// <summary>
        /// Выполняет поиск, действие и ждёт согласно всем настройкам.
        /// </summary>
        void FindAndPerformThisAction()
        {
            SearchingModelOnScreen();
            for (int i = 0; i < exemplarOfListActionsOfMinion.count; i++)
            {
                exemplarOfListActionsOfMinion.GetAction().RealizeAction();
                Thread.Sleep(Convert.ToInt32(exemplarOfListActionsOfMinion.GetAction().timeOfWaitingAfterActionInSecond) * 1000);//Надо умножать на 1000, чтобы из получить ожидание в секундах
                exemplarOfListActionsOfMinion.numberActionInList++;
            }
        }


        /// <summary>
        /// Создает новый экземпляр поиска и действия применяя конфигурацию нынешнего.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCloneThisAction_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarsOfLAM[numberLOEOLAM].Add(exemplarOfSearch.Clone(), exemplarOfListActionsOfMinion.Clone());
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();
            FillUINewDataFromListSearchAndAction();

            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);

        }

        private void ButtonDeleteAction_Click(object sender, EventArgs e)
        {
            exemplarsOfLAM[numberLOEOLAM].RemoveThisExemplarSearchingAndActions();
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfListActionsOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarListActionsOfMinion();
            FillUINewDataFromListSearchAndAction();

        }




        #endregion///Информация о списке, с которым сейчас идет работа КОНЕЦ

    }       
}
