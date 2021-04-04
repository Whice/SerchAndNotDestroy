using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLittleMinion
{
    partial class DialogWindowForSetting : Form
    {
        SettingOfMinion settingInDialog;

        public DialogWindowForSetting()
        {
            InitializeComponent();
            settingInDialog = new SettingOfMinion();
            settingInDialog.GetFullPathOfExeMinion();
            OpenSettingOfMinion();
        }

        public DialogWindowForSetting(ref SettingOfMinion setting)
        {
            InitializeComponent();
            this.settingInDialog = setting;
            this.settingInDialog.GetFullPathOfExeMinion();
            OpenSettingOfMinion();
            setting = this.settingInDialog;
            textBoxOfPathForSaveFile.Text = settingInDialog.pathForSaveOfList;
        }

        private void ButtonSaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
        public void SaveChanges()
        {
            settingInDialog.pathForSaveOfList = textBoxOfPathForSaveFile.Text;
            SaveSettingOfMinion();

            this.Close();
        }

        private void ButtonCancelChanges_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetDefaultSettings_Click(object sender, EventArgs e)
        {
            settingInDialog.SetSettingDefault();
            textBoxOfPathForSaveFile.Text = settingInDialog.pathForSaveOfList;
        }
        private void ButtonChangePathForSaveFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectPathOfFolder = new FolderBrowserDialog();
            if(selectPathOfFolder.ShowDialog() == DialogResult.OK)
            {
                textBoxOfPathForSaveFile.Text = selectPathOfFolder.SelectedPath;
            }
        }



        void SaveSettingOfMinion()
        {
            string FileName = settingInDialog.fullPathOfExeOfMinion + "SettingOfMinion.saveFile";
            Stream SaveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, this.settingInDialog);
            SaveFileStream.Close();
        }
        void OpenSettingOfMinion()
        {
            string FileName = settingInDialog.fullPathOfExeOfMinion + "SettingOfMinion.saveFile";
            if (File.Exists(FileName))
            {
                Stream openFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                this.settingInDialog = (SettingOfMinion)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
            }
            else
            {
                this.settingInDialog.SetSettingDefault();
            }
        }

        private void ButtonSetWhiteColorForFone_Click(object sender, EventArgs e)
        {
            this.settingInDialog.colorForBackColorMainForm = Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Помещает в fullPathOfExeOfMinion экземпляра путь к exe помощника.
        /// </summary>

    }
}

