namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHONGTRO")]
    public partial class PHONGTRO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONGTRO()
        {
            DIENNUOC = new HashSet<DIENNUOC>();
            HOPDONG = new HashSet<HOPDONG>();
        }

        [Key]
        [StringLength(5)]
        public string MaPhong { get; set; }

        public int? MaTrangThai { get; set; }

        [StringLength(8)]
        public string TenPhong { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        public double? DienTich { get; set; }

        public int? SoNguoiO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIENNUOC> DIENNUOC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOPDONG> HOPDONG { get; set; }

        public virtual TRANGTHAIPHONG TRANGTHAIPHONG { get; set; }
    }
}
