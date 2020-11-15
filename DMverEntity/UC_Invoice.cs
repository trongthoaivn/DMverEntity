using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DMverEntity.Entity;

namespace DMverEntity
{
    public partial class UC_Invoice : UserControl
    {
        public UC_Invoice()
        {
            InitializeComponent();
        }

        private void load()
        {
            
            connectDBEntity mod = new connectDBEntity();
            var listInvoice = mod.HOADON.Select(a => new { a.MaHoaDon, a.MaDienNuoc, a.MaPhong, a.MaNhanVien }).ToList();
            dgvInvoice.DataSource = listInvoice;
        }
        
        private void UC_Invoice_Load(object sender, EventArgs e)
        {
            load();

        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addInvoice add = new addInvoice();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                dgvInvoice.DataSource = null;
                load();
            }
        }

        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
