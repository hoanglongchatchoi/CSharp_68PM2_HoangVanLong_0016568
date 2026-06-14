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
    public partial class UC_QLLH : UserControl
    {
     databaseDataContext db = new databaseDataContext(
    @"Data Source=DESKTOP-PKD82K6;Initial Catalog=QLSV;User ID=sa;Password=123456;TrustServerCertificate=True"
);
        public UC_QLLH()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void LoadDataLopHoc()
        {
            try
            {
                dataGridView1.DataSource = null;

                // Lấy toàn bộ danh sách lớp học (Bạn kiểm tra xem tên bảng trong LINQ là tbl_lophocs hay tbl_lophoc nhé)
                dataGridView1.DataSource = db.tbl_lophocs.ToList();

                // Làm đẹp lưới, khử vùng xám thừa
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.BackgroundColor = Color.White;

                // Ẩn cột danh sách sinh viên liên kết thừa nếu LINQ tự sinh ra
         
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp học: " + ex.Message);
            }
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];


                tbl_lophoc lh = (tbl_lophoc)row.DataBoundItem;

                if (lh != null)
                {
                    txtMaLop.Text = lh.malop;
                    txtTenLop.Text = lh.tenlop;
                    txtGhiChu.Text = lh.ghichu;
                   
                    txtMaLop.ReadOnly = true;
                }
            }


        }

        private void UC_QLLH_Load_1(object sender, EventArgs e)
        {
        
            try
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AutoGenerateColumns = true;

                // --- SỬA LỖI TẠI ĐÂY: Gọi hàm tải dữ liệu lên lưới ---
                LoadDataLopHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                tbl_lophoc lh = new tbl_lophoc();
                lh.malop = txtMaLop.Text;
                lh.tenlop = txtTenLop.Text;
                lh.ghichu = txtGhiChu.Text;
               

                db.tbl_lophocs.InsertOnSubmit(lh);


                db.SubmitChanges();


                dataGridView1.DataSource = null;
                dataGridView1.DataSource = db.tbl_lophocs.ToList();

                MessageBox.Show("Thêm lop hoc thanh cong");


                txtMaLop.Clear();
                txtTenLop.Clear();
                txtGhiChu.Clear();
                txtMaLop.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearGroupBoxInputs()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtGhiChu.Clear();
            txtMaLop.ReadOnly = false;
            txtMaLop.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaLop.Text))
                {
                    MessageBox.Show("Vui long cho 1 lop tu danh sach");
                    return;
                }

                var lh = db.tbl_lophocs.SingleOrDefault(l => l.malop == txtMaLop.Text);
                if (lh != null)
                {
                    lh.tenlop = txtMaLop.Text;
                    lh.ghichu = txtGhiChu.Text;
                 
                    db.SubmitChanges();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = db.tbl_lophocs.ToList();
                    MessageBox.Show(" Cap nhap thong tin lop hoc thanh cong");
                    ClearGroupBoxInputs();
                }
                else
                {
                    MessageBox.Show("Khong tim thay lop hoc trong he thong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi cap nhap:" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaLop.Text))
                {
                    MessageBox.Show("Vui lòng chọn một lớp học từ danh sách để xóa!");
                    return;
                }
                string maLopCanXoa = txtMaLop.Text.Trim();
                if(maLopCanXoa.ToUpper() == "LOPTUDO")
                {
                    MessageBox.Show("Day la lop danh cho sinh vien tu do", "canh bao"
                        , MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                DialogResult dr = MessageBox.Show(
                    "Bạn có chắc muốn xóa lớp học này? Tất cả sinh viên thuộc lớp này sẽ bị hủy liên kết lớp!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                   
                    var danhSachSV = db.tbl_sinhviens.Where(sv => sv.malop == maLopCanXoa).ToList();
                    foreach (var sv in danhSachSV)
                    {
                        sv.malop = "LOPTUDO";
                    }

            
                    var lh = db.tbl_lophocs.SingleOrDefault(l => l.malop == maLopCanXoa);
                    if (lh != null)
                    {
                      
                        db.tbl_lophocs.DeleteOnSubmit(lh);

                        db.SubmitChanges();

                        LoadDataLopHoc();
                        MessageBox.Show("Xóa lớp học và cập nhật lại trạng thái sinh viên thành công!");
                        ClearGroupBoxInputs();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp học: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
      
         
            if (string.IsNullOrEmpty(txtMaLop.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn một lớp học từ danh sách hoặc nhập Mã lớp để xem!");
                return;
            }

            string maLopCanXem = txtMaLop.Text.Trim();

   
            mainform fChinh = (mainform)this.ParentForm;

            if (fChinh != null)
            {
          
                fChinh.ChuyenSangTabSinhVienVaLoc(maLopCanXem);
            }
        }
    }
    }
    
    

