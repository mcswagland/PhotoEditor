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
    public partial class PhotoEditorForm : Form
    {
        public PhotoEditorForm()
        {
            InitializeComponent();
        }

        public PhotoEditorForm(Image image)
        {
            InitializeComponent();
            this.pictureBox1.Image = image;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
