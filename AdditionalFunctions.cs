using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLittleMinion
{
    /// <summary>
    /// Класс содержащий дополнительные функции для помощника.
    /// </summary>
    class AdditionalFunctions
    {
        /// <summary>
        /// Возвращает Bitmap заданных размеров и заполненый одним цветом.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="colorForFill"></param>
        /// <returns></returns>
        public static Bitmap FillBitmapWithColor(int width, int height, Color colorForFill)
        {
            Bitmap pictureForReturn = new Bitmap(width, height);

            for (int i = 0; i < pictureForReturn.Width; i++)
                for (int j = 0; j < pictureForReturn.Height; j++)
                    pictureForReturn.SetPixel(i, j, colorForFill);

            return pictureForReturn;
        }

    }

    [Serializable()]
    /// <summary>
    /// Класс содержащий общие настройки для помощника.
    /// </summary>
    class SettingOfMinion
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
    }




}
