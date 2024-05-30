using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Models
{
    public class ListContent
    { 
        [Key]
        public int ListContentId { get; set; }

        [Required]
        [ForeignKey("UserShoppingList")]
        public int ShoppingListId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        [Display(Name = "Sztuk: ")]
        public decimal Quantity { get; set; }
        
        public UserShoppingList? UserShoppingList { get; set;}
        public Product? Product { get; set; }

    }
}