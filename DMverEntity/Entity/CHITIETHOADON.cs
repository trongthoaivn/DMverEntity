namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETHOADON")]
    public partial class CHITIETHOADON
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string MaKhachHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaHoaDon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        public double? TongCong { get; set; }

        public bool? TrangThai { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual HOADON HOADON { get; set; }
    }
}
