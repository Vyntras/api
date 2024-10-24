using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCom.Api.Models;

public class Card
{

    public int id { get; set; }

    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal price { get; set; }

    public bool onSale {get; set;} = false;

    [Required]
    [Range(0, 1, ErrorMessage = "The value must be between 0 and 1.")]
    [RegularExpression(@"^0(\.\d{1,3})?$|^1(\.0{1,3})?$", ErrorMessage = "The value must be between 0 and 1 with up to 3 decimal places.")]
    public decimal condition { get; set; }

    [Required]
    public int collectionId {get; set;}

    [Required]
    public Collection Collection { get; set; } = null!;

    [Required]
    public int ownerId { get; set; }

    // Navigation property to represent the user who owns the card
    [Required]
    public User Owner { get; set; } = null!;
}