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
    public partial class addStaff : Form
    {
        connectDBEntity mod = new connectDBEntity();
        public addStaff()
        {
            InitializeComponent();
        }
        private string getID()
        { 
            string result;
            List<NHANVIEN> ps = mod.NHANVIEN.ToList();
            if (ps.Any() == false)
            {
                result = "NV0001";
            }
            else
            {
                var R = ps.Last();
                int i = R.MaNhanVien.IndexOf("0");
                string first = "NV";
                int last = int.Parse(R.MaNhanVien.Substring(i + 1)) + 1;
                result = first + last.ToString().PadLeft(4, '0');
            }
            return result;
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
        private void AddStaff()
        {
            NHANVIEN nHANVIEN= new NHANVIEN()
            {
                MaNhanVien = getID(),
                HoNhanVien = txtFirstName.Text,
                TenNhanVien = txtLastName.Text,
                CMND = txtID.Text,
                SoDienThoai = txtPhone.Text,
                DiaChi = txtAddress.Text,
                NgaySinh = dtpBirth.Value,
                ThuDienTu = txtMail.Text,
                GioiTinh = getSex()
            };
            mod.NHANVIEN.Add(nHANVIEN);
            mod.SaveChanges();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtID.Text != "" && txtPhone.Text != "" && txtAddress.Text != "")
            {
                AddStaff();
                Close();
            }
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
    }
}
