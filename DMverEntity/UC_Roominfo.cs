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

namespace DMverEntity
{
    public partial class UC_Roominfo : UserControl
    {
        connectDBEntity mod = new connectDBEntity();
        List<PHONGTRO> pHONGTROs;
        public UC_Roominfo()
        {
            InitializeComponent();
        }

        private void ribbonControl_Click(object sender, EventArgs e)
        {
            
        }
        private void LoadRoom()
        {
            connectDBEntity mod1 = new connectDBEntity();
            List<PHONGTRO> pHONGTROs = mod1.PHONGTRO.ToList();
            foreach(var it in pHONGTROs)
            {
                ListViewItem item = new ListViewItem(it.TenPhong.ToString());
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, it.MaPhong.ToString());
                int status = int.Parse(it.MaTrangThai.ToString());
                switch (status)
                {
                    case 1:
                        item.ImageIndex = 0;
                        break;
                    case 2:
                        item.ImageIndex = 1;
                        break;
                    case 3:
                        item.ImageIndex = 2;
                        break;
                    case 4:
                        item.ImageIndex = 3;
                        break;
                    default:
                        item.ImageIndex = 0;
                        break;
                }
                bsiRecordsCount.Caption = "Tổng Số Phòng: " + pHONGTROs.Count;
                item.SubItems.Add(subItem);
                lsvRoom.Items.Add(item);
            }

        }

        private void UC_Roominfo_Load(object sender, EventArgs e)
        {
            LoadRoom();
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lsvRoom.Items.Clear();
            LoadRoom();
        }

        private void lsvRoom_Click(object sender, EventArgs e)
        {
            bbiDelete.Enabled = true; 
            connectDBEntity mod2 = new connectDBEntity();
            pHONGTROs = mod2.PHONGTRO.ToList();
            List<TRANGTHAIPHONG> tRANGTHAIPHONGs = mod.TRANGTHAIPHONG.ToList();
            if (lsvRoom.SelectedItems.Count > 0)
            {
                string st = lsvRoom.SelectedItems[0].SubItems[1].Text;
                foreach(var it in pHONGTROs)
                {
                    if (st == it.MaPhong.ToString())
                    {
                        txtRoomID.Text = it.MaPhong.ToString();
                        txtStatusid.Text = it.MaTrangThai.ToString();
                        if (txtStatusid.Text == "2" || txtStatusid.Text == "3")
                        {
                            bbiDelete.Enabled = false;
                        }
                        foreach(var tt in tRANGTHAIPHONGs)
                        {
                            if (it.MaTrangThai == tt.MaTrangThai)
                            {
                                txtStatus.Text = tt.TenTrangThai.ToString();
                            }
                        }
                        txtRoomName.Text = it.TenPhong.ToString();
                        txtDescription.Text = it.MoTa;
                        txtAcreage.Text = it.DienTich.ToString();
                        txtCapacity.Text = it.SoNguoiO.ToString();
                    }
                }
            }
        }
        private void setClear()
        {
            txtRoomID.Clear();
            txtRoomName.Clear();
            txtStatus.Clear();
            txtDescription.Clear();
            txtCapacity.Clear();
            txtAcreage.Clear();
        }
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addRoom a = new addRoom();
            a.ShowDialog();
            if (a.IsDisposed == false)
            {
                lsvRoom.Items.Clear();
                setClear();
                LoadRoom();
            }
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            editRoom a = new editRoom(txtRoomID.Text);
            if(txtRoomID.Text!="")
            a.ShowDialog();
            if (a.IsDisposed == false )
            {
                lsvRoom.Items.Clear();
                setClear();
                LoadRoom();
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtRoomID.Text != "")
            {
                if (MessageBox.Show("Bạn muốn xoá phòng này ?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    var pHONGTRO = mod.PHONGTRO.SingleOrDefault(p => p.MaPhong == txtRoomID.Text);
                    mod.PHONGTRO.Remove(pHONGTRO);
                    mod.SaveChanges();
                    lsvRoom.Items.Clear();
                    setClear();
                    LoadRoom();
                }
            } 
        }
    }
}
