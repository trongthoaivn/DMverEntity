namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOPDONG")]
    public partial class HOPDONG
    {
        [Key]
        [StringLength(10)]
        public string MaHopDong { get; set; }

        [StringLength(5)]
        public string MaPhong { get; set; }

        [StringLength(6)]
        public string MaNhanVien { get; set; }

        [StringLength(100)]
        public string MaDichVu { get; set; }

        [StringLength(6)]
        public string MaKhachHang { get; set; }

        public virtual CHITIETHOPDONG CHITIETHOPDONG { get; set; }

        public virtual PHONGTRO PHONGTRO { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
