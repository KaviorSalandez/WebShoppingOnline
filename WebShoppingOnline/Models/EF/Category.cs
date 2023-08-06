using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
       // Mục đích của việc khởi tạo News là để sẵn sàng lưu trữ danh sách các đối tượng New mà thuộc về đối tượng Category tương ứng.
       // Bằng cách sử dụng HashSet, chúng ta có thể quản lý các đối tượng New một cách hiệu quả 
       // và đảm bảo không có phần tử trùng lặp trong tập hợp.
        public Category()
        {
            this.News = new HashSet<New>();
            this.Posts = new HashSet<Post>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên danh mục không được để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        [StringLength(150)]
        public string SeoTitle { get; set; }//seo là từ khóa để google tìm kiếm
        [StringLength(150)]
        public string SeoDescription { get; set; }
        [StringLength(150)]
        public string SeoKeywords { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}