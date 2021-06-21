namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vote
    {
        public int VoteID { get; set; }

        public int CustomerID { get; set; }

        public int ProductID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
