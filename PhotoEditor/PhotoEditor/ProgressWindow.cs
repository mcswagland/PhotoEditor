using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        public void setProgressPercentage(int progressPercentage)
        {
            transformBar.Value = progressPercentage;
        }

        private delegate void SetProgressBarMaxCallback(int max);

        public void setProgressBarMax(int max)
        {
            if (transformBar.InvokeRequired)
            {
                SetProgressBarMaxCallback callback = new SetProgressBarMaxCallback(setProgressBarMax);
                this.Invoke(callback, max);
            }
            else
            {
                transformBar.Maximum = max;
            }
        }
    }
}
