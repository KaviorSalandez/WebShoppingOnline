using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory:CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên thể loại không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}