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

namespace SerchAndNotDestroy
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

        


        private void FindButton_Click(object sender, EventArgs e)
        {
            pictureBoxForModelForSearch.Visible = false;
            DateTime testTimeOfSearch = DateTime.Now;
            srPer.ScreenshotFullMonitor();
            srPer.pictureModelForSearch = (Bitmap)pictureBoxForModelForSearch.Image;
            labelForStatus.Text = "Выполняется поиск...";
            
            if(srPer.SearchModelInArea(true))
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

        

        public bool FindModelOnScreenshotOfFullscreen(Bitmap scrFullscreen, Bitmap model, double persCountComparison)
        {
            /*model тут имеется ввиду, как эталон*/
            //Бегаем до краев скрина экарана, недоходя на ширину/высоту эталона, дабы вылетов не было
            //for (int forSpeed = 0; forSpeed < 3; forSpeed++)//Для ускорения за чет передвиения в шахматном поряке, с пропуском по 3 пикселя
                for (int i = 0; i < (scrFullscreen.Width - model.Width); i ++)
                    for (int j = 0; j < (scrFullscreen.Height - model.Height); j++)
                    {
                        if (CheckColorPixelInPoint(scrFullscreen.GetPixel(i, j), model.GetPixel(0, 0)))
                        {
                            var cutSmallPicture = CutSmallPictureFromLargePicture(new Point(i, j), scrFullscreen, model.Width, model.Height);
                        double pCULDOTBP = PercentageComparisonUpLeftDiagonalOfTwoBitmapPictures(cutSmallPicture, model);
                            if (pCULDOTBP > persCountComparison)
                            {
                            double pCOTBP = PercentageComparisonOfTwoBitmapPictures(cutSmallPicture, model);
                                if (pCOTBP > persCountComparison)
                                {
                                    RememberCursorPoisition = new Point(i, j);
                                    return true;
                                }
                            }
                        }
                    }
            return false;
        }

        private Bitmap CutSmallPictureFromLargePicture(Point locationStartOfLargePicture, Bitmap largePicture, int widthSmallPicture, int heightSmallPicture)
        {
            Bitmap smallPicture = new Bitmap(widthSmallPicture, heightSmallPicture, PixelFormat.Format32bppArgb);
            for (int i = 0; i < widthSmallPicture; i++)
                for (int j = 0; j < heightSmallPicture; j++)
                {
                    smallPicture.SetPixel(i, j, largePicture.GetPixel(locationStartOfLargePicture.X + i, locationStartOfLargePicture.Y + j));
                }
            return smallPicture;
        }

        private double PercentageComparisonUpLeftDiagonalOfTwoBitmapPictures(Bitmap pictureOne, Bitmap pictureTwo)
        {
            //Находим меньшую из сторон
            int lesserSide = pictureOne.Height > pictureOne.Width ? pictureOne.Width : pictureOne.Height;
            int counter = 0;

            for (int i = 0; i < lesserSide; i++)
                if (CheckColorPixelInPoint(pictureOne.GetPixel(i, i), pictureTwo.GetPixel(i, i)))
                {
                    counter++;
                }

            return ((double)counter / lesserSide);
        }
        private double PercentageComparisonOfTwoBitmapPictures(Bitmap pictureOne, Bitmap pictureTwo)
        {
            int counter = 0;

            for (int i = 0; i < pictureOne.Width; i++)
                for (int j = 0; j < pictureOne.Height; j++)
                {
                    if (CheckColorPixelInPoint(pictureOne.GetPixel(i, j), pictureTwo.GetPixel(i, j)))
                    {
                        counter++;
                    }
                }
            return ((double)counter / (pictureOne.Width * pictureOne.Height));
        }

        public bool CheckColorPixelInPoint(Color firstColor, Color secondColor)
        {

            if (firstColor.R == secondColor.R)
            {
                if (firstColor.G == secondColor.G)
                {
                    if (firstColor.B == secondColor.B)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void MouseClickLeftButton(Point cursorPosition)
        {
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN,
            cursorPosition.X,
            cursorPosition.Y,
            0,
            0);
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP,
                        cursorPosition.X,
                        cursorPosition.Y,
                        0,
                        0);
        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            TestMemoryMindOfMyLittleMinion.captureInMemory.Add((Bitmap)pictureBoxForModelForSearch.Image);
            if (TestMemoryMindOfMyLittleMinion.captureInMemory.Count > 1)
            {
                numberOfCaptureInMemory = TestMemoryMindOfMyLittleMinion.captureInMemory.Count - 1;
            }
        }

        

        class QuantityColorInPicture
        {
            public Color pixelColor;
            public int quantity;

            public QuantityColorInPicture(Color pixelColorIn, int quantityIn)
            {
                pixelColor = pixelColorIn;
                quantity = quantityIn;
            }
        }

        QuantityColorInPicture[][] quantityColorOnImage;
        class BitmapAndCountThread
        {
            public Bitmap picture;
            public int countOfThread;

            public BitmapAndCountThread(Bitmap pictureIn, int countOfThreadIn)
            {
                picture = pictureIn;
                countOfThread = countOfThreadIn;
            }
        }

        private QuantityColorInPicture[] CalculateQuantityColorInPicture(Bitmap picture)
        {
            List<QuantityColorInPicture> masQuantityColorInPicture = new List<QuantityColorInPicture>();

            masQuantityColorInPicture.Add(new QuantityColorInPicture(picture.GetPixel(0, 0), 1));



            for (int i = 0; i < picture.Width; i++)
                for (int j = 1; j < picture.Height; j++)
                {
                    bool colorPresent = false;
                    for (int g = 0; g < masQuantityColorInPicture.Count; g++)
                    {
                        if (CheckColorPixelInPoint(picture.GetPixel(i, j), masQuantityColorInPicture[g].pixelColor))
                        {
                            masQuantityColorInPicture[g].quantity++;
                            colorPresent = true;
                            break;
                        }
                    }
                    if (!colorPresent)
                    {
                        masQuantityColorInPicture.Add(new QuantityColorInPicture(picture.GetPixel(i, j), 1));
                    }
                }

            return masQuantityColorInPicture.ToArray();
        }
        private void CalculateQuantityColorInPicture(object bitmapPictureAndCountThreadIn)
        {
            BitmapAndCountThread bitmapPictureAndCountThread = (BitmapAndCountThread)bitmapPictureAndCountThreadIn;

            Bitmap picture = bitmapPictureAndCountThread.picture;

            List<QuantityColorInPicture> masQuantityColorInPicture = new List<QuantityColorInPicture>();
            //Блокировка картинки
            /*Rectangle rect = new Rectangle((bitmapPictureAndCountThread.countOfThread * picture.Width) / maxCountOfThreadMinusTwo,
                0, ((bitmapPictureAndCountThread.countOfThread + 1) * picture.Width) / maxCountOfThreadMinusTwo, picture.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                picture.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                picture.PixelFormat);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * picture.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Set every third value to 255. A 24bpp bitmap will look red.  
            for (int counter = (bitmapPictureAndCountThread.countOfThread * rgbValues.Length) / maxCountOfThreadMinusTwo; counter < ((bitmapPictureAndCountThread.countOfThread + 1) * rgbValues.Length) / maxCountOfThreadMinusTwo; counter ++)
                rgbValues[counter] = 255;

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);*/


            masQuantityColorInPicture.Add(new QuantityColorInPicture(picture.GetPixel(0, 0), 1));



            /*for (int i = (bitmapPictureAndCountThread.countOfThread* picture.Width)/maxCountOfThreadMinusTwo; i < ((bitmapPictureAndCountThread.countOfThread+1) * picture.Width) / maxCountOfThreadMinusTwo; i++)
                for (int j = 1; j < picture.Height; j++)
                {
                    bool colorPresent = false;
                    for (int g = 0; g < masQuantityColorInPicture.Count; g++)
                    {
                        if (CheckColorPixelInPoint(picture.GetPixel(i, j), masQuantityColorInPicture[g].pixelColor))
                        {
                            masQuantityColorInPicture[g].quantity++;
                            colorPresent = true;
                            break;
                        }
                    }
                    if (!colorPresent)
                    {
                        masQuantityColorInPicture.Add(new QuantityColorInPicture(picture.GetPixel(i, j), 1));
                    }
                }*/

            //picture.UnlockBits(bmpData);
            quantityColorOnImage[bitmapPictureAndCountThread.countOfThread] = new QuantityColorInPicture[masQuantityColorInPicture.Count];
            quantityColorOnImage[bitmapPictureAndCountThread.countOfThread] = masQuantityColorInPicture.ToArray();
        }

        

        

        private void FindNextButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            while(numberOfCaptureInMemory< TestMemoryMindOfMyLittleMinion.captureInMemory.Count)
            {

            }
        }

        private Bitmap ReduceTheImageBySpecifiedPercentage(Bitmap picture, double percent)
        {
            Bitmap newSmallPicture;
            newSmallPicture = new Bitmap(picture, (int)(picture.Size.Width * percent), (int)(picture.Size.Height * percent));
            return newSmallPicture;

        }
        
        
        Search srPer;
        Rectangle rectA;
        
        private void TestButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            srPer.ScreenShotActiveWindow();
            pictureBox2.Image = (Image)srPer.pictureSearchArea;

            pictureBox2.Width = srPer.pictureSearchArea.Width;
            pictureBox2.Height = srPer.pictureSearchArea.Height; 

            //srPer.AddIgnorColorsInPicture((Bitmap)pictureBox1.Image);

            //selectionFilm.Show();
            /*Thread.Sleep(5000);
            srPer.AddIgnorColorInPoint(new Point(Cursor.Position.X*5/4, Cursor.Position.Y*5/4));
            ShowColor(srPer.ShowIgnorColor());*/

            /*
            srPer.ScreenShotActiveWindow();
            pictureBox3.Image = (Image)srPer.fullScreenshot;
            pictureBox3.Width = srPer.fullScreenshot.Width;
            pictureBox3.Height = srPer.fullScreenshot.Height;*/

            //persCountCompar = Convert.ToDouble(percentageOfComplianceTextBox.Text);
            /*Point sizeScreen = new Point(1920, 1080);
            //Скриншот всего экрана
            Bitmap fullScreenPixel = new Bitmap(sizeScreen.X, sizeScreen.Y, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(fullScreenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    hSrcDC = gsrc.GetHdc();
                    hDC = gdest.GetHdc();
                    retval = BitBlt(hDC, 0, 0, sizeScreen.X, sizeScreen.Y, hSrcDC, 0, 0, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            *//*
            double persOfFull = Convert.ToDouble(screenReductionPercentageTextBox.Text);

            pictureBox2.Image = (Image)(ReduceTheImageBySpecifiedPercentage((Bitmap)pictureBox1.Image, persOfFull));
            pictureBox2.Size = new Size((int)(pictureBox1.Image.Size.Width * persOfFull), (int)(pictureBox1.Image.Size.Height * persOfFull));
            */
              //QuantityColorInPicture[] quantityColorOnImage = CalculateQuantityColorInPicture((Bitmap)pictureBox1.Image);
              /*Bitmap[] aFewPicture= new Bitmap[maxCountOfThreadMinusTwo];
              //for (int i = 0; i < maxCountOfThreadMinusTwo; i++) { aFewPicture[i] = (Bitmap)pictureBox1.Image.Clone(); }
               quantityColorOnImage = new QuantityColorInPicture[maxCountOfThreadMinusTwo][];
              Thread[] threadForCalculateQuantityColorInPicture = new Thread[maxCountOfThreadMinusTwo];
              //maxCountOfThreadMinusTwo = 1;
              for (int i = 0; i < maxCountOfThreadMinusTwo; i++)
              {
                  threadForCalculateQuantityColorInPicture[i] = new Thread(new ParameterizedThreadStart(CalculateQuantityColorInPicture));
                  threadForCalculateQuantityColorInPicture[i].Start(new BitmapAndCountThread((Bitmap)pictureBox1.Image.Clone(), i));
              }
              for (int i = 0; i < maxCountOfThreadMinusTwo; i++)
              {
                  threadForCalculateQuantityColorInPicture[i].Join();
              }
              List<QuantityColorInPicture> list = new List<QuantityColorInPicture>();
              list.AddRange(quantityColorOnImage[0]);
              for (int i = 1; i < maxCountOfThreadMinusTwo; i++)
              {
                  list.AddRange(quantityColorOnImage[i]);
              }
              quantityColorOnImage[0] = list.ToArray();
              label1.Text = "";
              for (int i = 0; i< 6; i++)
              {
                  label1.Text += Convert.ToString(quantityColorOnImage[0][i].pixelColor) + " - " + Convert.ToString(quantityColorOnImage[0][i].quantity) + ";\n";
              }
              */
              /*
              Bitmap tbmp1 = (Bitmap)pictureBox1.Image.Clone();
              Bitmap tbmp2 = (Bitmap)pictureBox1.Image.Clone();
              pictureBox1.Image = (Image)(new Bitmap(12, 12));

              Thread mythr1 = new Thread(new ParameterizedThreadStart(TestFunction));
              Thread mythr2 = new Thread(new ParameterizedThreadStart(TestFunction));
              mythr1.Start(tbmp1);
              mythr2.Start(tbmp2);
              mythr1.Join();
              mythr2.Join();
              label1.Text = "end";*/
        }

        private void TestFunction(object objIm)
        {
            Bitmap btmp =(Bitmap)objIm;
            for (int g = 0; g < 999; g++)
            {
                for (int i = 0; i < btmp.Width; i++)
                {
                    for (int j = 0; j < btmp.Height; j++)
                    {
                        btmp.GetPixel(i, j);
                    }
                }
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
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
                    this.pictureBox2.Size = image.Size;
                    pictureBox2.Image = image;
                    pictureBox2.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            /*
            double persOfFull = (1/(Convert.ToDouble(screenReductionPercentageTextBox.Text)));

            pictureBox2.Image = (Image)(ReduceTheImageBySpecifiedPercentage((Bitmap)pictureBox2.Image, persOfFull));
            pictureBox2.Size = new Size((int)(pictureBox2.Image.Size.Width * persOfFull), (int)(pictureBox2.Image.Size.Height * persOfFull));
            */
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            labelForNameForIgnorColor.Text = "Выделено от " + Convert.ToString(rectA.X) + ":" +
           Convert.ToString(rectA.Y) + " до " +
           Convert.ToString(rectA.Width + rectA.X) + ":" +
           Convert.ToString(rectA.Height + rectA.Y);
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
            pictureBoxForIgnorColor.Image = FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                srPer.ShowIgnorColor());
            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(srPer.numberIgnorColorInList + 1);
        }
        private void Prev_Click(object sender, EventArgs e)
        {
            srPer.numberIgnorColorInList--;
            pictureBoxForIgnorColor.Image = FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                srPer.ShowIgnorColor());
            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(srPer.numberIgnorColorInList + 1);
        }
        private void ButtonAddSingleColorForIgnor_Click(object sender, EventArgs e)
        {
            string savedText = buttonAddSingleColorForIgnor.Text;
            for (int i = 0; i < 5; i++)
            {
                buttonAddSingleColorForIgnor.Text = "Наведите курсор на цвет \nдо истечения времени: " + Convert.ToString(5 - i);
                Thread newThr = new Thread(WaitFunction);
                newThr.Start();
                newThr.Join();
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
            pictureBoxForIgnorColor.Image = FillBitmapWithColor(pictureBoxForIgnorColor.Width, pictureBoxForIgnorColor.Height,
                    srPer.ShowIgnorColor());

            labelForNumberOfIgnorColor.Text = "Цвет номер: " + Convert.ToString(srPer.numberIgnorColorInList + 1);
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
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 
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
        //Панель для действий с эталоном КОНЕЦ


        //Вспомогательные методы НАЧАЛО
        private static Bitmap FillBitmapWithColor(int width, int height, Color colorForFill)
        {
            Bitmap pictureForReturn = new Bitmap(width, height);

            for (int i = 0; i < pictureForReturn.Width; i++)
                for (int j = 0; j < pictureForReturn.Height; j++)
                    pictureForReturn.SetPixel(i, j, colorForFill);
            
            return pictureForReturn;
        }
        void WaitFunction()
        {
            Thread.Sleep(1000);
        }









        //Вспомогательные методы КОНЕЦ

    }
}
