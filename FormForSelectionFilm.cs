using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

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

        private void SelectionFilm_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPointPrivate = Cursor.Position;
            //pbSelectionFilm.Location = this.PointToClient(Cursor.Position);
            pbSelectionFilm.Visible = true;
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Rectangle selectionPlace = new Rectangle();
            //Определяются размеры области выделения, относительно точки нажатия
            selectionPlace.Width = Cursor.Position.X - this.startPointPrivate.X;
            selectionPlace.Height = Cursor.Position.Y - this.startPointPrivate.Y;

            //Если курсор бежит влево от точки нажатия, то ширина становиться меньше нуля, и ничего не рисуется
            //Поэтому надо сделать ширину положительной, а левый верхний угол двигать вслед за курсором
            if(selectionPlace.Width<0)
            {
                selectionPlace.Width *= -1;
                pbSelectionFilm.Location = new Point(this.PointToClient(Cursor.Position).X,pbSelectionFilm.Location.Y);
            }
            else
            {
                pbSelectionFilm.Location = new Point(this.startPointPrivate.X, pbSelectionFilm.Location.Y);
            }
            //По вертикали все аналогично горизонтали по действиям
            if (selectionPlace.Height < 0)
            {
                selectionPlace.Height *= -1;
                pbSelectionFilm.Location = new Point(pbSelectionFilm.Location.X, this.PointToClient(Cursor.Position).Y);
            }
            else
            {
                pbSelectionFilm.Location = new Point(pbSelectionFilm.Location.X, this.startPointPrivate.Y);
            }

            //Теперь можно задать размер области выделения
            pbSelectionFilm.Size = new Size(selectionPlace.Width, selectionPlace.Height);
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

             pbSelectionFilm.Visible = false;
            this.Hide();
        }
    }
}
