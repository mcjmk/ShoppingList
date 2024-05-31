using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingList.Models.Product> Product { get; set; } = default!;

        public DbSet<ShoppingList.Models.Macro> Macro { get; set; } = default!;

        public DbSet<ShoppingList.Models.ProductMacro> ProductMacro { get; set; } = default!;

        public DbSet<ShoppingList.Models.User> User { get; set; } = default!;

        public DbSet<ShoppingList.Models.UserShoppingList> UserShoppingList { get; set; } = default!;

        public DbSet<ShoppingList.Models.ListContent> ListContent { get; set; } = default!;
    }
}