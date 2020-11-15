namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETHOPDONG")]
    public partial class CHITIETHOPDONG
    {
        [Key]
        [StringLength(10)]
        public string MaHopDong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLapHopDong { get; set; }

        [StringLength(8)]
        public string TenPhong { get; set; }

        [StringLength(255)]
        public string TenDichVu { get; set; }

        [StringLength(255)]
        public string GiaDichVu { get; set; }

        [StringLength(255)]
        public string DonViTinh { get; set; }

        public double? TienDatCoc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public virtual HOPDONG HOPDONG { get; set; }
    }
}
