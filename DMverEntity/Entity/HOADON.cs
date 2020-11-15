namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOADON")]
    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            CHITIETHOADON = new HashSet<CHITIETHOADON>();
        }

        [Key]
        [StringLength(10)]
        public string MaHoaDon { get; set; }

        [StringLength(6)]
        public string MaNhanVien { get; set; }

        [StringLength(10)]
        public string MaDienNuoc { get; set; }

        [StringLength(5)]
        public string MaPhong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADON { get; set; }

        public virtual DIENNUOC DIENNUOC { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
