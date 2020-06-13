namespace CourseworkTask
{
    partial class FilesAddingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListSelectedFiles = new System.Windows.Forms.ListBox();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.SelectedFilesCount = new System.Windows.Forms.TextBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.Prompt_PictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Prompt_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ListSelectedFiles
            // 
            this.ListSelectedFiles.AllowDrop = true;
            this.ListSelectedFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListSelectedFiles.FormattingEnabled = true;
            this.ListSelectedFiles.Location = new System.Drawing.Point(0, 0);
            this.ListSelectedFiles.Name = "ListSelectedFiles";
            this.ListSelectedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListSelectedFiles.Size = new System.Drawing.Size(464, 238);
            this.ListSelectedFiles.TabIndex = 0;
            this.ListSelectedFiles.Visible = false;
            // 
            // AcceptButton
            // 
            this.AcceptButton.Location = new System.Drawing.Point(326, 271);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(126, 38);
            this.AcceptButton.TabIndex = 1;
            this.AcceptButton.Text = "Ок";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // SelectedFilesCount
            // 
            this.SelectedFilesCount.Enabled = false;
            this.SelectedFilesCount.Location = new System.Drawing.Point(326, 244);
            this.SelectedFilesCount.Name = "SelectedFilesCount";
            this.SelectedFilesCount.Size = new System.Drawing.Size(126, 20);
            this.SelectedFilesCount.TabIndex = 2;
            this.SelectedFilesCount.Text = "Выбрано файлов: ";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(12, 271);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(126, 38);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Text = "Удалить выбранное";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // Prompt_PictureBox
            // 
            this.Prompt_PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Prompt_PictureBox.Image = global::CourseworkTask.Properties.Resources.DragAndDropImage;
            this.Prompt_PictureBox.Location = new System.Drawing.Point(0, 0);
            this.Prompt_PictureBox.Name = "Prompt_PictureBox";
            this.Prompt_PictureBox.Size = new System.Drawing.Size(464, 238);
            this.Prompt_PictureBox.TabIndex = 4;
            this.Prompt_PictureBox.TabStop = false;
            this.Prompt_PictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Prompt_PictureBox_DragDrop);
            this.Prompt_PictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.Prompt_PictureBox_DragEnter);
            // 
            // CopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.ControlBox = false;
            this.Controls.Add(this.Prompt_PictureBox);
            this.Controls.Add(this.SelectedFilesCount);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.ListSelectedFiles);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyForm";
            this.ShowIcon = false;
            this.Text = "CopyForm";
            ((System.ComponentModel.ISupportInitialize)(this.Prompt_PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListSelectedFiles;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.TextBox SelectedFilesCount;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.PictureBox Prompt_PictureBox;
    }
}