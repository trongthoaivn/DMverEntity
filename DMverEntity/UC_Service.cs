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
using DevExpress.XtraReports.UI;

namespace DMverEntity
{
    public partial class UC_Service : UserControl
    {
        connectDBEntity mod = new connectDBEntity();
        public UC_Service()
        {
            InitializeComponent();
        }
        private void load()
        {
            connectDBEntity mod = new connectDBEntity();
            dgvService.DataSource = mod.DICHVU.ToList();
            bsiRecordsCount.Caption = "Số Dịch Vụ: " + dgvService.Rows.Count;
        }
        private void UC_Service_Load(object sender, EventArgs e)
        {
            load();
        }
        private void settxt()
        {
            txtServiceID.Text = "";
            txtServiceName.Text = "";
            txtprice.Text = "";
            txtUnit.Text = "";
        }
        private void dgvService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvService.CurrentRow.Index;
            if(index < 7)
            {
                bbiEdit.Enabled = false;
                bbiDelete.Enabled = false;
            }
            else
            {
                bbiEdit.Enabled = true;
                bbiDelete.Enabled = true;
            }
            txtServiceID.Text = dgvService.Rows[index].Cells[0].Value.ToString();
            txtServiceName.Text = dgvService.Rows[index].Cells[1].Value.ToString();
            txtprice.Text = dgvService.Rows[index].Cells[2].Value.ToString();
            txtUnit.Text = dgvService.Rows[index].Cells[3].Value.ToString();
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addService add = new addService();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                settxt();
                dgvService.DataSource = null;
                load();
            }
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string id = txtServiceID.Text;

            if (txtServiceID.Text != "")
            {
                editService edit = new editService(id.ToString());
                edit.ShowDialog();
                if (edit.IsDisposed == false)
                {
                    settxt();
                    dgvService.DataSource = null;
                    load();
                }
            }

        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            
            if (txtServiceID.Text != "")
            {
                int id = int.Parse(txtServiceID.Text);
                if (MessageBox.Show("Bạn muốn xoá dịch vụ này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var Service = mod.DICHVU.FirstOrDefault(p => p.MaDichVu == id);
                    mod.DICHVU.Remove(Service);
                    mod.SaveChanges();
                    settxt();
                    dgvService.DataSource = null;
                    load();
                    
                }
            }

        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            settxt();
            dgvService.DataSource = null;
            load();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            connectDBEntity mod = new connectDBEntity();
            dgvService.DataSource = mod.DICHVU.Where(a => a.TenDichVu ==txtSearch.Text).ToList();
            bsiRecordsCount.Caption = "Số Dịch Vụ: " + dgvService.Rows.Count;
        }

        private void bbiPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reportService a = new reportService();
            a.CreateDocument();
            a.ShowPreviewDialog();
        }
    }
}
