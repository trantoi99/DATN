namespace Electric.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            ImageNews = new HashSet<ImageNew>();
        }

        [Key]
        public int NewID { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageURL { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Detail { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageNew> ImageNews { get; set; }
    }
}
