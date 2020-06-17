using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here is some tips for using the OS Imitation system.");
            MessageBox.Show("The left panel includes some conditons for u to set the properties OF the process.");
            MessageBox.Show("The right side will print the information of the process.");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            //将在富文本框中的显示出来的记录都打印到一个文件中
            //richTextBox1.SaveFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + @"\docOfProcess.txt", RichTextBoxStreamType.PlainText);

            //richTextBox1.Text = ;//在这里显示进程开始后的各类消息
        }
    }
}
