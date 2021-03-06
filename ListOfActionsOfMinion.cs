using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleMinion
{
    class ListOfActionsOfMinion
    {
        List<Search> listOfSearching;
        List<ActionOfMinion> listOfActionsAfterSearchin;
        public ListOfActionsOfMinion()
        {
            listOfSearching = new List<Search>();
            listOfActionsAfterSearchin = new List<ActionOfMinion>();
        }
    }
}
