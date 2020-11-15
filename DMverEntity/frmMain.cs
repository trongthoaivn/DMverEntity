using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMverEntity
{
    public partial class frmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void AddUC(Control cr)
        {
            cr.Dock = DockStyle.Fill;
            flumA.Controls.Clear();
            flumA.Controls.Add(cr);
        }

        private void aceRoominfo_Click(object sender, EventArgs e)
        {
            UC_Roominfo a = new UC_Roominfo();
            AddUC(a);
        }

        private void aceService_Click(object sender, EventArgs e)
        {
            UC_Service a = new UC_Service();
            AddUC(a);
        }

        private void aceCustomerinfo_Click(object sender, EventArgs e)
        {
            UC_Customer a = new UC_Customer();
            AddUC(a);
        }

        private void aceHistory_Click(object sender, EventArgs e)
        {
            UC_History a = new UC_History();
            AddUC(a);
        }

        private void aceSalary_Click(object sender, EventArgs e)
        {
            UC_Salary a = new UC_Salary();
            AddUC(a);
        }

        private void aceStaffinfo_Click(object sender, EventArgs e)
        {
            UC_Staff a = new UC_Staff();
            AddUC(a);
        }

        private void aceTenancyinfo_Click(object sender, EventArgs e)
        {
            UC_Tenancy a = new UC_Tenancy();
            AddUC(a);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn đăng xuất ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                frmLogin a = new frmLogin();
                a.Show();
                this.Hide();
            }
            
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            UC_Account a = new UC_Account();
            AddUC(a);
        }

        private void aceRecord_Click(object sender, EventArgs e)
        {
            UC_Electricwater a = new UC_Electricwater();
            AddUC(a);
        }

        private void aceInvoidinfo_Click(object sender, EventArgs e)
        {
            UC_Invoice a = new UC_Invoice();
            AddUC(a);
        }
    }
}
