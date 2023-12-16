using EisntFlix.Data.Access.DbContext;
using EisntFlix.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Data.Access.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        { _context = context; }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).Include(n => n.Serie).ToList());
        }

        public double GetShoppingCartTotal() => (double)_context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount + n.Serie.Price * n.Amount).Sum();
    }
}
