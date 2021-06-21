namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
       
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public int CategoryID { get; set; }

        public decimal UnitPrice { get; set; }

        public int? SaleDetailID { get; set; }

        public int? Amount { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageURL { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Detail { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

       
    }
}
