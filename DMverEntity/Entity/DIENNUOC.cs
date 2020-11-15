namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DIENNUOC")]
    public partial class DIENNUOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIENNUOC()
        {
            HOADON = new HashSet<HOADON>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaDienNuoc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGian { get; set; }

        public int? SoDienCu { get; set; }

        public int? SoNuocCu { get; set; }

        public int? SoDienMoi { get; set; }

        public int? SoNuocMoi { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string MaPhong { get; set; }

        public double? TienDienNuoc { get; set; }

        public virtual PHONGTRO PHONGTRO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADON { get; set; }
    }
}
