namespace DMverEntity.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [Key]
        [StringLength(20)]
        public string TenTaiKhoan { get; set; }

        [StringLength(10)]
        public string MatKhau { get; set; }

        [StringLength(45)]
        public string Email { get; set; }
    }
}
