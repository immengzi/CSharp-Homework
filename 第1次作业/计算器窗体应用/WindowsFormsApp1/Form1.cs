using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class 简易计算器 : Form
    {
        public 简易计算器()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object op = comboBox1.SelectedItem;
            double num1 = double.Parse(textBox1.Text);
            double num2 = double.Parse(textBox2.Text);
            switch (op.ToString())
            {
                case "+":
                    textBox3.Text = (num1 + num2).ToString(); break;
                case "-":
                    textBox3.Text = (num1 - num2).ToString(); break;
                case "*":
                    textBox3.Text = (num1 * num2).ToString(); break;
                case "/":
                    textBox3.Text = (num1 / num2).ToString(); break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            comboBox1.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
