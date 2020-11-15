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
    public partial class editTenancy : Form
    {
        connectDBEntity mod = new connectDBEntity();
        string ID;
        List<DICHVU> DICHVUs;
        public editTenancy(string id)
        {
            InitializeComponent();
            ID = id;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvCustomer.CurrentRow.Index;
            txtIDofCus.Text = dgvCustomer.Rows[index].Cells[0].Value.ToString();
            txtCustomerName.Text = dgvCustomer.Rows[index].Cells[1].Value.ToString() + " " + dgvCustomer.Rows[index].Cells[2].Value.ToString();
            txtCusFName.Text = dgvCustomer.Rows[index].Cells[1].Value.ToString();
            txtCusLName.Text = dgvCustomer.Rows[index].Cells[2].Value.ToString();
            txtIDCus.Text = dgvCustomer.Rows[index].Cells[5].Value.ToString();
            txtAddrCus.Text = dgvCustomer.Rows[index].Cells[8].Value.ToString();
        }

        private void editTenancy_Load(object sender, EventArgs e)
        {
            loadRoom();
            loadService();
            loadCustomer();
            loadStaff();
            loadTenancy();
            txtTenacyID.Text = ID;
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvStaff.CurrentRow.Index;
            txtIDofStaff.Text = dgvStaff.Rows[index].Cells[0].Value.ToString();
            txtStaffName.Text = dgvStaff.Rows[index].Cells[1].Value.ToString() + " " + dgvStaff.Rows[index].Cells[2].Value.ToString();
            txtStaFName.Text = dgvStaff.Rows[index].Cells[1].Value.ToString();
            txtStaLName.Text = dgvStaff.Rows[index].Cells[2].Value.ToString();
            txtIDStaff.Text = dgvStaff.Rows[index].Cells[5].Value.ToString();
            txtAddrStaff.Text = dgvStaff.Rows[index].Cells[8].Value.ToString();
        }
        private void loadRoom()
        {
            connectDBEntity mod1 = new connectDBEntity();
            var Tenancy = mod.HOPDONG.FirstOrDefault(a => a.MaHopDong == ID);
            List<PHONGTRO> pHONGTROs = mod1.PHONGTRO.Where(a => a.MaTrangThai != 4 || a.MaPhong == Tenancy.MaPhong).ToList();
            cboRoomName.DataSource = pHONGTROs;
            cboRoomName.DisplayMember = "TenPhong";
            cboRoomName.ValueMember = "MaPhong";
        }
        private void loadService()
        {
            connectDBEntity mod1 = new connectDBEntity();
            List<DICHVU> dICHVUs = mod1.DICHVU.ToList();
            cboService.DataSource = dICHVUs;
            cboService.ValueMember = "MaDichVu";
            cboService.DisplayMember = "TenDichVu";
            DICHVUs = dICHVUs;
        }
        private void loadCustomer()
        {
            connectDBEntity mod1 = new connectDBEntity();
            List<KHACHHANG> kHACHHANGs = mod1.KHACHHANG.ToList();
            dgvCustomer.DataSource = kHACHHANGs;
        }
        private void loadStaff()
        {
            connectDBEntity mod1 = new connectDBEntity();
            List<NHANVIEN> nHANVIENs = mod1.NHANVIEN.ToList();
            dgvStaff.DataSource = nHANVIENs;
        }
        private void loadTenancy()
        {
            var Tenancy = mod.HOPDONG.FirstOrDefault(a => a.MaHopDong == ID);
            var Tenancyinfo= mod.CHITIETHOPDONG.FirstOrDefault(a => a.MaHopDong == ID);
            foreach (DataGridViewRow row in dgvStaff.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(Tenancy.MaNhanVien))
                {
                    row.Selected = true;
                }
            }
            foreach (DataGridViewRow row in dgvCustomer.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(Tenancy.MaKhachHang))
                {
                    row.Selected = true;
                }
            }
            var staff = mod.NHANVIEN.FirstOrDefault(a => a.MaNhanVien == Tenancy.MaNhanVien);
            var customer = mod.KHACHHANG.FirstOrDefault(a => a.MaKhachHang == Tenancy.MaKhachHang);
            cboRoomName.SelectedValue = Tenancy.MaPhong;
            dtpDate.Value = Tenancyinfo.NgayLapHopDong.Value;
            dtpStart.Value = Tenancyinfo.NgayBatDau.Value;
            dtpEnd.Value = Tenancyinfo.NgayKetThuc.Value;
            txtDeposits.Text = Tenancyinfo.TienDatCoc.ToString();
            txtIDofStaff.Text = staff.MaNhanVien;
            txtStaFName.Text = staff.HoNhanVien;
            txtStaLName.Text = staff.TenNhanVien;
            txtStaffName.Text = staff.HoNhanVien + " " + staff.TenNhanVien;
            txtIDStaff.Text = staff.CMND;
            txtAddrStaff.Text = staff.DiaChi;
            txtIDofCus.Text = customer.MaKhachHang;
            txtCusFName.Text = customer.HoKhachHang;
            txtCusLName.Text = customer.TenKhachHang;
            txtCustomerName.Text = customer.HoKhachHang + " " + customer.TenKhachHang;
            txtIDCus.Text = customer.CMND;
            txtAddrCus.Text = customer.DiaChi;
            string[] c = Tenancy.MaDichVu.Split(',');
            string[] n = Tenancyinfo.TenDichVu.Split(',');
            string[] p = Tenancyinfo.GiaDichVu.Split(',');
            string[] u = Tenancyinfo.DonViTinh.Split(',');
            for (int i = 0; i < c.Length - 1; i++)
            {
                ListViewItem item = lsvService.Items.Add(c[i]);
                item.SubItems.Add(n[i]);
                item.SubItems.Add(p[i].Trim());
                item.SubItems.Add(u[i]);
            }
        }
        private bool check()
        {
            bool result = true;
            if (txtDeposits.Text == "")
            {
                txtDeposits.Text = "0";
            }
            if (txtIDofStaff.Text == "")
            {
                MessageBox.Show("Vui Lòng chọn nhân viên!");
                result = false;
            }
            if (txtIDofCus.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                result = false;
            }

            return result;
        }
        private string getIDservice()
        {
            string result = "";
            for (int i = 0; i < lsvService.Items.Count; i++)
            {
                result += lsvService.Items[i].Text.ToString() + ",";
            }
            return result;
        }
        //private string getID()
        //{
        //    string result = "";
        //    List<HOPDONG> ps = mod.HOPDONG.ToList();
        //    if (ps.Any() == false)
        //    {
        //        result = "HD00000001";
        //    }
        //    else
        //    {
        //        var R = ps.Last();
        //        int i = R.MaHopDong.IndexOf("0");
        //        string first = "HD";
        //        int last = int.Parse(R.MaHopDong.Substring(i + 1)) + 1;
        //        result = first + last.ToString().PadLeft(8, '0');
        //    }

        //    return result;
        //}
        private void updateRoom()
        {
            connectDBEntity mod1 = new connectDBEntity();
            DateTime now = DateTime.Now;
            PHONGTRO pHONGTRO = mod1.PHONGTRO.FirstOrDefault(p => p.MaPhong == cboRoomName.SelectedValue.ToString());
            if (dtpStart.Value <= now)
            {
                pHONGTRO.MaTrangThai = 3;
            }
            else
            {
                pHONGTRO.MaTrangThai = 2;
            }
            mod1.SaveChanges();
        }
        private string getRoomname()
        {
            string Result = "";
            connectDBEntity mod = new connectDBEntity();
            List<PHONGTRO> pHONGTROs = mod.PHONGTRO.ToList();
            foreach (var item in pHONGTROs)
            {

                if (cboRoomName.SelectedValue.ToString() == item.MaPhong)
                {
                    Result = item.TenPhong;
                }
            }
            return Result.ToString();
        }
        private void updateTenancy()
        {
            string listIDservice = getIDservice();
            connectDBEntity mod1 = new connectDBEntity();
            var Tenancy = mod1.HOPDONG.FirstOrDefault(a => a.MaHopDong == txtTenacyID.Text);
            Tenancy.MaHopDong = txtTenacyID.Text;
            Tenancy.MaPhong = cboRoomName.SelectedValue.ToString();
            Tenancy.MaNhanVien = txtIDofStaff.Text;
            Tenancy.MaDichVu = listIDservice;
            Tenancy.MaKhachHang = txtIDofCus.Text;
            mod1.SaveChanges();
        }
        private string getSer(ListView a, int b)
        {
            string result = "";
            for (int j = 0; j < a.Items.Count; j++)
            {
                result += a.Items[j].SubItems[b].Text.ToString() + ",";
            }
            return result;
        }
        private void updateTenancyinfo()
        {
            try
            {
                connectDBEntity mod1 = new connectDBEntity();
                double deposit = double.Parse(txtDeposits.Text);
                var Tenancyinfo = mod1.CHITIETHOPDONG.FirstOrDefault(a => a.MaHopDong == txtTenacyID.Text);
                Tenancyinfo.MaHopDong = txtTenacyID.Text;
                Tenancyinfo.NgayLapHopDong = dtpDate.Value;
                Tenancyinfo.TenPhong = getRoomname();
                Tenancyinfo.TenDichVu = getSer(lsvService, 1);
                Tenancyinfo.GiaDichVu = getSer(lsvService, 2);
                Tenancyinfo.DonViTinh = getSer(lsvService, 3);
                Tenancyinfo.TienDatCoc = deposit;
                Tenancyinfo.NgayBatDau = dtpStart.Value;
                Tenancyinfo.NgayKetThuc = dtpEnd.Value;
                mod1.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            addStaff add = new addStaff();
            if (add.IsDisposed == false)
            {
                add.ShowDialog();
                dgvStaff.DataSource = null;
                loadStaff();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            addStaff add = new addStaff();
            if (add.IsDisposed == false)
            {
                add.ShowDialog();
                dgvStaff.DataSource = null;
                loadStaff();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            txtIDofStaff.Text = null;
            txtStaffName.Text = null;
            txtIDStaff.Text = null;
            txtAddrStaff.Text = null;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            txtIDofCus.Text = null;
            txtCustomerName.Text = null;
            txtIDCus.Text = null;
            txtAddrCus.Text = null;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (var it in DICHVUs)
            {
                if (cboService.SelectedValue.ToString() == it.MaDichVu.ToString())
                {
                    ListViewItem item = lsvService.Items.Add(it.MaDichVu.ToString());
                    item.SubItems.Add(it.TenDichVu);
                    item.SubItems.Add(it.DonGia.ToString());
                    item.SubItems.Add(it.DonViTinh);
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (lsvService.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vui Lòng Chọn Dịch Vụ Cần Xoá");
            }
            else
            {
                lsvService.Items.RemoveAt(lsvService.SelectedIndices[0]);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {
                if (MessageBox.Show("Bạn Muốn Lưu Hợp Đồng Này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (txtIDofStaff.Text != null && txtIDofCus != null && lsvService.Items.Count > 0)
                    {
                        updateTenancy();
                        updateTenancyinfo();
                        updateRoom();
                        Close();
                    }
                }
            }
        }

        private void txtDeposits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
