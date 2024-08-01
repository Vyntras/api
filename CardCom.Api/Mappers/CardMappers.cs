using CardCom.Api.Dtos.Card;
using CardCom.Api.Models;

namespace CardCom.Api.Mappers;

public static class CardMappers
{
    public static Card CreateCardFromDto(this CreateCardRequestDto cardDto)
    {
        return new Card
        {
            name = cardDto.name,
            description = cardDto.description,
            price = cardDto.price,
            createdAt = DateTime.UtcNow
        };
    }
}