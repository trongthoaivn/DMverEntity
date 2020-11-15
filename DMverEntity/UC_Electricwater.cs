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
    public partial class UC_Electricwater : UserControl
    {
        public UC_Electricwater()
        {
            InitializeComponent();
        }
        private void setnull()
        {
            txtEWID.Text = "";
            txtRoomID.Text="";
            txtENumberO.Text = "";
            txtEnumberN.Text = "";
            txtWNumberN.Text = "";
            txtWNumberO.Text = "";
            txtSum.Text = "";
            txtCostE.Text = "";
            txtCostW.Text = "";
        }
        private void load()
        {
            connectDBEntity mod = new connectDBEntity();
            var dIENNUOCs = mod.DIENNUOC.Select(a => new { a.MaDienNuoc, a.MaPhong,a.ThoiGian,a.SoDienCu,a.SoDienMoi,a.SoNuocCu,a.SoNuocMoi,a.TienDienNuoc}).ToList();
            dgvEW.DataSource = dIENNUOCs;
            bsiRecordsCount.Caption = "Tổng số hoá đơn: " + dgvEW.Rows.Count;
        }
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addEW add = new addEW();
            add.ShowDialog();
            if (add.IsDisposed == false)
            {
                setnull();
                dgvEW.DataSource = null;
                load();
            }
        }

        private void UC_Electricwater_Load(object sender, EventArgs e)
        {
            load();
        }
        private double Caculate(int id, double O, double N)
        {
            connectDBEntity mod = new connectDBEntity();
            var E = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == id);
            double costE = (double)E.DonGia * (N - O);
            return costE;
        }

        private void dgvEW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvEW.CurrentRow.Index;
            txtEWID.Text = dgvEW.Rows[index].Cells[0].Value.ToString();
            txtRoomID.Text= dgvEW.Rows[index].Cells[1].Value.ToString();
            dtpDate.Text= dgvEW.Rows[index].Cells[2].Value.ToString();
            txtENumberO.Text= dgvEW.Rows[index].Cells[3].Value.ToString();
            txtEnumberN.Text= dgvEW.Rows[index].Cells[4].Value.ToString();
            txtWNumberO.Text= dgvEW.Rows[index].Cells[5].Value.ToString();
            txtWNumberN.Text= dgvEW.Rows[index].Cells[6].Value.ToString();
            txtCostE.Text = Caculate(6, double.Parse(txtENumberO.Text), double.Parse(txtEnumberN.Text)).ToString();
            txtCostW.Text = Caculate(7, double.Parse(txtWNumberO.Text), double.Parse(txtWNumberN.Text)).ToString();
            txtSum.Text= dgvEW.Rows[index].Cells[7].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            connectDBEntity mod = new connectDBEntity();
            var dIENNUOCs = mod.DIENNUOC.Where(a => a.MaDienNuoc == txtSearch.Text).Select(a => new { a.MaDienNuoc, a.MaPhong, a.ThoiGian, a.SoDienCu, a.SoDienMoi, a.SoNuocCu, a.SoNuocMoi, a.TienDienNuoc }).ToList();
            dgvEW.DataSource = dIENNUOCs;

        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setnull();
            dgvEW.DataSource = null;
            load();
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            connectDBEntity mod = new connectDBEntity();
            if (txtEWID.Text != "")
            {
                if (MessageBox.Show("Bạn muốn xoá phiếu ghi điện nước này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try 
                    {
                        var dIENNUOC = mod.DIENNUOC.FirstOrDefault(a => a.MaDienNuoc == txtEWID.Text);
                        mod.DIENNUOC.Remove(dIENNUOC);
                        mod.SaveChanges();
                        setnull();
                        dgvEW.DataSource = null;
                        load();
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Phiếu này đã xuất hoá đơn không thể xoá " +
                            "\n"+"\tHãy xoá hoá đơn trước !","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtEWID.Text != "")
            {
                editEW edit = new editEW(txtEWID.Text);
                edit.ShowDialog();
                if (edit.IsDisposed == false)
                {
                    setnull();
                    dgvEW.DataSource = null;
                    load();
                }
            }
            
            
        }

        private void bbiPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtEWID.Text != "")
            {
                report_EW report = new report_EW(txtEWID.Text);
                report.ShowPreviewDialog();
            }
        }
    }
}
