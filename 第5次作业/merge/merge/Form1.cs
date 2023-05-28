using System;
using System.Windows.Forms;
using System.IO;

namespace merge
{
    public partial class Form1 : Form
    {
        private string saveFolderPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFile1.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowse2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFile2.Text = openFileDialog.FileName;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            string file1Path = txtFile1.Text;
            string file2Path = txtFile2.Text;

            if (string.IsNullOrEmpty(file1Path) || string.IsNullOrEmpty(file2Path))
            {
                MessageBox.Show("请选择要合并的文件。");
                return;
            }

            if (!File.Exists(file1Path) || !File.Exists(file2Path))
            {
                MessageBox.Show("选择的文件不存在。");
                return;
            }

            try
            {
                string file1Content = File.ReadAllText(file1Path);
                string file2Content = File.ReadAllText(file2Path);

                if (string.IsNullOrEmpty(saveFolderPath))
                {
                    MessageBox.Show("请选择要保存合并文件的文件夹。");
                    return;
                }

                string mergedFilePath = Path.Combine(saveFolderPath, "merged.txt");

                // 检查文件是否已存在
                if (File.Exists(mergedFilePath))
                {
                    MessageBox.Show("合并文件已存在。");
                    return;
                }

                using (StreamWriter writer = new StreamWriter(mergedFilePath))
                {
                    writer.Write(file1Content + file2Content);
                }

                MessageBox.Show("文件合并完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误：" + ex.Message);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                saveFolderPath = folderBrowserDialog.SelectedPath;
                txtSaveFolder.Text = saveFolderPath;
            }
        }
    }
}
