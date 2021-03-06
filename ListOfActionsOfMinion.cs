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
        public int numberSearchAndActionInList { get; set; }
        public ListOfActionsOfMinion()
        {
            listOfSearching = new List<Search>();
            listOfActionsAfterSearchin = new List<ActionOfMinion>();
            this.Add(new Search(), new ActionOfMinion());
        }

        /// <summary>
        /// Добавляет объекты Search и ActionOfMinion в концы соответсвующих очередей этого экземпляра.
        /// </summary>
        /// <param name="listOfSearchingForAdding"></param>
        /// <param name="listOfActionsAfterSearchinForAdding"></param>
        public void Add(Search listOfSearchingForAdding, ActionOfMinion listOfActionsAfterSearchinForAdding)
        {
            this.listOfSearching.Add(listOfSearchingForAdding);
            this.listOfActionsAfterSearchin.Add(listOfActionsAfterSearchinForAdding);
            numberSearchAndActionInList = listOfSearching.Count - 1;
        }
    }
}
