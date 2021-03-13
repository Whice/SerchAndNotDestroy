using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLittleMinion
{
    [Serializable]
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
                if(listOfSearching.Count<1)
            this.nameOfListOfSearchingAndActions = "Default list search and action";

        }
        public int GetSizeOfListOfSearchAndActionsOfMinion()
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
        
        public bool SaveAs(SettingOfMinion settingOfMinion)
        {
            if(this.nameOfListOfSearchingAndActions=="")
                this.nameOfListOfSearchingAndActions = "Default list search and action";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(settingOfMinion.fullPathOfExeOfMinion+ this.nameOfListOfSearchingAndActions + ".MLM", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
            return true;
        }
        public bool Open(SettingOfMinion settingOfMinion)
        {
            string nameOpenFile = "";
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "My little minion files (*.MLM)|*.MLM*"; //формат загружаемого файла

            open_dialog.InitialDirectory = settingOfMinion.pathForSaveOfList;
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                nameOpenFile = open_dialog.FileName;
            }
            if (nameOpenFile != "")
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(nameOpenFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                ListOfActionsOfMinion loadFileListOfActionsOfMinion = (ListOfActionsOfMinion)formatter.Deserialize(stream);
                stream.Close();
                nameOpenFile = nameOpenFile.Replace(nameOpenFile.Replace(open_dialog.SafeFileName, ""), "");
                this.listOfSearching = loadFileListOfActionsOfMinion.listOfSearching;
                this.listOfActionsAfterSearchin = loadFileListOfActionsOfMinion.listOfActionsAfterSearchin;
                this.nameOfListOfSearchingAndActions = nameOpenFile;
                this.numberSearchAndActionInList = 0;

                return true;
            }
            else
            {
                return false;
            }
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
