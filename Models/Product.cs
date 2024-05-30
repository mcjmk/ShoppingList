using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Models
{
    public class Product
    { 
        [Key]
        public int ProductId { get; set; }

        [Required] 
        public string NameProduct { get; set; }

        [Required]
        [Display(Name = "Cena: ")]
        // [RegularExpression(@"^\d+(\.\d{1,2})?$|^\d+(\,\d{1,2})?$", ErrorMessage = "Please enter a valid number")]

        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Waga: ")]
        public decimal Weight { get; set; }
        public ICollection<Macro> ?Macro { get; set; }
        public ICollection<ListContent> ?ListContents { get; set; }

    }
}