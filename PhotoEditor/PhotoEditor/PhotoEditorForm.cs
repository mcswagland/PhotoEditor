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
            //TintImage(Color.Red);
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
            Color tint = Color.Red;
            
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                 tint = colorDialog.Color;
            }
            if(!tintColorWorker.IsBusy)
            {
               tintColorWorker.RunWorkerAsync(tint);
            }
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            if (!photoEditorWorker.IsBusy)
            {
                photoEditorWorker.RunWorkerAsync(2);
            }
        }

        private void photoEditorWorker_DoWork(object sender, DoWorkEventArgs e, int OptionSelect)
        {

        }

        private void InvertColors()
        {

            if(this.InvokeRequired)
            {

                
            }

            for (int y = 0; y < transformedBitmap.Height; y++)
            {
                for (int x = 0; x < transformedBitmap.Width; x++)
                {
                    Color color = transformedBitmap.GetPixel(x, y);
                    int newRed = Math.Abs(color.R - 255);
                    int newGreen = Math.Abs(color.G - 255);
                    int newBlue = Math.Abs(color.B - 255);
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    transformedBitmap.SetPixel(x, y, newColor);
                }
            }

            pictureBox1.Image = transformedBitmap;
            //pictureBox1.Invalidate();
        }

        private void TintImage(Color color)
        {
            if (this.InvokeRequired)
            {
                TintImageCallback callback = new TintImageCallback(TintImage);
                this.Invoke(callback, color);
            }
            else
            {
                setProgressBarMax(transformedBitmap.Height * transformedBitmap.Width);
                int count = 0;
                for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {
                        Color pictureColor = transformedBitmap.GetPixel(x, y);
                        float rgbAverage = (((float)(pictureColor.R + pictureColor.G + pictureColor.B) / 3) / 255);
                        int newR = (int)(color.R * rgbAverage);
                        int newG = (int)(color.G * rgbAverage);
                        int newB = (int)(color.B * rgbAverage);
                        Color newColor = Color.FromArgb(newR, newG, newB);
                        transformedBitmap.SetPixel(x, y, newColor);
                        count++;
                        tintColorWorker.ReportProgress(count);
                    }
                }
            }

            pictureBox1.Image = transformedBitmap;
        }

     

        private void brightnessBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (!photoEditorWorker.IsBusy)
            {
                photoEditorWorker.RunWorkerAsync(1);
            }
        }

        private void ChangeBrightness()
        {
            for (int y = 0; y < transformedBitmap.Height; y++)
            {
                for (int x = 0; x < transformedBitmap.Width; x++)
                {
                    

                    Color pictureColor = transformedBitmap.GetPixel(x, y);
                    int amount = Convert.ToInt32(2 * (50 - brightnessBar.Value) * 0.01 * 255);
                    int newR = pictureColor.R - amount;
                    int newG = pictureColor.G - amount;
                    int newB = pictureColor.B - amount;

                    if (newR >255)
                    {
                        newR = 255;
                    }

                    if (newG > 255)
                    {
                        newG = 255;
                    }

                    if (newB > 255)
                    {
                        newB = 255;
                    }

                    if (newR < 0)
                    {
                        newR = 0;
                    }

                    if (newG < 0)
                    {
                        newG = 0;
                    }

                    if (newB < 0)
                    {
                        newB = 0;
                    }


                    Color brightnessColor = Color.FromArgb(newR, newG, newB);
                    transformedBitmap.SetPixel(x, y, brightnessColor);

                    
                }
            }

            pictureBox1.Image = transformedBitmap;
        }

        private delegate void SetProgressBarMaxCallback(int max);

        void setProgressBarMax(int max)
        {
            if (colorButton.InvokeRequired)
            {
                SetProgressBarMaxCallback callback = new SetProgressBarMaxCallback(setProgressBarMax);
                this.Invoke(callback, max);
            }
            else
            {
                progressWindow.setProgressBarMax(max);
            }
        }

        private void tintColorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Color tint =  (Color)e.Argument;
            if(progressWindow == null)
            {
                progressWindow = new ProgressWindow();
            }
            progressWindow.Show();

            TintImage(tint);
            
        }

        private void tintColorWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressWindow.setProgressPercentage(e.ProgressPercentage);
        }
    }
}
