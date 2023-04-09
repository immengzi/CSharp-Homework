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


namespace merge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse1_Click_1(object sender, EventArgs e)
        {
            // 打开文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 显示所选文件路径到文本框
                txtFile1.Text = openFileDialog.FileName;
            }
        }

        private void btnBrowse2_Click_1(object sender, EventArgs e)
        {
            // 打开文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 显示所选文件路径到文本框
                txtFile2.Text = openFileDialog.FileName;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            // 读取文件1和文件2的内容
            string file1Content = File.ReadAllText(txtFile1.Text);
            string file2Content = File.ReadAllText(txtFile2.Text);

            // 创建新文件，将文件1和文件2的内容合并写入
            string dataDirectory = Path.Combine(Application.StartupPath, "Data");
            Directory.CreateDirectory(dataDirectory);
            string mergedFilePath = Path.Combine(dataDirectory, "merged.txt");
            using (StreamWriter writer = new StreamWriter(mergedFilePath))
            {
                writer.Write(file1Content + file2Content);
            }

            MessageBox.Show("文件合并完成！");
        }
    }
}
