using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;

namespace MyLittleMinion
{
    [Serializable]
    /// <summary>
    /// Это класс поиска.
    /// Он позволяет искать заданный эталон на экране, а так же предоставляет инструменты для ускорения поиска этого эталона.
    /// </summary>
    class Search
    {
        /// <summary>
        /// Bitmap для хранения области, в которой осуществляется поиск эталона. По умолчанию null.
        /// </summary>
        public Bitmap pictureSearchArea { get; set; }
        
        /// <summary>
        /// Массив точек, которые были найдены во время поиска. По умолчанию null.
        /// </summary>
        public Point[] foundPoints;
        /// <summary>
        /// Инициализирует новый экземпляр класса поиска.
        /// </summary>
        public Search()
        {
            this.pictureSearchArea = null;
            this.pictureModelForSearchPrivate = null;
            this.correctModelPrivate = null;
            this.aimModelPrivate = new Point(0,0);
            this.foundPoints = null;

            this.UsePlaceForSearch = false;
            this.locationOfPlaceForSearchPrivate = new Point();
            this.UseActiveWindow = false;
            this.isCreateScreenWindowPrivate = false;
            this.pointerOnActiveWindow = default(IntPtr);

            this.UseIgnorColors = false;
            this.listOfIgnorColors = null;
            this.numberIgnorColorInListPrivate = 0;
            this.isIgnorColorsPrivate = false;

            this.isEnableFourThreadsPrivate = false;

            this.percentageComplianceWithModelPivate = 100;
            this.stopSearchingAfterFirstPointFound = true;

            this.SetPlaceForSearchForFullMonitor();
            this.CreateBitmapForEmptyModel();
        }


        ///Уточнение образа эталона для более точного поиска НАЧАЛО

        private Point aimModelPrivate;
        public Point aimModel {
            get { return this.aimModelPrivate; }
            set
            {
                aimModelPrivate = value;
                //Чтобы лишнего нельзя было сюда запихать, только внутри эталона может быть прицел.
                if (value.X < 0 || value.X > this.pictureModelForSearchPrivate.Width)
                    aimModelPrivate.X = 0;
                if (value.Y < 0 || value.Y > this.pictureModelForSearchPrivate.Height)
                    aimModelPrivate.Y = 0;
            }
        }
        private Bitmap pictureModelForSearchPrivate;
        private Bitmap correctModelPrivate;
        public Bitmap correctModel { get { return this.correctModelPrivate; } }
        /// <summary>
        /// Bitmap для хранения эталона, поиск местонахождения которого выполняется.
        /// По умолчанию храниться картинка размером 50х50: две красные диагонали на белом фоне.
        /// </summary>
        public Bitmap pictureModelForSearch
        {
            get
            {
                return this.pictureModelForSearchPrivate;
            }
            set
            {
                this.aimModel = new Point(0, 0);
                this.pictureModelForSearchPrivate = value;
                this.correctModelPrivate = value;
            }
        }
        public void CorrectionModel(Bitmap newImageForCorrectModel)
        {
            List<Color> listColorsnewImageForCorrectModel = new List<Color>();
            List<Color> listColorscorrectModel = new List<Color>();
            Task threadForBitmapInListColors1 = Task.Run(() => { listColorsnewImageForCorrectModel = BreakOnColors(newImageForCorrectModel); });
            Task threadForBitmapInListColors2 = Task.Run(() => { listColorscorrectModel = BreakOnColors(this.correctModelPrivate); });
            threadForBitmapInListColors1.Wait();
            threadForBitmapInListColors2.Wait();
            List<Color> mergedList = MergerTwoListAmountOfColor(listColorscorrectModel, listColorsnewImageForCorrectModel);

            Bitmap newImageForChangeColors = new Bitmap(this.correctModelPrivate.Width, this.correctModelPrivate.Height);
            for (int i = 0; i < this.correctModelPrivate.Width; i++)
                for (int j = 0; j < this.correctModelPrivate.Height; j++)
                {

                    if (!ListColorsHaveColor(mergedList, this.correctModelPrivate.GetPixel(i, j)))
                    {
                        System.Drawing.Color newAddColor = System.Drawing.Color.FromArgb(0,
                            this.correctModelPrivate.GetPixel(i, j).R, this.correctModelPrivate.GetPixel(i, j).G, this.correctModelPrivate.GetPixel(i, j).B);
                        newImageForChangeColors.SetPixel(i, j, newAddColor);
                    }
                    else
                    {
                        newImageForChangeColors.SetPixel(i, j, this.correctModelPrivate.GetPixel(i, j));
                    }
                }

            this.correctModelPrivate = newImageForChangeColors;
        }
        private List<Color> BreakOnColors(Bitmap picture)
        {
            List<Color> colorsList = new List<Color>();
            for (int i = 0; i < picture.Width; i++)
                for (int j = 0; j < picture.Height; j++)
                    if (!ListColorsHaveColor(colorsList, picture.GetPixel(i, j)) && picture.GetPixel(i, j).A != 0)
                        colorsList.Add(picture.GetPixel(i, j));

            return colorsList;
        }
        private List<Color> MergerTwoListAmountOfColor(List<Color> colorsList1, List<Color> colorsList2)
        {
            List<Color> mergedColorsList = new List<Color>();
            for (int i = 0; i < colorsList1.Count; i++)
            {
                if (ListColorsHaveColor(colorsList2, colorsList1[i]))
                {
                    mergedColorsList.Add(colorsList1[i]);
                }
            }

            return mergedColorsList;
        }
        private bool ListColorsHaveColor(List<Color> colorList, System.Drawing.Color colorForCheck)
        {
            foreach (var color in colorList)
                if(CheckForEqualityOfTwoColorsByRGB(color, colorForCheck))
                    return true;

            return false;
        }
        /// <summary>
        /// Заполняет эталон небольшой картинкой, которая означает, что он пуст.
        /// Две красные дигонали на белом фоне. Размер картинки 50х50.
        /// </summary>
        private void CreateBitmapForEmptyModel()
        {
            this.pictureModelForSearchPrivate = new Bitmap(50, 50);
            for (int i = 0; i < this.pictureModelForSearchPrivate.Width; i++)
                for (int j = 0; j < this.pictureModelForSearchPrivate.Height; j++)
                {
                    if (i == j || i == (this.pictureModelForSearchPrivate.Height - j - 1))
                    {
                        this.pictureModelForSearchPrivate.SetPixel(i, j, Color.FromArgb(255, 0, 0));
                    }
                    else
                    {
                        this.pictureModelForSearchPrivate.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            this.pictureModelForSearch = this.pictureModelForSearchPrivate;
            this.correctModelPrivate = this.pictureModelForSearchPrivate;
        }

        ///Уточнение образа эталона для более точного поиска КОНЕЦ



        ///цвета, которые надо игнорировать НАЧАЛО

        /// <summary>
        /// Определяет будут ли использоваться игнорируемые цвета.
        /// </summary>
        public bool UseIgnorColors { get; set; }
        /// <summary>
        /// Внутреннее поле для хранения сведений о наличии игнорируемых цветов.
        /// </summary>
        private bool isIgnorColorsPrivate;
        /// <summary>
        /// Внешнее свойство для получния сведений о наличии игнорируемых цветов.
        /// </summary>
        public bool isIgnorColors
        {
            get { return this.isIgnorColorsPrivate; }
        }
        /// <summary>
        /// Список игнорируемых цветов. Очень нежелательно большое количество игнорируемых цветов. Чем меньше, тем лучше.
        /// </summary>
        private List<Color> listOfIgnorColors;
        /// <summary>
        /// Внутренний счетчик игнорируемых цветов.
        /// </summary>
        private UInt16 numberIgnorColorInListPrivate;
        /// <summary>
        /// Внешнее свойство для взаимодействия с счетчиком игнорируемых цветов.
        /// </summary>
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
        /// <summary>
        /// Добавляет цвет в список игнорируемых.
        /// </summary>
        /// <param name="newColorForIgnor"></param>
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
                    if (CheckForEqualityOfTwoColorsByRGB(color, newColorForIgnor))
                        isAddColor = false;
                }
                if (isAddColor)
                {
                    this.numberIgnorColorInListPrivate++;
                    this.listOfIgnorColors.Add(newColorForIgnor);
                }
            }
        }
        /// <summary>
        /// Добавляет все цвета из экземпляра Bitmap в список игнорируемых.
        /// </summary>
        /// <param name="picture"></param>
        public void AddIgnorColorsInPicture(Bitmap picture)
        {
            //добавить сразу несколько цветов удобно, да?
            for(int i=0; i<picture.Width; i++)
                for(int j=0; j<picture.Height;j++)
                {
                    AddIgnorColor(picture.GetPixel(i, j));
                }
        }
        /// <summary>
        /// Добавляет в список игнорируемых цвет, который находится в месте с заданными координатами.
        /// </summary>
        /// <param name="pointNewColorForIgnor"></param>
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
        /// <summary>
        /// Удаляет из списока игнорируемых цвет, номер которого соответсвует счетчику.
        /// </summary>
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
        /// <summary>
        /// Проверяет, есть ли цвет в списке игнорируемых. Если есть, то возвращает true.
        /// </summary>
        /// <param name="colorForTest"></param>
        /// <returns></returns>
        public bool IsColorForIgnor(Color colorForTest)
        {
            if(this.listOfIgnorColors!=null)
            {
                foreach(Color ignorColors in this.listOfIgnorColors)
                {
                    if(CheckForEqualityOfTwoColorsByRGB(ignorColors, colorForTest))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Показывает цвет, который соотвествует выбраному номеру.
        /// Если цветов нет, то возвращает прозрачный. Т.е. с альфа-каналом равным 0.
        /// </summary>
        public Color ShowIgnorColor()
        {
            if (this.listOfIgnorColors != null)
                return listOfIgnorColors[numberIgnorColorInListPrivate];
            else
                return Color.FromArgb(0, 255, 255, 255);
        }
        /// <summary>
        /// Проверяет наличие цвета в Bitmap. Сравнивает только свойства R, G и B.
        ///  Учитывает альфа-канал: прозрачный цвет(А=0) считается отсутствующим, сравнение с ним всегда false.
        /// </summary>
        /// <param name="colorForCheck"></param>
        /// <param name="bitmapForCheck"></param>
        /// <returns></returns>
        public static bool CheckColorIsInBitmap(Color colorForCheck, Bitmap bitmapForCheck)
        {
            if (bitmapForCheck == null)
                return false;

            for (int i = 0; i < bitmapForCheck.Width; i++)
                for (int j = 0; j < bitmapForCheck.Height; j++)
                    if (CheckForEqualityOfTwoColorsByRGB(colorForCheck, bitmapForCheck.GetPixel(i, j))//Если эти пиксели по RGB равны, то есть.
                        && bitmapForCheck.GetPixel(i, j).A!=0)//Но даже если по RGB равны, а цвет прозрачный, то счиать, что искомого цвета нет.
                        return true;

            return false;
        }
        /// <summary>
        /// Удаляет цвета из списка игнорируемых, если их нет в эталоне.
        /// </summary>
        private void RemoveIgnorColorsThatAreNotInModel()
        {
            List<Color> newIgnorColorList = new List<Color>();
            this.numberIgnorColorInList = 0;
            if (this.listOfIgnorColors != null)
            {
                foreach (Color ignorColor in this.listOfIgnorColors)
                {
                    if (CheckColorIsInBitmap(ignorColor, this.pictureModelForSearchPrivate))
                    {
                        newIgnorColorList.Add(ignorColor);
                    }
                }
            }
            if (newIgnorColorList.Count == 0)
                this.listOfIgnorColors = null;
            else
                this.listOfIgnorColors = newIgnorColorList;
        }

        ///цвета, которые надо игнорировать КОНЕЦ




        ///Область, в которой выполняется поиск НАЧАЛО

        /// <summary>
        /// Определяет будет ли использоваться область, в которой выполняется поиск.
        /// </summary>
        public bool UsePlaceForSearch { get; set; }
        /// <summary>
        /// Внутреннее поле для хранения расположения прямоугольника области, где выполняется поиск.
        /// </summary>
        private Point locationOfPlaceForSearchPrivate;
        /// <summary>
        /// Возвращает значение поля для хранения расположения прямоугольника области, где выполняется поиск.
        /// </summary>
        public Point GetLocationOfPlaceForSearch()
        {
            return this.locationOfPlaceForSearchPrivate;
        }
        /// <summary>
        /// Задает значение расположения и размеров прямоугольника области, где выполняется поиск.
        /// Принимает прямоугольник содержащий расположение и размеры.
        /// Если ширина или высота оказывается меньше 1, то этот параметр устанавливается равным 1.
        /// </summary>
        public void SetPlaceForSearching(Rectangle placeForSearching)
        {
            if (placeForSearching.Width < 1) placeForSearching.Width = 1;
            if (placeForSearching.Height < 1) placeForSearching.Height = 1;
            this.locationOfPlaceForSearchPrivate.X = placeForSearching.X;
            this.locationOfPlaceForSearchPrivate.Y = placeForSearching.Y;
            this.pictureSearchArea = new Bitmap(placeForSearching.Width, placeForSearching.Height);
        }
        /// <summary>
        /// Задает значение расположения и размеров прямоугольника области, где выполняется поиск.
        /// Принимает расположение верней левой и нижней правой точек.
        /// </summary>
        public void SetPlaceForSearching(Point leftTopAngle, Point rightBottomAngle)
        {
            SetPlaceForSearching(new Rectangle(leftTopAngle, 
                new Size(rightBottomAngle.X-leftTopAngle.X, rightBottomAngle.Y - leftTopAngle.Y)));
        }
        /// <summary>
        /// Задает значение расположения и размеров прямоугольника области, где выполняется поиск.
        /// Принимает четыре значения границ прямоугольника: левой, верхней, правой и нижней
        /// </summary>
        public void SetPlaceForSearching(int leftLine, int topLine, int rightLine, int bottomLine)
        {
            SetPlaceForSearching(new Point(leftLine, topLine), new Point(rightLine, bottomLine));
        }
        /// <summary>
        /// Устанавливает размер области поиска равный размеру всего экарана.
        /// </summary>
        public void SetPlaceForSearchForFullMonitor()
        {
            //Получаю размер экрана в пикселях.
            Size resolutionOfFullScreen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;

            //Оригинальное разрешение может масштабироваться системой. Потому с помощью метода getScalingFactor координаты приводятся к оригинальным
            this.locationOfPlaceForSearchPrivate = new Point(0, 0);
            this.pictureSearchArea = new Bitmap(
                (int)(resolutionOfFullScreen.Width * getScalingFactor()),
                (int)(resolutionOfFullScreen.Height * getScalingFactor()));
        }

        ///Область, в которой выполняется поиск КОНЕЦ




        ///Скриншот активного окна НАЧАЛО

        /// <summary>
        /// Определяет будет ли использоваться активное окно как область, в которой выполняется поиск.
        /// </summary>
        public bool UseActiveWindow { get; set; }
        /// <summary>
        /// Хранит внутренее поле дающего информацию о том, был сделан скриншот активного окна.
        /// </summary>
        private bool isCreateScreenWindowPrivate;
        /// <summary>
        /// Возвращает значение дающее информацию о том, был сделан скриншот активного окна.
        /// </summary>
        public bool isCreateScreenWindow
        {
            get { return this.isCreateScreenWindowPrivate; }
        }
        /// <summary>
        /// Хранит указатель на активное окно. По умолчанию равен default(IntPtr).
        /// </summary>
        public IntPtr pointerOnActiveWindow { get; set; }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
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

        /// <summary>
        /// Устанавливает активное окно в качестве прямоугольника области для поиска.
        /// </summary>
        public void SetActiveWindowForPlaceForSearching()
        {
            RECT rectangleOfActiveWindow;
            //Запомнить дескриптор активного окна
            RemmemberPointerOnActiveWindow();
            //Получить по дескриптору размер и координаты(в прямоугольнике) окна по указателю, который был запомнен
            GetWindowRect(this.pointerOnActiveWindow, out rectangleOfActiveWindow);
            
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
            //Оригинальное разрешение может масштабироваться системой. Потому с помощью метода getScalingFactor координаты приводятся к оригинальным
            rectangleOfActiveWindow.Right = (int)(rectangleOfActiveWindow.Right * getScalingFactor());
            rectangleOfActiveWindow.Left = (int)(rectangleOfActiveWindow.Left * getScalingFactor());
            rectangleOfActiveWindow.Top = (int)(rectangleOfActiveWindow.Top * getScalingFactor());
            rectangleOfActiveWindow.Bottom = (int)(rectangleOfActiveWindow.Bottom * getScalingFactor());

            //Окно это просто область, где надо искать
            SetPlaceForSearching(rectangleOfActiveWindow.Left, rectangleOfActiveWindow.Top,
                rectangleOfActiveWindow.Right, rectangleOfActiveWindow.Bottom);
            
            isCreateScreenWindowPrivate = true;
        }
        /// <summary>
        /// Запомнить указатель на активное окно.
        /// </summary>
        public void RemmemberPointerOnActiveWindow()
        {

            this.pointerOnActiveWindow = GetForegroundWindow();
        }

        ///Скриншот активного окна КОНЕЦ




        ///Выполнение поиска НАЧАЛО

        /// <summary>
        /// Определяет выполнятеся ли поиск до первого найденого элемента.
        /// </summary>
        public bool stopSearchingAfterFirstPointFound { get; set; }
        private byte percentageComplianceWithModelPivate;
        /// <summary>
        /// Свойство процентного соответсвия эталону. Значение может быть от 1 до 100, где 100 полное соответсвие эталону(100%).
        /// По умолчанию 100. Если неправильно задано значение, свойство будет установлено равным 100.
        /// Из-за дополнительных рассчетов на пиксель может сильно упасть производительность и скорость поиска.
        /// </summary>
        public byte percentageComplianceWithModel
        {
            get { return this.percentageComplianceWithModelPivate; }
            set
            {
                if (value < 1 || value > 100)
                    this.percentageComplianceWithModel = 100;
                else
                    this.percentageComplianceWithModelPivate = value;
            }
        }
        private bool ComparisonUpLeftDiagonalOfmodelAndAreaForSearch(Point pointBeginModelOnSerachArea)
        {
            //Находится меньшая из сторон
            int lesserSide =
                correctModelPrivate.Height > correctModelPrivate.Width ? correctModelPrivate.Width : correctModelPrivate.Height;

            //Если установлено полное соответствие эталону, используется алгоритм без подсчета для ускорения проверки.
            if (percentageComplianceWithModelPivate == 100)
            {
                //Просматривается диагональ, любое несовпадение завершает проверку возвращая ложь
                for (int i = 0; i < lesserSide; i++)
                    if (!CheckForEqualityOfTwoColorsByRGB(correctModelPrivate.GetPixel(i, i),
                    pictureSearchArea.GetPixel(pointBeginModelOnSerachArea.X + i, pointBeginModelOnSerachArea.Y + i)))
                            return false;

                return true;
            }
            else
            {
              
                //Счетчик для учета процентного соответствия эталону
                int counterPercentageCompliance = 0;
                //Просматривается диагональ
                for (int i = 0; i < lesserSide; i++)
                    if (CheckForEqualityOfTwoColorsByRGB(correctModelPrivate.GetPixel(i, i),
                        pictureSearchArea.GetPixel(pointBeginModelOnSerachArea.X + i, pointBeginModelOnSerachArea.Y + i)))
                        counterPercentageCompliance++;

                //Положительный рензультат выдается, если счетчик насчитал достаточную сумму, которая не меньше, чем указаное процентное соотношение
                if ((counterPercentageCompliance * 100) / lesserSide < percentageComplianceWithModelPivate)
                    return false;
                else
                    return true;
            }
        }

        private bool ComparisonOfModelAndAreaForSearch(Point pointBeginModelOnSerachArea)
        {
            //Если установлено полное соответствие эталону, используется алгоритм без подсчета для ускорения проверки.
            if (percentageComplianceWithModelPivate == 100)
            {
                //Просматривается вся площадь, любое несовпадение завершает проверку возвращая ложь
                for (int i = 0; i < correctModelPrivate.Width; i++)
                    for (int j = 0; j < correctModelPrivate.Height; j++)
                        if (!CheckForEqualityOfTwoColorsByRGB(correctModelPrivate.GetPixel(i, j),//Если цвета не совпадают, то можно смело выходить
                        pictureSearchArea.GetPixel(pointBeginModelOnSerachArea.X + i, pointBeginModelOnSerachArea.Y + j)))
                            return false;

                return true;
            }
            else
            {
                //Счетчик для учета процентного соответствия эталону
                int counterPercentageCompliance = 0;
                //Просматривается вся площадь
                for (int i = 0; i < correctModelPrivate.Width; i++)
                    for (int j = 0; j < correctModelPrivate.Height; j++)
                        if (CheckForEqualityOfTwoColorsByRGB(correctModelPrivate.GetPixel(i, j),
                            pictureSearchArea.GetPixel(pointBeginModelOnSerachArea.X + i, pointBeginModelOnSerachArea.Y + j)))
                            counterPercentageCompliance++;

                //Положительный рензультат выдается, если счетчик насчитал достаточную сумму, которая не меньше, чем указаное процентное соотношение
                if ((counterPercentageCompliance * 100) / (correctModelPrivate.Width * correctModelPrivate.Height) < percentageComplianceWithModelPivate)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Выполняет поиск эталона с учетом указанных параметров поиска и записывает в поле foundPoints нынешнего экземпляра. 
        /// Если stopSearchingAfterFirstPointFound = true, то ищет только до первой попавшейся точки.
        /// </summary>
        /// <param name="stopSearchingAfterFirstPointFound"></param>
        /// <returns></returns>
        public bool SearchModelInArea()
        {
            //Смешно подумать о таком, но...
            //Если эталон больше области поиска, его не стоит в ней искать.
            if (pictureModelForSearchPrivate.Width > pictureSearchArea.Width || pictureModelForSearchPrivate.Height > pictureSearchArea.Height)
                return false;

            //Перед поиском новых точек старые надо забыть
            this.foundPoints = null;
            //Создать список для записи найденых точек
            List<Point> listFoundPoints = null;

            //Если игнорируемых цветов нет в эталоне, то их надо исключить из спика игнорируемых, 
            //т.к. они в любом случае будут проигнорированы из-за отсутсвия в эталоне.
            //Если этого не сделать, то они будут проверяться в пустую, что может замедлить 
            //выполнение: в лучшем случае скорость останется такой же.
            RemoveIgnorColorsThatAreNotInModel();

            //Определить по какому пикселю будет идти поиск. Искать по игнорируемому цвету нельзя.
            Point pixelOfModelForSearch = new Point();
            if (listOfIgnorColors == null)//Если нет игнорируемых цветов
            {

                bool isFindPictureModelForSearch = false;
                for (int i = 0; i < correctModelPrivate.Width; i++)
                {
                    for (int j = 0; j < correctModelPrivate.Height; j++)
                    {
                        if (correctModelPrivate.GetPixel(i, j).A!=0)//Если не прозрачный, т.к. прозрачные надо игнорировать
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
            else
            {
                bool isFindPictureModelForSearch = false;
                for (int i = 0; i < correctModelPrivate.Width; i++)
                {
                    for (int j = 0; j < correctModelPrivate.Height; j++)
                    {
                        if (!IsColorForIgnor(correctModelPrivate.GetPixel(i, j)))
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
            //пиксель в эталоне, дабы не вылезать за верхнюю и левую границу области поиска.
            //Впрочем, этот пискель может быть и началом.
            //Так же это учитывается и снизу, и справа
            for(int i= pixelOfModelForSearch.X;
                i<(pictureSearchArea.Width- correctModelPrivate.Width+pixelOfModelForSearch.X); i++)
                for(int j = pixelOfModelForSearch.Y;
                    j < (pictureSearchArea.Height - correctModelPrivate.Height + pixelOfModelForSearch.Y); j++)
                {
                    if(!IsColorForIgnor(pictureSearchArea.GetPixel(i, j)))//Если цвет не игнорируется
                    {
                        if(CheckForEqualityOfTwoColorsByRGB(pictureSearchArea.GetPixel(i, j),
                            correctModelPrivate.GetPixel(pixelOfModelForSearch.X, pixelOfModelForSearch.Y)))
                        {//Если пиксели совпали, надо сравнить диагонали
                            if(ComparisonUpLeftDiagonalOfmodelAndAreaForSearch(new Point(i- pixelOfModelForSearch.X, j- pixelOfModelForSearch.Y)))
                            {//A если совпали и они, то полностью площадь эталона

                                if (ComparisonOfModelAndAreaForSearch(new Point(i - pixelOfModelForSearch.X, j - pixelOfModelForSearch.Y)))
                                {
                                    if (listFoundPoints == null)
                                        listFoundPoints = new List<Point>();
                                    
                                    listFoundPoints.Add(new Point(
                                        i - pixelOfModelForSearch.X + this.locationOfPlaceForSearchPrivate.X,
                                        j - pixelOfModelForSearch.Y + this.locationOfPlaceForSearchPrivate.Y));
                                    if (this.stopSearchingAfterFirstPointFound)
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
                SortAfterEndSearching();
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

            //В условиях масштабирования windows приводит координаты к правильным значениям
            for (int i = 0; i < foundPoints.Length; i++)
            {
                this.foundPoints[i] = new Point(
                (int)(this.foundPoints[i].X / getScalingFactor()),
                (int)(this.foundPoints[i].Y / getScalingFactor()));
            }

            //Добавление смещения согласно прицелу на эталоне
            for (int i = 0; i < foundPoints.Length; i++)
            {
                this.foundPoints[i] = new Point(
                this.foundPoints[i].X + aimModelPrivate.X,
                this.foundPoints[i].Y + aimModelPrivate.Y);
            }
        }

        ///Выполнение поиска КОНЕЦ



        ///Выполнение поиска с использованием потоков НАЧАЛО
        
        /// <summary>
        /// Использование многопоточности во время поиска. 0 - Четыре потока для четвертей по углам. 1 или больше, число становиться количеством потоков: область поиска делиться потоками на столбцы.
        /// </summary>
        public int multyThreadSearch { get; set; }


        /// <summary>
        /// Хранит информацию о том, идет ли выполнение многопоточного поиска
        /// </summary>
        /// <param name="listFoundPointsIn"></param>
        private bool isEnableFourThreadsPrivate;
        /// <summary>
        /// Предоставляет информацию о том, идет ли выполнение многопоточного поиска
        /// </summary>
        public bool isEnableFourThreads { get { return this.isEnableFourThreadsPrivate; } }
        
        /// <summary>
        /// Сигнатура делегата для суммирования и погружения методов в потоки
        /// </summary>
        private delegate void IterSearchModelInAreaDelegate();
        /// <summary>
        /// Выполняет обычный поиск, но делит область поиска на четыре части, поиск в каждой части выполняется в своем отдельном потоке.
        /// Вполне может работать медленней, чем последовательный поиск в случае поиска первой попавшейся точки(true).
        /// Выполняет поиск эталона с учетом указанных параметров поиска и записывает в поле foundPoints нынешнего экземпляра. 
        /// Если принимает true, то ищет только до первой попавшейся точки.
        /// </summary>
        public bool SearchModelInAreaInFourThreads()
        {
            //Если эталон слишком велик для области подсчета в потоке и не влезает в него, то может пострадать точность.
            //Поэтому в случае слишком большого рамера ширины или высоты эталона стоит выполнять последовательный поиск.
            if ((this.pictureSearchArea.Width / 2 < correctModelPrivate.Width) ||
                (this.pictureSearchArea.Height / 2 < correctModelPrivate.Height))
            {
                return SearchModelInArea();
            }

            //Здесь начинается параллельный расчет
            this.isEnableFourThreadsPrivate = true;

            //Если игнорируемых цветов нет в эталоне, то их надо исключить из спика игнорируемых, 
            //т.к. они в любом случае будут проигнорированы из-за отсутсвия в эталоне.
            //Если этого не сделать, то они будут проверяться в пустую, что может замедлить 
            //выполнение: в лучшем случае скорость останется такой же.
            RemoveIgnorColorsThatAreNotInModel();

            //Перед поиском новых точек старые надо забыть
            this.foundPoints = null;

            //Для краткости чтения половина ширины и высоты области поиска вычисляются сразу
            int halfWidthPSA = (int)(this.pictureSearchArea.Width * 0.5);
            int halfHeightPSA = (int)(this.pictureSearchArea.Height * 0.5);

            //Инициализация потоков и будущих клонов этого экземпляра для этих потоков
            Search[] fourSearchsForThreadPrivate = new Search[4];
            Thread[] fourThread = new Thread[4];



            //В первый поток отправляется первая четверть
            fourSearchsForThreadPrivate[0] = this.Clone();

            //Задать для этого потока координаты и размер новой области, соответсвующие его четверти
            IterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject1 = (() => fourSearchsForThreadPrivate[0].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X, this.locationOfPlaceForSearchPrivate.Y,
                    //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    halfWidthPSA + this.correctModelPrivate.Width, halfHeightPSA + this.correctModelPrivate.Height
                    )));
            iterSearchModelInAreaDelegateObject1 += (() => fourSearchsForThreadPrivate[0].CreateScreenShot());
            iterSearchModelInAreaDelegateObject1 += (() => fourSearchsForThreadPrivate[0].SearchModelInArea());

            fourThread[0] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject1));
            fourThread[0].Start();



            //Во второй поток отправляется вторая четверть
            fourSearchsForThreadPrivate[1] = this.Clone();

            IterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject2 = (() => fourSearchsForThreadPrivate[1].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X,
                    this.locationOfPlaceForSearchPrivate.Y + halfHeightPSA, //Вертикальная точка начала для этой четверти сдвигается
                    halfWidthPSA + this.correctModelPrivate.Width,//Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    this.pictureSearchArea.Height - halfHeightPSA//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    )));
            iterSearchModelInAreaDelegateObject2 += (() => fourSearchsForThreadPrivate[1].CreateScreenShot());
            iterSearchModelInAreaDelegateObject2 += (() => fourSearchsForThreadPrivate[1].SearchModelInArea());

            fourThread[1] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject2));
            fourThread[1].Start();



            //В третий поток отправляется третья четверть 
            fourSearchsForThreadPrivate[2] = this.Clone();

            IterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject3 = (() => fourSearchsForThreadPrivate[2].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X + halfWidthPSA,//Горизонтальная точка начала для этой четверти сдвигается
                    this.locationOfPlaceForSearchPrivate.Y,
                    this.pictureSearchArea.Width - halfWidthPSA,//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    halfHeightPSA + this.correctModelPrivate.Height//Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    )));
            iterSearchModelInAreaDelegateObject3 += (() => fourSearchsForThreadPrivate[2].CreateScreenShot());
            iterSearchModelInAreaDelegateObject3 += (() => fourSearchsForThreadPrivate[2].SearchModelInArea());

            fourThread[2] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject3));
            fourThread[2].Start();



            //В четвертый поток отправляется четвертая четверть
            fourSearchsForThreadPrivate[3] = this.Clone();

            IterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject4 = (() => fourSearchsForThreadPrivate[3].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X + halfWidthPSA,//Горизонтальная точка начала для этой четверти сдвигается
                    this.locationOfPlaceForSearchPrivate.Y + halfHeightPSA, //Вертикальная точка начала для этой четверти сдвигается
                    this.pictureSearchArea.Width - halfWidthPSA,//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    this.pictureSearchArea.Height - halfHeightPSA//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    )));
            iterSearchModelInAreaDelegateObject4 += (() => fourSearchsForThreadPrivate[3].CreateScreenShot());
            iterSearchModelInAreaDelegateObject4 += (() => fourSearchsForThreadPrivate[3].SearchModelInArea());

            fourThread[3] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject4));
            fourThread[3].Start();


            //Ожидание заершения всех потоков
            for (int i = 0; i < 4; i++)
                fourThread[i].Join();

            //Объединение всех найденых точек
            List<Point> listFoundPointsInFourThread = null;//Создается новый список дл простоты добавления
            for (int i = 0; i < 4; i++)
            {
                //Если есть что и куда добавить, то можно добавлять
                if (listFoundPointsInFourThread != null && fourSearchsForThreadPrivate[i].foundPoints != null)
                {
                    //Просматривается каждый элемент на возможность добавления
                    foreach (Point newPointForList in fourSearchsForThreadPrivate[i].foundPoints)
                    {
                        //Если такого элемента еще нет, то происходит добавление
                        if (!CheckPointInList(listFoundPointsInFourThread, newPointForList))
                        {
                            //Сортировка при добавлении не нужна, т.к. поиск происходит по строкам каждого столбца. Добавление четвертей происходит в таком же порядке.
                            //Т.е. сперва врехняя левая, потом нижняя лева, потом верхняя парвая, и потом нижняя правая
                            listFoundPointsInFourThread.Add(newPointForList);
                        }
                    }
                }
                else//Если массив точек все еще пуст, то ему надо присвоить любой найденый массив точек
                {
                    if (fourSearchsForThreadPrivate[i].foundPoints != null)//Присваивать пустой, конечно. незачем
                    {
                        listFoundPointsInFourThread = new List<Point>();
                        listFoundPointsInFourThread = fourSearchsForThreadPrivate[i].foundPoints.ToList();
                    }
                }
            }
            
            //Здесь параллельный расчет считается законченым
            this.isEnableFourThreadsPrivate = false;

            //Если точки так и не нашлись, надо вернуть ложь, иначе точки есть и возращается истина
            if (listFoundPointsInFourThread == null)
            {
                return false;
            }

            //Записать список точек в массив, отсортировать и вернуть истину
            this.foundPoints = listFoundPointsInFourThread.ToArray();
            SortAfterEndSearching();
            return true;
        }
        public bool SearchModelInAreaInMultyThreads(int countOfThreads)
        {
            //Если эталон слишком велик для области подсчета в потоке и не влезает в него, то может пострадать точность.
            //Поэтому в случае слишком большого рамера ширины эталона стоит выполнять последовательный поиск.
            if (this.pictureSearchArea.Width / countOfThreads < correctModelPrivate.Width)
            {
                return SearchModelInArea();
            }

            //Здесь начинается параллельный расчет
            this.isEnableFourThreadsPrivate = true;

            //Если игнорируемых цветов нет в эталоне, то их надо исключить из спика игнорируемых, 
            //т.к. они в любом случае будут проигнорированы из-за отсутсвия в эталоне.
            //Если этого не сделать, то они будут проверяться в пустую, что может замедлить 
            //выполнение: в лучшем случае скорость останется такой же.
            RemoveIgnorColorsThatAreNotInModel();
            //Перед поиском новых точек старые надо забыть
            this.foundPoints = null;
            //У каждого нового потока будет совй сриншот, незачем передавать туда что-то из нынешнего эукземпляра. Однако его размеры понадобятся.
            Size thispictureSearchArea = new Size(this.pictureSearchArea.Width, this.pictureSearchArea.Height);
            this.pictureSearchArea = null;

            //Для краткости чтения половина ширины области поиска вычисляется сразу
            int partWidthPSA = (int)(thispictureSearchArea.Width / countOfThreads);

            //Инициализация потоков и будущих клонов этого экземпляра для этих потоков
            Search[] muchSearchsForThreadPrivate = new Search[countOfThreads];
            System.Threading.Tasks.Task[] muchTasks = new Task[countOfThreads];

            //Создание клонов нынешнего экземпдяра
            for (int i = 0; i < countOfThreads; i++)
            {
                muchSearchsForThreadPrivate[i] = this.Clone();
            }


            //Передача всех данных в функцию и клонирование клонов, потому что если сразу запускать потоки в цикле,
            //то выскакивает исключение об обращении к одной и той же памяти из разных потоков несомтря на то,
            //что в каждом потоке разные экземляры класса.
            for (int numOfThread = 0; numOfThread < countOfThreads - 1; numOfThread++)//В последней части надо не брать нахлест, т.к. это самый конец
            {
                muchSearchsForThreadPrivate[numOfThread] = TaskFunctionThread(ref muchTasks[numOfThread], muchSearchsForThreadPrivate[numOfThread].Clone(),
                    numOfThread, partWidthPSA, thispictureSearchArea.Height);
            }



            //Последняя часть, без нахлеста
            muchTasks[countOfThreads - 1] = Task.Run(() =>
            {
                muchSearchsForThreadPrivate[countOfThreads - 1].SetPlaceForSearching(
                        new Rectangle(
                            this.locationOfPlaceForSearchPrivate.X + partWidthPSA * (countOfThreads - 1),//Перемещение на последнюю не тронутую часть ширины
                            this.locationOfPlaceForSearchPrivate.Y,
                            //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                            thispictureSearchArea.Width - (partWidthPSA * (countOfThreads - 1)),//Последняя не тронутая часть ширины
                            thispictureSearchArea.Height
                            ));
                muchSearchsForThreadPrivate[countOfThreads - 1].CreateScreenShot();
                muchSearchsForThreadPrivate[countOfThreads - 1].SearchModelInArea();
            });

            //Ожидание завершения выполнения всех потоков
            for (int i = 0; i < countOfThreads; i++)
                muchTasks[i].Wait();

            //Объединение всех найденых точек
            List<Point> listFoundPointsInFourThread = null;//Создается новый список дл простоты добавления
            for (int i = 0; i < countOfThreads ; i++)
            {
                //Если есть что и куда добавить, то можно добавлять
                if (listFoundPointsInFourThread != null && muchSearchsForThreadPrivate[i].foundPoints != null)
                {
                    //Просматривается каждый элемент на возможность добавления
                    foreach (Point newPointForList in muchSearchsForThreadPrivate[i].foundPoints)
                    {
                        //Если такого элемента еще нет, то происходит добавление
                        if (!CheckPointInList(listFoundPointsInFourThread, newPointForList))
                        {
                            //Сортировка при добавлении не нужна, т.к. поиск происходит по строкам каждого столбца. Добавление частей происходит в таком же порядке.
                            //Т.е. сперва левые, потом правые
                            listFoundPointsInFourThread.Add(newPointForList);
                        }
                    }
                }
                else//Если массив точек все еще пуст, то ему надо присвоить любой найденый массив точек
                {
                    if (muchSearchsForThreadPrivate[i].foundPoints != null)//Присваивать пустой, конечно. незачем
                    {
                        listFoundPointsInFourThread = new List<Point>();
                        listFoundPointsInFourThread = muchSearchsForThreadPrivate[i].foundPoints.ToList();
                    }
                }
            }

            //Здесь параллельный расчет считается законченым
            this.isEnableFourThreadsPrivate = false;

            //Если точки так и не нашлись, надо вернуть ложь, иначе точки есть и возращается истина
            if (listFoundPointsInFourThread == null)
            {
                return false;
            }

            //Записать список точек в массив, отсортировать и вернуть истину
            this.foundPoints = listFoundPointsInFourThread.ToArray();
            SortAfterEndSearching();
            return true;
        }
        /// <summary>
        /// Метод для всех данных и клонов, потому что если сразу запускать потоки в цикле,
        /// то выскакивает исключение об обращении к одной и той же памяти из разных потоков несомтря на то,
        /// что в каждом потоке разные экземляры класса.
        /// Принимает ссылку на задачу, клон экземпляра класса поиска и дополнительные параметра для расчета области поиска.
        /// </summary>
        /// <param name="taskNum"></param>
        /// <param name="searchExemplarClone"></param>
        /// <param name="numOfThread"></param>
        /// <param name="partWidthPSA"></param>
        /// <param name="thispictureSearchAreaHeight"></param>
        /// <param name="stopSearchingAfterFirstPointFound"></param>
        /// <returns></returns>
        Search TaskFunctionThread(ref Task taskNum, Search searchExemplarClone, int numOfThread, int partWidthPSA, int thispictureSearchAreaHeight)
        {
            taskNum = Task.Run(() =>
            {
                searchExemplarClone.SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X + partWidthPSA * numOfThread,//Перемещение начала с номером потока
                    this.locationOfPlaceForSearchPrivate.Y,
                    //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    partWidthPSA + this.correctModelPrivate.Width,
                    thispictureSearchAreaHeight
                    ));
                searchExemplarClone.CreateScreenShot();
                searchExemplarClone.SearchModelInArea();
            });
            return searchExemplarClone;
        }
        /// <summary>
        /// Проверяет в списке наличие точки. Возвращает true, если точка там есть.
        /// </summary>
        /// <param name="massiveOfPoints"></param>
        /// <param name="pointForCheck"></param>
        /// <returns></returns>
        private bool CheckPointInList(List<Point> massiveOfPoints, Point pointForCheck)
        {
            foreach(Point pointInMassive in massiveOfPoints)
                if(pointForCheck.X == pointInMassive.X && pointForCheck.Y == pointInMassive.Y)
                    return true;

            return false;
        }

        ///Выполнение поиска с использованием потоков КОНЕЦ




        ///Вспомогательные методы:
        
        /// <summary>
        ///Метод для сортировки найденных точек, чтобы они перечислялись всегда сторого в определенном порядке после любого способа поиска.
        ///Точки сортируются по составляющей X, если она равна, то по состовляющей Y.
        /// </summary>
        private void SortAfterEndSearching()
        {
            if (this.foundPoints != null)
            {
                for (int numberPoint = 1; numberPoint < this.foundPoints.Length; numberPoint++)
                {
                    int numberForSwap = numberPoint;
                    while (numberForSwap > 0)
                    {
                        if (this.foundPoints[numberForSwap].X < this.foundPoints[numberForSwap - 1].X)
                        {
                            Point swapPoint = this.foundPoints[numberForSwap];
                            this.foundPoints[numberForSwap] = this.foundPoints[numberForSwap - 1];
                            this.foundPoints[numberForSwap - 1] = swapPoint;
                            numberForSwap--;
                        }
                        else
                            break;
                    }
                    while (numberForSwap > 0)
                    {
                        if ((this.foundPoints[numberForSwap].X == this.foundPoints[numberForSwap - 1].X) &&
                            (this.foundPoints[numberForSwap].Y < this.foundPoints[numberForSwap - 1].Y))
                        {
                            Point swapPoint = this.foundPoints[numberForSwap];
                            this.foundPoints[numberForSwap] = this.foundPoints[numberForSwap - 1];
                            this.foundPoints[numberForSwap - 1] = swapPoint;
                            numberForSwap--;
                        }
                        else
                            break;
                    }
                }
            }
        }
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int leftUpX, int leftUpY, int rightBottomX, int rightBottomY, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        /// <summary>
        /// Сорздание скришота и запоминание его в pictureSearchArea. Ширина и высота скришота соответствует ширине и высоте параметра pictureSearchArea.
        /// Точка отсчета соотвествует точке отсчета запомненой экземляром. По умочанию равна 0,0. Узнать значение можно через свойство locationOfPlaceForSearch.
        /// </summary>
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
                        this.pictureSearchArea.Width + this.locationOfPlaceForSearchPrivate.X,
                        this.pictureSearchArea.Height + this.locationOfPlaceForSearchPrivate.Y,
                        hSrcDC, 0, 0, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
        }
        
        /// <summary>
        /// Сравнивает два цвета только по свойствам R, G и B.
        /// Если свойство афльфа-канала одно из цветов равено 0, то цвета считаются одинаковыми.
        /// </summary>
        /// <param name="firstColor"></param>
        /// <param name="secondColor"></param>
        /// <returns></returns>
        public static bool CheckForEqualityOfTwoColorsByRGB(Color firstColor, Color secondColor)
        {
            if (firstColor.A == 0 || secondColor.A == 0)
                return true;
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
        /// <summary>
        /// Создает клон нынешнего экземпляра.
        /// </summary>
        /// <returns></returns>
        public Search Clone()
        {
            Search cloneThisSearch = new Search();
            cloneThisSearch.foundPoints = this.foundPoints;
            cloneThisSearch.isEnableFourThreadsPrivate = this.isEnableFourThreadsPrivate;
            cloneThisSearch.isIgnorColorsPrivate = this.isIgnorColorsPrivate;
            cloneThisSearch.UseIgnorColors = this.UseIgnorColors;

            if (this.listOfIgnorColors != null)
            {
                Color[] colorListImassiveInList = this.listOfIgnorColors.ToArray();
                cloneThisSearch.listOfIgnorColors = colorListImassiveInList.ToList();
            }
            else
                cloneThisSearch.listOfIgnorColors = null;

            cloneThisSearch.UseActiveWindow = this.UseActiveWindow;
            cloneThisSearch.UsePlaceForSearch = this.UsePlaceForSearch;
            cloneThisSearch.locationOfPlaceForSearchPrivate = this.locationOfPlaceForSearchPrivate;
            cloneThisSearch.numberIgnorColorInListPrivate = this.numberIgnorColorInListPrivate;
            cloneThisSearch.pictureModelForSearchPrivate = (Bitmap)this.pictureModelForSearchPrivate.Clone();
            cloneThisSearch.correctModelPrivate = (Bitmap)this.correctModelPrivate.Clone();
            cloneThisSearch.aimModelPrivate = this.aimModelPrivate;

            if (this.pictureSearchArea != null)
                cloneThisSearch.pictureSearchArea = (Bitmap)this.pictureSearchArea.Clone();
            else
                cloneThisSearch.pictureSearchArea = null;

            cloneThisSearch.isCreateScreenWindowPrivate = this.isCreateScreenWindowPrivate;
            cloneThisSearch.pointerOnActiveWindow = this.pointerOnActiveWindow;
            cloneThisSearch.percentageComplianceWithModelPivate = this.percentageComplianceWithModelPivate;
            cloneThisSearch.stopSearchingAfterFirstPointFound = this.stopSearchingAfterFirstPointFound;

            return cloneThisSearch;
        }
        /// <summary>
        /// Делает null (самую большую по логике) картинку, где хзраниться скришот экрана или его области.
        /// </summary>
        public void RemoveScreenshot()
        {
            this.pictureSearchArea = null;
        }
        ///Помогает узнать масштабирование системы
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
        /// <summary>
        /// Вырезает маленькую картинку заданного размера из большой картинки начиная с указаной точки.
        /// </summary>
        /// <param name="locationStartOfLargePicture"></param>
        /// <param name="largePicture"></param>
        /// <param name="widthSmallPicture"></param>
        /// <param name="heightSmallPicture"></param>
        /// <returns></returns>
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

//Мысли:
//Можно сделать фукнцию создания из нескольких похожих эталонов один, в котором будут устранены места, которые меняются, из-за которых при поиске 100% соответствия он будет неудачным.