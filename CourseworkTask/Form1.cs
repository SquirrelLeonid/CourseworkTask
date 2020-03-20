using System;
using System.Text;
using System.Windows.Forms;

namespace CourseworkTask
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void ToggleMenu_Click(object sender, EventArgs e)
        {
            int delta;
            if (MenuMapContainer.SplitterDistance == MenuMapContainer.Panel1MinSize)
                delta = 1;
            else
                delta = -1;
            for (int i = 0; i < MenuMapContainer.Size.Width - MenuMapContainer.Panel2MinSize; i++)
                MenuMapContainer.SplitterDistance += delta;
        }
    }
}
