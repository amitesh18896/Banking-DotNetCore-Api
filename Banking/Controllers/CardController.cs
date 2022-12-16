using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Data;
using Banking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly cardDbContext cardsDbcontext;
        public CardController(cardDbContext cardsDbcontext)
        {
            this.cardsDbcontext = cardsDbcontext;
        }
        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCard()
        {
            var cards = await cardsDbcontext.cards.ToListAsync();
            return Ok(cards);
        }
        //Get Singal cxard
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("Getcard")]
        public async Task<IActionResult> Getcard([FromRoute]Guid id)
        {
            var card = await cardsDbcontext.cards.FirstOrDefaultAsync(x => x.id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("card not found");
        }
        //Add card
        [HttpPost]
        public async Task<IActionResult> Addcard([FromBody] Card card)
        {
            card.id = Guid.NewGuid();
            await cardsDbcontext.cards.AddAsync(card);
            await cardsDbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(Getcard), new { id = card.id }, card);
        }
        //updating A Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Updatecard([FromRoute]Guid id, [FromBody]Card card)
        {

            var existingcard = await cardsDbcontext.cards.FirstOrDefaultAsync(x => x.id == id);
            if (existingcard != null)
            {
                existingcard.CardholderName = card.CardholderName;
                existingcard.CardNumber = card.CardNumber;
                existingcard.ExpiryMonth = card.ExpiryMonth;
                existingcard.ExpiryYear = card.ExpiryYear;
                existingcard.CVC = card.CVC;
                await cardsDbcontext.SaveChangesAsync();
                return Ok(existingcard);
            }
           
            return NotFound("card not found");
        }

        //Delete a card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute]Guid id)
        {
            var existingcard = await cardsDbcontext.cards.FirstOrDefaultAsync(x => x.id == id);
            if (existingcard != null)
            {
                cardsDbcontext.Remove(existingcard);
                await cardsDbcontext.SaveChangesAsync();
                return Ok(existingcard);
            }
            return NotFound("card not found");
        }
    }
}