using AutoService.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoService.Models
{
    public class OrderInfo
    {
        public Order Order { get; set; }

        public Employee Employee { get; set; }

        public List<TypeOfWork> Works { get; set; }

        public List<Sparepart> Spareparts { get; set; }
    }
}