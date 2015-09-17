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
        private string root = @"M:\";

        public Form1()
        {
            InitializeComponent();
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

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                root = folderBrowser.SelectedPath;
        }
    }
}
