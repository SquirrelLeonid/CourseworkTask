using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseworkTask
{
    public partial class CopyForm : Form
    {
        public CopyForm()
        {
            InitializeComponent();
        }

        private void UpdateInfo()
        {
            SelectedFilesCount.Text = "Выбрано файлов: " + ListSelectedFiles.Items.Count;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListSelectedFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void ListSelectedFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                ListSelectedFiles.Items.Add(file);
                UpdateInfo();
            }
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
            UpdateInfo();
        }
    }
}
