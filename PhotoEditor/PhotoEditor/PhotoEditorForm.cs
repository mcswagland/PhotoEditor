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

        private Bitmap originalImage;
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
            originalImage = new Bitmap(image);
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
            var preTransformImage = new Bitmap(transformedBitmap);
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
            
            DialogResult result = progressWindow.ShowDialog();
            if(result != DialogResult.Cancel)
            {
                pictureBox1.Image = transformedBitmap;
            }
            else
            {
                transformedBitmap = preTransformImage;
            }

            pictureBox1.Image = new Bitmap(transformedBitmap);
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            var preTransformImage = new Bitmap(transformedBitmap);
            progressWindow = new ProgressWindow(3, transformedBitmap);
            DialogResult result = progressWindow.ShowDialog();

            if(result != DialogResult.Cancel)
            {
                pictureBox1.Image = transformedBitmap;
            }
            else
            {
                transformedBitmap = preTransformImage;
            }
        }


        private void brightnessBar_MouseUp(object sender, MouseEventArgs e)
        {
            var preTransformImage = new Bitmap(transformedBitmap);
            progressWindow = new ProgressWindow(1, brightnessBar.Value, transformedBitmap);
            DialogResult result = progressWindow.ShowDialog();

            if(result != DialogResult.Cancel)
            {
                pictureBox1.Image = transformedBitmap;
            }
            else
            {
                transformedBitmap = preTransformImage;
            }
        }  
    }
}
