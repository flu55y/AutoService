namespace AutoService.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderSparepart
    {
        public int OrderSparepartId { get; set; }

        public int OrderId { get; set; }

        public int SparepartId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Sparepart Sparepart { get; set; }
    }
}
