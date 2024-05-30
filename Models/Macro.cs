using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Xml.Serialization;

namespace ShoppingList.Models
{
    public class Macro
    {
        [Key]
        public int MacroID { get; set; }

        [Display(Name = "Macro: ")]
        public string MacroName { get; set; } 
        public ICollection<ProductMacro>? ProductMacros { get; set; }

    }
}