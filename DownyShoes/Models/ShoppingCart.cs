using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownyShoes.Models
{
    public class ShoppingCart
    {
        private int id;
        private string hinhanh, tensanpham;
        private int gia,soluong;

        public string Hinhanh { get => hinhanh; set => hinhanh = value; }
        public string Tensanpham { get => tensanpham; set => tensanpham = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public int Gia { get => gia; set => gia = value; }
        public int Id { get => id; set => id = value; }
    }
}