

using CardCom.Api.Data;
using CardCom.Api.Dtos.Card;
using CardCom.Api.Interfaces;
using CardCom.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace CardCom.Api.Repositories;

public class CardRepository : ICardRepository
{

    private readonly AppDbContext _context;

    private readonly ILogger<CardRepository> _logger;
    public CardRepository(AppDbContext context, ILogger<CardRepository> logger)
    {
        _context = context;
        _logger = logger;
    }




    public async Task<List<Card>> GetAllAsync()
    {
        try
        {
            // return await _context.Cards.ToListAsync();
            var cards = await _context.Cards.AsQueryable()
                .OrderByDescending(c => c.createdAt)
                .Take(5)
                .ToListAsync();

            return cards;




        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all cards from the database.");
            throw;
        }

    }



    public async Task<Card?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Cards.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching one card from the database.");
            throw;
        }
    }



    public async Task<Card> CreateAsync(Card cardModel)
    {
        try
        {
            await _context.Cards.AddAsync(cardModel);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while inserting a card in the database.");
            throw;
        }



        return cardModel;
    }




    public async Task<Card?> UpdateAsync(int id, UpdateCardRequestDto cardDto)
    {
        try
        {
            var cardModel = await _context.Cards.FirstOrDefaultAsync(x => x.id == id);

            if (cardModel == null)
            {
                return null;
            }

            cardModel.price = cardDto.price;
            cardModel.description = cardDto.description;

            await _context.SaveChangesAsync();

            return cardModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a card in the database.");
            throw;
        }






    }

    public async Task<Card?> DeleteAsync(int id)
    {

        try
        {
            var cardModel = await _context.Cards.FirstOrDefaultAsync(x => x.id == id);

            if (cardModel == null)
            {
                return null;
            }

            _context.Cards.Remove(cardModel);
            await _context.SaveChangesAsync();

            return cardModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a card in the database.");
            throw;
        }



    }




}