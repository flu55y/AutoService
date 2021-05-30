namespace AutoService.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderTypeOfWork
    {
        public int OrderTypeOfWorkId { get; set; }

        public int OrderId { get; set; }

        public int TypeOfWorkId { get; set; }

        public virtual Order Order { get; set; }

        public virtual TypeOfWork TypeOfWork { get; set; }
    }
}
