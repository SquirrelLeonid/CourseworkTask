using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkTask
{
    public partial class MainForm : Form
    {
        private MapModel _mapModel;

        public MainForm()
        {
            InitializeComponent();
            InitFieldsState();
            //InitAdditionalEvents();
        }

        private void InitFieldsState()
        {
            _mapModel = new MapModel(Panel_Map);
        }

        private void InitAdditionalEvents()
        {

        }

        #region MainForm's buttons event
        private void ToggleMenu_Click(object sender, EventArgs e)
        {
            int delta = MenuMapContainer.SplitterDistance == MenuMapContainer.Panel1MinSize ? 1 : -1;
            for (int i = 0; i < MenuMapContainer.Panel1MinSize; i++)
            {
                MenuMapContainer.SplitterDistance += delta;
                this.Update();
            }
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyForm copyForm = new CopyForm();
            copyForm.ShowDialog();
        }
        #endregion

        #region Drag and drop from outside
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        }
        #endregion
    }
}