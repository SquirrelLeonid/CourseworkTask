﻿namespace CourseworkTask
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GeneralMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleMenu = new System.Windows.Forms.Button();
            this.MenuMapContainer = new System.Windows.Forms.SplitContainer();
            this.Panel_Map = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.GeneralMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenuMapContainer)).BeginInit();
            this.MenuMapContainer.Panel1.SuspendLayout();
            this.MenuMapContainer.Panel2.SuspendLayout();
            this.MenuMapContainer.SuspendLayout();
            this.Panel_Map.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeneralMenuStrip
            // 
            this.GeneralMenuStrip.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GeneralMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.GeneralMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.GeneralMenuStrip.Name = "GeneralMenuStrip";
            this.GeneralMenuStrip.Size = new System.Drawing.Size(739, 24);
            this.GeneralMenuStrip.TabIndex = 0;
            this.GeneralMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsКакToolStripMenuItem,
            this.LoadToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.CreateToolStripMenuItem.Text = "Создать";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить";
            // 
            // SaveAsКакToolStripMenuItem
            // 
            this.SaveAsКакToolStripMenuItem.Name = "SaveAsКакToolStripMenuItem";
            this.SaveAsКакToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.SaveAsКакToolStripMenuItem.Text = "Сохранить как";
            // 
            // LoadToolStripMenuItem
            // 
            this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            this.LoadToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.LoadToolStripMenuItem.Text = "Открыть";
            // 
            // ToggleMenu
            // 
            this.ToggleMenu.BackgroundImage = global::CourseworkTask.Properties.Resources.MenuIcon;
            this.ToggleMenu.Location = new System.Drawing.Point(0, 0);
            this.ToggleMenu.Name = "ToggleMenu";
            this.ToggleMenu.Size = new System.Drawing.Size(35, 35);
            this.ToggleMenu.TabIndex = 0;
            this.ToggleMenu.UseVisualStyleBackColor = true;
            this.ToggleMenu.Click += new System.EventHandler(this.ToggleMenu_Click);
            // 
            // MenuMapContainer
            // 
            this.MenuMapContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuMapContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MenuMapContainer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MenuMapContainer.IsSplitterFixed = true;
            this.MenuMapContainer.Location = new System.Drawing.Point(0, 24);
            this.MenuMapContainer.Name = "MenuMapContainer";
            // 
            // MenuMapContainer.Panel1
            // 
            this.MenuMapContainer.Panel1.Controls.Add(this.ToggleMenu);
            this.MenuMapContainer.Panel1MinSize = 35;
            // 
            // MenuMapContainer.Panel2
            // 
            this.MenuMapContainer.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MenuMapContainer.Panel2.Controls.Add(this.Panel_Map);
            this.MenuMapContainer.Panel2MinSize = 559;
            this.MenuMapContainer.Size = new System.Drawing.Size(739, 455);
            this.MenuMapContainer.SplitterDistance = 35;
            this.MenuMapContainer.SplitterWidth = 1;
            this.MenuMapContainer.TabIndex = 1;
            // 
            // Panel_Map
            // 
            this.Panel_Map.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Panel_Map.Controls.Add(this.listBox1);
            this.Panel_Map.Location = new System.Drawing.Point(114, 26);
            this.Panel_Map.Name = "Panel_Map";
            this.Panel_Map.Size = new System.Drawing.Size(400, 400);
            this.Panel_Map.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Какой то текст",
            "Какой то текст",
            "Какой то текст"});
            this.listBox1.Location = new System.Drawing.Point(145, 137);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 121);
            this.listBox1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 479);
            this.Controls.Add(this.MenuMapContainer);
            this.Controls.Add(this.GeneralMenuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.GeneralMenuStrip;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.GeneralMenuStrip.ResumeLayout(false);
            this.GeneralMenuStrip.PerformLayout();
            this.MenuMapContainer.Panel1.ResumeLayout(false);
            this.MenuMapContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MenuMapContainer)).EndInit();
            this.MenuMapContainer.ResumeLayout(false);
            this.Panel_Map.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip GeneralMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        private System.Windows.Forms.Button ToggleMenu;
        private System.Windows.Forms.SplitContainer MenuMapContainer;
        private System.Windows.Forms.Panel Panel_Map;
        private System.Windows.Forms.ListBox listBox1;
    }
}

