using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Models
{
    public class UserShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }
        
        [Required]
        public String ShoppingListName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }
        public ICollection<ListContent>? ListContents { get; set; }
    }
}
