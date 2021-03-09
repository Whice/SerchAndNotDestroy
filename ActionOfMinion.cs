using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace MyLittleMinion
{
    class ActionOfMinion
    {
        /// <summary>
        /// Задает и получает свойство местонахождения курсора. Используется для указания точки, к которой должны быть применены действия.
        /// </summary>
        public Point cursorPosition { get; set; }
        /// <summary>
        /// Хранит номер(id) действия, которое надо выполнить.
        /// 0 - Click LBM, 1 - double click LBM.
        /// </summary>
        public ushort numberOfAction { get; set; }
        public int timeOfWaitingAfterAction { get; set; }
        public ActionOfMinion()
        {
            this.cursorPosition = new Point(0, 0);
            this.numberOfAction = 0;
            this.timeOfWaitingAfterAction = 1;
        }

        /// <summary>
        /// Создает и возвращает копию этого экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public ActionOfMinion Clone()
        {
            ActionOfMinion cloneOfActionOfMinion = new ActionOfMinion();

            cloneOfActionOfMinion.cursorPosition = this.cursorPosition ;
            cloneOfActionOfMinion.numberOfAction = this.numberOfAction;
            cloneOfActionOfMinion.timeOfWaitingAfterAction = this.timeOfWaitingAfterAction;

            return cloneOfActionOfMinion;
        }

        /// <summary>
        /// Выполняет действие заданного ноемра.
        /// </summary>
        public void RealizeAction()
        {
            if (this.numberOfAction == 0)
                MouseClickLeftButton();
            else if (this.numberOfAction == 1)
                MouseDoubleClickLeftButton();
            else if (this.numberOfAction == 2)
                PasteFromBuffer();
        }
        private void MouseClickLeftButton()
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
        private void MouseDoubleClickLeftButton()
        {
            MouseClickLeftButton();
            Thread.Sleep(10);
            MouseClickLeftButton();
        }
        private void PasteFromBuffer()
        {
            SendKeys.Send("^v");
        }
        private void MouseDownLeftButton()
        {
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN,
            cursorPosition.X,
            cursorPosition.Y,
            0,
            0);
        }
        private void MouseUpLeftButton()
        {
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP,
                        cursorPosition.X,
                        cursorPosition.Y,
                        0,
                        0);
        }
        private static void DragAndDropWithMouse(Point pointWhereToGet, Point pointWhereToDrop)
        {
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN,
           pointWhereToGet.X,
           pointWhereToGet.Y,
           0,
           0);
            Thread.Sleep(10);
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP,
                        pointWhereToDrop.X,
                        pointWhereToDrop.Y,
                        0,
                        0);
        }

        //Для кликов мышью
        private enum MouseEvent
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10,
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(MouseEvent dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //Зачем? Разве он используется?
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

    }
}
