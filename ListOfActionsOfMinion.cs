using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleMinion
{
    class ListOfActionsOfMinion
    {
        public List<Search> listOfSearching;
        public List<ActionOfMinion> listOfActionsAfterSearchin;
        public string nameOfListOfSearchingAndActions { get; set; }
        public int numberSearchAndActionInList { get; set; }
        public ListOfActionsOfMinion()
        {
            listOfSearching = new List<Search>();
            listOfSearching.Add(new Search());
            listOfActionsAfterSearchin = new List<ActionOfMinion>();
            listOfActionsAfterSearchin.Add(new ActionOfMinion());
            this.Add(new Search(), new ActionOfMinion());
            this.nameOfListOfSearchingAndActions = "Default list search and action.";
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
            numberSearchAndActionInList = listOfSearching.Count - 1;
        }
    }

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
            multyThreadSearch = false;
        }
        /// <summary>
        /// Использование многопоточности во время поиска.
        /// </summary>
        public bool multyThreadSearch { get; set; }
        //Для хранения установок поиска!!
    }
}
