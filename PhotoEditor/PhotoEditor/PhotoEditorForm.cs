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

        private Image originalImage;
        private Bitmap transformedBitmap;
        private delegate void TintImageCallback(Color color);
        public ProgressWindow progressWindow;
        private Color tint;
        

        public PhotoEditorForm()
        {
            InitializeComponent();
        }

        public PhotoEditorForm(Image image)
        {
            InitializeComponent();
            this.pictureBox1.Image = image;
            originalImage = image;
            transformedBitmap = (Bitmap)image;
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                tint = colorDialog.Color;
            }
            else
            {
                return;
            }
            progressWindow = new ProgressWindow(2, tint, transformedBitmap);
            progressWindow.Show();

            pictureBox1.Image = transformedBitmap;
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            progressWindow = new ProgressWindow(3, transformedBitmap);
            progressWindow.Show();

       

            pictureBox1.Image = transformedBitmap;
        }


        private void brightnessBar_MouseUp(object sender, MouseEventArgs e)
        {
            progressWindow = new ProgressWindow(1, brightnessBar.Value, transformedBitmap);
            progressWindow.Show();

            pictureBox1.Image = transformedBitmap;
        }



        



       

      
        
    }
}
