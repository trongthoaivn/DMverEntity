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
    public partial class addCustomer : Form
    {
        connectDBEntity mod = new connectDBEntity();
        public addCustomer()
        {
            InitializeComponent();
        }
        private string getID()
        {   
            string result;
            List<KHACHHANG> ps = mod.KHACHHANG.ToList();
            if (ps.Any() == false)
            {
                result = "KH0001";
            }
            else
            {
                var R = ps.Last();
                int i = R.MaKhachHang.IndexOf("0");
                string first = "KH";
                int last = int.Parse(R.MaKhachHang.Substring(i + 1)) + 1;
                result = first + last.ToString().PadLeft(4, '0');
            }
            return result;
        }
         private  string getSex()
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
        private void AddCustomer()
        {
            KHACHHANG kHACHHANG = new KHACHHANG
            {
                MaKhachHang = getID(),
                HoKhachHang = txtFirstName.Text,
                TenKhachHang = txtLastName.Text,
                CMND=txtID.Text,
                SoDienThoai=txtPhone.Text,
                DiaChi = txtAddress.Text,
                NgaySinh = dtpBirth.Value,
                ThuDienTu = txtMail.Text,
                GioiTinh = getSex(),
                MaPhong="00000"
                
            };
            mod.KHACHHANG.Add(kHACHHANG);
            mod.SaveChanges();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtFirstName.Text!="" && txtLastName.Text!="" && txtID.Text!="" && txtPhone.Text != ""&&txtAddress.Text!="")
            {
                AddCustomer();
                Close();
            }
            
        }
    }
}
