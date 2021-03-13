using System;
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

        [Serializable()]
        public class MemoryMindOfMyLittleMinion
        {
            public string nameOfCellOfMemory;
            public List<Bitmap> captureInMemory = new List<Bitmap>();
        }

        
        const string fullAdressOfFileName = @"D:/Documents/My little minion saves/SavedMemoryMindOfMyLittleMinion.bin";
        MemoryMindOfMyLittleMinion TestMemoryMindOfMyLittleMinion;
        int numberOfCaptureInMemory;
        SettingOfMinion settingOfMinion;
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
        ActionOfMinion exemplarOfActionOfMinion;

        public MyLittleMonion()
        {
            InitializeComponent();
            TestMemoryMindOfMyLittleMinion = new MemoryMindOfMyLittleMinion();
            TestMemoryMindOfMyLittleMinion.nameOfCellOfMemory = "TestNaborImage";
            numberOfCaptureInMemory = 0;
            settingOfMinion = new SettingOfMinion();
            settingOfMinion.pathForSaveOfList = fullAdressOfFileName;


            exemplarsOfLAM.Add(new ListOfActionsOfMinion());
            FillUINewDataFromListSearchAndAction();

            SetImageModelConfig();


            FillVariantsOfActionsForComboBoxForSelectAction();
        }

        

        delegate bool srPerSearchModelInArea();
        private void FindButton_Click(object sender, EventArgs e)
        {
            SearchingModelOnScreen();
        }


        
        void SearchingModelOnScreen()
        {
            pictureBoxForModelForSearch.Visible = false;
            DateTime testTimeOfSearch = DateTime.Now;
            labelForStatus.Text = "Выполняется поиск...";

            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarOfSearch.CreateScreenShot();

            srPerSearchModelInArea srPerSearchModelInArea1;
            if (checkBoxCountOfThreads.Checked)
                srPerSearchModelInArea1 = (() => exemplarOfSearch.SearchModelInAreaInMultyThreads(Convert.ToInt32(textBoxCountOfThreads.Text)));
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


        private void SaveImage_Click(object sender, EventArgs e)
        {
            TestMemoryMindOfMyLittleMinion.captureInMemory.Add((Bitmap)pictureBoxForModelForSearch.Image);
            if (TestMemoryMindOfMyLittleMinion.captureInMemory.Count > 1)
            {
                numberOfCaptureInMemory = TestMemoryMindOfMyLittleMinion.captureInMemory.Count - 1;
            }
        }

        

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


        



        //Панель игнорирования цветов НАЧАЛО
        private void CheckBoxForColorsForIgnor_CheckedChanged(object sender, EventArgs e)
        {
            panelForColorsForIgnor.Visible = checkBoxForColorsForIgnor.Checked;
            //panelForColorsForIgnor.Size = new Size(330, 170);
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
            DisableButtonsForEmptyList(true);
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

        void DisableButtonsForEmptyList(bool isNotDisable)
        {
            buttonNextForColorsForIgnor.Enabled = isNotDisable;
            buttonPreviousForColorsForIgnor.Enabled = isNotDisable;
            buttonForDeleteSelectedColor.Enabled = isNotDisable;
            pictureBoxForIgnorColor.Visible = isNotDisable;
        }
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

                labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(exemplarOfSearch.numberIgnorColorInList + 1);
            }
        }

        //Панель игнорирования цветов КОНЕЦ

        //Панель для действий с эталоном НАЧАЛО
        private void ButtonAddModelForSearch_Click(object sender, EventArgs e)
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
        private void ButtonCorrectModelForSearch_Click(object sender, EventArgs e)
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
                        exemplarOfSearch.CorrectionModel((Bitmap)image.Clone());
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
        private void CheckBoxShowCorrectModel_CheckedChanged(object sender, EventArgs e)
        {
            SetImageModelConfig();
        }
        private void PictureBoxForModelForSearch_Click(object sender, EventArgs e)
        {
            //Поиск местположения pictureBox в глобальных координатах.
            //Сперва смотрю всех его родительских окон до формы, а потом и координаты самой формы добавляю. Точнее наоброт.
            Point locationPBForModel = new Point(this.Location.X+ pictureBoxForModelForSearch.Location.X, this.Location.Y + pictureBoxForModelForSearch.Location.Y);
            Control pbParent = pictureBoxForModelForSearch.Parent;
            while(pbParent != this)
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
        private void ButtonForChangeSizeOferyBigModelForSearch_Click(object sender, EventArgs e)
        {
            if (panelForModelForSearch.Width == 8000)
            {
                panelForModelForSearch.Width = 350;
                panelForModelForSearch.Height = 200;
                panelConfigurationOfSearch.Width = 850;
                panelConfigurationOfSearch.Height = 450;
            }
            else
            {
                panelForModelForSearch.Width = 8000;
                panelForModelForSearch.Height = 4000;
                panelConfigurationOfSearch.Width = 8500;
                panelConfigurationOfSearch.Height = 4500;
            }
        }

        private void CheckBoxParallelSearch_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCountOfThreads.Enabled = checkBoxParallelSearch.Checked;
            if (!checkBoxParallelSearch.Checked)
                checkBoxCountOfThreads.Checked = false;
        }
        private void CheckBoxCountOfThreads_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCountOfThreads.Enabled = checkBoxCountOfThreads.Checked;
        }
        //Панель для действий с эталоном КОНЕЦ

        //Панель с настройками области поиска НАЧАЛО
        private void CheckBoxSelectActiveWindow_CheckedChanged(object sender, EventArgs e)
        {
            textBoxXBegin.Enabled = !checkBoxSelectActiveWindow.Checked;
            textBoxXEnd.Enabled = !checkBoxSelectActiveWindow.Checked;
            textBoxYBegin.Enabled = !checkBoxSelectActiveWindow.Checked;
            textBoxYEnd.Enabled = !checkBoxSelectActiveWindow.Checked;
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            FillUINewDataFromListSearchAndAction();
        }

        private void CheckBoxForPlaceOfSearch_CheckedChanged(object sender, EventArgs e)
        {
            panelForPlaceOfSearch.Visible = checkBoxForPlaceOfSearch.Checked;
        }

        //Панель с настройками области поиска КОНЕЦ

        //Вспомогательные методы НАЧАЛО
        
        private void NumericUpDownPercentageComplianceWithModel_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPercentageComplianceWithModel.Value < 1)
                numericUpDownPercentageComplianceWithModel.Value = 1;
            if (numericUpDownPercentageComplianceWithModel.Value >100)
                numericUpDownPercentageComplianceWithModel.Value = 100;

            exemplarOfSearch.percentageComplianceWithModel = (byte)numericUpDownPercentageComplianceWithModel.Value;
        }

        private void FillVariantsOfActionsForComboBoxForSelectAction()
        {
            comboBoxForSelectAction.Items.Add("Щелчек левой кнопкой");
            comboBoxForSelectAction.SelectedIndex = 0;
            comboBoxForSelectAction.Items.Add("Двойной щелчек левой кнопкой");
            comboBoxForSelectAction.Items.Add("Вставить из буфера");

        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("Помощник - хороший мальчик!");
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogWindowForSetting = new DialogWindowForSetting(settingOfMinion);
            dialogWindowForSetting.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exemplarsOfLAM[numberLOEOLAM].SaveAs(settingOfMinion);
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exemplarsOfLAM[numberLOEOLAM].Open(settingOfMinion);
        }
        private void TextBoxNameOfLisActions_TextChanged(object sender, EventArgs e)
        {
            exemplarsOfLAM[numberLOEOLAM].nameOfListOfSearchingAndActions = textBoxNameOfLisActions.Text;
        }

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
            FillUINewDataFromListSearchAndAction();
        }
        Bitmap imageForUpdateModelPicture;
        void FillUINewDataFromListSearchAndAction()
        {

            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfActionOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarActionOfMinion();

            textBoxNameOfLisActions.Text = exemplarsOfLAM[numberLOEOLAM].nameOfListOfSearchingAndActions;

            if(comboBoxForSelectAction.SelectedIndex>=0)//призагрузке формы имеет значение -1 и отказывается принимать другие. В работе уже все нормально.
                comboBoxForSelectAction.SelectedIndex = exemplarOfActionOfMinion.numberOfAction;
            numericUpDownWaitAfterThisAction.Value = exemplarOfActionOfMinion.timeOfWaitingAfterAction;

            if (exemplarOfSearch.multyThreadSearch == 0)
                checkBoxParallelSearch.Checked = true;
            else if (exemplarOfSearch.multyThreadSearch > 0)
            {
                checkBoxCountOfThreads.Checked = true;
                textBoxCountOfThreads.Text = Convert.ToString(exemplarOfSearch.multyThreadSearch);
            }
            else
                checkBoxParallelSearch.Checked = false;

            checkBoxFirstFoundModelIsEnd.Checked = exemplarOfSearch.stopSearchingAfterFirstPointFound;
            numericUpDownPercentageComplianceWithModel.Value = exemplarOfSearch.percentageComplianceWithModel;
            pictureBoxForModelForSearch.Image = (Image)imageForUpdateModelPicture;// exemplarOfSearch.pictureModelForSearch;
            /*pictureBoxForModelForSearch.Size = exemplarOfSearch.pictureModelForSearch.Size;
            pictureBoxForCorrectModelForSearch.Image = (Image)exemplarOfSearch.correctModel;
            pictureBoxForCorrectModelForSearch.Size = exemplarOfSearch.correctModel.Size;
            DrawAimOnModel();*/

            checkBoxForPlaceOfSearch.Checked = exemplarOfSearch.UsePlaceForSearch;
            checkBoxSelectActiveWindow.Checked = exemplarOfSearch.UseActiveWindow;
            textBoxXBegin.Text = Convert.ToString(exemplarOfSearch.GetLocationOfPlaceForSearch().X);
            textBoxYBegin.Text = Convert.ToString(exemplarOfSearch.GetLocationOfPlaceForSearch().Y);
            textBoxXEnd.Text = Convert.ToString(exemplarOfSearch.GetLocationOfPlaceForSearch().X + exemplarOfSearch.pictureSearchArea.Width);
            textBoxYEnd.Text = Convert.ToString(exemplarOfSearch.GetLocationOfPlaceForSearch().Y + exemplarOfSearch.pictureSearchArea.Height);

            checkBoxForColorsForIgnor.Checked = exemplarOfSearch.UseIgnorColors;
            UpdateContentPanelOfColorsForIgnor();
        }
        void FillExemplarsOfListOfSearchAndActionDataFromUI()
        {
            exemplarsOfLAM[numberLOEOLAM].nameOfListOfSearchingAndActions = textBoxNameOfLisActions.Text;

            exemplarOfActionOfMinion.numberOfAction = (ushort)comboBoxForSelectAction.SelectedIndex;
            exemplarOfActionOfMinion.timeOfWaitingAfterAction = (int)numericUpDownWaitAfterThisAction.Value;

            exemplarOfSearch.multyThreadSearch = -1;
            if(checkBoxParallelSearch.Checked)
                exemplarOfSearch.multyThreadSearch = 0;
            if (checkBoxCountOfThreads.Checked)
                exemplarOfSearch.multyThreadSearch = Convert.ToInt32(textBoxCountOfThreads.Text);

            exemplarOfSearch.percentageComplianceWithModel = Convert.ToByte(numericUpDownPercentageComplianceWithModel.Value);
            exemplarOfSearch.stopSearchingAfterFirstPointFound = checkBoxFirstFoundModelIsEnd.Checked;

            exemplarOfSearch.UsePlaceForSearch = checkBoxForPlaceOfSearch.Checked;
            exemplarOfSearch.UseActiveWindow = checkBoxSelectActiveWindow.Checked;
            if (checkBoxForPlaceOfSearch.Checked)
            {
                if (checkBoxSelectActiveWindow.Checked)
                    exemplarOfSearch.SetActiveWindowForPlaceForSearching();
                else
                    exemplarOfSearch.SetPlaceForSearching(Convert.ToInt32(textBoxXBegin.Text), Convert.ToInt32(textBoxYBegin.Text),
                        Convert.ToInt32(textBoxXEnd.Text), Convert.ToInt32(textBoxYEnd.Text));
            }
            else
            {
                exemplarOfSearch.SetPlaceForSearchForFullMonitor();
            }

            exemplarOfSearch.UseIgnorColors = checkBoxForColorsForIgnor.Checked;
        }

        private void ButtonAddAction_Click(object sender, EventArgs e)
        {

            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarsOfLAM[numberLOEOLAM].Add(new Search(), new ActionOfMinion());
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfActionOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarActionOfMinion();
            FillUINewDataFromListSearchAndAction();


            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
        }

        private void ButtonPrevAction_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            if (exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList >0)
                exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList--;
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfActionOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarActionOfMinion();
            FillUINewDataFromListSearchAndAction();

            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
        }

        private void ButtonNextAction_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            if (exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList < exemplarsOfLAM[numberLOEOLAM].GetSizeOfListOfSearchAndActionsOfMinion()-1)
                exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList++;
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfActionOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarActionOfMinion();
            FillUINewDataFromListSearchAndAction();

            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);
        }

        private void ButtonFindAndPerformThisAction_Click(object sender, EventArgs e)
        {
            FindAndPerformThisAction();
        }
        private void FindNextButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < exemplarsOfLAM[numberLOEOLAM].GetSizeOfListOfSearchAndActionsOfMinion(); i++)
            {
                exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList = i;
                FillUINewDataFromListSearchAndAction();
                FindAndPerformThisAction();

                //Каждый пятый раз запускаю очистку памяти, чтобы не кушать много.
                if(i%5==0)
                    GC.Collect();
            }
        }

        void FindAndPerformThisAction()
        {
            IamBorn();
            SearchingModelOnScreen();
            exemplarOfActionOfMinion.RealizeAction();
            Thread.Sleep(Convert.ToInt32(numericUpDownWaitAfterThisAction.Value));
        }

        void IamBorn()
        {
            Clipboard.SetDataObject("Я родился!");
            IDataObject iData = Clipboard.GetDataObject();
        }

        /// <summary>
        /// Создает новый экземпляр поиска и действия применяя конфигурацию нынешнего.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCloneThisAction_Click(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            exemplarsOfLAM[numberLOEOLAM].Add(exemplarOfSearch.Clone(), exemplarOfActionOfMinion.Clone());
            exemplarOfSearch = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarSearch();
            exemplarOfActionOfMinion = exemplarsOfLAM[numberLOEOLAM].GetThisExemplarActionOfMinion();
            FillUINewDataFromListSearchAndAction();

            labelNumberOfSearchAndAction.Text = "Номер действия: " + Convert.ToString(exemplarsOfLAM[numberLOEOLAM].numberSearchAndActionInList);

        }

        /// <summary>
        /// Обновляет данные в конфигарации при перемещении окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyLittleMonion_Move(object sender, EventArgs e)
        {
            FillExemplarsOfListOfSearchAndActionDataFromUI();
            FillUINewDataFromListSearchAndAction();
        }

        private void ИнструкцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstructionFrom InsForm = new InstructionFrom();
            InsForm.Show();
        }

       










        //Вспомогательные методы КОНЕЦ

    }
}
