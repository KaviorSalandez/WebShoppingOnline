using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    [Table("tb_Adver")]
    public class Adver:CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title  { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
    }
}