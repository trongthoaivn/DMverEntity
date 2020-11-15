using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMverEntity.Entity;
using DevExpress.Utils.VisualEffects;

namespace DMverEntity
{
    public partial class UC_Account : UserControl
    {
        public UC_Account()
        {
            InitializeComponent();
        }
        private void settxt()
        {
            txtPassword.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
        }
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addAccount add = new addAccount();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                settxt();
                dgvAccount.DataSource = null;
                load();
            }
        }
        private void load()
        {
            connectDBEntity mod = new connectDBEntity();
            List<TAIKHOAN> tAIKHOANs = mod.TAIKHOAN.ToList();
            dgvAccount.DataSource = tAIKHOANs;
            bsiRecordsCount.Caption = "Số tài khoản :" + dgvAccount.Rows.Count;
        }

        private void UC_Account_Load(object sender, EventArgs e)
        {
            load();
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string id = txtUsername.Text;

            if (txtUsername.Text != "")
            {
                editAccount edit = new editAccount(id.ToString());
                edit.ShowDialog();
                if (edit.IsDisposed == false)
                {
                    settxt();
                    dgvAccount.DataSource = null;
                    load();
                }
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvAccount.CurrentRow.Index;
            txtUsername.Text = dgvAccount.Rows[index].Cells[0].Value.ToString();
            txtPassword.Text = dgvAccount.Rows[index].Cells[1].Value.ToString();
            txtEmail.Text = dgvAccount.Rows[index].Cells[2].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            connectDBEntity mod = new connectDBEntity();
            List<TAIKHOAN> tAIKHOANs = mod.TAIKHOAN.Where(p => p.TenTaiKhoan==txtSearch.Text).ToList();
            dgvAccount.DataSource = tAIKHOANs;
            bsiRecordsCount.Caption = "Số tài khoản :" + dgvAccount.Rows.Count;
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtUsername.Text != "")
            {
                if (MessageBox.Show("Bạn muốn xoá tài khoản này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    connectDBEntity mod = new connectDBEntity();
                    var Acc = mod.TAIKHOAN.FirstOrDefault(p => p.TenTaiKhoan == txtUsername.Text);
                    mod.TAIKHOAN.Remove(Acc);
                    mod.SaveChanges();
                    dgvAccount.DataSource = null;
                    load();
                }
            }
        }
    }
}
