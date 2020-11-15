using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMverEntity.Entity;
using DevExpress.XtraReports.UI;

namespace DMverEntity
{
    public partial class UC_Staff : UserControl
    {
        connectDBEntity mod = new connectDBEntity();
        public UC_Staff()
        {
            InitializeComponent();
        }
        private void load()
        {
            dgvStaffinfo.Rows.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            List<NHANVIEN>nHANVIENs=mod1.NHANVIEN.ToList();
            foreach(var item in nHANVIENs)
            {
                int index = dgvStaffinfo.Rows.Add();
                dgvStaffinfo.Rows[index].Cells[0].Value = item.MaNhanVien;
                dgvStaffinfo.Rows[index].Cells[1].Value = item.HoNhanVien;
                dgvStaffinfo.Rows[index].Cells[2].Value = item.TenNhanVien;
                dgvStaffinfo.Rows[index].Cells[3].Value = item.NgaySinh;
                dgvStaffinfo.Rows[index].Cells[4].Value = item.GioiTinh;
                dgvStaffinfo.Rows[index].Cells[5].Value = item.CMND;
                dgvStaffinfo.Rows[index].Cells[6].Value = item.SoDienThoai;
                dgvStaffinfo.Rows[index].Cells[7].Value = item.ThuDienTu;
                dgvStaffinfo.Rows[index].Cells[8].Value = item.DiaChi;      
            }
            bsiRecordsCount.Caption = "Số Nhân Viên: " + dgvStaffinfo.Rows.Count;
        }

        private void UC_Staff_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dgvStaffinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvStaffinfo.CurrentRow.Index;
            for (int i = 0; i < dgvStaffinfo.Rows.Count; i++)
            {
                txtStaffID.Text = dgvStaffinfo.Rows[index].Cells[0].Value.ToString();
                txtStaffName.Text = dgvStaffinfo.Rows[index].Cells[1].Value.ToString() + " " + dgvStaffinfo.Rows[index].Cells[2].Value.ToString();
                dtpBirth.Text = dgvStaffinfo.Rows[index].Cells[3].Value.ToString();
                string Sex = dgvStaffinfo.Rows[index].Cells[4].Value.ToString();
                if (Sex == "Nam")
                {
                    robMale.Checked = true;
                }
                if (Sex == "Nữ")
                {
                    robFemale.Checked = true;
                }
                if (Sex == "Khác")
                {
                    robOther.Checked = true;
                }
                txtID.Text = dgvStaffinfo.Rows[index].Cells[5].Value.ToString();
                txtPhone.Text = dgvStaffinfo.Rows[index].Cells[6].Value.ToString();
                txtMail.Text = dgvStaffinfo.Rows[index].Cells[7].Value.ToString();
                txtAddress.Text = dgvStaffinfo.Rows[index].Cells[8].Value.ToString();
            }
        }
        private void setnull()
        {
            txtStaffID.Text = "";
            txtStaffName.Text = "";
            txtID.Text = "";
            txtMail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addStaff add = new addStaff();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                setnull();
                dgvStaffinfo.DataSource = null;
                load();
            }
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            editStaff edit = new editStaff(txtStaffID.Text);

            if (txtStaffID.Text != "")
            {
                edit.ShowDialog();
                if (edit.IsDisposed == false)
                {
                    setnull();
                    dgvStaffinfo.Rows.Clear();
                    load();
                }
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtStaffID.Text != "")
            {
                if (MessageBox.Show("Bạn muốn xoá nhân viên này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var Customer = mod.NHANVIEN.FirstOrDefault(p => p.MaNhanVien == txtStaffID.Text);
                        mod.NHANVIEN.Remove(Customer);
                        mod.SaveChanges();
                        setnull();
                        dgvStaffinfo.Rows.Clear();
                        load();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nhân viên này đang quản lý hợp đồng không thể xoá" +
                            "\n" + "\tHãy xoá hợp đồng trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
            setnull();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvStaffinfo.Rows.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            List<NHANVIEN> nHANVIENs = mod1.NHANVIEN.Where(a => a.TenNhanVien ==txtSearch.Text).ToList();
            foreach (var item in nHANVIENs)
            {
                int index = dgvStaffinfo.Rows.Add();
                dgvStaffinfo.Rows[index].Cells[0].Value = item.MaNhanVien;
                dgvStaffinfo.Rows[index].Cells[1].Value = item.HoNhanVien;
                dgvStaffinfo.Rows[index].Cells[2].Value = item.TenNhanVien;
                dgvStaffinfo.Rows[index].Cells[3].Value = item.NgaySinh;
                dgvStaffinfo.Rows[index].Cells[4].Value = item.GioiTinh;
                dgvStaffinfo.Rows[index].Cells[5].Value = item.CMND;
                dgvStaffinfo.Rows[index].Cells[6].Value = item.SoDienThoai;
                dgvStaffinfo.Rows[index].Cells[7].Value = item.ThuDienTu;
                dgvStaffinfo.Rows[index].Cells[8].Value = item.DiaChi;
            }
            bsiRecordsCount.Caption = "Số Nhân Viên: " + dgvStaffinfo.Rows.Count;
        }

        private void bbiPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reportStaff a = new reportStaff();
            a.ShowPreviewDialog();
        }
    }
}
