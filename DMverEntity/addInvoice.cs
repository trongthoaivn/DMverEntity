using DMverEntity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMverEntity
{
    public partial class addInvoice : Form
    {
        public addInvoice()
        {
            InitializeComponent();
        }
        public class Comboboxitem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private void loadRoom()
        {
            connectDBEntity mod = new connectDBEntity();
            var pHONGTROs = mod.PHONGTRO.Select(a => new { a.MaPhong, a.TenPhong }).ToList();
            foreach (var item in pHONGTROs)
            {
                Comboboxitem it = new Comboboxitem();
                it.Text = item.TenPhong;
                it.Value = item.MaPhong;
                cboRoom.Items.Add(it);
            }
        }
        private void loadEW()
        {
            connectDBEntity mod = new connectDBEntity();
            var dIENNUOCs = mod.DIENNUOC.Select(a => new { a.MaDienNuoc, a.MaPhong, a.ThoiGian, a.SoDienCu, a.SoDienMoi, a.SoNuocCu, a.SoNuocMoi, a.TienDienNuoc }).ToList();
            dgvEW.DataSource = dIENNUOCs;
        }
        private void loadStaff()
        {
            connectDBEntity mod = new connectDBEntity();
            var nHANVIENs = mod.NHANVIEN.Select(a => new { a.HoNhanVien, a.TenNhanVien, a.MaNhanVien }).ToList();
            cboStaff.DataSource = nHANVIENs;
            cboStaff.ValueMember = "MaNhanVien";
        }
        private void loadService(int id)
        {
            if (id != 6 && id != 7)
            {
                connectDBEntity mod = new connectDBEntity();
                var Service = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == id);
                ListViewItem item = lsvService.Items.Add(Service.TenDichVu);
                item.SubItems.Add(Service.DonGia.ToString());
                item.SubItems.Add(Service.DonViTinh);
            }
        }
        private void addInvoice_Load(object sender, EventArgs e)
        {
            loadStaff();
            loadRoom();
            loadEW();
            txtTotal.Text = "0";
        }

        private void cboRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

            connectDBEntity mod = new connectDBEntity();
            string id = (cboRoom.SelectedItem as Comboboxitem).Value.ToString();
            var Tenancy = mod.HOPDONG.Select(a => new { a.MaHopDong, a.MaPhong, a.MaDichVu }).FirstOrDefault(a => a.MaPhong == id);
            var EW = mod.DIENNUOC.Select(a => new { a.MaDienNuoc, a.MaPhong, a.ThoiGian, a.SoDienCu, a.SoDienMoi, a.SoNuocCu, a.SoNuocMoi, a.TienDienNuoc }).Where(a => a.MaPhong == id).ToList();
            dgvEW.DataSource = EW;
            lsvService.Items.Clear();
            double sum = 0;
            if (Tenancy != null)
            {
                string[] S = Tenancy.MaDichVu.Split(',');
                for (int i = 0; i < S.Length - 1; i++)
                {
                    loadService(int.Parse(S[i]));
                }
                foreach (ListViewItem item in lsvService.Items)
                {
                    sum += double.Parse(item.SubItems[1].Text);
                }
            }
            txtTotal.Text = sum.ToString();
        }

        private void dgvEW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool flag = true;
            foreach (ListViewItem it in lsvService.Items)
            {
                if (it.SubItems[0].Text == "Tiền Điện Nước" )
                {
                    flag = false;
                    break;
                } 
            }
            if(flag == true)
            {
                int index = dgvEW.CurrentRow.Index;
                string idEW = dgvEW.Rows[index].Cells[0].Value.ToString();
                connectDBEntity mod = new connectDBEntity();
                var EW = mod.DIENNUOC.Select(a => new { a.MaDienNuoc, a.TienDienNuoc }).FirstOrDefault(a => a.MaDienNuoc == idEW);
                ListViewItem item = lsvService.Items.Add("Tiền Điện Nước");
                item.SubItems.Add(EW.TienDienNuoc.Value.ToString());
                item.SubItems.Add("VNĐ");
                double sum = double.Parse(txtTotal.Text);
                if (sum != 0)
                {
                    sum += double.Parse(item.SubItems[1].Text);
                }
                txtTotal.Text = sum.ToString();
            } 
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cboRoom.Items.Clear();
            loadRoom();
            loadEW();
            txtTotal.Text = "0";
        }
        private string getID()
        {
            string result;
            result = "Hd" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
            return result;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string idInvoice = getID();
            connectDBEntity mod = new connectDBEntity();
            string id = (cboRoom.SelectedItem as Comboboxitem).Value.ToString();
            var Tenancy = mod.HOPDONG.Select(a => new { a.MaHopDong,a.MaKhachHang, a.MaPhong }).FirstOrDefault(a => a.MaPhong == id);
            if(lsvService.Items.Count > 0)
            {
                if (MessageBox.Show("Bạn muốn lưu hoá đơn này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int index = dgvEW.CurrentRow.Index;
                    connectDBEntity HD = new connectDBEntity();
                    HOADON hOADON = new HOADON
                    {
                        MaHoaDon = idInvoice,
                        MaNhanVien = cboStaff.SelectedValue.ToString(),
                        MaPhong = (cboRoom.SelectedItem as Comboboxitem).Value.ToString(),
                        MaDienNuoc = dgvEW.Rows[index].Cells[0].Value.ToString()
                    };
                    HD.HOADON.Add(hOADON);
                    HD.SaveChanges();
                    CHITIETHOADON cHITIETHOADON = new CHITIETHOADON
                    {
                        MaHoaDon = idInvoice,
                        MaKhachHang = Tenancy.MaKhachHang,
                        NgayLap = DateTime.Now,
                        TongCong = float.Parse(txtTotal.Text),
                        TrangThai = false,
                    };
                    HD.CHITIETHOADON.Add(cHITIETHOADON);
                    HD.SaveChanges();
                    MessageBox.Show("Lưu thành công!");
                    Close();
                }
            }
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dgvEW.Rows)
            {
                if (cboMonth.Text == (row.Cells[2].Value.ToString()).Substring(3,2))
                {
                    row.DefaultCellStyle.BackColor = Color.Blue;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}
