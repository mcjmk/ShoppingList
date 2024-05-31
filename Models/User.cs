using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Models
{
    public class User 
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public  string UserName { get; set; }

        [Required]
        public string HashedPassword{ get; set; }

        [Required]
        public  bool IsAdmin { get; set; } = false;

        public ICollection<UserShoppingList>? ShoppingLists { get; set; }

    }
}
