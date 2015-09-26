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

        public PhotoEditorForm()
        {
            InitializeComponent();
        }

        public PhotoEditorForm(Image image)
        {
            InitializeComponent();
            this.pictureBox1.Image = image;
            originalImage = image;
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

        }

        private void invertButton_Click(object sender, EventArgs e)
        {

        }

        private void photoEditorWorker_DoWork(object sender, DoWorkEventArgs e, int OptionSelect)
        {

        }

        private void InvertColors()
        {
            //for (int y = 0; y < transformedBitmap.Height; y++)
            //{
            //    for (int x = 0; x < transformedBitmap.Width; x++)
            //    {
            //        Color color = transformedBitmap.GetPixel(x, y);
            //        int newRed = Math.Abs(color.R - 255);
            //        int newGreen = Math.Abs(color.G - 255);
            //        int newBlue = Math.Abs(color.B - 255);
            //        Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
            //        transformedBitmap.SetPixel(x, y, newColor);
            //    }
            //} 
        }
    }
}
