using System.ComponentModel.DataAnnotations;

namespace CardCom.Api.Models;

public class User
{
    public int id { get; set; }

    [Required(ErrorMessage = "googleId is required")]
    public string googleId { get; set; } = string.Empty;

    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Username is required.")]
    public string username { get; set; } = string.Empty;


    [Required(ErrorMessage = "Email is required")]
    public string email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Image is required.")]
    public string image { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "The value for money must be a positive number.")]
    public int money { get; set; } = 0;

    public DateTime roolCooldown { get; set; } = DateTime.UtcNow;

    public ICollection<Card> Cards { get; set; } = new List<Card>();

    public ICollection<Pack> Packs { get; set; } = new List<Pack>();

    public ICollection<Collection> Collections { get; set; } = new List<Collection>();

}