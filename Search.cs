using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;

namespace SerchAndNotDestroy
{
    /// <summary>
    /// Это класс поиска!!!
    /// </summary>
    class Search
    {
        public Bitmap pictureSearchArea;
        public Bitmap pictureModelForSearch;
        public Point[] foundPoints = new Point[1];
        public Search()
        {
            pictureSearchArea = null;
            pictureModelForSearch = null;
            foundPoints[0] = new Point(-1, -1);

            locationOfPlaceForSearchPrivate = new Point();
            screeningWindowPrivate = false;

            listOfIgnorColors = null;
            numberIgnorColorInListPrivate = 0;
            isIgnorColorsPrivate = false;



            CreateBitmapForEmptyModel();
        }
        /*цвета, которые надо игнрировать НАЧАЛО*/
        private bool isIgnorColorsPrivate;
        public bool isIgnorColors
        {
            get { return this.isIgnorColorsPrivate; }
        }
        private List<Color> listOfIgnorColors;
        private UInt16 numberIgnorColorInListPrivate;
        public UInt16 numberIgnorColorInList
        {
            get { return this.numberIgnorColorInListPrivate; }
            set
            {
                if(value<=0 || listOfIgnorColors==null  || value==ushort.MaxValue)
                {
                    this.numberIgnorColorInListPrivate=0;
                }
                else if(value> (listOfIgnorColors.Count-1))
                {
                    this.numberIgnorColorInListPrivate = (ushort)(listOfIgnorColors.Count - 1);
                }
                else
                {
                    this.numberIgnorColorInListPrivate = value;
                }
            }
        }
        public void AddIgnorColor(Color newColorForIgnor)
        {
            //Если еще не было цветом для игнорирования, то выделить для них память
            //А если уже были, то переместить индекс, чтобы показывать последний добавленный
            if (listOfIgnorColors == null)
            {
                this.listOfIgnorColors = new List<Color>();
                this.listOfIgnorColors.Add(newColorForIgnor);
                isIgnorColorsPrivate = true;
            }
            else
            {
                bool isAddColor = true;
                foreach(Color color in this.listOfIgnorColors)
                {
                    if (CheckColorPixelInPoint(color, newColorForIgnor))
                        isAddColor = false;
                }
                if (isAddColor)
                {
                    this.numberIgnorColorInListPrivate++;
                    this.listOfIgnorColors.Add(newColorForIgnor);
                }
            }
        }

        public void AddIgnorColorsInPicture(Bitmap picture)
        {
            //добавить сразу несколько цветов удобно, да?
            for(int i=0; i<picture.Width; i++)
                for(int j=0; j<picture.Height;j++)
                {
                    AddIgnorColor(picture.GetPixel(i, j));
                }
        }

        public void AddIgnorColorInPoint(Point pointNewColorForIgnor)
        {
            Bitmap newColorForIgnorInPixel = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(newColorForIgnorInPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC;
                    IntPtr hDC;
                    int retval;
                    hSrcDC = gsrc.GetHdc();
                    hDC = gdest.GetHdc();
                    retval = BitBlt(hDC, 0, 0, 1, 1,
                        hSrcDC, pointNewColorForIgnor.X, pointNewColorForIgnor.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            AddIgnorColor(newColorForIgnorInPixel.GetPixel(0, 0));
        }
        public void RemoveIgnorColor()
        {
            if(listOfIgnorColors != null)
            {
                if (listOfIgnorColors.Count == 1)
                {
                    listOfIgnorColors = null;
                    isIgnorColorsPrivate = false;
                }
                else
                {
                    listOfIgnorColors.RemoveAt(numberIgnorColorInList);
                    this.numberIgnorColorInListPrivate = 0;
                }
            }
        }

        public bool IsColorForIgnor(Color colorForTest)
        {
            if(this.listOfIgnorColors!=null)
            {
                foreach(Color ignorColors in this.listOfIgnorColors)
                {
                    if(CheckColorPixelInPoint(ignorColors, colorForTest))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Показать цвет, который соотвествует выбраному номеру
        /// Если возвращает прозрачный, то цветов нет
        /// </summary>
        public Color ShowIgnorColor()
        {
            if (this.listOfIgnorColors != null)
                return listOfIgnorColors[numberIgnorColorInListPrivate];
            else
                return Color.FromArgb(0, 255, 255, 255);
        }

        /*цвета, которые надо игнрировать КОНЕЦ*/

        //Область, в которой выполняется поиск НАЧАЛО
        private Point locationOfPlaceForSearchPrivate;
        public Point locationOfPlaceForSearch
        {
            get
            {
                if (this.screeningWindowPrivate)
                    return this.locationOfPlaceForSearchPrivate;
                return new Point(0, 0);
            }
        }


        public void SetPlaceForSearching(Rectangle placeForSearching)
        {
            this.locationOfPlaceForSearchPrivate.X = placeForSearching.X;
            this.locationOfPlaceForSearchPrivate.Y = placeForSearching.Y;
            this.pictureSearchArea = new Bitmap(placeForSearching.Width, placeForSearching.Height);
        }
        public void SetPlaceForSearching(Point leftTopAngle, Point rightBottomAngle)
        {
            SetPlaceForSearching(new Rectangle(leftTopAngle, 
                new Size(rightBottomAngle.X-leftTopAngle.X, rightBottomAngle.Y - leftTopAngle.Y)));
        }
        public void SetPlaceForSearching(int leftLine, int topLine, int rightLine, int bottomLine)
        {
            SetPlaceForSearching(new Point(leftLine, topLine), new Point(rightLine, bottomLine));
        }


        //Область, в которой выполняется поиск КОНЕЦ

        //Скриншот активного окна НАЧАЛО
        //Не забыть, что для подсчета координат крусора надо учитывать местоположение окна относительно начала экрана
        
        private bool screeningWindowPrivate;
        public bool screeningWindow
        {
            get { return this.screeningWindowPrivate; }
        }


        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public void ScreenShotActiveWindow()
        {
            RECT rectangleOfActiveWindow;
            //Получаю дескриптор активного окна
            IntPtr pointerOnActiveWindow = GetForegroundWindow();
            //Получаю по дескриптору его размер и координаты(в прямоугольнике)
            GetWindowRect(pointerOnActiveWindow, out rectangleOfActiveWindow);
            /*System.Windows.Forms.MessageBox.Show(Convert.ToString(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width) + 
                "; " + Convert.ToString(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height));
                */

            //Почему-то окно на весь экран имеет начало координат -8; -8. Так же окно может иметь отрицательные координаты
            if(rectangleOfActiveWindow.Left < 0)
            {
                rectangleOfActiveWindow.Right -= rectangleOfActiveWindow.Left;
                rectangleOfActiveWindow.Left = 0;
            }
            if (rectangleOfActiveWindow.Top < 0)
            {
                rectangleOfActiveWindow.Bottom -= rectangleOfActiveWindow.Top;
                rectangleOfActiveWindow.Top = 0;
            }
            //Оригинальное разрешение почему-то всегда масштабируется и становиться 0.8 от оригинала.
            //Это потому что в винде у меня стоит мастаирование всего на 125%. Если это убрать и оставить 100%, все будет ок.
            //Потому возвращаю его назад
            rectangleOfActiveWindow.Right = (int)(rectangleOfActiveWindow.Right * getScalingFactor());
            rectangleOfActiveWindow.Left = (int)(rectangleOfActiveWindow.Left * getScalingFactor());
            rectangleOfActiveWindow.Top = (int)(rectangleOfActiveWindow.Top * getScalingFactor());
            rectangleOfActiveWindow.Bottom = (int)(rectangleOfActiveWindow.Bottom * getScalingFactor());


            //Окно это просто область, где надо искать
            SetPlaceForSearching(rectangleOfActiveWindow.Left, rectangleOfActiveWindow.Top,
                rectangleOfActiveWindow.Right, rectangleOfActiveWindow.Bottom);
            /*
            //Запомнить, где находиться окно на экране
            locationOfPlaceForSearchPrivate = new Point(rectangleOfActiveWindow.Left, rectangleOfActiveWindow.Top);

            //Создать картинку нужного размера
            this.pictureSearchArea = new Bitmap(
                //rectangleOfActiveWindow.Bottom- rectangleOfActiveWindow.Top,
                rectangleOfActiveWindow.Right- rectangleOfActiveWindow.Left,
                rectangleOfActiveWindow.Bottom - rectangleOfActiveWindow.Top,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //Заполнить картинку изображение активного окна с учетом знания его координат и размера
            //Получается чуть больше по краям хватает, но это не очень страшно, по логике.
            CreateScreenShot(rectangleOfActiveWindow.Left, rectangleOfActiveWindow.Top);
            */
            screeningWindowPrivate = true;
        }


        //Скриншот активного окна КОНЕЦ

        //Выполнение поиска НАЧАЛО

        private bool ComparisonUpLeftDiagonalOfmodelAndAreaForSearch(Point pointBeginModelOnSerachArea)
        {
            //Находится меньшая из сторон
            int lesserSide = 
                pictureModelForSearch.Height > pictureModelForSearch.Width ? pictureModelForSearch.Width : pictureModelForSearch.Height;

            //Просматривается диагональ, любое несовпадение завершает проверку возвращая ложь
            for (int i = 0; i < lesserSide; i++)
                if (!CheckColorPixelInPoint(pictureModelForSearch.GetPixel(i, i),
                    pictureSearchArea.GetPixel(pointBeginModelOnSerachArea.X + i, pointBeginModelOnSerachArea.Y + i)))
                    return false;

            return true;
        }

        private bool ComparisonOfmodelAndAreaForSearch(Point pointBeginModelOnSerachArea)
        {
            //Просматривается вся площадь, любое несовпадение завершает проверку возвращая ложь
            for (int i = 0; i < pictureModelForSearch.Width; i++)
                for (int j = 0; j < pictureModelForSearch.Height; j++)
                {
                    if (!CheckColorPixelInPoint(pictureModelForSearch.GetPixel(i, j),
                        pictureSearchArea.GetPixel(pointBeginModelOnSerachArea.X + i, pointBeginModelOnSerachArea.Y + j)))
                        return false;
                }

            return true;
        }

        /// <summary>
        /// Выполняет поиск эталона с учетом указанных параметров поиска и записывает в поле foundPoints нынешнего экземпляра. 
        /// Если принимает true то ищет только до первой  попавшейся точки.
        /// </summary>
        /// <param name="stopSearchingAfterFirstPointFound"></param>
        /// <returns></returns>
        public bool SearchModelInArea(bool stopSearchingAfterFirstPointFound)
        {
            //Создать список для записи найденых точек
            List<Point> listFoundPoints = null; 
            //Определить по какому пикселю будет идти поиск. Искать по игнорируемому цвету нельзя.
            Point pixelOfModelForSearch = new Point();
            if (listOfIgnorColors == null)//Если нет игнорируемых цветов
                pixelOfModelForSearch = new Point(0, 0);
            else
            {
                bool isFindPictureModelForSearch = false;
                for (int i = 0; i < pictureModelForSearch.Width; i++)
                {
                    for (int j = 0; j < pictureModelForSearch.Height; j++)
                    {
                        if (!IsColorForIgnor(pictureModelForSearch.GetPixel(i, j)))
                        {
                            isFindPictureModelForSearch = true;
                            pixelOfModelForSearch = new Point(i, j);
                        }

                        if (isFindPictureModelForSearch)//Нашел цвет, который не надо игнорить? Беги дальше!
                            break;
                    }
                    if (isFindPictureModelForSearch)//Нашел цвет, который не надо игнорить? Беги дальше!
                        break;
                }
            }

            //Начинать поиск надо не с начала, а сделав отступ сверху и слева на столько же, на сколько смещен искомый
            //пиксель в эталоне, дабы не вылезать за верхнюю и левую границу оалсти поиска.
            //Впрочем, этот пискель моет быть и началом.
            //Так же это учитывается и снизу, и справа
            for(int i= pixelOfModelForSearch.X;
                i<(pictureSearchArea.Width-pictureModelForSearch.Width+pixelOfModelForSearch.X); i++)
                for(int j = pixelOfModelForSearch.Y;
                    j < (pictureSearchArea.Height - pictureModelForSearch.Height + pixelOfModelForSearch.Y); j++)
                {
                    if(!IsColorForIgnor(pictureSearchArea.GetPixel(i, j)))//Если цвет не игнорируется
                    {
                        if(CheckColorPixelInPoint(pictureSearchArea.GetPixel(i, j), 
                            pictureModelForSearch.GetPixel(pixelOfModelForSearch.X, pixelOfModelForSearch.Y)))
                        {//Если пиксели совпали, надо сравнить диагонали
                            if(ComparisonUpLeftDiagonalOfmodelAndAreaForSearch(new Point(i- pixelOfModelForSearch.X, j- pixelOfModelForSearch.Y)))
                            {//A если совпали и они, то полностью площадь эталона
                                if(ComparisonOfmodelAndAreaForSearch(new Point(i - pixelOfModelForSearch.X, j - pixelOfModelForSearch.Y)))
                                {
                                    if (listFoundPoints == null)
                                        listFoundPoints = new List<Point>();

                                    if (screeningWindow)//Если поиск был в окне, то местонахождение окна надо прибавить
                                    {
                                        listFoundPoints.Add(new Point(
                                            i - pixelOfModelForSearch.X + this.locationOfPlaceForSearchPrivate.X,
                                            j - pixelOfModelForSearch.Y + this.locationOfPlaceForSearchPrivate.Y));
                                    }
                                    else
                                    {
                                        listFoundPoints.Add(new Point(i - pixelOfModelForSearch.X, j - pixelOfModelForSearch.Y));
                                    }

                                    if (stopSearchingAfterFirstPointFound)
                                    {
                                        ListToMassiveOfFoundPoints(listFoundPoints);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            if (listFoundPoints != null)
            {
                ListToMassiveOfFoundPoints(listFoundPoints);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Делает список массивом найденных точек. Корректирует местонахождение точек, если в windows есть масштабирование.
        /// </summary>
        /// <param name="listFoundPointsIn"></param>
        private void ListToMassiveOfFoundPoints(List<Point> listFoundPointsIn)
        {
            this.foundPoints = listFoundPointsIn.ToArray();

            //В условиях масштабирования windows привит координаты к правильным значениям
            for (int i = 0; i < foundPoints.Length; i++)
            {
                this.foundPoints[i] = new Point(
                (int)(this.foundPoints[i].X / getScalingFactor()),
                (int)(this.foundPoints[i].Y / getScalingFactor()));
            }
        }

        //Выполнение поиска КОНЕЦ

        //Вспомогательные методы:
        public void CreateScreenShot()
        {
            using (Graphics gdest = Graphics.FromImage(this.pictureSearchArea))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    
                    IntPtr hSrcDC;
                    IntPtr hDC;
                    int retval;
                    hSrcDC = gsrc.GetHdc();
                    hDC = gdest.GetHdc();
                    retval = BitBlt(hDC, -this.locationOfPlaceForSearchPrivate.X, -this.locationOfPlaceForSearchPrivate.Y,
                        this.pictureSearchArea.Width, this.pictureSearchArea.Height,
                        hSrcDC, 0, 0, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
        }
        /// <summary>
        /// Заполняет эталон небольшой картинкой, которая означает, что он пуст
        /// </summary>
        private void CreateBitmapForEmptyModel()
        {
            this.pictureModelForSearch = new Bitmap(50, 50);
            for(int i=0; i< this.pictureModelForSearch.Width; i++)
                for(int j=0; j<this.pictureModelForSearch.Height; j++)
                {
                    if(i==j || i==(this.pictureModelForSearch.Height-j-1))
                    {
                        this.pictureModelForSearch.SetPixel(i, j, Color.FromArgb(255, 0, 0));
                    }
                    else
                    {
                        this.pictureModelForSearch.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
        }
        private bool CheckColorPixelInPoint(Color firstColor, Color secondColor)
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
        
        public void ScreenshotFullMonitor()
        {
            //Получаю размер экрана в пикселях.
            Size resolutionOfFullScreen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            //Оригинальное разрешение почему-то всегда масштабируется и становиться 0.8 от оригинала.
            //Это потому что в винде у меня стоит мастаирование всего на 125%. Если это убрать и оставить 100%, все будет ок.
            //Потому возвращаю его назад

            this.pictureSearchArea = new Bitmap(
                (int)(resolutionOfFullScreen.Width * getScalingFactor()),
                (int)(resolutionOfFullScreen.Height * getScalingFactor()));
            CreateScreenShot();

        }

        //Помогает узнать масштабирование системы
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }
        /// <summary>
        /// Сообщает соотношение между логическим и физическим разрешениями монитора(используется при поиске во время масштабирования
        /// интерфейса windows).
        /// </summary>
        /// <returns></returns>
        public static float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }

        private static Bitmap CutSmallPictureFromLargePicture(Point locationStartOfLargePicture, Bitmap largePicture, int widthSmallPicture, int heightSmallPicture)
        {
            Bitmap smallPicture = new Bitmap(widthSmallPicture, heightSmallPicture, PixelFormat.Format32bppArgb);
            for (int i = 0; i < widthSmallPicture; i++)
                for (int j = 0; j < heightSmallPicture; j++)
                {
                    smallPicture.SetPixel(i, j, largePicture.GetPixel(locationStartOfLargePicture.X + i, locationStartOfLargePicture.Y + j));
                }
            return smallPicture;
        }
    }
}
