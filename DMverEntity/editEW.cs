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
    public partial class editEW : Form
    {
        string idRoom;
        string ID;
        public editEW(string id)
        {
            InitializeComponent();
            ID = id;
        }
        private void loadRoom()
        {
            connectDBEntity mod = new connectDBEntity();
            var pHONGTROs = mod.PHONGTRO.Select(a => new { a.MaPhong, a.TenPhong }).ToList();
            foreach (var item in pHONGTROs)
            {
                Comboboxitem it = new Comboboxitem();
                it.Text = item.TenPhong;
                it.Value = item.MaPhong;
                cboRoom.Items.Add(it);
            }
            foreach (Comboboxitem item in cboRoom.Items)
            {
                if (idRoom == item.Value.ToString())
                {
                    cboRoom.SelectedItem = item;
                }
            }
        }
        private void load()
        {
            connectDBEntity mod = new connectDBEntity();
            var dIENNUOC = mod.DIENNUOC.FirstOrDefault(a => a.MaDienNuoc == ID);
            txtEWID.Text = ID;
            idRoom = dIENNUOC.MaPhong.ToString();
            dtpDate.Enabled = false;
            dtpDate.Value= dIENNUOC.ThoiGian.Value;
            txtENumberO.Text = dIENNUOC.SoDienCu.Value.ToString();
            txtEnumberN.Text = dIENNUOC.SoDienMoi.Value.ToString();
            txtWNumberO.Text = dIENNUOC.SoNuocCu.Value.ToString();
            txtWNumberN.Text = dIENNUOC.SoNuocMoi.Value.ToString();
            MessageBox.Show(dIENNUOC.MaPhong.ToString());
        }
        public class Comboboxitem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private void EditEW()
        {
            connectDBEntity mod = new connectDBEntity();
            var dIENNUOC = mod.DIENNUOC.FirstOrDefault(a => a.MaDienNuoc == ID);
            dIENNUOC.MaDienNuoc = ID;
            dIENNUOC.MaPhong = (cboRoom.SelectedItem as Comboboxitem).Value.ToString();
            dIENNUOC.ThoiGian = dtpDate.Value;
            dIENNUOC.SoDienCu = int.Parse(txtENumberO.Text);
            dIENNUOC.SoDienMoi = int.Parse(txtEnumberN.Text);
            dIENNUOC.SoNuocCu = int.Parse(txtWNumberN.Text);
            dIENNUOC.SoNuocMoi = int.Parse(txtWNumberN.Text);
            dIENNUOC.TienDienNuoc = double.Parse(txtSum.Text);
            mod.SaveChanges();
        }
        private void editEW_Load(object sender, EventArgs e)
        {
            load();
            loadRoom();
            
        }
        private double Caculate(int id, double O, double N)
        {
            connectDBEntity mod = new connectDBEntity();
            var E = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == id);
            double costE = (double)E.DonGia * (N - O);
            return costE;
        }

        private void txtEnumberN_TextChanged(object sender, EventArgs e)
        {
            if (txtEnumberN.Text != "" && txtENumberO.Text != "")
            {
                txtCostE.Text = Caculate(6, double.Parse(txtENumberO.Text), double.Parse(txtEnumberN.Text)).ToString();
            }
            else
            {
                txtCostE.Text = "0";
            }
        }

        private void txtWNumberN_TextChanged(object sender, EventArgs e)
        {
            if (txtWNumberN.Text != "" && txtWNumberO.Text != "")
            {
                txtCostW.Text = Caculate(7, double.Parse(txtWNumberO.Text), double.Parse(txtWNumberN.Text)).ToString();
            }
            else
            {
                txtCostW.Text = "0";

            }
        }

        private void txtEnumberN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtWNumberN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            double E = double.Parse(txtCostE.Text);
            double W = double.Parse(txtCostW.Text);
            txtSum.Text = Sum(E, W).ToString();
        }
        private double Sum(double a, double b)
        {
            double total = 0;
            if (a >= 0 && b>=0)
            {
                total += a + b;
            }
            else
            {
                MessageBox.Show("Chỉ số mới phải lớn hơn chỉ số cũ");
                setnull();
            }
            return total;
        }
        private void setnull()
        {
            txtWNumberN.Text = "";
            txtEnumberN.Text = "";
            txtCostE.Text = "";
            txtCostW.Text = "";
            txtSum.Text = "0";
            txtCostE.Text = "0";
            txtCostW.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSum.PerformClick();
            if (txtWNumberN.Text != "" && txtEnumberN.Text != "")
            {
                if (MessageBox.Show("Bạn muốn lưu phiếu ghi điện nước này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EditEW();
                    setnull();
                    Close();
                }
            }
        }
    }

}
