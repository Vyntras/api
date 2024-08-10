using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCom.Api.Models;

public class Collection
{

    public int id { get; set; }

    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string name { get; set; } = string.Empty;

    [MaxLength(255, ErrorMessage = "Description cannot be longer than 255 characters.")]
    public string? description { get; set; } = string.Empty;

    [Range(0, 1, ErrorMessage = "The value must be between 0 and 1.")]
    [RegularExpression(@"^0(\.\d{1,3})?$|^1(\.0{1,3})?$", ErrorMessage = "The value must be between 0 and 1 with up to 3 decimal places.")]
    public decimal rarity { get; set; }

    [Required]
    public int creatorId { get; set; }

    // Navigation property to represent the user who owns the card
    [Required]
    public User Creator { get; set; } = null!;

    public ICollection<Card> Cards { get; set; } = new List<Card>();
}