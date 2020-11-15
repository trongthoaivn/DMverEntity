using DevExpress.PivotGrid.OLAP;
using DevExpress.XtraEditors.Controls;
using DMverEntity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMverEntity
{
    public partial class addEW : Form
    { 
        public addEW()
        {
            InitializeComponent();
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
        private void loadRoom()
        {
            connectDBEntity mod = new connectDBEntity();
            var pHONGTROs = mod.PHONGTRO.Select( a => new { a.MaPhong, a.TenPhong}).ToList();
            foreach (var item in pHONGTROs)
            {
                Comboboxitem it = new Comboboxitem();
                it.Text = item.TenPhong;
                it.Value = item.MaPhong;
                cboRoom.Items.Add(it);
            }
        }
        private string getID()
        {
            string result;
            result = "DN" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
            return result;
        }
        private void load()
        {
            connectDBEntity mod = new connectDBEntity();
            string roomID= (cboRoom.SelectedItem as Comboboxitem).Value.ToString();
            var dIENNUOCs=mod.DIENNUOC.OrderByDescending(a =>a.SoDienMoi ).FirstOrDefault( a => a.MaPhong.Equals(roomID));
            if (dIENNUOCs != null)
            {
                txtENumberO.Text = dIENNUOCs.SoDienMoi.ToString();
                txtWNumberO.Text = dIENNUOCs.SoNuocMoi.ToString();
            }
            else
            {
                txtWNumberO.Text = "0";
                txtENumberO.Text = "0";
            }

        }
        private void addEW_Load(object sender, EventArgs e)
        {
            loadRoom(); 

        }
        private double Caculate(int id,double O,double N )
        {
            connectDBEntity mod = new connectDBEntity();
            var E = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == id);
            double costE = (double)E.DonGia * (N - O);
            return costE;
        }

        private void AddEW()
        {
            connectDBEntity mod = new connectDBEntity();
            var ElectricWater = new DIENNUOC
            {
                MaDienNuoc = getID(),
                MaPhong = (cboRoom.SelectedItem as Comboboxitem).Value.ToString(),
                ThoiGian = DateTime.Now,
                SoDienCu = int.Parse(txtENumberO.Text),
                SoDienMoi = int.Parse(txtEnumberN.Text),
                SoNuocMoi = int.Parse(txtWNumberN.Text),
                SoNuocCu = int.Parse(txtWNumberO.Text),
                TienDienNuoc = double.Parse(txtSum.Text)
            };
            mod.DIENNUOC.Add(ElectricWater);
            mod.SaveChanges();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSum.PerformClick();
            if (txtWNumberN.Text != "" && txtEnumberN.Text != "")
            {
                if (MessageBox.Show("Bạn muốn lưu phiếu ghi điện nước này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AddEW();
                    setnull();
                    txtEnumberN.ReadOnly = true;
                    txtWNumberN.ReadOnly = true;
                }
            }   
        }

        private void cboRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            load();
            setnull();
            txtEnumberN.ReadOnly = false;
            txtWNumberN.ReadOnly = false;
           
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
        private double Sum(double a,double b)
        {
            double total =0;
            if(a >= 0 && b>=0)
            {
                total += a+b;
            }
            else
            {
                MessageBox.Show("Chỉ số mới phải lớn hơn chỉ số cũ");
                setnull();
            }
            return total;
        }
        private void txtEnumberN_TextChanged(object sender, EventArgs e)
        {
            
            if (txtEnumberN.Text != "" && txtENumberO.Text !="")
            {
                txtCostE.Text = Caculate(6, double.Parse(txtENumberO.Text), double.Parse(txtEnumberN.Text)).ToString();
            }
            else if (txtEnumberN.Text != "" && txtENumberO.Text == "")
            {
                txtCostE.Text = Caculate(6, 0, double.Parse(txtEnumberN.Text)).ToString();
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
            else if (txtWNumberN.Text != "" && txtWNumberO.Text == "")
            {
                txtCostW.Text = Caculate(7,0, double.Parse(txtWNumberN.Text)).ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            double E = double.Parse(txtCostE.Text);
            double W = double.Parse(txtCostW.Text);
            txtSum.Text = Sum(E, W).ToString();
            
        }

        private void addEW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            
        }
    }
}
