using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLittleMinion
{
    [Serializable]
    /// <summary>
    /// Это класс последовательности поисков и действий.
    /// Он объединяет в пару поиск-действие два класса, позволяя использовать их экемпляры вместе.
    /// Внутри задается и храниться номер пары, указывающий, какую именно пару можно спользовать.
    /// Этот класс можно сериализовать.
    /// </summary>
    class SequenceOfSearchesAndActions
    {
        #region//Поля и свойства послеовательности

        /// <summary>
        /// Список класса поиска.
        /// </summary>
        private List<Search> listOfSearching;
        /// <summary>
        /// Списки класса действия.
        /// </summary>
        private List<ListActionOfMinion> listsOfActionsAfterSearching;

        private string nameOfSequenceOfSearchesAndActionsPrivate;
        /// <summary>
        /// Имя принадлежащее этому экземпляру списка поисков и действий.
        /// По умолчанию Default list search and action.
        /// Если подается пустая строка, то устанавливается имя по умолчанию.
        /// </summary>private
        public string nameOfSequenceOfSearchesAndActions
        {
            get { return this.nameOfSequenceOfSearchesAndActionsPrivate; }
            set
            {
                if (value.Replace(" ", "") == "")//Потому что только одни пробелы тоже можно считать пустотой, ну разве это название?!
                    this.nameOfSequenceOfSearchesAndActionsPrivate = "Default list search and action";
                else
                    this.nameOfSequenceOfSearchesAndActionsPrivate = value;
            }
        }
        /// <summary>
        /// Внутренний номер пары, указывающий, какую именно пару можно спользовать.
        /// Номер не должен быть меньше 0 и больше длинны списка.
        /// </summary>
        private int numberSearchAndActionInSequencePrivate;
        /// <summary>
        /// Задает и возвращает номер пары, указывающий, какую именно пару можно спользовать.
        /// Номер не может быть меньше 0 и больше длинны списка.
        /// </summary>
        public int numberSearchAndActionInSequence
        {
            get { return this.numberSearchAndActionInSequencePrivate; }
            set
            {
                if (value <= 0)
                    this.numberSearchAndActionInSequencePrivate = 0;
                else if(value > this.listOfSearching.Count - 1)
                    this.numberSearchAndActionInSequencePrivate = this.listOfSearching.Count - 1;
                else
                    this.numberSearchAndActionInSequencePrivate = value;
            }
        }

        #endregion

        /// <summary>
        /// Инициализирует списки и добавляет первые экзепляры поиска и действия со стандарными значениями.
        /// </summary>
        public SequenceOfSearchesAndActions()
        {
            this.listOfSearching = new List<Search>();
            this.listsOfActionsAfterSearching = new List<ListActionOfMinion>();
            this.Add(new Search(), new ListActionOfMinion(this));
        }

        #region//Действия редактирования/запроса данных последовательности

        /// <summary>
        /// Добавляет объекты Search и ActionOfMinion в концы соответсвующих очередей этого экземпляра.
        /// </summary>
        public void Add(Search SearchingForAdding, ListActionOfMinion ActionsAfterSearchinForAdding)
        {
            if (listOfSearching.Count < 1)
                this.nameOfSequenceOfSearchesAndActions = "Default list search and action";
            this.listOfSearching.Add(SearchingForAdding);
            this.numberSearchAndActionInSequence = GetSizeOfListOfSearchAndActionsOfMinion() - 1;
            this.listsOfActionsAfterSearching.Add(ActionsAfterSearchinForAdding);
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
            return this.listOfSearching[numberSearchAndActionInSequence];
        }
        /// <summary>
        /// Возвращает экземпляр действия, соотвествующий установленому номеру списка.
        /// </summary>
        public ListActionOfMinion GetThisExemplarListActionsOfMinion()
        {
            return this.listsOfActionsAfterSearching[numberSearchAndActionInSequence];
        }
        /// <summary>
        /// Удаляет экземпляры действия и поиска, соотвествующие установленому номеру списка.
        /// Счетчик движется назад. Если осталось одно действие в списке, то оно стирается и добавлется действие по умолчанию.
        /// </summary>
        public void RemoveThisExemplarSearchingAndActions()
        {
            if (listOfSearching.Count == 1)
            {
                this.listOfSearching = new List<Search>();
                this.listsOfActionsAfterSearching = new List<ListActionOfMinion>();
                this.Add(new Search(), new ListActionOfMinion(this));
            }
            else
            {
                this.listOfSearching.RemoveAt(numberSearchAndActionInSequence);
                this.listsOfActionsAfterSearching.RemoveAt(numberSearchAndActionInSequence);
                if (this.numberSearchAndActionInSequence > 0)
                    this.numberSearchAndActionInSequence--;
            }

        }

        #endregion

        #region//Выполнение последовательности

        /// <summary>
        /// Выполняет поиск, список действий согласно всем настройкам, для указанного номером последовательности элемента.
        /// </summary>
        public bool SearchAndPerformThisAction()
        {
            //Упростить имена:
            Search thisSearch = this.listOfSearching[numberSearchAndActionInSequence];
            ListActionOfMinion thisActions = this.listsOfActionsAfterSearching[numberSearchAndActionInSequence];


            bool result = false;
            //выполнить поиск
            result = thisSearch.SearchModel();
            //Указать успешность поиска
            thisActions.isFound = result;
            //Если поиск не успешен, то точек не будет, значит местоположение курсора не должно измениться для последующих действий
            if (!result)
            {
                thisSearch.foundPoints = new System.Drawing.Point[1];
                thisSearch.foundPoints[0] = Cursor.Position;
            }
            //Выполнение действий
            for (int i = 0; i < thisActions.count; i++)
            {
                //Выполнить действие для всех найденых точек, кроме последней без ожидания.
                for (int numPoint = 0; numPoint < thisSearch.foundPoints.Length-1; numPoint++)
                {
                    thisActions.GetAction().cursorPosition = thisSearch.foundPoints[numPoint];
                    thisActions.GetAction().RealizeAction();
                }
                //А для последней точки сделать действие с ожиданием
                thisActions.GetAction().cursorPosition = thisSearch.foundPoints[thisSearch.foundPoints.Length - 1];
                thisActions.GetAction().RealizeActionWithWaiting();

                thisActions.numberActionInList++;
            }

            return result;
        }
        /// <summary>
        /// Устанавливает номер на один меньше, чем указаный, чтобы к нему можно было переместиться после выполнения действия.
        /// </summary>
        public void GoToNumber(int number)
        {
            this.numberSearchAndActionInSequence = number;
            this.numberSearchAndActionInSequencePrivate--;
        }

        #endregion

        #region//Сохранение/загрузка послеовательности

        /// <summary>
        /// Сохраняет список в ПЗУ по указанному в settingOfMinion адресу.
        /// </summary>
        public bool Save(SettingOfMinion settingOfMinion)
        {
            if(this.nameOfSequenceOfSearchesAndActions=="")
                this.nameOfSequenceOfSearchesAndActions = "Default list search and action";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(settingOfMinion.pathForSaveOfList + "\\" + this.nameOfSequenceOfSearchesAndActions + ".MLM", FileMode.Create, FileAccess.Write, FileShare.None);
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
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "My little minion files (*.MLM)|*.MLM*"; //формат загружаемого файла

            //Пустое имя не хррошо. Путь будет хотя бы по умолчанию.
            if (this.nameOfSequenceOfSearchesAndActions == "")
                this.nameOfSequenceOfSearchesAndActions = "Default list search and action";
            save_dialog.FileName = this.nameOfSequenceOfSearchesAndActions;

            //Проверка на существующие
            int countCopies = 1;
            string tempFileName = save_dialog.FileName;
            while (File.Exists(settingOfMinion.pathForSaveOfList + "\\" + tempFileName + ".MLM"))
            {
                tempFileName = save_dialog.FileName + " (" + countCopies.ToString() + ")";
                countCopies++;
            }
            //Когда насчет существующих было выяснено, можно запомнить конечное название и добавить расширение.
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
                SequenceOfSearchesAndActions loadFileListOfActionsOfMinion = (SequenceOfSearchesAndActions)formatter.Deserialize(stream);
                stream.Close();
                nameOpenFile = nameOpenFile.Replace(nameOpenFile.Replace(open_dialog.SafeFileName, ""), "");
                nameOpenFile = nameOpenFile.Replace(".MLM" , "");


                this.listOfSearching = loadFileListOfActionsOfMinion.listOfSearching;
                this.listsOfActionsAfterSearching = loadFileListOfActionsOfMinion.listsOfActionsAfterSearching;
                UpdatePointersOnParentForNestedInstances();
                this.nameOfSequenceOfSearchesAndActions = nameOpenFile;
                this.numberSearchAndActionInSequence = 0;

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region//Дополнительные функции или методы

        /// <summary>
        /// Обновление ссылок на родительские классы для внутренних.
        /// </summary>
        private void UpdatePointersOnParentForNestedInstances()
        {
            for (int i = 0; i < this.listsOfActionsAfterSearching.Count; i++)
            {
                this.listsOfActionsAfterSearching[i].pointerOnInstanceParent = this;
                List<ActionOfMinion> actionsOfMinion = this.listsOfActionsAfterSearching[i].GetListActions();
                for (int j = 0; j < actionsOfMinion.Count; j++)
                {
                    actionsOfMinion[j].pointerOnInstanceParent = this.listsOfActionsAfterSearching[i];
                }
            }
        }

        #endregion
    }
}
