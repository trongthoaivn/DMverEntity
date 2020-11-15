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
    public partial class editStaff : Form
    {
        connectDBEntity mod = new connectDBEntity();
        string ID;
        public editStaff(string id)
        {
            InitializeComponent();
            ID = id;
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void load()
        {
            var nHANVIEN = mod.NHANVIEN.FirstOrDefault(p => p.MaNhanVien == ID);
            txtFirstName.Text = nHANVIEN.HoNhanVien;
            txtLastName.Text = nHANVIEN.TenNhanVien;
            dtpBirth.Text = nHANVIEN.NgaySinh.ToString();
            string Sex = nHANVIEN.GioiTinh;
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
            txtID.Text = nHANVIEN.CMND;
            txtPhone.Text = nHANVIEN.SoDienThoai;
            txtMail.Text = nHANVIEN.ThuDienTu;
            txtAddress.Text = nHANVIEN.DiaChi;
        }
        private string getSex()
        {
            string Sex = robMale.Text.ToString();
            if (robMale.Checked == true)
            {
                Sex = robMale.Text.ToString();
            }
            if (robFemale.Checked == true)
            {
                Sex = robFemale.Text.ToString();
            }
            if (robOther.Checked == true)
            {
                Sex = robOther.Text.ToString();
            }
            return Sex;
        }
        private void update()
        {
            NHANVIEN nHANVIEN = mod.NHANVIEN.FirstOrDefault(p => p.MaNhanVien == ID);
            nHANVIEN.MaNhanVien = ID;
            nHANVIEN.HoNhanVien = txtFirstName.Text;
            nHANVIEN.TenNhanVien = txtLastName.Text;
            nHANVIEN.CMND = txtID.Text;
            nHANVIEN.SoDienThoai = txtPhone.Text;
            nHANVIEN.DiaChi = txtAddress.Text;
            nHANVIEN.NgaySinh = dtpBirth.Value;
            nHANVIEN.ThuDienTu = txtMail.Text;
            nHANVIEN.GioiTinh = getSex();
            mod.SaveChanges();
        }
        private void editStaff_Load(object sender, EventArgs e)
        {
            load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtID.Text != "" && txtPhone.Text != "" && txtAddress.Text != "")
            {
                update();
                Close();
            }
        }
    }
}
