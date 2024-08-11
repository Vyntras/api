


using Google.Apis.Auth;
using CardCom.Api.Models;
using CardCom.Api.Dtos.User;

namespace CardCom.Api.Interfaces;

public interface IUserRepository
{

    Task<GetUserRequestDto> ValidateGoogleToken(string tokenId);

    Task<string> ValidateGoogleCode(string code);

    // Task<User> FindOrCreateUserAsync(GoogleJsonWebSignature.Payload payload);
}