using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyLittleMinion
{
    [Serializable]
    class ListActionOfMinion
    {
        /// <summary>
        /// Список действий
        /// </summary>
        private List<ActionOfMinion> actionsOfMinion;
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
        /// Указывает на экземпляр объекта, который содержит этот экземпляр объекта.
        /// </summary>
        public SequenceOfSearchesAndActions pointerOnInstanceParent { get; set; }
        public ListActionOfMinion(SequenceOfSearchesAndActions pointerOnInstanceParent)
        {
            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            this.actionsOfMinion = new List<ActionOfMinion>();

            this.actionsOfMinion.Add(new ActionOfMinion(this));
        }
        public ListActionOfMinion(Point cursorPositionIn, byte typeOfActionIn, ushort numberOfActionIn, int timeOfWaitingAfterActionInSecondIn,
            string textForActionIn, SequenceOfSearchesAndActions pointerOnInstanceParent)
        {
            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            actionsOfMinion = new List<ActionOfMinion>();
            if (actionsOfMinion.Count == 0)
            {
                actionsOfMinion.Add(new ActionOfMinion(cursorPositionIn, typeOfActionIn, numberOfActionIn, timeOfWaitingAfterActionInSecondIn, textForActionIn, this));
            }

        }
        public ListActionOfMinion(ActionOfMinion newActionOfMinion, SequenceOfSearchesAndActions pointerOnInstanceParent)
        {
            //Во время создания запоминается родитель.
            this.pointerOnInstanceParent = pointerOnInstanceParent;

            actionsOfMinion = new List<ActionOfMinion>();
            actionsOfMinion.Add(newActionOfMinion);
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
        /// Указатель на родительский объект указывает на этот жкземпляр списка.
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
            ListActionOfMinion cloneListActionOfMinion = new ListActionOfMinion(this.actionsOfMinion[0].Clone(), this.pointerOnInstanceParent);
            for (int i = 1; i < this.actionsOfMinion.Count; i++)
                cloneListActionOfMinion.Add(this.actionsOfMinion[i].Clone());

            return cloneListActionOfMinion;
        }
    }
}
