using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCom.Api.Dtos.Card;

public class UpdateCardRequestDto
{
    [MaxLength(255, ErrorMessage = "Description cannot be longer than 255 characters.")]
    public string description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal price { get; set; }
}