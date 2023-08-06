﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models.EF
{
    [Table("tb_Contact")]
    public class Contact: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên không được để trống")]
        [StringLength(150,ErrorMessage ="Không được vượt quá 150 kí tự")]
        public string Name { get; set; }

        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Địa chỉ email không hợp lệ.")]

        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        
    }
}