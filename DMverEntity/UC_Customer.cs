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
    public partial class UC_Customer : UserControl
    {
        connectDBEntity mod = new connectDBEntity();
        public UC_Customer()
        {
            InitializeComponent();
        }
        private void load()
        {
            dgvCustomerinfo.Rows.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            List<KHACHHANG> kHACHHANGs= mod1.KHACHHANG.ToList();
            foreach(var item in kHACHHANGs)
            {
                int index = dgvCustomerinfo.Rows.Add();
                dgvCustomerinfo.Rows[index].Cells[0].Value = item.MaKhachHang;
                dgvCustomerinfo.Rows[index].Cells[1].Value = item.HoKhachHang;
                dgvCustomerinfo.Rows[index].Cells[2].Value = item.TenKhachHang;
                dgvCustomerinfo.Rows[index].Cells[3].Value = item.NgaySinh;
                dgvCustomerinfo.Rows[index].Cells[4].Value = item.GioiTinh;
                dgvCustomerinfo.Rows[index].Cells[5].Value = item.CMND;
                dgvCustomerinfo.Rows[index].Cells[6].Value = item.SoDienThoai;
                dgvCustomerinfo.Rows[index].Cells[7].Value = item.ThuDienTu;
                dgvCustomerinfo.Rows[index].Cells[8].Value = item.DiaChi;
                dgvCustomerinfo.Rows[index].Cells[9].Value = item.MaPhong;
            }
            bsiRecordsCount.Caption = "Số Khách Hàng: " + dgvCustomerinfo.Rows.Count;
        }

        private void UC_Customer_Load(object sender, EventArgs e)
        {
            load();
        }
        private void setnull()
        {
            txtCusID.Text = "";
            txtCusName.Text = "";
            txtID.Text = "";
            txtMail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        private void dgvCustomerinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvCustomerinfo.CurrentRow.Index;
                txtCusID.Text = dgvCustomerinfo.Rows[index].Cells[0].Value.ToString();
                txtCusName.Text = dgvCustomerinfo.Rows[index].Cells[1].Value.ToString() + " " + dgvCustomerinfo.Rows[index].Cells[2].Value.ToString();
                dtpBirth.Text = dgvCustomerinfo.Rows[index].Cells[3].Value.ToString();
                string Sex = dgvCustomerinfo.Rows[index].Cells[4].Value.ToString();
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
                txtID.Text = dgvCustomerinfo.Rows[index].Cells[5].Value.ToString();
                txtPhone.Text = dgvCustomerinfo.Rows[index].Cells[6].Value.ToString();
            if (txtMail.Text != "")
                txtMail.Text = dgvCustomerinfo.Rows[index].Cells[7].Value.ToString();
            else
                txtMail.Text = "";
                txtAddress.Text = dgvCustomerinfo.Rows[index].Cells[8].Value.ToString();
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addCustomer add = new addCustomer();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                setnull();
                dgvCustomerinfo.DataSource = null;
                load();
            }
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            editCustomer edit = new editCustomer(txtCusID.Text);

            if (txtCusID.Text != "")
            {
                edit.ShowDialog();
                if (edit.IsDisposed == false)
                {
                    setnull();
                    dgvCustomerinfo.DataSource = null;
                    load();
                }
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtCusID.Text != "")
            {
                if (MessageBox.Show("Bạn muốn xoá khách hàng này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var Customer = mod.KHACHHANG.FirstOrDefault(p => p.MaKhachHang == txtCusID.Text);
                        mod.KHACHHANG.Remove(Customer);
                        mod.SaveChanges();
                        setnull();
                        dgvCustomerinfo.DataSource = null;
                        load();
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Khác hàng này đang có hợp đồng không thể xoá" +
                            "\n"+"\tHãy xoá hợp đồng trước!","Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvCustomerinfo.Rows.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            List<KHACHHANG> kHACHHANGs = mod1.KHACHHANG.Where(a => a.TenKhachHang == txtSearch.Text).ToList();
            foreach (var item in kHACHHANGs)
            {
                int index = dgvCustomerinfo.Rows.Add();
                dgvCustomerinfo.Rows[index].Cells[0].Value = item.MaKhachHang;
                dgvCustomerinfo.Rows[index].Cells[1].Value = item.HoKhachHang;
                dgvCustomerinfo.Rows[index].Cells[2].Value = item.TenKhachHang;
                dgvCustomerinfo.Rows[index].Cells[3].Value = item.NgaySinh;
                dgvCustomerinfo.Rows[index].Cells[4].Value = item.GioiTinh;
                dgvCustomerinfo.Rows[index].Cells[5].Value = item.CMND;
                dgvCustomerinfo.Rows[index].Cells[6].Value = item.SoDienThoai;
                dgvCustomerinfo.Rows[index].Cells[7].Value = item.ThuDienTu;
                dgvCustomerinfo.Rows[index].Cells[8].Value = item.DiaChi;
                dgvCustomerinfo.Rows[index].Cells[9].Value = item.MaPhong;
            }
            bsiRecordsCount.Caption = "Số Khách Hàng: " + dgvCustomerinfo.Rows.Count;
        }

        private void bbiPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reportCustomer a = new reportCustomer();
            a.ShowPreviewDialog();
        }
    }
}
