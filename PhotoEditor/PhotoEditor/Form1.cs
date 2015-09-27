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
using System.Threading;
using System.Diagnostics;

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

        private AutoResetEvent doneEvent = new AutoResetEvent(false);

        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Add("Name", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Date", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Size", 80, HorizontalAlignment.Left);

            listView1.MultiSelect = false;
            imageListLarge.ImageSize = new Size(100, 100);
            imageListSmall.ImageSize = new Size(64, 64);
            listView1.SmallImageList = imageListSmall;
            listView1.LargeImageList = imageListLarge;

            listView1.View = View.Details;
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
            DirectoryInfo homeDir = new DirectoryInfo(root);
            form1_addDirectory(CreateDirectoryNode(homeDir));
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
                clearListView();
                root = folderBrowser.SelectedPath;
                currentDirectory = new DirectoryInfo(root);
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
                if(imageListWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
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

        private string buildDirectoryPath(TreeNode node, string path)
        {
            string result = path;
            if(node.Parent != null)
            {
                if (node.Parent.Text.Contains('\\'))
                {
                    result = buildDirectoryPath(node.Parent, node.Parent.Text) + result;
                }
                else
                {
                    result = buildDirectoryPath(node.Parent, node.Parent.Text) + '\\' + result;
                }
            }
            return result;
        }

        private void directoryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            directoryView.SelectedNode = directoryView.TopNode;
            if (!imageListWorker.IsBusy)
                imageListWorker.RunWorkerAsync();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //dont think this ever gets called, maybe should delete
            PhotoEditorForm photoEditor = new PhotoEditorForm();
            photoEditor.ShowDialog();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            var a = listView1.SelectedItems[0];
            Image img = fullSizeImages[listView1.SelectedItems[0].ImageIndex];
            string filePath = currentDirectory.ToString() + '\\' + listView1.SelectedItems[0].Text;
            PhotoEditorForm photoEditor = new PhotoEditorForm(img, filePath);
            photoEditor.ShowDialog();
        }


        // thanks to Viacheslav Smityukh at stackoverflow for creating this function
        // http://stackoverflow.com/questions/4702506/treenode-selection-problems-in-c-sharp
        private bool IsClickOnText(TreeView treeView, TreeNode node, Point location)
        {
            var hitTest = treeView.HitTest(location);

            return hitTest.Node == node
                && hitTest.Location == TreeViewHitTestLocations.Label;
        }

        private void clearListView()
        {
            listView1.SmallImageList.Images.Clear();
            listView1.LargeImageList.Images.Clear();
            fullSizeImages.Clear();
            listView1.Items.Clear();
        }

        private void directoryView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (IsClickOnText(directoryView, e.Node, e.Location))
            {
                //if the user clicks on the node they're already on, don't do anything, otherwise load the images
                if (directoryView.SelectedNode.Text != e.Node.Text)
                {
                    if (imageListWorker.IsBusy)
                    {
                        imageListWorker.CancelAsync();
                    }
                    clearListView();

                    if (e.Node.Text != root.Substring(root.LastIndexOf('\\') + 1)&& e.Node.Text != root)
                    {
                        string path;
                        if(root.Contains('\\'))
                        {
                            path = buildDirectoryPath(e.Node, e.Node.Text);
                        }
                        else
                        {
                            path = root.Substring(0, root.LastIndexOf('\\')) + '\\' + buildDirectoryPath(e.Node, e.Node.Text);
                        }
                        currentDirectory = new DirectoryInfo(path);
                    }
                    else
                    {
                        currentDirectory = new DirectoryInfo(root);
                    }

                    if (!imageListWorker.IsBusy)
                    {
                        progressBar1.Visible = true;
                        imageListWorker.RunWorkerAsync();
                    }
                }
            }
        }

        private void imageListWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!detailsToolStripMenuItem.Checked)
            {
                detailsToolStripMenuItem.Checked = true;
                listView1.View = View.Details;
            }
            smallToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!smallToolStripMenuItem.Checked)
            {
                smallToolStripMenuItem.Checked = true;
                listView1.View = View.SmallIcon;
            }
            detailsToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!largeToolStripMenuItem.Checked)
            {
                largeToolStripMenuItem.Checked = true;
                listView1.View = View.LargeIcon;
            }
            detailsToolStripMenuItem.Checked = false;
            smallToolStripMenuItem.Checked = false;
        }

        private void locateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
            {
                DialogResult result = MessageBox.Show("Please select an image, then select this option again to locate it on disk.", "Select an image", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                string path = currentDirectory.ToString() + '\\' + listView1.SelectedItems[0].Text;

                //code suggested by Mahmoud Al-Qudsi at stack overflow
                //http://stackoverflow.com/questions/13680415/how-to-open-explorer-with-a-specific-file-selected
                Process.Start("explorer.exe", string.Format("/select,\"{0}\"", path));
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }
    
    }
}
