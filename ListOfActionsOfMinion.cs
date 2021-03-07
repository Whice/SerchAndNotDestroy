using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleMinion
{
    class ListOfActionsOfMinion
    {
        private List<Search> listOfSearching;
        private List<ActionOfMinion> listOfActionsAfterSearchin;
        public string nameOfListOfSearchingAndActions { get; set; }
        private int numberSearchAndActionInListPrivate;
        public int numberSearchAndActionInList
        {
            get { return this.numberSearchAndActionInListPrivate; }
            set
            {
                if (value <= 0 || value > this.listOfSearching.Count - 1)
                    this.numberSearchAndActionInListPrivate = 0;
                else
                    this.numberSearchAndActionInListPrivate = value;
            }
        }
        public ListOfActionsOfMinion()
        {
            this.listOfSearching = new List<Search>();
            this.listOfActionsAfterSearchin = new List<ActionOfMinion>();
            this.Add(new Search(), new ActionOfMinion());
        }

        /// <summary>
        /// Добавляет объекты Search и ActionOfMinion в концы соответсвующих очередей этого экземпляра.
        /// </summary>
        /// <param name="SearchingForAdding"></param>
        /// <param name="ActionsAfterSearchinForAdding"></param>
        public void Add(Search SearchingForAdding, ActionOfMinion ActionsAfterSearchinForAdding)
        {
            this.listOfSearching.Add(SearchingForAdding);
            this.listOfActionsAfterSearchin.Add(ActionsAfterSearchinForAdding);
            this.numberSearchAndActionInList = this.listOfSearching.Count - 1;
            this.nameOfListOfSearchingAndActions = "Default list search and action.";
        }
        public int GetSizeOfListOfActionsOfMinion()
        {
            return this.listOfSearching.Count;
        }
        public Search GetThisExemplarSearch()
        {
            return this.listOfSearching[numberSearchAndActionInList];
        }
        public ActionOfMinion GetThisExemplarActionOfMinion()
        {
            return this.listOfActionsAfterSearchin[numberSearchAndActionInList];
        }
        void RemoveThisExemplarSearchingAndAction()
        {
            if(listOfSearching.Count ==1)
            {
                this.listOfSearching = new List<Search>();
                this.listOfActionsAfterSearchin = new List<ActionOfMinion>();
                this.Add(new Search(), new ActionOfMinion());
            }
            this.listOfSearching.RemoveAt(numberSearchAndActionInList);
            this.listOfActionsAfterSearchin.RemoveAt(numberSearchAndActionInList);
            if (this.numberSearchAndActionInList > 0)
                this.numberSearchAndActionInList--;

        }

    }










    /*
    /// <summary>
    /// Структура хранящая иформацию о настройках поиска и выборе действия для одного экземпляра соответсвующего номеру.
    /// </summary>
    public struct ConfigOfSearch
    {
        /// <summary>
        /// Если true, то устанавливает заданные структурой настройки по умолчанию.
        /// </summary>
        /// <param name="defaultConfig"></param>
        public ConfigOfSearch(bool defaultConfig)
        {
            multyThreadSearch = 0;
        }
        /// <summary>
        /// Использование многопоточности во время поиска. 0 - Четыре потока для четвертей по углам. 1 или больше, число становиться количеством потоков: область поиска делиться потоками на столбцы.
        /// </summary>
        public int multyThreadSearch { get; set; }

        //Для хранения установок поиска!!
    }*/
}
