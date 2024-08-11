

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

    private readonly IConfiguration _configuration;


    public AuthController(ILogger<CardsController> logger, IUserRepository userRepo, IConfiguration configuration)
    {
        _logger = logger;
        _userRepo = userRepo;
        _configuration = configuration;

    }

    public class TokenRequest
    {
        public string tokenId { get; set; } = string.Empty;
    }


    // [HttpPost(Name = "Authenticate")]
    // public async Task<IActionResult> Authenticate([FromBody] TokenRequest tokenRequest)
    // {
    //     try
    //     {
    //         Console.WriteLine("Validating...");
    //         // var payload = await ValidateGoogleToken(tokenRequest.TokenId);
    //         var user = await _userRepo.ValidateGoogleToken(tokenRequest.tokenId);

    //         // Console.WriteLine(payload);
    //         // Do something with the payload, e.g., create or find a user in your database
    //         // Console.WriteLine(payload.Name);
    //         // Console.WriteLine(payload.Email);
    //         // var user = await _userRepo.FindOrCreateUserAsync(payload);
    //         // Console.WriteLine(user);
    //         return Ok(user);
    //     }
    //     catch (InvalidJwtException ex)
    //     {
    //         Console.WriteLine(ex);
    //         return BadRequest(new { message = "Invalid token." });
    //     }
    // }


    [HttpGet("callback", Name = "Callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        Console.WriteLine(code);


        try
        {
            var token = await _userRepo.ValidateGoogleCode(code);
            // var user = await _userRepo.ValidateGoogleToken(token);

            // Console.WriteLine(user);

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine(environment);

            var cookieOptions = new CookieOptions
            {
                Domain = "localhost", // Use "localhost" to ensure the cookie is set for the client
                Path = "/",
                HttpOnly = true,  // Set HttpOnly to true if you don't need access via JavaScript
                Secure = environment == "Production" ? true:false,   // Secure should be false because you are on HTTP in development
                SameSite = environment == "Production" ? SameSiteMode.Strict:SameSiteMode.Lax, // Necessary to allow cross-origin cookies
                Expires = DateTimeOffset.UtcNow.AddHours(1) // Optional: set an appropriate expiration
            };

            Response.Cookies.Append("auth_token", token, cookieOptions);

            return Redirect("http://localhost:3000");

            // return Ok(user);
        }
        catch (Exception ex)
        {
            string message = $"An unexpected error occured while signing in";
            _logger.LogError(ex, message, HttpContext.TraceIdentifier);
            return StatusCode(500, message);
        }




        // var cookieOptions = new CookieOptions
        // {
        //     Domain = "localhost", // Use "localhost" to ensure the cookie is set for the client
        //     Path = "/",
        //     HttpOnly = true,  // Set HttpOnly to true if you don't need access via JavaScript
        //     Secure = false,   // Secure should be false because you are on HTTP in development
        //     SameSite = SameSiteMode.Lax, // Necessary to allow cross-origin cookies
        //     Expires = DateTimeOffset.UtcNow.AddHours(1) // Optional: set an appropriate expiration
        // };

        // Response.Cookies.Append("YourCookieName", "YourCookieValue", cookieOptions);

        // return Redirect("http://localhost:3000");





    }



}