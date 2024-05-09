using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}