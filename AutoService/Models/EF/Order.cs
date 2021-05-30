namespace AutoService.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderSpareparts = new HashSet<OrderSparepart>();
            OrderTypeOfWorks = new HashSet<OrderTypeOfWork>();
        }

        public int OrderId { get; set; }

        public int? EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        public string CarNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string Reason { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime Date { get; set; }

        public virtual Car Car { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderSparepart> OrderSpareparts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTypeOfWork> OrderTypeOfWorks { get; set; }
    }
}
