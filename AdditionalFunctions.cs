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
    class AdditionalFunctions
    {
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
    class SettingOfMinion
    {
        public void SetSettingDefault()
        {
            GetFullPathOfExeMinion();
            this.pathForSaveOfList = this.fullPathOfExeOfMinion;
        }
        public void GetFullPathOfExeMinion()
        {
            //Узнаю полный адрес exe файла.
            this.fullPathOfExeOfMinion = Path.GetFullPath("e");
            this.fullPathOfExeOfMinion = this.fullPathOfExeOfMinion.Remove(this.fullPathOfExeOfMinion.Length - 1);
        }

        public string fullPathOfExeOfMinion { get; set; }
        public string pathForSaveOfList { get; set; }
    }




}
