using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    public class CommonAbstract
    {
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}