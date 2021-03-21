using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            this.BackgroundImage = (Image)picture;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int leftUpX, int leftUpY, int rightBottomX, int rightBottomY, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        public void FilmActiveted(Rectangle ptrRectangeForSelectionPlace)
        {
            this.rectangeSelectionPlace = ptrRectangeForSelectionPlace;
            pbSelectionFilm.BackColor = Color.FromArgb(50, 255, 255, 255);
            this.Show();

        }

        public Point startPoint
        {
            get { return this.startPointPrivate; }
        }
        public Point finishPoint
        {
            get { return this.finishPointPrivate; }
        }

        Rectangle selectionPlace = new Rectangle();
        private void SelectionFilm_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPointPrivate = Cursor.Position;
            timer1.Enabled = true;
        }
        private void PbSelectionFilm_MouseDown(object sender, MouseEventArgs e)
        {
            this.Opacity = 0;
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


            Graphics battlefield = pbSelectionFilm.CreateGraphics();
            SolidBrush d = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
            SolidBrush fone = new SolidBrush(pbSelectionFilm.BackColor);
            battlefield.FillRectangle(fone, pbSelectionFilm.Location.X, pbSelectionFilm.Location.Y, pbSelectionFilm.Width, pbSelectionFilm.Height);
            battlefield.FillRectangle(d, selectionPlace);
        }

        public delegate void GetPointsOfRectangle(int xBeg, int yBeg, int xEnd, int yEnd);
        public event GetPointsOfRectangle SendTwoPointsOfRectangle;
        public delegate void GetRectangle(Rectangle rect);
        public event GetRectangle SendRectangle;
        public delegate void UpdateSender();
        /// <summary>
        /// Принимает на вход функцию обновления без аргументов и просто ее запускает.
        /// Больше ничего не делает.
        /// </summary>
        public event UpdateSender IndicateUpdateFromSender;

        

        private void PbSelectionFilm_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            this.finishPointPrivate = Cursor.Position;

            //Надо сделать так, чтобы левая координата области была с верхней, а правая с нижней
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

            this.rectangeSelectionPlace.X = this.startPoint.X;
            this.rectangeSelectionPlace.Y = this.startPoint.Y;
            this.rectangeSelectionPlace.Width = this.finishPoint.X - this.startPoint.X;
            this.rectangeSelectionPlace.Height = this.finishPoint.Y - this.startPoint.Y;

            SendTwoPointsOfRectangle?.Invoke(rectangeSelectionPlace.X, rectangeSelectionPlace.Y,
                rectangeSelectionPlace.X + rectangeSelectionPlace.Width, rectangeSelectionPlace.Y + rectangeSelectionPlace.Height);
            SendRectangle?.Invoke(rectangeSelectionPlace);
            IndicateUpdateFromSender?.Invoke();

            pbSelectionFilm.Visible = false;
            this.Close();
        }

        private void FormForSelectionFilm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void SelectionFilm_MouseUp(object sender, MouseEventArgs e)
        {

            timer1.Enabled = false;
            this.finishPointPrivate = Cursor.Position;

            //Надо сделать так, чтобы левая координата области была с верхней, а правая с нижней
            if(this.finishPointPrivate.X< this.startPointPrivate.X)
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

            this.rectangeSelectionPlace.X = this.startPoint.X;
            this.rectangeSelectionPlace.Y = this.startPoint.Y;
            this.rectangeSelectionPlace.Width = this.finishPoint.X - this.startPoint.X;
            this.rectangeSelectionPlace.Height = this.finishPoint.Y - this.startPoint.Y;

            SendTwoPointsOfRectangle?.Invoke(rectangeSelectionPlace.X,rectangeSelectionPlace.Y,
                rectangeSelectionPlace.X+ rectangeSelectionPlace.Width,rectangeSelectionPlace.Y+ rectangeSelectionPlace.Height);
            SendRectangle?.Invoke(rectangeSelectionPlace);
            IndicateUpdateFromSender?.Invoke();

             pbSelectionFilm.Visible = false;
            this.Close();
        }
    }
}
