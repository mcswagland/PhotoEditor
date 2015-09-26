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
        public ProgressWindow(int selection, Bitmap bitmap)
        {
            InitializeComponent();
            optionSelected = selection;
            transformedBitmap = bitmap;
            originalImage = new Bitmap(bitmap);
            if(!progressBarWorker.IsBusy)
            {
                progressBarWorker.RunWorkerAsync();
            }

            
        }

        public ProgressWindow(int selection, int brightness, Bitmap bitmap)
        {
            InitializeComponent();
            optionSelected = selection;
            selectedBrightness = brightness;
            transformedBitmap = bitmap;
            originalImage = new Bitmap(bitmap);

            if (!progressBarWorker.IsBusy)
            {
                progressBarWorker.RunWorkerAsync();
            }
        }

        public ProgressWindow(int selection, Color color, Bitmap bitmap)
        {
            InitializeComponent();
            optionSelected = selection;
            selectedColor = color;
            transformedBitmap = bitmap;
            originalImage = new Bitmap(bitmap);

            if (!progressBarWorker.IsBusy)
            {
                progressBarWorker.RunWorkerAsync();
            }
        }

        private Color selectedColor;
        private int optionSelected;
        private Bitmap originalImage;
        private Bitmap transformedBitmap;
        private int selectedBrightness;

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

        private void progressBarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
          if(optionSelected == 1)
          {
              ChangeBrightness(selectedBrightness);
          }

          if (optionSelected == 2)
          {
              TintImage(selectedColor);
          }

          if (optionSelected == 3)
          {
              InvertColors();
          }

          setProgressBarMax((transformedBitmap.Width * transformedBitmap.Height));
          for(int y = 0; y <transformedBitmap.Height; y++)
          {
              for(int x = 0; x < transformedBitmap.Width; x++)
              {
                  progressBarWorker.ReportProgress((y*transformedBitmap.Width) + (x));
              }
          }
          System.Threading.Thread.Sleep(1000);


        }

        private delegate void InvertColorsCallback();

        private void InvertColors()
        {

            if (this.InvokeRequired)
            {
                InvertColorsCallback callback = new InvertColorsCallback(InvertColors);
                this.Invoke(callback);

            }
            else
            {
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
            }

            //pictureBox1.Image = transformedBitmap;

        }


        private delegate void ChangeBrightnessCallback(int brightness);
        private void ChangeBrightness(int brightnessSetting)
        {
            if (this.InvokeRequired)
            {
                ChangeBrightnessCallback callback = new ChangeBrightnessCallback(ChangeBrightness);
                this.Invoke(callback, brightnessSetting);
            }
            else
            { 
                for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {


                        Color pictureColor = transformedBitmap.GetPixel(x, y);
                        int amount = Convert.ToInt32(2 * (50 - brightnessSetting) * 0.01 * 255);
                        int newR = pictureColor.R - amount;
                        int newG = pictureColor.G - amount;
                        int newB = pictureColor.B - amount;

                        if (newR > 255)
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
            }

            
        }

        private delegate void TintImageCallback(Color color);
        private void TintImage(Color color)
        {
            if (this.InvokeRequired)
            {
                TintImageCallback callback = new TintImageCallback(TintImage);
                this.Invoke(callback, color);
            }
            else
            {

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
                        
                    }
                }
            }

            
        }

        private delegate void CancelButtonCallback();
        private void cancelButton_Click(object sender, EventArgs e)
        {
           if(progressBarWorker.WorkerSupportsCancellation)
           {
               transformedBitmap = originalImage;
               progressBarWorker.CancelAsync();
           }
        }

        private void progressBarWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
            Close();
        }

        private void progressBarWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            transformBar.Value = e.ProgressPercentage;
        }
    }
}
