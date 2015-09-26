namespace PhotoEditor
{
    partial class ProgressWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressWindow));
            this.transformBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.progressBarWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // transformBar
            // 
            this.transformBar.Location = new System.Drawing.Point(62, 47);
            this.transformBar.Name = "transformBar";
            this.transformBar.Size = new System.Drawing.Size(315, 23);
            this.transformBar.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(191, 87);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // progressBarWorker
            // 
            this.progressBarWorker.WorkerReportsProgress = true;
            this.progressBarWorker.WorkerSupportsCancellation = true;
            this.progressBarWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.progressBarWorker_DoWork);
            this.progressBarWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.progressBarWorker_ProgressChanged);
            this.progressBarWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.progressBarWorker_RunWorkerCompleted);
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(481, 122);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.transformBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transforming...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar transformBar;
        private System.Windows.Forms.Button cancelButton;
        private System.ComponentModel.BackgroundWorker progressBarWorker;
    }
}