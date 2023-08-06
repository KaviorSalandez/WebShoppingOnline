using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    [Table("tb_Product")]
    public class Product:CommonAbstract
    {   
        public Product() {
            this.ProductImage = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }   
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }

        public string Image { get; set; }

        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int Quantity { get; set; }
            
        public int ViewCount { get; set; }
        // muốn cho nó hiển thị ở home hay không
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<ProductImage> ProductImage { get; set; } 
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
    
        

    }

}