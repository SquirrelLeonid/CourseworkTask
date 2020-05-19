using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkTask
{
    public partial class MainForm : Form
    {
        private bool _mouseHold;
        private Point _prevMousePos;

        public MainForm()
        {
            InitializeComponent();
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _prevMousePos = PointToClient(Cursor.Position);
            Cursor.Current = Cursors.Hand;
            _mouseHold = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseHold = false;
            Timer_UpdateMap.Stop();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Timer_UpdateMap.Start();
        }

        private void Timer_UpdateMap_Tick(object sender, EventArgs e)
        {
            //TODO вынести в отдельный класс MapVisualizer
            if (_mouseHold)
            {
                int mousePosX = PointToClient(Cursor.Position).X;
                int mousePosY = PointToClient(Cursor.Position).Y;

                int diffX = mousePosX - _prevMousePos.X;
                int diffY = mousePosY - _prevMousePos.Y;

                panel1.Location = new Point(panel1.Location.X + diffX, panel1.Location.Y + diffY);

                _prevMousePos.X = mousePosX;
                _prevMousePos.Y = mousePosY;
            }
        }
    }
}
