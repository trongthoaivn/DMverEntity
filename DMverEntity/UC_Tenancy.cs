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
    public partial class UC_Tenancy : UserControl
    {
        connectDBEntity mod = new connectDBEntity();
        public UC_Tenancy()
        {
            InitializeComponent();
            load();
        }
        private void load()
        {
            lsvService.Items.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            List<HOPDONG> hOPDONGs = mod1.HOPDONG.ToList();
            foreach( var item in hOPDONGs)
            {
                int index = dgvTenacylist.Rows.Add();
                dgvTenacylist.Rows[index].Cells[0].Value = item.MaHopDong;
                dgvTenacylist.Rows[index].Cells[1].Value = item.MaPhong;
                dgvTenacylist.Rows[index].Cells[3].Value = item.MaNhanVien;
                dgvTenacylist.Rows[index].Cells[2].Value = item.MaKhachHang;
            }
            bsiRecordsCount.Caption = "Số Hợp Đồng:" + dgvTenacylist.Rows.Count;
        }

        private void UC_Tenancy_Load(object sender, EventArgs e)
        {
            load();
        }
        private void Show(string id)
        {
            lsvService.Items.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            var hITIETHOPDONGs = mod1.CHITIETHOPDONG.FirstOrDefault(a => a.MaHopDong == id);
            var hopdong = mod1.HOPDONG.FirstOrDefault(a => a.MaHopDong == id);
            var staff = mod1.NHANVIEN.FirstOrDefault(a => a.MaNhanVien == hopdong.MaNhanVien);
            var customer = mod.KHACHHANG.FirstOrDefault(a => a.MaKhachHang == hopdong.MaKhachHang);
                txtTenacyID.Text = hITIETHOPDONGs.MaHopDong;
                txtDate.Text = hITIETHOPDONGs.NgayLapHopDong.ToString();
                txtCustomerName.Text = customer.HoKhachHang + " " + customer.TenKhachHang;
                txtIDCus.Text =customer.CMND;
                txtAddrCus.Text = customer.DiaChi;
                txtStaffName.Text = staff.HoNhanVien + " " + staff.TenNhanVien;
                txtIDStaff.Text =staff.CMND;
                txtAddrStaff.Text =staff.DiaChi;
                txtRoomname.Text = hITIETHOPDONGs.TenPhong;
                string[] n = hITIETHOPDONGs.TenDichVu.Split(',');
                string[] p = hITIETHOPDONGs.GiaDichVu.Split(',');
                string[] u = hITIETHOPDONGs.DonViTinh.Split(',');
                for (int i = 0; i < n.Length - 1; i++)
                {
                    ListViewItem item = lsvService.Items.Add(n[i]);
                    item.SubItems.Add(p[i].Trim());
                    item.SubItems.Add(u[i]);
                }
                txtDeposits.Text = hITIETHOPDONGs.TienDatCoc + " VNĐ";
                txtStart.Text = hITIETHOPDONGs.NgayBatDau.ToString();
                txtEnd.Text = hITIETHOPDONGs.NgayKetThuc.ToString();
        }
        private void dgvTenacylist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvTenacylist.CurrentRow.Index;
            string id =dgvTenacylist.Rows[index].Cells[0].Value.ToString();
            Show(id);
        }
        private void setnull()
        {
            txtIDStaff.Text = null;
            txtIDCus.Text = null;
            txtCustomerName.Text = null;
            txtStaffName.Text = null;
            txtTenacyID.Text = null;
            txtDeposits.Text = null;
            txtDate.Text = null;
            txtStart.Text = null;
            txtEnd.Text = null;
            txtAddrStaff.Text = null;
            txtAddrCus.Text = null;
            lsvService.Items.Clear();
            txtRoomname.Text = null;
        }
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addTenancy add = new addTenancy();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                setnull();
                dgvTenacylist.Rows.Clear();
                load();
            }
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            editTenancy edit = new editTenancy(txtTenacyID.Text);
            if (txtTenacyID.Text != "")
            {
                edit.ShowDialog();
                if (edit.IsDisposed == false)
                {
                    setnull();
                    dgvTenacylist.Rows.Clear();
                    load();
                }
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTenacyID.Text != "")
            {
                if (MessageBox.Show("Bạn muốn xoá hợp đồng này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var Tenancyinfo = mod.CHITIETHOPDONG.FirstOrDefault(a => a.MaHopDong == txtTenacyID.Text);
                    var Tenancy = mod.HOPDONG.FirstOrDefault(a => a.MaHopDong == txtTenacyID.Text);
                    var Room = mod.PHONGTRO.FirstOrDefault(a => a.MaPhong == Tenancy.MaPhong);
                    Room.MaTrangThai = 1;
                    mod.CHITIETHOPDONG.Remove(Tenancyinfo);
                    mod.HOPDONG.Remove(Tenancy);
                    mod.SaveChanges();
                    setnull();
                    dgvTenacylist.Rows.Clear();
                    load();
                    
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvTenacylist.Rows.Clear();
            connectDBEntity mod1 = new connectDBEntity();
            List<HOPDONG> hOPDONGs = mod1.HOPDONG.Where(a => a.MaHopDong== txtSearch.Text).ToList();
            foreach (var item in hOPDONGs)
            {
                int index = dgvTenacylist.Rows.Add();
                dgvTenacylist.Rows[index].Cells[0].Value = item.MaHopDong;
                dgvTenacylist.Rows[index].Cells[1].Value = item.MaPhong;
                dgvTenacylist.Rows[index].Cells[2].Value = item.MaNhanVien;
                dgvTenacylist.Rows[index].Cells[3].Value = item.MaKhachHang;
            }
            bsiRecordsCount.Caption = "Số Hợp Đồng:" + dgvTenacylist.Rows.Count;
        }

        private void bbiPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtTenacyID.Text != "")
            {
                reportTenancy reportTenancy = new reportTenancy(txtTenacyID.Text);
                reportTenancy.ShowPreviewDialog();
            }
            
        }
    }
}
