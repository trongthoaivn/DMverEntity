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
    public partial class frmLogin : Form
    {
        connectDBEntity mod = new connectDBEntity();
        List<TAIKHOAN> tAIKHOANs;
        public frmLogin()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (txtPass.Text!="" && txtUsername.Text!="")
            {
                foreach(var item in tAIKHOANs)
                {
                    if(item.TenTaiKhoan==txtUsername.Text && item.MatKhau == txtPass.Text)
                    {
                        frmMain a = new frmMain();
                        a.Show();
                        this.Hide();
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            if (flag == false) MessageBox.Show("Tài khoản hoặc mật khẩu không đúng !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            tAIKHOANs = mod.TAIKHOAN.ToList();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            txtPass.PasswordChar = '\0';
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            txtPass.PasswordChar = '*';
        }
        private bool Exsitsform(Form a)
        {
            foreach(var child in MdiChildren)
            {
                if (child.Name == a.Name)
                {
                    child.Activate();
                    return true;
                }
            }
            return false;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            connectDBEntity mod1 = new connectDBEntity();
            frmForgot frmForgot = new frmForgot();
            frmForgot.ShowDialog();
            if (frmForgot.IsDisposed == false)
            {
                tAIKHOANs = mod1.TAIKHOAN.ToList();
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel=true;
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}
