using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkTask
{
    public partial class MainForm : Form
    {
        private MapModel _mapModel;
        private FileParser _fileParser;
        private CodeBlockFormer _blockFormer;

        public MainForm()
        {
            InitializeComponent();
            InitFieldsState();
            groupBox1.Size = groupBox1.MaximumSize;
            Button button = new Button();
            button.Size = new Size(20,20);
            button.Text = "v";
            button.Location = new Point(groupBox1.Width - button.Width, 0);
            button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(button);
            button.Visible = true;
            //InitAdditionalEvents();
        }

        private void InitFieldsState()
        {
            _fileParser = new FileParser();
            _mapModel = new MapModel(Panel_Map);
            _blockFormer = new CodeBlockFormer();
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
            FilesAddingForm filesAddingForm = new FilesAddingForm();
            filesAddingForm.ShowDialog();
            //TODO: Нужно как то заменить получение результата. В идеале передавать сюда, а не просить отсюда.
            List<Document> result = filesAddingForm.GetReadResult();
            //Начиная с этого момента можно добавить многопоточность
            ParsedCodeKeeper parseResult = _fileParser.ParseCodeListings(result);
            //_blockFormer.FormCodeBlocks(parseResult);
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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var element in groupBox1.Controls)
            {
                if (element is Button)
                    continue;
                TextBox box = (TextBox) element;
                box.Visible = !box.Visible;
            }
            groupBox1.Size = groupBox1.Size == groupBox1.MinimumSize ? groupBox1.MaximumSize : groupBox1.MinimumSize;
        }
    }
}