namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Imei")]
    public partial class Imei
    {
        public int Id { get; set; }

        [Column("Imei")]
        [Required]
        [StringLength(50)]
        public string Imei1 { get; set; }

        public int OrderDeatilID { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}
