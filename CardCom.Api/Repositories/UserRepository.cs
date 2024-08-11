using System.Text.Json;
using CardCom.Api.Data;
using CardCom.Api.Dtos.User;
using CardCom.Api.Interfaces;
using CardCom.Api.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CardCom.Api.Repositories;


public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    private readonly ILogger<CardRepository> _logger;

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;
    public UserRepository(AppDbContext context, ILogger<CardRepository> logger, IConfiguration configuration, HttpClient httpClient)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
        _httpClient = httpClient;
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

    private class GoogleTokenResponse
    {
        public string id_token { get; set; } = string.Empty;
    }

    public async Task<string> ValidateGoogleCode(string code)
    {


        // Prepare the request body
        var formData = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", _configuration["Authentication:Google:ClientId"]! },
            { "client_secret", _configuration["Authentication:Google:ClientSecret"]! },
            { "redirect_uri", _configuration["Authentication:Google:RedirectUri"]! },
            { "grant_type", "authorization_code" }
        };

        var requestContent = new FormUrlEncodedContent(formData);

        try
        {


            // Send the POST request
            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", requestContent);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();

            // Read and deserialize the response content
            var data = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonSerializer.Deserialize<GoogleTokenResponse>(responseContent);

            Console.WriteLine(tokenResponse!.id_token);



            return tokenResponse!.id_token;

        }
        catch
        {
            throw;
        }



    }
}