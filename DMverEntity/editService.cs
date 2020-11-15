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
    public partial class editService : Form
    {
        connectDBEntity mod = new connectDBEntity();
        int ID;
        public editService(string id)
        {
            InitializeComponent();
            ID = int.Parse(id);
        }
        private void load()
        {
            var Service = mod.DICHVU.FirstOrDefault(s => s.MaDichVu == ID);
            txtServiceName.Text = Service.TenDichVu;
            txtPrice.Text = Service.DonGia.ToString();
            txtUnit.Text = Service.DonViTinh;
        }
        private void editService_Load(object sender, EventArgs e)
        {
            load();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void update()
        {
            DICHVU dICHVU = mod.DICHVU.FirstOrDefault(p => p.MaDichVu == ID);
            dICHVU.TenDichVu = txtServiceName.Text;
            dICHVU.DonGia = double.Parse(txtPrice.Text);
            dICHVU.DonViTinh = txtUnit.Text;
            mod.SaveChanges();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtServiceName.Text != "" && txtPrice.Text != "" & txtUnit.Text != "")
            {
                update();
                Close();
            }
        }
    }
}
