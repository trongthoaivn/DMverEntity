using DMverEntity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMverEntity
{
    public partial class editAccount : Form
    {
        connectDBEntity mod = new connectDBEntity();
        TAIKHOAN tAIKHOANs;
        string Username;
        public editAccount(string US)
        {
            InitializeComponent();
            Username = US;
        }

        private void editAccount_Load(object sender, EventArgs e)
        {
            tAIKHOANs = mod.TAIKHOAN.FirstOrDefault(a =>a.TenTaiKhoan ==Username);
            txtUsername.Text = tAIKHOANs.TenTaiKhoan;
            txtoldPass.Text = tAIKHOANs.MatKhau;
            txtMail.Text = tAIKHOANs.Email;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TAIKHOAN Acc = mod.TAIKHOAN.Where(p => p.TenTaiKhoan == txtUsername.Text).SingleOrDefault();
            Acc.TenTaiKhoan = txtUsername.Text;
            if (txtnewPass.Text == "")
                Acc.MatKhau = txtoldPass.Text;
            else
            Acc.MatKhau = txtnewPass.Text;
            Acc.Email = txtMail.Text;
            mod.Entry(Acc).State = EntityState.Modified;
            mod.SaveChanges();
            Close();
        }
    }
}
