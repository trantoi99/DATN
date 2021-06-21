namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageNew
    {
        [Key]
        public int ImageID { get; set; }

        [StringLength(50)]
        public string ImageName { get; set; }

        public int NewID { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageURL { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public virtual News News { get; set; }
    }
}
