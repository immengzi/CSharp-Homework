using System;
using System.IO;
using System.Windows.Forms;

namespace FileBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 在左侧树形视图中显示用户目录
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            TreeNode userNode = new TreeNode(userFolder);
            userNode.Tag = userFolder;
            treeView1.Nodes.Add(userNode);

            // 加载用户目录下的文件和文件夹
            LoadFolder(userNode);
        }

        private void LoadFolder(TreeNode folderNode)
        {
            try
            {
                string folderPath = folderNode.Tag.ToString();

                // 加载文件夹中的子文件夹
                foreach (string subDir in Directory.GetDirectories(folderPath))
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(subDir));
                    subNode.Tag = subDir;
                    folderNode.Nodes.Add(subNode);

                    // 加载子文件夹中的文件和子文件夹
                    LoadFolder(subNode);
                }

                // 加载文件夹中的文件
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    string ext = Path.GetExtension(file);
                    if (ext == ".txt")
                    {
                        TreeNode fileNode = new TreeNode(Path.GetFileName(file));
                        fileNode.Tag = file;
                        folderNode.Nodes.Add(fileNode);
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果加载文件或文件夹失败，则在控制台中输出错误信息
                Console.WriteLine("Error loading folder: " + ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 当用户在左侧树形视图中选择一个节点时，更新右侧列表中的文件和文件夹
            listView1.Items.Clear();
            string selectedFolder = e.Node.Tag.ToString();
            try
            {
                // 显示文件夹中的子文件夹
                foreach (string subDir in Directory.GetDirectories(selectedFolder))
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(subDir));
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add("");
                    listView1.Items.Add(item);
                }

                // 显示文件夹中的txt文件
                foreach (string file in Directory.GetFiles(selectedFolder))
                {
                    string ext = Path.GetExtension(file);
                    if (ext == ".txt")
                    {
                        ListViewItem item = new ListViewItem(Path.GetFileName(file));
                        item.SubItems.Add("");
                        item.SubItems.Add(Path.GetFullPath(file));
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果加载文件或文件夹失败，则在控制台中输出错误信息
                Console.WriteLine("Error loading folder: " + ex.Message);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 当用户在右侧列表中双击一个txt文件时，打开该文件
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].SubItems[2].Text;
                if (File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(filePath);
                }
            }
        }
    }
}
