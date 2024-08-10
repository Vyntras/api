

// using CardCom.Api.Dtos.Card;
using CardCom.Api.Models;

namespace CardCom.Api.Interfaces;

public interface ICardRepository
{
    Task<List<Card>> GetAllAsync();

    Task<Card?> GetByIdAsync(int id);

    // Task<Card> CreateAsync(Card cardModel);

    // Task<Card?> UpdateAsync(int id, UpdateCardRequestDto cardDto);

    // Task<Card?> DeleteAsync(int id);
}