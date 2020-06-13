using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseworkTask
{
    public partial class FilesAddingForm : Form
    {
        private List<Document> _readResult;

        public FilesAddingForm()
        {
            InitializeComponent();
            ListSelectedFiles.Visible = false;
            Prompt_PictureBox.Visible = true;
            Prompt_PictureBox.AllowDrop = true;
        }

        public List<Document> GetReadResult()
        {
            return _readResult;
        }

        private void UpdateInfo()
        {
            SelectedFilesCount.Text = "Выбрано файлов: " + ListSelectedFiles.Items.Count;
        }

        //TODO: Добавить проверку на то что файлы вообще были добавлены
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            FileReaderSingleton fileReader = FileReaderSingleton.GetReference();
            _readResult = fileReader.ReadFiles(ListSelectedFiles.Items);
            this.Close();
        }


        private void Prompt_PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Prompt_PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<string> correctFiles = CheckExtensions(files);

            foreach (string file in correctFiles)
            {
                ListSelectedFiles.Items.Add(file);
                UpdateInfo();
            }

            if (ListSelectedFiles.Items.Count > 0)
            {
                ListSelectedFiles.Visible = true;
                Prompt_PictureBox.Visible = false;
            }
        }

        //TODO: NullReference exception
        private List<string> CheckExtensions(string[] files)
        {
            List<string> correctFiles = new List<string>();
            foreach (string file in files)
            {
                if (Path.GetExtension(file).CompareTo(".cs") == 0)
                    correctFiles.Add(file);
            }

            return correctFiles;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var selectedItems = ListSelectedFiles.SelectedItems;
            while (selectedItems.Count != 0)
            {
                var item = selectedItems[0];
                selectedItems.Remove(item);
                ListSelectedFiles.Items.Remove(item);
            }

            if (ListSelectedFiles.Items.Count == 0)
            {
                ListSelectedFiles.Visible = false;
                Prompt_PictureBox.Visible = true;
            }
            UpdateInfo();
        }
    }
}
