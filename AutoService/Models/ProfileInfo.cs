using AutoService.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoService.Models
{
    public class ProfileInfo
    {
        public Owner Owner { get; set; }
        public List<Order> Orders{ get; set; }
    }
}