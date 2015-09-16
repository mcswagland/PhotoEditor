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
            this.directoryView = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.directoryWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // directoryView
            // 
            this.directoryView.Location = new System.Drawing.Point(12, 12);
            this.directoryView.Name = "directoryView";
            this.directoryView.Size = new System.Drawing.Size(200, 425);
            this.directoryView.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(217, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(572, 424);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // directoryWorker
            // 
            this.directoryWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.directoryWorker_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 449);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.directoryView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView directoryView;
        private System.Windows.Forms.ListView listView1;
        private System.ComponentModel.BackgroundWorker directoryWorker;
    }
}

