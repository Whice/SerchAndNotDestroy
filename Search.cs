﻿using System;
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
    /// Это класс поиска.
    /// Он позволяет искать заданный эталон на экране, а так же предоставляет инструменты для ускорения поиска этого эталона.
    /// </summary>
    class Search
    {
        /// <summary>
        /// Bitmap для хранения области, в которой осуществляется поиск эталона. По умолчанию null.
        /// </summary>
        public Bitmap pictureSearchArea;
        /// <summary>
        /// Bitmap для хранения эталона, поиск местонахождения которого выполняется.
        /// По умолчанию храниться картинка размером 50х50: две красные диагонали на белом фоне.
        /// </summary>
        public Bitmap pictureModelForSearch;
        /// <summary>
        /// Массив точек, которые были найдены во время поиска. По умолчанию null.
        /// </summary>
        public Point[] foundPoints;
        /// <summary>
        /// Инициализирует новый экземпляр класса поиска.
        /// </summary>
        public Search()
        {
            pictureSearchArea = null;
            pictureModelForSearch = null;
            foundPoints = null;

            locationOfPlaceForSearchPrivate = new Point();
            isCreateScreenWindowPrivate = false;
            pointerOnActiveWindow = default(IntPtr);

            listOfIgnorColors = null;
            numberIgnorColorInListPrivate = 0;
            isIgnorColorsPrivate = false;

            isEnableFourThreadsPrivate = false;

            CreateBitmapForEmptyModel();
        }




        ///цвета, которые надо игнорировать НАЧАЛО

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
                    if(CheckColorPixelInPoint(ignorColors, colorForTest))
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
        /// Проверяет наличие цвета в Bitmap. Не учитывает альфа-канал. Сравнивает только свойства R, G и B.
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
                    if (CheckColorPixelInPoint(colorForCheck, bitmapForCheck.GetPixel(i, j)))
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
                    if (CheckColorIsInBitmap(ignorColor, this.pictureModelForSearch))
                    {
                        newIgnorColorList.Add(ignorColor);
                    }
                }
            }
            this.listOfIgnorColors = newIgnorColorList;
        }

        ///цвета, которые надо игнорировать КОНЕЦ




        ///Область, в которой выполняется поиск НАЧАЛО

        /// <summary>
        /// Внутреннее поле для хранения расположения прямоугольника области, где выполняется поиск.
        /// </summary>
        private Point locationOfPlaceForSearchPrivate;
        /// <summary>
        /// Возвращает значение поля для хранения расположения прямоугольника области, где выполняется поиск.
        /// </summary>
        public Point GetLocationOfPlaceForSearch()
        {
                if (this.isCreateScreenWindowPrivate)
                    return this.locationOfPlaceForSearchPrivate;
                return new Point(0, 0);
        }
        /// <summary>
        /// Задает значение расположения и размеров прямоугольника области, где выполняется поиск.
        /// Принимает прямоугольник содержащий расположение и размеры.
        /// </summary>
        public void SetPlaceForSearching(Rectangle placeForSearching)
        {
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

        ///Область, в которой выполняется поиск КОНЕЦ




        ///Скриншот активного окна НАЧАЛО

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
        /// Устанавливает активное окна в качестве прямоугольника области для поиска.
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
        /// Если принимает true, то ищет только до первой попавшейся точки.
        /// </summary>
        /// <param name="stopSearchingAfterFirstPointFound"></param>
        /// <returns></returns>
        public bool SearchModelInArea(bool stopSearchingAfterFirstPointFound)
        {
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
            //пиксель в эталоне, дабы не вылезать за верхнюю и левую границу области поиска.
            //Впрочем, этот пискель может быть и началом.
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
                                if (ComparisonOfmodelAndAreaForSearch(new Point(i - pixelOfModelForSearch.X, j - pixelOfModelForSearch.Y)))
                                {
                                    if (listFoundPoints == null)
                                        listFoundPoints = new List<Point>();

                                    listFoundPoints.Add(new Point(
                                        i - pixelOfModelForSearch.X + this.locationOfPlaceForSearchPrivate.X,
                                        j - pixelOfModelForSearch.Y + this.locationOfPlaceForSearchPrivate.Y));

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
        }

        ///Выполнение поиска КОНЕЦ



        ///Выполнение поиска с использованием четырех потоков НАЧАЛО
        private bool isEnableFourThreadsPrivate;
        public bool isEnableFourThreads { get { return this.isEnableFourThreadsPrivate; } }
        
        private delegate void iterSearchModelInAreaDelegate();
        /// <summary>
        /// Выполняет обычный поиск, но делит область поиска на четыре части, поиск в каждой части выполняется в своем отдельном потоке.
        /// Вполне может работать медленней, чем последовательный поиск в случае поиска первой попавшейся точки(true).
        /// Выполняет поиск эталона с учетом указанных параметров поиска и записывает в поле foundPoints нынешнего экземпляра. 
        /// Если принимает true, то ищет только до первой попавшейся точки.
        /// </summary>
        public bool SearchModelInAreaInFourThreads(bool stopSearchingAfterFirstPointFound)
        {
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
            iterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject1 = (() => fourSearchsForThreadPrivate[0].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X, this.locationOfPlaceForSearchPrivate.Y,
                    //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    halfWidthPSA + this.pictureModelForSearch.Width, halfHeightPSA + this.pictureModelForSearch.Height
                    )));
            iterSearchModelInAreaDelegateObject1 += (() => fourSearchsForThreadPrivate[0].CreateScreenShot());
            iterSearchModelInAreaDelegateObject1 += (() => fourSearchsForThreadPrivate[0].SearchModelInArea(stopSearchingAfterFirstPointFound));


            fourThread[0] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject1));
            fourThread[0].Start();



            //Во второй поток отправляется вторая четверть
            fourSearchsForThreadPrivate[1] = this.Clone();

            iterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject2 = (() => fourSearchsForThreadPrivate[1].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X,
                    this.locationOfPlaceForSearchPrivate.Y + halfHeightPSA, //Вертикальная точка начала для этой четверти сдвигается
                    halfWidthPSA + this.pictureModelForSearch.Width,//Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    this.pictureSearchArea.Height - halfHeightPSA//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    )));
            iterSearchModelInAreaDelegateObject2 += (() => fourSearchsForThreadPrivate[1].CreateScreenShot());
            iterSearchModelInAreaDelegateObject2 += (() => fourSearchsForThreadPrivate[1].SearchModelInArea(stopSearchingAfterFirstPointFound));

            fourThread[1] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject2));
            fourThread[1].Start();



            //В третий поток отправляется третья четверть 
            fourSearchsForThreadPrivate[2] = this.Clone();

            iterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject3 = (() => fourSearchsForThreadPrivate[2].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X + halfWidthPSA,//Горизонтальная точка начала для этой четверти сдвигается
                    this.locationOfPlaceForSearchPrivate.Y,
                    this.pictureSearchArea.Width - halfWidthPSA,//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    halfHeightPSA + this.pictureModelForSearch.Height//Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    )));
            iterSearchModelInAreaDelegateObject3 += (() => fourSearchsForThreadPrivate[2].CreateScreenShot());
            iterSearchModelInAreaDelegateObject3 += (() => fourSearchsForThreadPrivate[2].SearchModelInArea(stopSearchingAfterFirstPointFound));

            fourThread[2] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject3));
            fourThread[2].Start();




            //В четвертый поток отправляется четвертая четверть
            fourSearchsForThreadPrivate[3] = this.Clone();

            iterSearchModelInAreaDelegate iterSearchModelInAreaDelegateObject4 = (() => fourSearchsForThreadPrivate[3].SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X + halfWidthPSA,//Горизонтальная точка начала для этой четверти сдвигается
                    this.locationOfPlaceForSearchPrivate.Y + halfHeightPSA, //Вертикальная точка начала для этой четверти сдвигается
                    this.pictureSearchArea.Width - halfWidthPSA,//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    this.pictureSearchArea.Height - halfHeightPSA//Лучше отнять предыдущую половину, т.к. не известно куда округлит, в большую или меньшую.
                    )));
            iterSearchModelInAreaDelegateObject4 += (() => fourSearchsForThreadPrivate[3].CreateScreenShot());
            iterSearchModelInAreaDelegateObject4 += (() => fourSearchsForThreadPrivate[3].SearchModelInArea(stopSearchingAfterFirstPointFound));

            fourThread[3] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject4));
            fourThread[3].Start();


            this.isEnableFourThreadsPrivate = true;

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
                        if (!CheckPointInMassive(listFoundPointsInFourThread, newPointForList))
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
            

            //Освободить память надо
            fourSearchsForThreadPrivate = null;
            fourThread = null;

            //Если точки так и не нашлись, надо вернуть ложь, иначе точки есть и возращается истина
            if (listFoundPointsInFourThread == null)
            {
                return false;
            }

            this.foundPoints = listFoundPointsInFourThread.ToArray();
            SortAfterEndSearching();
            return true;
        }
        public bool SearchModelInAreaInMultyThreads(bool stopSearchingAfterFirstPointFound, int countOfThreads)
        {
            //Если игнорируемых цветов нет в эталоне, то их надо исключить из спика игнорируемых, 
            //т.к. они в любом случае будут проигнорированы из-за отсутсвия в эталоне.
            //Если этого не сделать, то они будут проверяться в пустую, что может замедлить 
            //выполнение: в лучшем случае скорость останется такой же.
            RemoveIgnorColorsThatAreNotInModel();
            //Перед поиском новых точек старые надо забыть
            this.foundPoints = null;
            Size thispictureSearchArea = new Size(this.pictureSearchArea.Width, this.pictureSearchArea.Height);
            this.pictureSearchArea = null;

            //Для краткости чтения половина ширины области поиска вычисляется сразу
            int partWidthPSA = (int)(thispictureSearchArea.Width / countOfThreads);

            //Инициализация потоков и будущих клонов этого экземпляра для этих потоков
            Search[] muchSearchsForThreadPrivate = new Search[countOfThreads];
            Thread[] muchThreads = new Thread[countOfThreads];
            System.Threading.Tasks.Task[] muchTasks = new Task[countOfThreads];
            iterSearchModelInAreaDelegate[] iterSearchModelInAreaDelegateObject = new iterSearchModelInAreaDelegate[countOfThreads];

            
            for (int i = 0; i < countOfThreads; i++)
            {
                muchSearchsForThreadPrivate[i] = this.Clone();
            }


            //Попробую с таксками
            for (int numOfThread = 0; numOfThread < countOfThreads - 1; numOfThread++)//В последней части надо не брать нахлест, т.к. это самый конец
            {
                muchSearchsForThreadPrivate[numOfThread] = TaskFunctionThrad(ref muchTasks[numOfThread], muchSearchsForThreadPrivate[numOfThread].Clone(),
                    numOfThread, partWidthPSA, thispictureSearchArea.Height, stopSearchingAfterFirstPointFound);
            }
               /* for (int numOfThread = 0; numOfThread < countOfThreads - 1; numOfThread++)//В последней части надо не брать нахлест, т.к. это самый конец
            { //Задать для этого потока координаты и размер новой области, соответсвующие его части
                fourSearchsForThreadPrivate[numOfThread].SetPlaceForSearching(
                    new Rectangle(
                        this.locationOfPlaceForSearchPrivate.X + partWidthPSA * numOfThread,//Перемещение начала с номером потока
                        this.locationOfPlaceForSearchPrivate.Y,
                        //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                        partWidthPSA + this.pictureModelForSearch.Width,
                        thispictureSearchArea.Height
                        ));
                //fourSearchsForThreadPrivate[numOfThread].CreateScreenShot();
            }
            for (int numOfThread = 0; numOfThread < countOfThreads - 1; numOfThread++)//В последней части надо не брать нахлест, т.к. это самый конец
            {
                iterSearchModelInAreaDelegateObject[numOfThread] = (() => fourSearchsForThreadPrivate[numOfThread].CreateScreenShot());
                iterSearchModelInAreaDelegateObject[numOfThread] +=  (() => fourSearchsForThreadPrivate[numOfThread].SearchModelInArea(stopSearchingAfterFirstPointFound));

                fourThread[numOfThread] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject[numOfThread]));
                fourThread[numOfThread].Start();
            }*/



            //Последняя часть, без нахлеста
            iterSearchModelInAreaDelegateObject[countOfThreads - 1] = (() => muchSearchsForThreadPrivate[countOfThreads - 1].SetPlaceForSearching(
                    new Rectangle(
                        this.locationOfPlaceForSearchPrivate.X + partWidthPSA * (countOfThreads-1),//Перемещение на последнюю не тронутую часть ширины
                        this.locationOfPlaceForSearchPrivate.Y,
                        //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                        thispictureSearchArea.Width - (partWidthPSA * (countOfThreads - 1)),//Последняя не тронутая часть ширины
                        thispictureSearchArea.Height
                        )));
            iterSearchModelInAreaDelegateObject[countOfThreads - 1] += (() => muchSearchsForThreadPrivate[countOfThreads - 1].CreateScreenShot());
            iterSearchModelInAreaDelegateObject[countOfThreads - 1] += (() => muchSearchsForThreadPrivate[countOfThreads - 1].SearchModelInArea(stopSearchingAfterFirstPointFound));

            muchThreads[countOfThreads - 1] = new Thread(new ThreadStart(iterSearchModelInAreaDelegateObject[countOfThreads - 1]));
            muchThreads[countOfThreads - 1].Start();
            muchThreads[countOfThreads-1].Join();


            this.isEnableFourThreadsPrivate = true;

            for (int i = 0; i < countOfThreads-1; i++)
                muchTasks[i].Wait();//fourThread[i].Join();

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
                        if (!CheckPointInMassive(listFoundPointsInFourThread, newPointForList))
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

            //Освободить память надо
            muchSearchsForThreadPrivate = null;
            muchThreads = null;
            //iterSearchModelInAreaDelegateObject = null;

            //Если точки так и не нашлись, надо вернуть ложь, иначе точки есть и возращается истина
            if (listFoundPointsInFourThread == null)
            {
                return false;
            }

            this.foundPoints = listFoundPointsInFourThread.ToArray();
            SortAfterEndSearching();
            return true;
        }
        Search TaskFunctionThrad(ref Task taskNum, Search srPerClone, int numOfThread, int partWidthPSA, int thispictureSearchAreaHeight,  bool stopSearchingAfterFirstPointFound)
        {
            taskNum = Task.Run(() =>
            {
                srPerClone.SetPlaceForSearching(
                new Rectangle(
                    this.locationOfPlaceForSearchPrivate.X + partWidthPSA * numOfThread,//Перемещение начала с номером потока
                    this.locationOfPlaceForSearchPrivate.Y,
                    //Выполнить поиск с небольшим нахлестом, чтобы точно найти все точки.
                    partWidthPSA + this.pictureModelForSearch.Width,
                    thispictureSearchAreaHeight
                    ));
                srPerClone.CreateScreenShot();
                srPerClone.SearchModelInArea(stopSearchingAfterFirstPointFound);
            });
            return srPerClone;
        }

        private bool CheckPointInMassive(List<Point> massiveOfPoints, Point pointForCheck)
        {
            foreach(Point pointInMassive in massiveOfPoints)
                if(pointForCheck.X == pointInMassive.X && pointForCheck.Y == pointInMassive.Y)
                    return true;

            return false;
        }
        ///Выполнение поиска с использованием четырех потоков КОНЕЦ




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
        /// Заполняет эталон небольшой картинкой, которая означает, что он пуст.
        /// Две красные дигонали на белом фоне. Размер картинки 50х50.
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
        /// <summary>
        /// сравнивает два цвета только по свойствам R, G и B
        /// </summary>
        /// <param name="firstColor"></param>
        /// <param name="secondColor"></param>
        /// <returns></returns>
        public static bool CheckColorPixelInPoint(Color firstColor, Color secondColor)
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
        public Search Clone()
        {
            Search cloneThisSearch = new Search();
            cloneThisSearch.foundPoints = this.foundPoints;
            cloneThisSearch.isEnableFourThreadsPrivate = this.isEnableFourThreadsPrivate;
            cloneThisSearch.isIgnorColorsPrivate = this.isIgnorColorsPrivate;

            if (this.listOfIgnorColors != null)
            {
                Color[] colorListImassiveInList = this.listOfIgnorColors.ToArray();
                cloneThisSearch.listOfIgnorColors = colorListImassiveInList.ToList();
            }
            else
                cloneThisSearch.listOfIgnorColors = null;

            cloneThisSearch.locationOfPlaceForSearchPrivate = this.locationOfPlaceForSearchPrivate;
            cloneThisSearch.numberIgnorColorInListPrivate = this.numberIgnorColorInListPrivate;
            cloneThisSearch.pictureModelForSearch = (Bitmap)this.pictureModelForSearch.Clone();

            if (this.pictureSearchArea != null)
                cloneThisSearch.pictureSearchArea = (Bitmap)this.pictureSearchArea.Clone();
            else
                cloneThisSearch.pictureSearchArea = null;

            cloneThisSearch.isCreateScreenWindowPrivate = this.isCreateScreenWindowPrivate;
            cloneThisSearch.pointerOnActiveWindow = this.pointerOnActiveWindow;

            return cloneThisSearch;
        }
        public void ScreenshotFullMonitor()
        {
            //Получаю размер экрана в пикселях.
            Size resolutionOfFullScreen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            //Оригинальное разрешение почему-то всегда масштабируется и становиться 0.8 от оригинала.
            //Это потому что в винде у меня стоит мастаирование всего на 125%. Если это убрать и оставить 100%, все будет ок.
            //Потому возвращаю его назад
            this.locationOfPlaceForSearchPrivate = new Point(0, 0);
            this.pictureSearchArea = new Bitmap(
                (int)(resolutionOfFullScreen.Width * getScalingFactor()),
                (int)(resolutionOfFullScreen.Height * getScalingFactor()));
            CreateScreenShot();
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
