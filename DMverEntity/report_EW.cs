using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DMverEntity.Entity;
using System.Linq;

namespace DMverEntity
{
    public partial class report_EW : DevExpress.XtraReports.UI.XtraReport
    {
        connectDBEntity mod = new connectDBEntity();
        string ID;
        public report_EW(string id)
        {
            InitializeComponent();
            ID = id;
            load();
        }
        private double Caculate(int id, double O, double N)
        {
            connectDBEntity mod = new connectDBEntity();
            var E = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == id);
            double costE = (double)E.DonGia * (N - O);
            return costE;
        }
        private double Sum(double a, double b)
        {
            double total = 0;
            if (a >= 0)
            {
                total = a + b;
            }
            return total;
        }
        private void load()
        {
            
            var Report = mod.DIENNUOC.FirstOrDefault(a => a.MaDienNuoc == ID);
            var Room = mod.PHONGTRO.FirstOrDefault(a => a.MaPhong == Report.MaPhong);
            var E = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == 6);
            var W = mod.DICHVU.FirstOrDefault(a => a.MaDichVu == 7);
            lbRoomName.Text = Room.TenPhong.ToString();
            xrBarCode1.Text = Report.MaDienNuoc;
            ENumberO.Text =  Report.SoDienCu.Value.ToString();
            ENumberN.Text =  Report.SoDienMoi.Value.ToString();
            WNumberO.Text =  Report.SoNuocCu.Value.ToString();
            WNumberN.Text =  Report.SoNuocMoi.Value.ToString();
            CostE.Text = Caculate(6, double.Parse(ENumberO.Text), double.Parse(ENumberN.Text)).ToString();
            CostW.Text = Caculate(7, double.Parse(WNumberO.Text), double.Parse(WNumberN.Text)).ToString();
            double En = double.Parse(CostE.Text);
            double Wn = double.Parse(CostW.Text);
            Total.Text = Sum(En, Wn).ToString();
        }
    }
}
