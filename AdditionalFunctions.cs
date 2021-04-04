using System;
using System.Drawing;
using System.IO;

namespace MyLittleMinion
{
    /// <summary>
    /// Класс содержащий дополнительные функции для помощника.
    /// </summary>
    static class AdditionalFunctions
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
        /// <summary>
        /// Изменяет каждый из основных цветов стремясь к величине указанной номером от 1 до 8.
        /// </summary>
        /// <param name="colorForChange"></param>
        /// <param name="numBasicSet"></param>
        /// <returns></returns>
        static Color StriveForColor(Color colorForChange, int numBasicSet)
        {
            /*   R       G       B
            1    255    255	    255
            2    255	0	    255
            3    0	    255	    255
            4    0	    0	    255
            5    255	255	    0
            6    255	0	    0
            7    0	    255	    0
            8    0	    0	    0
            */
            //Для B
            if (numBasicSet > 0 && numBasicSet < 5 && colorForChange.B < 255)
                colorForChange = Color.FromArgb(colorForChange.R, colorForChange.G, colorForChange.B + 1);
            if (numBasicSet > 5 && numBasicSet < 9 && colorForChange.B > 0)
                colorForChange = Color.FromArgb(colorForChange.R, colorForChange.G, colorForChange.B - 1);

            //Для G
            if (numBasicSet % 2 == 0 && colorForChange.G < 255)
                colorForChange = Color.FromArgb(colorForChange.R, colorForChange.G + 1, colorForChange.B);
            if (numBasicSet % 2 != 0 && colorForChange.G > 0)
                colorForChange = Color.FromArgb(colorForChange.R, colorForChange.G - 1, colorForChange.B);

            //Для A
            if ((numBasicSet == 1 || numBasicSet == 2 || numBasicSet == 5 || numBasicSet == 6) && colorForChange.R < 255)
                colorForChange = Color.FromArgb(colorForChange.R + 1, colorForChange.G, colorForChange.B);
            if ((numBasicSet == 3 || numBasicSet == 4 || numBasicSet == 7 || numBasicSet == 8) && colorForChange.R > 0)
                colorForChange = Color.FromArgb(colorForChange.R - 1, colorForChange.G, colorForChange.B);
            return colorForChange;
        }
        /// <summary>
        /// Выполняет глубокое копирование объекта.
        /// </summary>
        public static T CloneOfObject<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("Тип объекта должен поддерживать сериализацию/The type must be serializable.", nameof(source));
            }

            // Don't serialize a null object, simply return the default for that object
            //Нельзя сериализовать не инициализированный объект, просто возвращается значение по умолчанию этого объекста
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream stream = new System.IO.MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
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

        public Color colorForBackColorMainForm { get; set; }
    }




}
