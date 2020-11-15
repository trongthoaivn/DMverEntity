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
    public partial class addService : Form
    {
        connectDBEntity mod = new connectDBEntity();
        public addService()
        {
            InitializeComponent();
        }
        private int getId()
        {
            var s = mod.DICHVU.OrderByDescending(a => a.MaDichVu).FirstOrDefault();
            return s.MaDichVu;
        }
        private void AddService()
        {
            DICHVU dICHVU = new DICHVU
            {
                MaDichVu = getId() + 1,
                TenDichVu = txtServiceName.Text,
                DonGia = double.Parse(txtPrice.Text),
                DonViTinh = txtPrice.Text,
            };
            mod.DICHVU.Add(dICHVU);
            mod.SaveChanges();
            Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtServiceName.Text!=""&& txtPrice.Text!=""&& txtUnit.Text=="")
            AddService();
        }
    }
}
