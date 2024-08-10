using CardCom.Api.Data;
using CardCom.Api.Dtos.User;
using CardCom.Api.Interfaces;
using CardCom.Api.Models;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
namespace CardCom.Api.Repositories;


public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    private readonly ILogger<CardRepository> _logger;

    private readonly IConfiguration _configuration;
    public UserRepository(AppDbContext context, ILogger<CardRepository> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }
    private async Task<GetUserRequestDto> FindOrCreateUserAsync(GoogleJsonWebSignature.Payload payload)
    {
        try
        {
            var user = await _context.Users.Include(u => u.Cards).SingleOrDefaultAsync(u => u.googleId == payload.Subject);

            if (user == null)
            {

                user = new User
                {
                    googleId = payload.Subject,
                    email = payload.Email,
                    username = payload.Name,
                    image = payload.Picture
                };
                // const userModel = CreateUserFromDto()

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // user
            }
            // var userModel = user.G
            var userDto = new GetUserRequestDto
            {
                id = user.id,
                username = user.username,
                image = user.image,
                // Cards = user.Cards.Select(c => new GetCardRequestDto
                // {
                //     id = c.id,
                //     name = c.name,
                //     description = c.description,
                //     price = c.price,
                //     createdAt = c.createdAt
                // }).ToList()
            };
            

            // var userDto = GetUse


            return userDto;
        }
        catch
        {
            throw;
        }



    }

    public async Task<GetUserRequestDto> ValidateGoogleToken(string tokenId)
    {
        var GoogleAuthClientId = _configuration["Authentication:Google:ClientId"];
        // Console.WriteLine(GoogleAuthClientId);
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>() { GoogleAuthClientId! } // Add your Google client ID here
        };

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, settings);

            var user = await FindOrCreateUserAsync(payload);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while authenticating");
            throw;
        }



    }
}