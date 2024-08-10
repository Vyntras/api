

using Microsoft.AspNetCore.Mvc;
using CardCom.Api.Data;
using Google.Apis.Auth;
using CardCom.Api.Interfaces;

namespace CardCom.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{


    private readonly ILogger<CardsController> _logger;


    private readonly IUserRepository _userRepo;


    public AuthController(ILogger<CardsController> logger, IUserRepository userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;

    }

    public class TokenRequest
    {
        public string tokenId { get; set; } = string.Empty;
    }


    [HttpPost(Name = "Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] TokenRequest tokenRequest)
    {
        try
        {
            Console.WriteLine("Validating...");
            // var payload = await ValidateGoogleToken(tokenRequest.TokenId);
            var user = await _userRepo.ValidateGoogleToken(tokenRequest.tokenId);
            
            // Console.WriteLine(payload);
            // Do something with the payload, e.g., create or find a user in your database
            // Console.WriteLine(payload.Name);
            // Console.WriteLine(payload.Email);
            // var user = await _userRepo.FindOrCreateUserAsync(payload);
            // Console.WriteLine(user);
            return Ok(user);
        }
        catch (InvalidJwtException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(new { message = "Invalid token." });
        }
    }

}