using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    [Table("tb_New")]
    public class New : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên tin tức không được để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        // alias để lưu lại thành 1 đường link URL
        public string Alias { get; set; }
        [Required(ErrorMessage = "Cần chọn loại danh mục")]

        public int CategoryID { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }
    } 
}