using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    public class ThongKe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime  ThoiGian { get; set; }    

        public long SoLuotTruyCap { get; set; } 


    }
}