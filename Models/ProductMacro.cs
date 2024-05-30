using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Xml.Serialization;

namespace ShoppingList.Models
{
    public class ProductMacro
    {
        [Key]
        public int ProductMacroId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Macro")]
        [Display(Name = "MacroId: ")]
        public int MacroId { get; set; }

        [Display(Name = "Wartosc: ")]
        public decimal Value { get; set; }
        public Product? Product { get; set; }
        public Macro? Macro { get; set; }
    }
}