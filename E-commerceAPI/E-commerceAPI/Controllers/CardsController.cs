using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerceAPI.Model;
using E_commerceAPI.Repository.CardRepository;
using E_commerceAPI.DTO;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _CardRepository;

        public CardsController(ICardRepository CardRepository)
        {
            _CardRepository = CardRepository;
        }

        // GET: api/Category
        [HttpGet]
        //[ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
        public IActionResult GetCards()
        {

            try
            {
                var cards = _CardRepository.GetAll();
                if (cards != null)
                {
                    return Ok(cards);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Card/5
        [HttpGet("{id:int}", Name = "GetCard")]
        public IActionResult GetCard(int id)
        {
            try
            {
                var card = _CardRepository.GetById(id);
                if (card == null)
                {
                    return NotFound();
                }
                return Ok(card);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{UserID}", Name = "GetCardItem")]
        public IActionResult GetCardItem(string UserID)
        {
            try
            {
                var cards = _CardRepository.GetItemsByUserID(UserID);
                if (cards == null)
                {
                    return NotFound();
                }
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: api/card/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public IActionResult PatchCard(int id, Card card)
        {
            if (id != card.ID)
            {
                return BadRequest();
            }
            try
            {
                
                if (_CardRepository.Update(id, card) > 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/card
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCard(CardDTO cardDTO)
        {
            try
            {
                List<ProductQuantity> productQuantities = new List<ProductQuantity>()
                {
                    new ProductQuantity()
                    {
                        ProductID=cardDTO.ProductQuantity.ProductID,
                        Quantity=cardDTO.ProductQuantity.Quantity,
                    }
                };
                Card card = new Card() { ProductQuantity = productQuantities, UserID = cardDTO.UserID, TotalPrice = cardDTO.TotalPrice };
                if (_CardRepository.Insert(card) > 0)
                {
                    //string URL = Url.Link("GetCard", new { id = card.ID });
                    return Ok(card);
                }
                return BadRequest(card);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Card/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCard(int id)
        {
            try
            {
                if (_CardRepository.Delete(id) > 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Data Deleted");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
