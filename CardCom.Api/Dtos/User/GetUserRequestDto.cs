using System.ComponentModel.DataAnnotations;
using CardCom.Api.Models;

namespace CardCom.Api.Dtos.User;

public class GetUserRequestDto
{

    public int id { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    public string username { get; set; } = string.Empty;


    [Required(ErrorMessage = "Image is required.")]  
    public string image {get; set; } = string.Empty;

    // public ICollection<GetCardRequestDto> Cards { get; set; } = new List<GetCardRequestDto>();

}