using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PhotoEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        void form1_addDirectory(string dir)
        {

        }

        private void directoryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //public delegate void addDirectoryDelegate(string dirName);

            //public event addDirectoryDelegate addDirectory;

            //addDirectory += form1_addDirectory;
            // change this to a variable probably holding the root directory
            DirectoryInfo homeDir = new DirectoryInfo(@"M:\");
            GetDirectories(homeDir);
            /*
            List<DirectoryInfo> directories = homeDir.GetDirectories().ToList();
            directories.Add(homeDir);
            //directories.
            foreach (DirectoryInfo dir in directories)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    try
                    {
                        if (file.Extension.ToLower() == ".jpg")
                        {
                            byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
                            MemoryStream ms = new MemoryStream(bytes);
                            Image img = Image.FromStream(ms);
                            Console.WriteLine("Filename: " + file.Name);
                            Console.WriteLine("Last mod: " + file.LastWriteTime.ToString());
                            Console.WriteLine("File size: " + file.Length);

                            directoryView.Nodes.Add("");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("This is not an image file");
                    }
                }
            }
             * */
        }

        private void GetDirectories(DirectoryInfo info)
        {
            foreach(DirectoryInfo dir in info.GetDirectories())
            {
                if(info.Root.ToString() == info.Name)
                {
                    directoryView.Nodes.Add(info.Name);
                }
                else
                {
                    directoryView.Nodes.Add(info.Name, dir.Name);
                }
                GetDirectories(dir);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!directoryWorker.IsBusy)
                directoryWorker.RunWorkerAsync();  
        }
    }
}
