using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DMverEntity.Entity;
using System.Collections.Generic;
using DevExpress.Data.ODataLinq.Helpers;
using System.Linq;

namespace DMverEntity
{
    public partial class reportTenancy : DevExpress.XtraReports.UI.XtraReport
    {
        string ID;
        
        public reportTenancy(string id)
        {
            InitializeComponent();
            ID = id;
            Load();
        }
        private void Load()
        {
            connectDBEntity mod = new connectDBEntity();
            var Tenancy = mod.HOPDONG.FirstOrDefault(a => a.MaHopDong == ID);
            var Tenancyinfo = mod.CHITIETHOPDONG.FirstOrDefault(a => a.MaHopDong == ID);
            var Staff = mod.NHANVIEN.FirstOrDefault(a => a.MaNhanVien == Tenancy.MaNhanVien);
            var Customer = mod.KHACHHANG.FirstOrDefault(a => a.MaKhachHang == Tenancy.MaKhachHang);
            List<DICHVU> service = mod.DICHVU.ToList();
            dateDay.Text = Tenancyinfo.NgayLapHopDong.ToString().Substring(0, 2);
            dateMonth.Text = Tenancyinfo.NgayLapHopDong.ToString().Substring(3, 2);
            dateYear.Text = Tenancyinfo.NgayLapHopDong.ToString().Substring(6, 4);
            staffName.Text = Staff.HoNhanVien + " " + Staff.TenNhanVien;
            staffBirth.Text = Staff.NgaySinh.ToString().Substring(0, 9);
            staffID.Text = Staff.CMND.ToString();
            staffNumber.Text = Staff.SoDienThoai.ToString();
            staffAddress.Text = Staff.DiaChi.ToString();
            customerName.Text = Customer.HoKhachHang + " " + Customer.TenKhachHang;
            customerBirth.Text = Customer.NgaySinh.ToString().Substring(0, 9);
            customerId.Text = Customer.CMND.ToString();
            customerNumber.Text = Customer.SoDienThoai.ToString();
            customerAddress.Text = Customer.DiaChi.ToString();
            roomName.Text = Tenancyinfo.TenPhong.ToString();
            foreach (var item in service)
            {
                if (item.TenDichVu == "Tiền Phòng")
                {
                    roomPrice.Text = item.DonGia.ToString();

                }
                if (item.TenDichVu == "Điện")
                {
                    serviceElectric.Text = item.DonGia.ToString();
                }
                if (item.TenDichVu == "Nước")
                {
                    serviceWater.Text = item.DonGia.ToString();
                }

            }
            roomDeposit.Text = Tenancyinfo.TienDatCoc.ToString();
            dateDayStart.Text = Tenancyinfo.NgayBatDau.ToString().Substring(0, 2);
            dateMonthStart.Text = Tenancyinfo.NgayBatDau.ToString().Substring(3, 2);
            dateYearStart.Text = Tenancyinfo.NgayBatDau.ToString().Substring(6, 4);
            dateDayEnd.Text = Tenancyinfo.NgayKetThuc.ToString().Substring(0, 2);
            dateMonthEnd.Text = Tenancyinfo.NgayKetThuc.ToString().Substring(3, 2);
            dateYearEnd.Text = Tenancyinfo.NgayKetThuc.ToString().Substring(6, 4);
        }
    }
}
