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
using System.Net;
using System.Net.Mail;
using System.IO;

namespace DMverEntity
{
    public partial class addAccount : Form
    {
        string[] verify = new string[5];
        public addAccount()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connectDBEntity mod = new connectDBEntity();
            TAIKHOAN tAIKHOAN = new TAIKHOAN
            {
                TenTaiKhoan = txtUsername.Text,
                MatKhau = txtnewPass.Text,
                Email = txtMail.Text
            };
            mod.TAIKHOAN.Add(tAIKHOAN);
            mod.SaveChanges();
            Close();
        }
    }
}
