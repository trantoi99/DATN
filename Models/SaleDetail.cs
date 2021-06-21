namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SaleDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleDetailID { get; set; }

        public int? SaleID { get; set; }

        public int? ProductID { get; set; }

        public virtual Product Product { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
