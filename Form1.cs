using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thuchanhdangnhap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click (object sender, EventArgs e)
        {
            string taikhoan = "0016568@huce.edu.vn";
            string matkhau = "0016568";
            if (textBox1.Text == taikhoan && textBox2.Text == matkhau)
            {
                MessageBox.Show("Đăng nhập thành công");
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
            
        }
    }
}
    