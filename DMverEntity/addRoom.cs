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
    public partial class addRoom : Form
    {
        connectDBEntity mod = new connectDBEntity();
        public addRoom()
        {
            InitializeComponent();
        }

        private void addRoom_Load(object sender, EventArgs e)
        {
            loadcboStatus();
            txtRoomID.Text = getID();
        }
        private void loadcboStatus()
        {
            List<TRANGTHAIPHONG> dt = mod.TRANGTHAIPHONG.ToList() ;
            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "TenTrangThai";
            cboStatus.ValueMember = "MaTrangThai";
        }

        private void txtCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtAcreage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private string getID()
        {   string result;
            List<PHONGTRO> ps = mod.PHONGTRO.ToList();
            if (ps.Any() == false)
            {
                result = "P0001";
            }
            else
            {
                var R = ps.Last();
                int i = R.MaPhong.IndexOf("0");
                string first = "P";
                int last = int.Parse(R.MaPhong.Substring(i + 1)) + 1;
                result = first + last.ToString().PadLeft(4, '0');
            }
            return result;
        }
        private void AddRoom()
        {
            PHONGTRO pHONGTRO = new PHONGTRO
            {
                MaPhong = getID(),
                TenPhong = txtRoomName.Text,
                MaTrangThai = int.Parse(cboStatus.SelectedValue.ToString()),
                DienTich = double.Parse(txtCapacity.Text),
                MoTa = txtDescription.Text,
            };
            mod.PHONGTRO.Add(pHONGTRO);
            mod.SaveChanges();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAcreage.Text == "")
                txtAcreage.Text = "0";
            if (txtCapacity.Text == "")
                txtCapacity.Text = "0";
            if (txtRoomName.Text == "")
                txtRoomName.Text = "P.Mới";
            AddRoom();
            Close();
        }
    }
}
