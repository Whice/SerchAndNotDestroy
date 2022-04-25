using System;
using System.Drawing;
using System.IO;

namespace SerchAndNotDestroy.Classes
{
    [Serializable]
    /// <summary>
    /// Класс содержащий общие настройки для помощника.
    /// </summary>
    public class SettingOfMinion
    {
        public void SetSettingDefault()
        {
            GetFullPathOfExeMinion();
            this.pathForSaveOfList = this.fullPathOfExeOfMinion;
        }
        /// <summary>
        /// Получает адресс, по которому находиться исполняемый файл.
        /// </summary>
        public void GetFullPathOfExeMinion()
        {
            //Узнаю полный адрес exe файла.
            this.fullPathOfExeOfMinion = Path.GetFullPath("e");
            this.fullPathOfExeOfMinion = this.fullPathOfExeOfMinion.Remove(this.fullPathOfExeOfMinion.Length - 1);
        }
        /// <summary>
        /// Содержит путь до исполняемого файла помощника.
        /// </summary>
        public string fullPathOfExeOfMinion { get; set; }
        /// <summary>
        /// Хранит путь, по которому надо сохранять списки поиска и действий.
        /// </summary>
        public string pathForSaveOfList { get; set; }

        public Color colorForBackColorMainForm { get; set; }
    }
}
