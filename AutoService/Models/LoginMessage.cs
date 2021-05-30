using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoService.Models
{
    public class LoginMessage
    {
        public string Message { get; set; }
        public LoginMessage(string msg)
        {
            Message = msg;
        }
    }
}