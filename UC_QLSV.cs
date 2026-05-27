using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace thuchanhdangnhap
{
    public partial class UC_QLSV : UserControl
    {
        databaseDataContext db = new databaseDataContext(
    @"Data Source=DESKTOP-PKD82K6;Initial Catalog=QLSV;User ID=sa;Password=123456;TrustServerCertificate=True"
);


        public UC_QLSV()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UC_QLSV_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = db.tbl_sinhviens.ToList();
                comboBox1.Items.Add("Nam");
                comboBox1.Items.Add("Nữ");
                   comboBox2.Items.Add("68PM1");
                comboBox2.Items.Add("68PM2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                tbl_sinhvien sv = new tbl_sinhvien();

                sv.id = txtMaSV.Text;                 
                sv.hoten = txtHoTen.Text;                
                sv.gioitinh = comboBox1.Text;             
                sv.ngaysinh = dateTimePicker1.Value;   
                sv.malop = comboBox2.Text;             

                db.tbl_sinhviens.InsertOnSubmit(sv);

            
                db.SubmitChanges();

              
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = db.tbl_sinhviens.ToList();

                MessageBox.Show("Thêm sinh viên thành công");

               
                txtMaSV.Clear();
                txtHoTen.Clear();
                comboBox2.SelectedIndex = -1;

                comboBox1.SelectedIndex = -1;

                txtMaSV.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

