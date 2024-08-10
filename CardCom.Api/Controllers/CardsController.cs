using CardCom.Api.Data;
using CardCom.Api.Models;
// using CardCom.Api.Dtos.Card;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardCom.Api.Interfaces;

namespace CardCom.Api.Controllers;

[ApiController]
[Route("api/cards")]
public class CardsController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly ILogger<CardsController> _logger;

    private readonly ICardRepository _cardRepo;

    private readonly IUserRepository _userRepo;
    public CardsController(AppDbContext context, ILogger<CardsController> logger, ICardRepository cardRepo, IUserRepository userRepo)
    {
        _context = context;
        _logger = logger;
        _cardRepo = cardRepo;
        _userRepo = userRepo;

    }







    [HttpGet(Name = "GetCards")]
    public async Task<ActionResult<IEnumerable<Card>>> GetList()
    {

        try
        {
            var cards = await _cardRepo.GetAllAsync();
            _logger.LogInformation("Cards retrived");
            return Ok(cards);
        }
        catch (Exception ex)
        {
            string message = "An unexpected error occured while retriving all the cards";
            _logger.LogError(ex, message, HttpContext.TraceIdentifier);
            return StatusCode(500, message);
        }


    }






    [HttpGet("{id:int}", Name = "GetCardById")]
    public async Task<ActionResult<Card>> GetById([FromRoute] int id)
    {
        try
        {
            var card = await _cardRepo.GetByIdAsync(id);

            if (card == null)
            {
                _logger.LogError($"Card id:{id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Card id:{id} retrived");
            return Ok(card);
        }
        catch (Exception ex)
        {
            string message = $"An unexpected error occured while retriving the card id:{id}";
            _logger.LogError(ex, message, HttpContext.TraceIdentifier);
            return StatusCode(500, message);
        }


    }







    // [HttpPost(Name = "CreateCard")]
    // public async Task<IActionResult> Create([FromBody] CreateCardRequestDto card)
    // {
    //     try
    //     {
    //         var token = Request.Cookies["auth_token"];
    //         if (token == null) return Unauthorized();
    //         var user = await _userRepo.ValidateGoogleToken(token);
            
            
    //         // if (!ModelState.IsValid) return BadRequest(ModelState);
    //         // Console.WriteLine(user.id);
    //         // var cardModel = card.CreateCardFromDto(user.id);
    //         // // await _context.Cards.AddAsync(cardModal);
    //         // // await _context.SaveChangesAsync();
    //         // await _cardRepo.CreateAsync(cardModel);
            

    //         // _logger.LogInformation($"Card id:{cardModel.id} created");
    //         // return CreatedAtAction(nameof(GetById), new { id = cardModel.id }, card);
    //         return Ok(user);
    //     }
    //     catch (Exception e)
    //     {
    //         string message = $"An unexpected error occured while creating a card";
    //         _logger.LogError(e, message, card);
    //         return StatusCode(500, message);
    //     }


    // }






    // [HttpPut("{id:int}", Name = "UpdateCard")]
    // public async Task<ActionResult<Card>> Update([FromRoute] int id, [FromBody] UpdateCardRequestDto cardUpdateDto)
    // {

    //     if (!ModelState.IsValid) return BadRequest(ModelState);

    //     try
    //     {
    //         var cardModel = await _context.Cards.FirstOrDefaultAsync(x => x.id == id);

    //         if (cardModel == null)
    //         {
    //             return NotFound();
    //         }

    //         cardModel.price = cardUpdateDto.price;
    //         cardModel.description = cardUpdateDto.description;

    //         await _context.SaveChangesAsync();

    //         _logger.LogInformation($"Card id:{id} updated");
    //         return Ok(cardModel);
    //     }
    //     catch (Exception e)
    //     {
    //         string message = $"An unexpected error occured while updating the card id:{id}";
    //         _logger.LogError(e, message, cardUpdateDto);
    //         return StatusCode(500, message);
    //     }



    // }





    // [HttpDelete("{id}", Name = "DeleteCard")]
    // public async Task<IActionResult> Delete([FromRoute] int id)
    // {
    //     try
    //     {
    //         var cardModel = await _context.Cards.FirstOrDefaultAsync(x => x.id == id);

    //         if (cardModel == null)
    //         {
    //             return NotFound();
    //         }

    //         _context.Cards.Remove(cardModel);
    //         await _context.SaveChangesAsync();

    //         _logger.LogInformation($"Card id:{id} deleted");
    //         return NoContent();
    //     }
    //     catch (Exception e)
    //     {
    //         string message = $"An unexpected error occured while deleting the card id:{id}";
    //         _logger.LogError(e, message);
    //         return StatusCode(500, message);
    //     }


    // }
}