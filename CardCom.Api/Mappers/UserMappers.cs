// using CardCom.Api.Dtos.User;
// using CardCom.Api.Models;

// namespace CardCom.Api.Mappers;

// public static class UserMappers
// {

//     public static GetUserRequestDto GetUserFromDto(User user)
//     {
//         return new GetUserRequestDto
//         {
//             username = user.username,
//             image = user.image,
//             Cards = user.Cards.Select(c => new GetCardRequestDto
//             {
//                 id = c.id,
//                 name = c.name,
//                 description = c.description,
//                 price = c.price,
//                 createdAt = c.createdAt
//             }).ToList()
//         };
//     }
// }