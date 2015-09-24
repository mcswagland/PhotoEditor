namespace PhotoEditor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.directoryView = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.directoryWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateOnDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectRootFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListWorker = new System.ComponentModel.BackgroundWorker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryView
            // 
            this.directoryView.Location = new System.Drawing.Point(12, 62);
            this.directoryView.Name = "directoryView";
            this.directoryView.Size = new System.Drawing.Size(200, 398);
            this.directoryView.TabIndex = 0;
            this.directoryView.Click += new System.EventHandler(this.directoryView_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(218, 62);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(572, 398);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // directoryWorker
            // 
            this.directoryWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.directoryWorker_DoWork);
            this.directoryWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.directoryWorker_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(801, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateOnDiskToolStripMenuItem,
            this.selectRootFolderToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // locateOnDiskToolStripMenuItem
            // 
            this.locateOnDiskToolStripMenuItem.Name = "locateOnDiskToolStripMenuItem";
            this.locateOnDiskToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.locateOnDiskToolStripMenuItem.Text = "Locate on Disk";
            // 
            // selectRootFolderToolStripMenuItem
            // 
            this.selectRootFolderToolStripMenuItem.Name = "selectRootFolderToolStripMenuItem";
            this.selectRootFolderToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.selectRootFolderToolStripMenuItem.Text = "Select Root Folder...";
            this.selectRootFolderToolStripMenuItem.Click += new System.EventHandler(this.selectRootFolderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // imageListWorker
            // 
            this.imageListWorker.WorkerReportsProgress = true;
            this.imageListWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.imageListWorker_DoWork);
            this.imageListWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.imageListWorker_ProgressChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 37);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(778, 19);
            this.progressBar1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 492);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.directoryView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView directoryView;
        private System.Windows.Forms.ListView listView1;
        private System.ComponentModel.BackgroundWorker directoryWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locateOnDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectRootFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker imageListWorker;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

