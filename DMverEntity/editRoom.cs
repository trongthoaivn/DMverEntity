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
    public partial class editRoom : Form
    {
        connectDBEntity mod = new connectDBEntity();
        string ID = "";
        public editRoom(string id)
        {
            InitializeComponent();
            ID = id;
            txtRoomID.Text = ID;
        }

        private void editRoom_Load(object sender, EventArgs e)
        {
            loadcboStatus();
            loadinfo();
        }
        private void loadcboStatus()
        {
            List<TRANGTHAIPHONG> dt = mod.TRANGTHAIPHONG.ToList();
            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "TenTrangThai";
            cboStatus.ValueMember = "MaTrangThai";
        }
        private void loadinfo()
        {
            List<PHONGTRO> pHONGTROs = mod.PHONGTRO.ToList();
            foreach(var it in pHONGTROs)
            {
                if (it.MaPhong == txtRoomID.Text)
                {
                    txtRoomName.Text = it.TenPhong.ToString();
                    cboStatus.SelectedValue = int.Parse(it.MaTrangThai.ToString());
                    txtAcreage.Text = it.DienTich.ToString();
                    txtCapacity.Text = it.SoNguoiO.ToString();
                    txtDescription.Text = it.MoTa.ToString();
                }
            }
        }
        private void update()
        {
            PHONGTRO pHONGTRO = mod.PHONGTRO.FirstOrDefault(p => p.MaPhong == ID);
            pHONGTRO.TenPhong = txtRoomName.Text;
            pHONGTRO.MaPhong = txtRoomID.Text;
            pHONGTRO.MaTrangThai = int.Parse(cboStatus.SelectedValue.ToString());
            pHONGTRO.DienTich = double.Parse(txtAcreage.Text.ToString());
            pHONGTRO.SoNguoiO = int.Parse(txtCapacity.Text);
            pHONGTRO.MoTa = txtDescription.Text;
            var TY = mod.HOPDONG.FirstOrDefault(a => a.MaPhong ==txtRoomID.Text);

            var CT = mod.CHITIETHOPDONG.FirstOrDefault(a => a.MaHopDong == TY.MaHopDong);
            CT.TenPhong = txtRoomName.Text;
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
            update();
            Close();
        }
    }
}
