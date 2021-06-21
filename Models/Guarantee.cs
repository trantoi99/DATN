namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Guarantee")]
    public partial class Guarantee
    {
        public int GuaranteeID { get; set; }

        public int? OrderDetailID { get; set; }

        public int? ProductID { get; set; }

        public int? Product_Number { get; set; }

        public Guid? Serial_Number { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }

        public virtual Product Product { get; set; }
    }
}
