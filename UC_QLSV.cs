using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.RowIndex >= 0)
            {
           
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

         
                tbl_sinhvien sv = (tbl_sinhvien)row.DataBoundItem;

                if (sv != null)
                {
                   
                    txtMaSV.Text = sv.id;
                    txtHoTen.Text = sv.hoten;
                    cbbgioitinh.Text = sv.gioitinh;
                    cbblop.Text = sv.malop;
                    dateTimePicker1.Value = sv.ngaysinh;

                  
                    txtMaSV.ReadOnly = true;
                }
            }
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UC_QLSV_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoGenerateColumns = true;
               LoadDataWithPaging();
                cbbgioitinh.Items.Add("Nam");
                cbbgioitinh.Items.Add("Nữ");
                   cbblop.Items.Add("68PM1");
                cbblop.Items.Add("68PM2");

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
                sv.gioitinh = cbbgioitinh.Text;             
                sv.ngaysinh = dateTimePicker1.Value;   
                sv.malop = cbblop.Text;             

                db.tbl_sinhviens.InsertOnSubmit(sv);

            
                db.SubmitChanges();

              
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = db.tbl_sinhviens.ToList();

                MessageBox.Show("Thêm sinh viên thành công");

               
                txtMaSV.Clear();
                txtHoTen.Clear();
                cbblop.SelectedIndex = -1;

                cbbgioitinh.SelectedIndex = -1;

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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSV.Text))
                {
                    MessageBox.Show("Vui long cho 1 sinh vien tu danh sach");
                    return;
                }

                var sv = db.tbl_sinhviens.SingleOrDefault(s => s.id == txtMaSV.Text);
                if (sv != null)
                {
                    sv.hoten = txtHoTen.Text;
                    sv.gioitinh = cbbgioitinh.Text;
                    sv.ngaysinh = dateTimePicker1.Value;
                    sv.malop = cbblop.Text;
                    db.SubmitChanges();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.tbl_sinhviens.ToList();
                    MessageBox.Show(" Cap nhap thong tin sinh vien thanh cong");
                    ClearGroupBoxInputs();
                }
                else
                {
                    MessageBox.Show("Khong tim thay sinh vien trong he thong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi cap nhap:" + ex.Message);
            } 
        }
                    private void ClearGroupBoxInputs()
                {
                    txtMaSV.Clear();
                    txtHoTen.Clear();
                    cbbgioitinh.SelectedIndex = -1;
                    cbblop.SelectedIndex = -1;
                    dateTimePicker1.Value = DateTime.Now;

                    txtMaSV.ReadOnly = false; // Mở khóa lại ô Mã SV
                    txtMaSV.Focus();
                }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSV.Text))
                {
                    MessageBox.Show("Vui long chon 1 sinh vien tu danh sach");
                    return;
                }
                DialogResult dr = MessageBox.Show(
                    "Ban co chac muon xoa sinh vien nay khong",
                    "Xac nhan xoa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes)
                {
                    var sv = db.tbl_sinhviens.SingleOrDefault(s => s.id == txtMaSV.Text);
                    if (sv != null)
                    {
                        db.tbl_sinhviens.DeleteOnSubmit(sv);
                        db.SubmitChanges();
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = db.tbl_sinhviens.ToList();
                        MessageBox.Show("Xoa sinh vien thanh cong");
                        ClearGroupBoxInputs();
                    }
                    else
                    {
                        MessageBox.Show("Khong tim thay sinh vien");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi khi xoa:" + ex.Message);
            }
        }

        int pageNumber = 1;
        int pageSize = 10;
        private void LoadDataWithPaging()
        {
            try
            {
                string keyword = textBox5.Text.Trim();
                var query = db.tbl_sinhviens.Where(s => s.hoten.Contains(keyword));
                int totalRecords = query.Count();
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                if (totalPages == 0) totalPages = 1; 

               
                if (pageNumber > totalPages) pageNumber = totalPages;
                if (pageNumber < 1) pageNumber = 1;

                var listPage = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

         
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listPage;
                int currentRecoeds = listPage.Count;
                lblPhantrang.Text = String.Format("Trang{0}/{1} | {2} ban ghi", pageNumber, totalPages, currentRecoeds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai du lieu:" + ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            pageNumber = 1;
            LoadDataWithPaging();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
                pageNumber++;
                LoadDataWithPaging();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(pageNumber > 1) 
            { 
                pageNumber--;
                LoadDataWithPaging() ;
            }
        }
    }
}






