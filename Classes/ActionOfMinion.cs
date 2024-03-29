﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyLittleMinion
{
    
    [Serializable]
    /// <summary>
    /// Это класс действия.
    /// Он позволяет выполнять действия мышкой, при заданных для координатах, или действия с клавиатурой.
    /// Этот класс можно сериализовать.
    /// </summary>
    class ActionOfMinion
    {
        #region//Поля и свойства класса действия

        /// <summary>
        /// Списки действий. 0 - Мышь, 1 - Клавиатура, 2 - Дополнительно.
        /// </summary>
        static public List<string>[] listNameOfMauseAction = new List<string>[3];
        /// <summary>
        /// Задает и получает свойство местонахождения курсора. Используется для указания точки, к которой должны быть применены действия.
        /// </summary>
        public Point cursorPosition { get; set; }
        private byte typeOfActionPrivate;
        /// <summary>
        /// Вид дествия. Клавиатура, мышь и т.п.
        /// </summary>
        public byte typeOfAction
        {
            get { return this.typeOfActionPrivate; }
            set
            {
                if (value < 0 || value > listNameOfMauseAction.Length - 1)
                    this.typeOfActionPrivate = 0;
                else
                    this.typeOfActionPrivate = value;
            }
        }
        private ushort numberOfActionPrivate;
        /// <summary>
        /// Хранит номер(id) действия, которое надо выполнить. У каждого типа свой список.
        /// </summary>
        public ushort numberOfAction
        {
            get { return this.numberOfActionPrivate; }
            set
            {
                if (value < 0 || value > listNameOfMauseAction[typeOfActionPrivate].Count - 1)
                    this.numberOfActionPrivate = 0;
                else
                    this.numberOfActionPrivate = value;
            }
        }
        /// <summary>
        /// Хранит время ожидания после совершенного действия.
        /// </summary>
        public int timeOfWaitingAfterActionInSecond { get; set; }
        public string textForAction { get; set; }

        #endregion

        /// <summary>
        /// Создает действие по умолчанию.
        /// Принимает на вход указатель на экземпляр списка действий, который буде считатьтся родительским контейнером.
        /// </summary>
        /// <param name="pointerOnInstanceParent">Указатель на экземпляр списка действий, который будет считатьтся родительским контейнером для этого экземпляра.</param>
        public ActionOfMinion(ListActionOfMinion pointerOnInstanceParent)
        {
            this.cursorPosition = new Point(0, 0);

            //Из-за того, что numberOfAction работает с созданным списком, его надо объявить сразу.
            for (int i = 0; i < listNameOfMauseAction.Length; i++)
                listNameOfMauseAction[i] = new List<string>();
            this.typeOfAction = 2;
            this.numberOfAction = 0;
            FillMassiveOfListsOfNames();

            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            this.NumberOfTimesGoTo = 0;
            this.GoToNumberOfSequence = this.pointerOnInstanceParent.pointerOnInstanceParent.numberSearchAndActionInSequence;

            this.timeOfWaitingAfterActionInSecond = 1;
            this.textForAction = "";
        }
        /// <summary>
        /// Создает действие не по умолчанию, с указанными параметрами.
        /// Принимает на вход указатель на экземпляр списка действий, который буде считатьтся родительским контейнером.
        /// </summary>
        /// <param name="pointerOnInstanceParent">Указатель на экземпляр списка действий, который будет считатьтся родительским контейнером для этого экземпляра.</param>
        public ActionOfMinion(Point cursorPositionIn, byte typeOfActionIn, ushort numberOfActionIn, int timeOfWaitingAfterActionInSecondIn,
            string textForActionIn, ListActionOfMinion pointerOnInstanceParent)
        {
            this.cursorPosition = cursorPositionIn;

            //Из-за того, что numberOfAction работает с созданным списком, его надо объявить сразу.
            listNameOfMauseAction = new List<string>[2];
            for (int i = 0; i < listNameOfMauseAction.Length; i++)
                listNameOfMauseAction[i] = new List<string>();
            this.typeOfAction = typeOfActionIn;
            this.numberOfAction = numberOfActionIn;
            FillMassiveOfListsOfNames();

            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            this.NumberOfTimesGoTo = 0;
            this.GoToNumberOfSequence = this.pointerOnInstanceParent.pointerOnInstanceParent.numberSearchAndActionInSequence;

            this.timeOfWaitingAfterActionInSecond = timeOfWaitingAfterActionInSecondIn;
            this.textForAction = textForActionIn;
        }
        private void FillMassiveOfListsOfNames()
        {
            //Заполнение списка для мыша
            listNameOfMauseAction[0].Add("Щелчек левой кнопкой");
            listNameOfMauseAction[0].Add("Двойной щелчек левой кнопкой");
            listNameOfMauseAction[0].Add("Щелчек правой кнопкой");
            listNameOfMauseAction[0].Add("Нажать на левую кнопку");
            listNameOfMauseAction[0].Add("Отпустить левую кнопку");

            //Заполнение списка для клавиатуры
            listNameOfMauseAction[1].Add("Вставить из буфера");
            listNameOfMauseAction[1].Add("Скопировать в буфер");
            listNameOfMauseAction[1].Add("Напечатать указаный текст");
            listNameOfMauseAction[1].Add("Напечатать текст из буфера");

            //Заполнение списка для дополнительно
            listNameOfMauseAction[2].Add("Ничего не делать");
            listNameOfMauseAction[2].Add("Отправиться к действию по номеру заданое количество раз");
            listNameOfMauseAction[2].Add("Вывести сообщение, что не удалось.(для тестов)");
            listNameOfMauseAction[2].Add("Повторить поиск");//дописать логику простое готу на свой номер


        }

        /// <summary>
        /// Создает и возвращает копию этого экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public ActionOfMinion Clone()
        {
            ActionOfMinion cloneOfActionOfMinion = new ActionOfMinion(this.pointerOnInstanceParent);

            cloneOfActionOfMinion.cursorPosition = this.cursorPosition ;
            cloneOfActionOfMinion.typeOfAction = this.typeOfActionPrivate;
            cloneOfActionOfMinion.numberOfAction = this.numberOfAction;
            cloneOfActionOfMinion.timeOfWaitingAfterActionInSecond = this.timeOfWaitingAfterActionInSecond;
            cloneOfActionOfMinion.textForAction = this.textForAction;
            cloneOfActionOfMinion.GoToNumberOfSequence = this.GoToNumberOfSequence;
            cloneOfActionOfMinion.NumberOfTimesGoTo = this.NumberOfTimesGoTo;

            return cloneOfActionOfMinion;
        }

        /// <summary>
        /// Выполняет действие заданного номера без задержки.
        /// </summary>
        public void RealizeAction()
        {
            Cursor.Position = this.cursorPosition;
            //Мышь
            if (typeOfAction == 0)
            {
                if (this.numberOfAction == 0)
                    MouseClickLeftButton();
                else if (this.numberOfAction == 1)
                    MouseDoubleClickLeftButton();
                else if (this.numberOfAction == 2)
                    MouseClickRightButton();
                else if (this.numberOfAction == 3)
                    MouseDownLeftButton();
                else if (this.numberOfAction == 4)
                    MouseUpLeftButton();

            }
            //Клавиатура
            if (typeOfAction == 1)
            {
                if (this.numberOfAction == 0)
                    PasteFromBuffer();
                else if (this.numberOfAction == 1)
                    CopyInBuffer();
                else if (this.numberOfAction == 2)
                    TypingText();
                else if (this.numberOfAction == 3)
                    TypingTextFromBuffer();
            }
            if (typeOfAction == 2)
            {
                //при 0 выборе надо ничего не делать.
                if (this.numberOfAction == 1)
                    GoToNumberOfListOfActionsMinion();
                else if(this.numberOfAction == 2)
                    MessageBox.Show("Не получилось!");

            }
        }
        /// <summary>
        /// Выполняет действие заданного номера с указаной в действии задержкой.
        /// </summary>
        public void RealizeActionWithWaiting()
        {
            RealizeAction();
            //Для "очеловечивания" добавляется разброс в 20%
            Random spreading = new Random();
            int countOfMilliseconds = spreading.Next(800, 1200);
            Thread.Sleep((this.timeOfWaitingAfterActionInSecond) * countOfMilliseconds);//Надо умножать на 1000, чтобы из получить ожидание в секундах
        }

        #region//Методы реализующие дейcтвия мыши
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
        private void MouseClickRightButton()
        {
            mouse_event(MouseEvent.MOUSEEVENTF_RIGHTDOWN,
            cursorPosition.X,
            cursorPosition.Y,
            0,
            0);
            mouse_event(MouseEvent.MOUSEEVENTF_RIGHTUP,
                        cursorPosition.X,
                        cursorPosition.Y,
                        0,
                        0);
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
        #endregion

        #region//Методы реализующие дейcтвия клавиатуры
        private void PasteFromBuffer()
        {
            SendKeys.Send("^v");
        }
        private void CopyInBuffer()
        {
            SendKeys.Send("^c");
        }
        /// <summary>
        /// Имитация печати текста. Берет данные из textForAction.
        /// </summary>
        private void TypingText()
        {
            for(int i=0; i<textForAction.Length; i++)
            {
                if (i == 0)//Надо проверить спец. символы. в начале
                {
                    if (textForAction[i] == '\\' && textForAction[+1] == 'n')
                    {
                        SendKeys.Send("{ENTER}");
                        i++;
                        continue;
                    }
                }
                if (i == textForAction.Length - 2)//Надо проверить спец. символы. в конце
                {
                    if (textForAction[i-1] != '\\' && textForAction[i] == '\\' && textForAction[+1] == 'n')
                    {
                        SendKeys.Send("{ENTER}");
                        break;
                    }
                }
                if (i > 0 && i < textForAction.Length - 1)//Если это не край, то надо проверить спец. символы.
                {
                    if (textForAction[i - 1] != '\\' && textForAction[i] == '\\' && textForAction[+1] == 'n')
                    {
                        SendKeys.Send("{ENTER}");
                        i++;
                        continue;
                    }
                }
                SendKeys.Send(textForAction[i].ToString());
            }
        }
        private void TypingTextFromBuffer()
        {
            string fieldForMemorizationTextForAction = textForAction;
            textForAction = Clipboard.GetText();
            TypingText();
            textForAction = fieldForMemorizationTextForAction;
        }
        #endregion

        #region//Методы реализующие дополнительные дейcтвия

        /// <summary>
        /// Хранит колчиство раз для выполнения перемщений согласно GoToNumberOfSequence.
        /// </summary>
        public int NumberOfTimesGoTo { get; set; }
        /// <summary>
        /// Хранит номер в последовательности, к которому надо переместиться.
        /// </summary>
        public int GoToNumberOfSequence { get; set; }
        /// <summary>
        /// Указывает на экземпляр объекта, который содержит этот экземпляр объекта.
        /// </summary>
        public ListActionOfMinion pointerOnInstanceParent { get; set; } 
        private void GoToNumberOfListOfActionsMinion()
        {
            //Если выполнилось нужное количство раз, то ничего не делать и просто продолжить выполнение в штатном режиме.
            if (this.NumberOfTimesGoTo > 0)
            {
                //будет выполнен переход на предыдущий указаному номеру, после чего в последовательности,
                //при переходе на следующий поиск, он будет увеличен.
                this.pointerOnInstanceParent.pointerOnInstanceParent.GoToNumber(this.GoToNumberOfSequence);
                this.NumberOfTimesGoTo--;
            }
        }
        #endregion

        #region//Дополнительные функции из неуправляемого кода.
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
        #endregion

    }
}
/*/// <summary>
   /// Я родился! Что ж уж тут еще сказать?!
   /// </summary>
   /// этот код копирует в буефр обмена текст.
            void IamBorn()
        {
            Clipboard.SetDataObject("Я родился!");
            IDataObject iData = Clipboard.GetDataObject();
        }
*/