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

        //Для кликов мышью
        public enum MouseEvent
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10,
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(MouseEvent dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);


        //Чтобы помнить, где был курсор
        Point RememberCursorPoisition;


        const string FileName = @"C:/Users/Forni/source/repos/ReadForDisplayPixel/SavedMemoryMindOfMyLittleMinion.bin";
        MemoryMindOfMyLittleMinion TestMemoryMindOfMyLittleMinion;
        int numberOfCaptureInMemory;

        public MyLittleMonion()
        {
            InitializeComponent();
            TestMemoryMindOfMyLittleMinion = new MemoryMindOfMyLittleMinion();
            TestMemoryMindOfMyLittleMinion.nameOfCellOfMemory = "TestNaborImage";
            numberOfCaptureInMemory = 0;

            srPer = new Search();
            pictureBoxForModelForSearch.Image = (Image)srPer.pictureModelForSearch;
            pictureBoxForModelForSearch.Size = srPer.pictureModelForSearch.Size;
        }



        delegate bool srPerSearchModelInArea();
        private void FindButton_Click(object sender, EventArgs e)
        {
            pictureBoxForModelForSearch.Visible = false;
            DateTime testTimeOfSearch = DateTime.Now;
            srPer.pictureModelForSearch = (Bitmap)pictureBoxForModelForSearch.Image;
            labelForStatus.Text = "Выполняется поиск...";

            if(checkBoxForPlaceOfSearch.Checked)
            {
                if(checkBoxSelectActiveWindow.Checked)
                    srPer.SetActiveWindowForPlaceForSearching();
                else
                    srPer.SetPlaceForSearching(Convert.ToInt32(textBoxXBegin.Text), Convert.ToInt32(textBoxYBegin.Text), Convert.ToInt32(textBoxXEnd.Text), Convert.ToInt32(textBoxYEnd.Text));
                srPer.CreateScreenShot();
            }
            else
            {
                srPer.ScreenshotFullMonitor();
            }

            srPerSearchModelInArea srPerSearchModelInArea1;
            if(checkBoxCountOfThreads.Checked)
                srPerSearchModelInArea1 = (() => srPer.SearchModelInAreaInMultyThreads(checkBoxFirstFoundModelIsEnd.Checked, Convert.ToInt32(textBoxCountOfThreads.Text)));
            else if (checkBoxParallelSearch.Checked)
                srPerSearchModelInArea1 = (() => srPer.SearchModelInAreaInFourThreads(checkBoxFirstFoundModelIsEnd.Checked));
            else
                srPerSearchModelInArea1 = (() => srPer.SearchModelInArea(checkBoxFirstFoundModelIsEnd.Checked));


            if (srPerSearchModelInArea1())
            { 
                    //MessageBox.Show("Нашел!");
                Cursor.Position = srPer.foundPoints[0];
                //MouseClickLeftButton(RememberCursorPoisition);
            }
            else
            {
                MessageBox.Show("No!");
            }
            pictureBoxForModelForSearch.Visible = true;
            labelForStatus.Text = "Поиск завершен за " + Convert.ToString(DateTime.Now - testTimeOfSearch);
        }
        

        private void SaveImage_Click(object sender, EventArgs e)
        {
            TestMemoryMindOfMyLittleMinion.captureInMemory.Add((Bitmap)pictureBoxForModelForSearch.Image);
            if (TestMemoryMindOfMyLittleMinion.captureInMemory.Count > 1)
            {
                numberOfCaptureInMemory = TestMemoryMindOfMyLittleMinion.captureInMemory.Count - 1;
            }
        }

        private void FindNextButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            /*if (srPer.isEnableFourThreads)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (srPer.fourSearchsForThreadPrivate[i].foundPoints!=null)
                    {
                        srPer.AbortAllThreadForSearch();
                        srPer.foundPoints = srPer.fourSearchsForThreadPrivate[i].foundPoints;

                        pictureBox2.Image = (Image)srPer.pictureSearchArea;
                        pictureBox2.Width = srPer.pictureSearchArea.Width;
                        pictureBox2.Height = srPer.pictureSearchArea.Height;

                        break;
                    }
                }
            }*/
            labelMousePosiotonView.Text = "Mouse position: " + Convert.ToString(Cursor.Position.X) + "; " + Convert.ToString(Cursor.Position.Y) + ";";
            while (numberOfCaptureInMemory< TestMemoryMindOfMyLittleMinion.captureInMemory.Count)
            {

            }
        }

        
        

        Search srPer;
        private void TestButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            ActionOfMinion.MouseDoubleClickLeftButton(Cursor.Position);
        }


        



        //Панель игнорирования цветов НАЧАЛО
        private void CheckBoxForColorsForIgnor_CheckedChanged(object sender, EventArgs e)
        {
            panelForColorsForIgnor.Visible = checkBoxForColorsForIgnor.Checked;
            //panelForColorsForIgnor.Size = new Size(330, 170);
        }
        private void Next_Click(object sender, EventArgs e)
        {
            srPer.numberIgnorColorInList++;
            pictureBoxForIgnorColor.Image = AdditionalFunctions.FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                srPer.ShowIgnorColor());
            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(srPer.numberIgnorColorInList + 1);
        }
        private void Prev_Click(object sender, EventArgs e)
        {
            srPer.numberIgnorColorInList--;
            pictureBoxForIgnorColor.Image = AdditionalFunctions.FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                srPer.ShowIgnorColor());
            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(srPer.numberIgnorColorInList + 1);
        }
        private void ButtonAddSingleColorForIgnor_Click(object sender, EventArgs e)
        {
            string savedText = buttonAddSingleColorForIgnor.Text;
            for (int i = 0; i < 5; i++)
            {
                buttonAddSingleColorForIgnor.Text = "Наведите курсор на цвет \nдо истечения времени: " + Convert.ToString(5 - i);

                Task waitTask = Task.Run(() => { Thread.Sleep(1000); });
                waitTask.Wait();
            }

            srPer.AddIgnorColorInPoint(new Point((int)(Cursor.Position.X * Search.getScalingFactor()), (int)(Cursor.Position.Y * Search.getScalingFactor())));
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
                    srPer.AddIgnorColorsInPicture(image);
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
            
            if (srPer.ShowIgnorColor().A == 0)
            {
                DisableButtonsForEmptyList(false);
                labelForNumberOfIgnorColor.Text = "Нет цветов";
            }
            else
            {
                srPer.RemoveIgnorColor();
                UpdateContentPanelOfColorsForIgnor();
                if (srPer.ShowIgnorColor().A == 0)
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
            if (srPer.ShowIgnorColor().A == 0)
            {
                DisableButtonsForEmptyList(false);
                labelForNumberOfIgnorColor.Text = "Нет цветов";
            }
            else
            {
                pictureBoxForIgnorColor.Image = AdditionalFunctions.FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                    srPer.ShowIgnorColor());

                labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(srPer.numberIgnorColorInList + 1);
            }
        }

        //Панель игнорирования цветов КОНЕЦ

        //Панель для действий с эталоном НАЧАЛО
        private void ButtonAddModelForSearch_Click(object sender, EventArgs e)
        {
            Bitmap image; //Bitmap для открываемого изображения

            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    //укажите pictureBox, в который нужно загрузить изображение 
                    this.pictureBoxForModelForSearch.Size = image.Size;
                    pictureBoxForModelForSearch.Image = image;
                    pictureBoxForModelForSearch.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void PictureBoxForModelForSearch_Click(object sender, EventArgs e)
        {
            //Тут можно написать код, который будет указвать куда надо нажать на найденом эталоне.
        }
        private void ButtonForChangeSizeOferyBigModelForSearch_Click(object sender, EventArgs e)
        {
            if (panelForModelForSearch.Width == 8000)
            {
                panelForModelForSearch.Width = 350;
                panelForModelForSearch.Height = 200;
            }
            else
            {
                panelForModelForSearch.Width = 8000;
                panelForModelForSearch.Height = 4000;
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

            srPer.percentageComplianceWithModel = (byte)numericUpDownPercentageComplianceWithModel.Value;
        }



        //Вспомогательные методы КОНЕЦ

    }
}
