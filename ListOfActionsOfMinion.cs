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
    /// <summary>
    /// Это класс списка поисков и действий.
    /// Он объединяет в пару поиск-действие два класса, позволяя использовать их экемпляры вместе.
    /// Внутри задается и храниться номер пары, указывающий, какую именно пару можно спользовать.
    /// Этот класс можно сериализовать.
    /// </summary>
    class ListOfActionsOfMinion
    {
        /// <summary>
        /// Список класса поиска.
        /// </summary>
        private List<Search> listOfSearching;
        /// <summary>
        /// Списко класса действия.
        /// </summary>
        private List<ActionOfMinion> listOfActionsAfterSearchin;
        private string nameOfListOfSearchingAndActionsPrivate;
        /// <summary>
        /// Имя принадлежащее этому экземпляру списка поисков и действий.
        /// По умолчанию Default list search and action.
        /// Если подается пустая строка, то устанавливается имя по умолчанию.
        /// </summary>private
        public string nameOfListOfSearchingAndActions
        {
            get { return this.nameOfListOfSearchingAndActionsPrivate; }
            set
            {
                if (value.Replace(" ", "") == "")//Потому что только одни пробелы тоже можно считать пустотой, ну разве это название?!
                    this.nameOfListOfSearchingAndActionsPrivate = "Default list search and action";
                else
                    this.nameOfListOfSearchingAndActionsPrivate = value;
            }
        }
        /// <summary>
        /// Внутренний номер пары, указывающий, какую именно пару можно спользовать.
        /// Номер не должен быть меньше 0 и больше длинны списка.
        /// </summary>
        private int numberSearchAndActionInListPrivate;
        /// <summary>
        /// Задает и возвращает номер пары, указывающий, какую именно пару можно спользовать.
        /// Номер не может быть меньше 0 и больше длинны списка.
        /// </summary>
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
        /// <summary>
        /// Инициализирует списки и добавляет первые экзепляры поиска и действия со стандарными значениями.
        /// </summary>
        public ListOfActionsOfMinion()
        {
            this.listOfSearching = new List<Search>();
            this.listOfActionsAfterSearchin = new List<ActionOfMinion>();
            this.Add(new Search(), new ActionOfMinion());
        }

        /// <summary>
        /// Добавляет объекты Search и ActionOfMinion в концы соответсвующих очередей этого экземпляра.
        /// </summary>
        public void Add(Search SearchingForAdding, ActionOfMinion ActionsAfterSearchinForAdding)
        {
            this.listOfSearching.Add(SearchingForAdding);
            this.listOfActionsAfterSearchin.Add(ActionsAfterSearchinForAdding);
            this.numberSearchAndActionInList = this.listOfSearching.Count - 1;
            if (listOfSearching.Count < 1)
                this.nameOfListOfSearchingAndActions = "Default list search and action";
        }
        /// <summary>
        /// Возвращает длину списка.
        /// </summary>
        public int GetSizeOfListOfSearchAndActionsOfMinion()
        {
            return this.listOfSearching.Count;
        }
        /// <summary>
        /// Возвращает экземпляр поиска соотвествующий установленому номеру списка.
        /// </summary>
        public Search GetThisExemplarSearch()
        {
            return this.listOfSearching[numberSearchAndActionInList];
        }
        /// <summary>
        /// Возвращает экземпляр действия, соотвествующий установленому номеру списка.
        /// </summary>
        public ActionOfMinion GetThisExemplarActionOfMinion()
        {
            return this.listOfActionsAfterSearchin[numberSearchAndActionInList];
        }
        /// <summary>
        /// Удаляет экземпляры действия и поиска, соотвествующие установленому номеру списка.
        /// Счетчик движется назад. Если осталось одно действие в списке, то оно стирается и добавлется действие по умолчанию.
        /// </summary>
        public void RemoveThisExemplarSearchingAndAction()
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
        /// <summary>
        /// Сохраняет список в ПЗУ по указанному в settingOfMinion адресу.
        /// </summary>
        public bool Save(SettingOfMinion settingOfMinion)
        {
            if(this.nameOfListOfSearchingAndActions=="")
                this.nameOfListOfSearchingAndActions = "Default list search and action";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(settingOfMinion.pathForSaveOfList + "\\" + this.nameOfListOfSearchingAndActions + ".MLM", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
            return true;
        }
        /// <summary>
        /// Предоставлят диалоговое окно, где можно выбрать путь для сохранения и назание файла.
        /// После сохраняет список в ПЗУ по указанному адресу.
        /// </summary>
        public bool SaveAs(SettingOfMinion settingOfMinion)
        {
            string nameOpenFile = "";
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "My little minion files (*.MLM)|*.MLM*"; //формат загружаемого файла

            if (this.nameOfListOfSearchingAndActions == "")
                this.nameOfListOfSearchingAndActions = "Default list search and action";
            save_dialog.FileName = this.nameOfListOfSearchingAndActions;
            int countCopies = 1;
            string tempFileName = save_dialog.FileName;
            while (File.Exists(settingOfMinion.pathForSaveOfList + "\\" + tempFileName + ".MLM"))
            {
                tempFileName = save_dialog.FileName + " (" + countCopies.ToString() + ")";
                countCopies++;
            }
            save_dialog.FileName = tempFileName;
            save_dialog.FileName +=".MLM";

            save_dialog.InitialDirectory = settingOfMinion.pathForSaveOfList;
            if (save_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(save_dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();
            }

            
            
            return true;
        }
        /// <summary>
        /// Открывает список из ПЗУ по указанному пользователем адресу.
        /// </summary>
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
                nameOpenFile = nameOpenFile.Replace(".MLM" , "");
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


}
