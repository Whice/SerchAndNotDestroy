using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyLittleMinion
{
    //Класс пленки для выбора области.
    //Не смог реализовать нормальную передачу данных после выбора области
    //Подумать похже
    public partial class FormForSelectionFilm : Form
    {
        private Point startPointPrivate, finishPointPrivate;
        private Rectangle rectangeSelectionPlace;
        public FormForSelectionFilm()
        {
            InitializeComponent();
            pbSelectionFilm.Location = new Point(0, 0);
            pbSelectionFilm.Size = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            Bitmap picture = new Bitmap(pbSelectionFilm.Width, pbSelectionFilm.Height);
            using (Graphics gdest = Graphics.FromImage(picture))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {

                    IntPtr hSrcDC;
                    IntPtr hDC;
                    int retval;
                    hSrcDC = gsrc.GetHdc();
                    hDC = gdest.GetHdc();
                    retval = BitBlt(hDC, 0, 0,
                        picture.Width,
                        picture.Height,
                        hSrcDC, 0, 0, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            panelBackGroundImage.Location = new Point(0, 0);
            panelBackGroundImage.Size = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            panelBackGroundImage.BackgroundImage = (Image)picture;
        }

        //Для скриншота. Он нужен, хотя не очень понятно, что он дает.
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int leftUpX, int leftUpY, int rightBottomX, int rightBottomY, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        
        /// <summary>
        /// Точка, где была опущена ЛКМ.
        /// </summary>
        public Point startPoint
        {
            get { return this.startPointPrivate; }
        }
        /// <summary>
        /// Точка, где была поднята ЛКМ.
        /// </summary>
        public Point finishPoint
        {
            get { return this.finishPointPrivate; }
        }

        //Для рисования прямоугольника на pictureBox и визуального отображения ввыделения.
        Graphics areaForSelect;
        Rectangle selectionPlace = new Rectangle();
        private void PbSelectionFilm_MouseDown(object sender, MouseEventArgs e)
        {
            //При нажатии ЛКМ запомнить, где было нажато, и запустить таймер.
            this.startPointPrivate = Cursor.Position;
            timer1.Enabled = true;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            selectionPlace.Location = this.startPointPrivate;
            //Определяются размеры области выделения, относительно точки нажатия
            selectionPlace.Width = Cursor.Position.X - this.startPointPrivate.X;
            selectionPlace.Height = Cursor.Position.Y - this.startPointPrivate.Y;

            //Если курсор бежит влево от точки нажатия, то ширина становиться меньше нуля, и ничего не рисуется
            //Поэтому надо сделать ширину положительной, а левый верхний угол двигать вслед за курсором
            if(selectionPlace.Width<0)
            {
                selectionPlace.Width *= -1;
                selectionPlace.Location = new Point(this.PointToClient(Cursor.Position).X, selectionPlace.Location.Y);
            }
            else
            {
                selectionPlace.Location = new Point(this.startPointPrivate.X, selectionPlace.Location.Y);
            }
            //По вертикали все аналогично горизонтали по действиям
            if (selectionPlace.Height < 0)
            {
                selectionPlace.Height *= -1;
                selectionPlace.Location = new Point(selectionPlace.Location.X, this.PointToClient(Cursor.Position).Y);
            }
            else
            {
                selectionPlace.Location = new Point(selectionPlace.Location.X, this.startPointPrivate.Y);
            }

            //Создается новая картинка, в которой по зкрашивается все, кроме области которая выделяется.
            //Просто рисовать саму область не вышло на разных формах, но на битмапе возможно получится.
            //Если выйдет, то можно сделать проще.
            Bitmap imageForRectangle = new Bitmap(pbSelectionFilm.Width, pbSelectionFilm.Height);
            areaForSelect = Graphics.FromImage(imageForRectangle);
            pbSelectionFilm.BackColor = Color.FromArgb(0, 0, 0, 0);
            SolidBrush notClearRetangle = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
            areaForSelect.FillRectangle(
                new SolidBrush(Color.FromArgb(0, 255, 255, 255)), pbSelectionFilm.Location.X, pbSelectionFilm.Location.Y, pbSelectionFilm.Width, pbSelectionFilm.Height);
            areaForSelect.FillRectangle(
                notClearRetangle, pbSelectionFilm.Location.X, pbSelectionFilm.Location.Y, pbSelectionFilm.Width - (pbSelectionFilm.Width - selectionPlace.X), pbSelectionFilm.Height);
            areaForSelect.FillRectangle(
                notClearRetangle, pbSelectionFilm.Location.X, pbSelectionFilm.Location.Y, pbSelectionFilm.Width, pbSelectionFilm.Height - (pbSelectionFilm.Height - selectionPlace.Y));
            areaForSelect.FillRectangle(
                notClearRetangle, pbSelectionFilm.Location.X+ (selectionPlace.X + selectionPlace.Width), pbSelectionFilm.Location.Y, pbSelectionFilm.Width, pbSelectionFilm.Height);
            areaForSelect.FillRectangle(
                notClearRetangle, pbSelectionFilm.Location.X, pbSelectionFilm.Location.Y+ (selectionPlace.Y + selectionPlace.Height), pbSelectionFilm.Width, pbSelectionFilm.Height);
            //Заполнение новой картинкой pictureBox, который показывает на экран нарисованую область.
            pbSelectionFilm.Image = imageForRectangle;
            
        }

        public delegate void GetPointsOfRectangle(int xBeg, int yBeg, int xEnd, int yEnd);
        /// <summary>
        /// Принимает функцию с аргументами координат левой верхней и нижней правой точек прямоугольника, область которого была найдена.
        /// </summary>
        public event GetPointsOfRectangle SendTwoPointsOfRectangle;
        
        public delegate void GetRectangle(Rectangle rect);
        /// <summary>
        /// Принимает функцию с аргументом прямоугольника, область которого была найдена.
        /// </summary>
        public event GetRectangle SendRectangle;

        public delegate void UpdateSender();
        /// <summary>
        /// Принимает на вход функцию обновления без аргументов и просто ее запускает.
        /// Больше ничего не делает.
        /// </summary>
        public event UpdateSender IndicateUpdateFromSender;

        

        private void PbSelectionFilm_MouseUp(object sender, MouseEventArgs e)
        {
            //Кнопка поднята, таймер больше ненужен, координата конца должна быть запомнена.
            timer1.Enabled = false;
            this.finishPointPrivate = Cursor.Position;

            //Надо сделать так, чтобы левая координата области была с верхней, а правая с нижней.
            if (this.finishPointPrivate.X < this.startPointPrivate.X)
            {
                int swap = this.finishPointPrivate.X;
                this.finishPointPrivate.X = this.startPointPrivate.X;
                this.startPointPrivate.X = swap;
            }
            if (this.finishPointPrivate.Y < this.startPointPrivate.Y)
            {
                int swap = this.finishPointPrivate.Y;
                this.finishPointPrivate.Y = this.startPointPrivate.Y;
                this.startPointPrivate.Y = swap;
            }

            //Подсчет игового прямоугольника.
            this.rectangeSelectionPlace.X = this.startPoint.X;
            this.rectangeSelectionPlace.Y = this.startPoint.Y;
            this.rectangeSelectionPlace.Width = this.finishPoint.X - this.startPoint.X;
            this.rectangeSelectionPlace.Height = this.finishPoint.Y - this.startPoint.Y;

            //Скрывается перед выполнением вызовов событий, т.к. они могут делать скриншот.
            //Если скрыть, точно не будет никаких искажений цветов.
            this.Hide();

            //Потому что не может быть ни ширина, ни высота нолем.
            if (rectangeSelectionPlace.Height!=0 && rectangeSelectionPlace.Width!=0)
            {
                SendTwoPointsOfRectangle?.Invoke(rectangeSelectionPlace.X, rectangeSelectionPlace.Y,
                    rectangeSelectionPlace.X + rectangeSelectionPlace.Width, rectangeSelectionPlace.Y + rectangeSelectionPlace.Height);
                SendRectangle?.Invoke(rectangeSelectionPlace);
            }
            //Для всяких обновлений, если требуется.
            IndicateUpdateFromSender?.Invoke();

            pbSelectionFilm.Visible = false;
            this.Close();
        }

        private void FormForSelectionFilm_KeyDown(object sender, KeyEventArgs e)
        {
            //Escape для принудительного закрытия.
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

    }
}
