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
        private List<Image> fullSizeImages = new List<Image>();

        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Add("Name", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Date", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Size", 40, HorizontalAlignment.Right);

            imageListLarge.ImageSize = new Size(100, 100);
            imageListSmall.ImageSize = new Size(64, 64);
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

        private delegate void AddImageToListViewCallback(ListViewItem item);

        void addImageToListView(ListViewItem item)
        {
            if(listView1.InvokeRequired)
            {
                AddImageToListViewCallback callback = new AddImageToListViewCallback(addImageToListView);
                this.Invoke(callback, item);
            }
            else
            {
                listView1.Items.Add(item);
            }
        }

        private delegate void AddImageToImageViews(Image img);
        void addImageToImageViews(Image img)
        {
            if(listView1.InvokeRequired)
            {
                AddImageToImageViews callback = new AddImageToImageViews(addImageToImageViews);
                this.Invoke(callback, img);
            }
            else
            {
                imageListSmall.Images.Add(img);
                imageListLarge.Images.Add(img);
                fullSizeImages.Add(img);
            }
        }

        private void imageListWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int fileCount = currentDirectory.GetFiles().Where(x => x.Extension.ToLower() == ".jpg").Count();
            setProgressBarMax(fileCount);
            List<FileInfo> imageFiles = new List<FileInfo>();
            int count = 0;
            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                try
                {
                    if (file.Extension.ToLower() == ".jpg")
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(file.FullName);
                        MemoryStream ms = new MemoryStream(bytes);
                        Image img = Image.FromStream(ms);
                        addImageToImageViews(img);
                        ListViewItem item = new ListViewItem(file.Name, count);
                        item.SubItems.Add(file.LastWriteTime.ToString());
                        item.SubItems.Add(file.Length.ToString());
                        addImageToListView(item);
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("This is not an image file");
                }
                imageListWorker.ReportProgress(count);
            }
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

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            PhotoEditorForm photoEditor = new PhotoEditorForm();
            photoEditor.ShowDialog();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            var a = listView1.SelectedItems[0];
            Image img = fullSizeImages[listView1.SelectedItems[0].ImageIndex];
            PhotoEditorForm photoEditor = new PhotoEditorForm(img);
            photoEditor.ShowDialog();
        }

    
    }
}
