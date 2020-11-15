namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            CHITIETHOADON = new HashSet<CHITIETHOADON>();
            HOPDONG = new HashSet<HOPDONG>();
        }

        [Key]
        [StringLength(6)]
        public string MaKhachHang { get; set; }

        [StringLength(45)]
        public string HoKhachHang { get; set; }

        [StringLength(5)]
        public string TenKhachHang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(4)]
        public string GioiTinh { get; set; }

        [StringLength(9)]
        public string CMND { get; set; }

        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [StringLength(40)]
        public string ThuDienTu { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(5)]
        public string MaPhong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADON { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOPDONG> HOPDONG { get; set; }
    }
}
