using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyLittleMinion
{
    [Serializable]
    class ListActionOfMinion
    {
        /// <summary>
        /// Общий список действий
        /// </summary>
        private List<ActionOfMinion> actionsOfMinion;
        /// <summary>
        /// Список действий для неудачного поиска
        /// </summary>
        private List<ActionOfMinion> actionsOfMinionForNotFound;
        /// <summary>
        /// Список действий для удачного поиска
        /// </summary>
        private List<ActionOfMinion> actionsOfMinionForFound;
        /// <summary>
        /// Хранит значение, какой список используется: для удачного поиска или нет.
        /// При изменении начения, меняет и список.
        /// </summary>
        public bool isFound
        {
            get { return this.isFoundPrivate; }
            set
            {
                this.isFoundPrivate = value;
                this.numberActionInListPrivate = 0;
                //после получения значения, общему списку назначается нужный из конекретных.
                if (this.isFoundPrivate)
                    actionsOfMinion = actionsOfMinionForFound;
                else
                    actionsOfMinion = actionsOfMinionForNotFound;
            }
        }
        private bool isFoundPrivate;
        /// <summary>
        /// Количество действий в списке.
        /// </summary>
        public int count { get { return actionsOfMinion.Count; } }
        private ushort numberActionInListPrivate;
        /// <summary>
        /// Номер действия в списке
        /// </summary>
        public ushort numberActionInList
        {
            get { return this.numberActionInListPrivate; }
            set
            {
                if (value < 0 || value > actionsOfMinion.Count - 1)
                    this.numberActionInListPrivate = 0;
                else
                    this.numberActionInListPrivate = value;
            }
        }
        /// <summary>
        /// Указатель на экземпляр последовательности, который будет считатьтся родительским контейнером для этого экземпляра.
        /// </summary>
        public SequenceOfSearchesAndActions pointerOnInstanceParent { get; set; }
        /// <summary>
        /// Создает новый экземпляр списка действий, с первым дествием созданным по умолчанию.
        /// Принимает на вход указатель на экземпляр последовательности, который буде считатьтся родительским контейнером.
        /// </summary>
        /// <param name="pointerOnInstanceParent">Указатель на экземпляр последовательности, который будет считатьтся родительским контейнером для этого экземпляра.</param>
        public ListActionOfMinion(SequenceOfSearchesAndActions pointerOnInstanceParent)
        {
            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            this.actionsOfMinionForNotFound = new List<ActionOfMinion>();
            this.actionsOfMinionForFound = new List<ActionOfMinion>();

            this.actionsOfMinionForNotFound.Add(new ActionOfMinion(this));
            this.actionsOfMinionForFound.Add(new ActionOfMinion(this));

            this.isFound = true;
        }
        /// <summary>
        /// Создает новый экземпляр списка действий. Принимая на вход параметры для создания первого действия не по умолчанию.
        /// Принимает на вход указатель на экземпляр последовательности, который буде считатьтся родительским контейнером.
        /// </summary>
        /// <param name="pointerOnInstanceParent">Указатель на экземпляр последовательности, который будет считатьтся родительским контейнером для этого экземпляра.</param>
        public ListActionOfMinion(Point cursorPositionIn, byte typeOfActionIn, ushort numberOfActionIn, int timeOfWaitingAfterActionInSecondIn,
            string textForActionIn, SequenceOfSearchesAndActions pointerOnInstanceParent)
        {
            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            ActionOfMinion newActionOfMinion = new ActionOfMinion(cursorPositionIn, typeOfActionIn, numberOfActionIn, timeOfWaitingAfterActionInSecondIn, textForActionIn, this);

            this.actionsOfMinionForNotFound = new List<ActionOfMinion>();
            this.actionsOfMinionForFound = new List<ActionOfMinion>();

            this.actionsOfMinionForNotFound.Add(newActionOfMinion);
            this.actionsOfMinionForFound.Add(newActionOfMinion);

            this.isFound = true;
        }
        /// <summary>
        /// Создает новый экземпляр списка действий, с первыми действиями предоставленными для списков в качестве параметра.
        /// Принимает на вход указатель на экземпляр последовательности, который буде считатьтся родительским контейнером.
        /// </summary>
        /// <param name="newActionOfMinionForNotFoundList">Экземпляр действия для списка, который будет зайдествован, если указан НЕудачный поиск.</param>
        /// <param name="newActionOfMinionForFoundList">Экземпляр действия для списка, который будет зайдествован, если указан удачный поиск.</param>
        /// <param name="pointerOnInstanceParent">Указатель на экземпляр последовательности, который будет считатьтся родительским контейнером для этого экземпляра.</param>
        public ListActionOfMinion(ActionOfMinion newActionOfMinionForNotFoundList, ActionOfMinion newActionOfMinionForFoundList, SequenceOfSearchesAndActions pointerOnInstanceParent)
        {
            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            this.actionsOfMinionForNotFound = new List<ActionOfMinion>();
            this.actionsOfMinionForFound = new List<ActionOfMinion>();

            this.actionsOfMinionForNotFound.Add(newActionOfMinionForNotFoundList);
            this.actionsOfMinionForFound.Add(newActionOfMinionForFoundList);

            this.isFound = true;
        }
        /// <summary>
        /// Возвращает экземпляр ActionOfMinion соответсвующий нынешнему номеру.
        /// </summary>
        public ActionOfMinion GetAction()
        {
            return actionsOfMinion[numberActionInListPrivate];
        }
        /// <summary>
        /// Возвращает копию списка действий этого экземпляра.
        /// </summary>
        public List<ActionOfMinion> GetListActions()
        {
            return this.Clone().actionsOfMinion;
        }
        /// <summary>
        /// Добавить новый экземпляр действия.
        /// Новому экзмплеру объекта действия нужно указать родительский объект.
        /// </summary>
        /// <param name="newActionOfMinion"></param>
        public void Add(ActionOfMinion newActionOfMinion)
        {
            actionsOfMinion.Add(newActionOfMinion);
        }
        /// <summary>
        /// Добавить новый экземпляр действия.
        /// Указатель на родительский объект указывает на этот же кземпляр списка.
        /// </summary>
        public void AddAction()
        {
            actionsOfMinion.Add(new ActionOfMinion(this));
        }

        /// <summary>
        /// Удаляет нынешний экземпляр действия.
        /// </summary>
        public void Remove()
        {
            if (actionsOfMinion.Count > 1)
            {
                actionsOfMinion.RemoveAt(this.numberActionInList);
                if (this.numberActionInList > 0)
                    this.numberActionInList--;
            }
        }
        /// <summary>
        /// Возвращает полный клон экземпляра списка действий.
        /// </summary>
        /// <returns></returns>
        public ListActionOfMinion Clone()
        {
            //Запомнить выбранный из списков
            bool selectedActionList = this.isFound;
            //Создать новый список и передать туда два первых действия
            ListActionOfMinion cloneListActionOfMinion = 
                new ListActionOfMinion(this.actionsOfMinionForNotFound[0].Clone(), this.actionsOfMinionForFound[0].Clone(), this.pointerOnInstanceParent);
            //Установить фокус на список с найденым эталоном и передать действия 
            this.isFound = true;
            cloneListActionOfMinion.isFound = true;
            for (int i = 1; i < this.actionsOfMinion.Count; i++)
                cloneListActionOfMinion.Add(this.actionsOfMinion[i].Clone());
            //Установить фокус на список с не найденым эталоном и передать действия 
            this.isFound = false;
            cloneListActionOfMinion.isFound = false;
            for (int i = 1; i < this.actionsOfMinion.Count; i++)
                cloneListActionOfMinion.Add(this.actionsOfMinion[i].Clone());
            //Установить изначальный выбор списка
            this.isFound = selectedActionList;
            cloneListActionOfMinion.isFound = selectedActionList;

            return cloneListActionOfMinion;
        }
    }
}
