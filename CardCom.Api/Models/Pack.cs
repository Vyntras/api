using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCom.Api.Models;

public class Pack
{

    public int id { get; set; }

    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal price { get; set; }

    public bool onSale {get; set;} = false;

    [Range(0, 1, ErrorMessage = "The value must be between 0 and 1.")]
    [RegularExpression(@"^0(\.\d{1,3})?$|^1(\.0{1,3})?$", ErrorMessage = "The value must be between 0 and 1 with up to 3 decimal places.")]
    public decimal rarity { get; set; }

    [Required]
    public int ownerId { get; set; }

    [Required]
    public User Owner { get; set; } = null!;
}