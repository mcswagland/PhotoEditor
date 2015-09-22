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
        private FolderBrowserDialog folderBrowser;

        //TODO: It says in the assignment that this is supposed to start out as the pictures directory
        private string root = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        private static string startingDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        private DirectoryInfo currentDirectory = new DirectoryInfo(startingDirectory);

        private ImageList imageListSmall = new ImageList();
        private ImageList imageListLarge = new ImageList();

        public Form1()
        {
            InitializeComponent();

            imageListLarge.ImageSize = new Size(32, 32);
            listView1.SmallImageList = imageListSmall;
            listView1.LargeImageList = imageListLarge;
            //change this to details
            listView1.View = View.SmallIcon;
        }


        void form1_addDirectory(TreeNode dir)
        {
            if (directoryView.InvokeRequired)
            {
                UpdateTreeViewCallback callback = new UpdateTreeViewCallback(form1_addDirectory);
                this.Invoke(callback, new object[] { dir });
            }
            else
            {
                directoryView.Nodes.Add(dir);
            }
        }

        private void directoryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //public delegate void addDirectoryDelegate(string dirName);

            //public event addDirectoryDelegate addDirectory;

            //addDirectory += form1_addDirectory;
            // change this to a variable probably holding the root directory
            DirectoryInfo homeDir = new DirectoryInfo(root);
            form1_addDirectory(CreateDirectoryNode(homeDir));
            /*
            List<DirectoryInfo> directories = homeDir.GetDirectories().ToList();
            directories.Add(homeDir);
            //directories.
           
                }
            }
             * */
        }

        private delegate void UpdateTreeViewCallback(TreeNode name);

        //Function by Alex Aza at http://stackoverflow.com/questions/6239544/populate-treeview-with-file-system-directory-structure
        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            return directoryNode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!directoryWorker.IsBusy)
                directoryWorker.RunWorkerAsync();  
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowser == null)
                folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK && folderBrowser.SelectedPath != root)
            {
                directoryView.Nodes.Clear();
                root = folderBrowser.SelectedPath;
                if (!directoryWorker.IsBusy)
                    directoryWorker.RunWorkerAsync();
            }
        }

        private delegate void SetProgressBarMaxCallback(int max);

        void setProgressBarMax(int max)
        {
            if (progressBar1.InvokeRequired)
            {
                SetProgressBarMaxCallback callback = new SetProgressBarMaxCallback(setProgressBarMax);
                this.Invoke(callback, max);
            }
            else
            {
                progressBar1.Maximum = max;
            }
        }

        private void imageListWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int fileCount = currentDirectory.GetFiles().Count();
            setProgressBarMax(fileCount);
            int count = 0;
            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                count++;
                try
                {
                    if (file.Extension.ToLower() == ".jpg")
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
                        MemoryStream ms = new MemoryStream(bytes);
                        Image img = Image.FromStream(ms);
                        imageListSmall.Images.Add(img);
                        imageListLarge.Images.Add(img);
                        Console.WriteLine("Filename: " + file.Name);
                        Console.WriteLine("Last mod: " + file.LastWriteTime.ToString());
                        Console.WriteLine("File size: " + file.Length);
                    }
                }
                catch
                {
                    Console.WriteLine("This is not an image file");
                }
                imageListWorker.ReportProgress(count);
            }
            Console.WriteLine("Blah");
        }

        private void imageListWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void directoryView_Click(object sender, EventArgs e)
        {
            if (!imageListWorker.IsBusy)
                imageListWorker.RunWorkerAsync();
        }

        private void directoryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!imageListWorker.IsBusy)
                imageListWorker.RunWorkerAsync();
        }

    
    }
}
