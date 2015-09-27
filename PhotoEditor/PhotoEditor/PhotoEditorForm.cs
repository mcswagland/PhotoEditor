using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PhotoEditor
{
    public partial class PhotoEditorForm : Form
    {

        private Bitmap originalImage;
        private Bitmap transformedBitmap;
        private delegate void TintImageCallback(Color color);
        public ProgressWindow progressWindow;
        private Color tint;
        private string filePath;
        private bool dimensionsChanged = false;
        

        public PhotoEditorForm()
        {
            InitializeComponent();
        }

        public PhotoEditorForm(Image image, string file)
        {
            InitializeComponent();
            filePath = file;
            originalImage = new Bitmap(image);
            Bitmap pictureBoxImage = new Bitmap(image);
            if(image.Width > pictureBox1.Width && image.Height > pictureBox1.Height)
            {
                pictureBoxImage = ResizeImage(image, pictureBox1.Width, pictureBox1.Height);
                dimensionsChanged = true;
            }
            else if (image.Width > pictureBox1.Width)
            {
                pictureBoxImage = ResizeImage(image, pictureBox1.Width, image.Height);
                dimensionsChanged = true;
            }
            else if (image.Height > pictureBox1.Height)
            {
                pictureBoxImage = ResizeImage(image, image.Width, pictureBox1.Height);
                dimensionsChanged = true;
            }
            this.pictureBox1.Image = pictureBoxImage;
            transformedBitmap = (Bitmap)pictureBoxImage;
            
        }

        // function by Mark at stack overflow
        // http://stackoverflow.com/questions/1922040/resize-an-image-c-sharp
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            transformedBitmap = originalImage;
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(dimensionsChanged)
            {
                transformedBitmap = ResizeImage(transformedBitmap, originalImage.Width, originalImage.Height);
            }
            transformedBitmap.Save(filePath, ImageFormat.Jpeg);
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
