using DevExpress.XtraDashboardLayout;
using DMverEntity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMverEntity
{
    public partial class frmForgot : Form
    {
        connectDBEntity mod = new connectDBEntity();
        List<TAIKHOAN> tAIKHOANs;
        string code;
        public frmForgot()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (txtUsername.Text != "" && txtnewPass.Text != "" && (txtoldPass.Text != "" || txtMail.Text != ""))
            {
                foreach(var item in tAIKHOANs)
                {
                    if(item.TenTaiKhoan==txtUsername.Text && (item.MatKhau == txtoldPass.Text || (item.Email == txtMail.Text && txtCode.Text==code)))
                    {
                            TAIKHOAN Acc = mod.TAIKHOAN.Where(p => p.TenTaiKhoan == txtUsername.Text).SingleOrDefault();
                            Acc.TenTaiKhoan = txtUsername.Text;
                            Acc.MatKhau = txtnewPass.Text;
                            //Acc.Email = txtMail.Text;
                            mod.Entry(Acc).State = EntityState.Modified;
                            mod.SaveChanges();
                            MessageBox.Show("Đổi mật khẩu thành công !");
                            Close();
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            if(flag==true) 
                MessageBox.Show("Nhập sai thông tin tài khoản !", "Cảnh báo !");
        }

        private void frmForgot_Load(object sender, EventArgs e)
        {

            tAIKHOANs = mod.TAIKHOAN.ToList();
            txtCode.Enabled = false;
            linkLabel1.Enabled = false;
        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCode.Enabled = true;
            linkLabel1.Enabled = true;
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            if(txtMail.Text=="")
            {
                linkLabel1.Enabled = false;
                txtCode.Enabled = !true;
            }
        }
        private static void sendMail( string Mailto,string Code)
        {
            try
            {
                SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.Credentials = new NetworkCredential("trongthoai001@gmail.com", "18008198");
                MailMessage message = new MailMessage("trongthoai001@gmail.com",Mailto);
                message.Subject = "Xác nhận đổi mật khẩu !";
                message.Body =
                    "<p><span><strong>Ai đó đang gửi yêu cầu đổi mật khẩu cho tài khoản của bạn</strong></span></p>"+
                    "           <p><span> Mã xác nhận là :"+Code+" </span></p>" +
                    "           <p><strong><span> Mọi chi tiết xin liên hệ Người quản trị phần mềm </span></strong></p>" +
                    "           <p><span> Công ty Phần mềm TTK Software</span></p> ";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                mailclient.Send(message);
                MessageBox.Show("Mã xác nhận đã được gửi", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65,87)));
                sb.Append(c);
            }
            if (lowerCase)
                return sb.ToString().ToLower();
            return sb.ToString();
            
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Enabled = false;
            string TextRd=RandomString(5,false);
            code=TextRd;
            sendMail(txtMail.Text, TextRd);
        }
    }
}
