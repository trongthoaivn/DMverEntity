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
    public partial class editCustomer : Form
    {
        connectDBEntity mod = new connectDBEntity();
        string ID;
        public editCustomer(string id)
        {
            InitializeComponent();
            ID = id;
        }
        private void load()
        {
            var kHACHHANGs = mod.KHACHHANG.FirstOrDefault(p => p.MaKhachHang == ID);
                txtFirstName.Text = kHACHHANGs.HoKhachHang;
                txtLastName.Text = kHACHHANGs.TenKhachHang;
                dtpBirth.Text = kHACHHANGs.NgaySinh.ToString();
                string Sex = kHACHHANGs.GioiTinh;
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
                txtID.Text =kHACHHANGs.CMND;
                txtPhone.Text = kHACHHANGs.SoDienThoai;
                txtMail.Text = kHACHHANGs.ThuDienTu;
                txtAddress.Text = kHACHHANGs.DiaChi;
        }
        private void update()
        {
            KHACHHANG kHACHHANG = mod.KHACHHANG.FirstOrDefault(p => p.MaKhachHang == ID);
            kHACHHANG.MaKhachHang = ID;
            kHACHHANG.HoKhachHang = txtFirstName.Text;
            kHACHHANG.TenKhachHang = txtLastName.Text;
            kHACHHANG.CMND = txtID.Text;
            kHACHHANG.SoDienThoai = txtPhone.Text;
            kHACHHANG.DiaChi = txtAddress.Text;
            kHACHHANG.NgaySinh = dtpBirth.Value;
            kHACHHANG.ThuDienTu = txtMail.Text;
            kHACHHANG.GioiTinh = getSex(); 
            mod.SaveChanges();
        }
        private string getSex()
        {
            string Sex = "";
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
        private void editCustomer_Load(object sender, EventArgs e)
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
