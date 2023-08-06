using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShoppingOnline.Models
{
    public class ShoppingCart
    {
        // khởi tạo 1 list gồm các cartitem
        public List<ShoppingCartItem> Items { get; set; }   
        public ShoppingCart() { 
            this.Items = new List<ShoppingCartItem>();   
        }
        public void AddToCart(ShoppingCartItem item, int Quantity)
        {
            // cần kiểm tra sản phẩm đã có trong list này chưa
            var checkExists = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExists != null)
            {
                checkExists.Quantity += Quantity;
                checkExists.PriceTotal  = checkExists.Quantity * checkExists.Price;
            }
            else
            {
                Items.Add(item);
            }
        }

        // hàm xóa 1 item trong giỏ hàng
        public void Remove(int id) {
            var checkExists = Items.SingleOrDefault(x => x.ProductId == id);
            if(checkExists != null)
            {
                Items.Remove(checkExists);
            }
        }

        // hàm cập nhật đơn hàng
        public void UpdateQuantity(int id, int Quantity)
        {
            var checkExists = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExists != null)
            {
                checkExists.Quantity=Quantity;
                checkExists.PriceTotal = checkExists.Price * checkExists.Quantity;
            }
        }
        // hàm lấy ra tổng số tiền của đơn hàng
        public decimal GetPriceTotal()
        {
            return Items.Sum(x => x.PriceTotal);
        }
        // lấy ra tổng số lượng
        public decimal GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }

        // xóa sạch giỏ hàng
        public void ClearCart()
        {
            Items.Clear();
        }

    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set;}
    }
}