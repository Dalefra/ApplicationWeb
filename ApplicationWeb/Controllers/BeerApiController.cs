using ApplicationWeb.DTOs;
using ApplicationWeb.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BeerApiController : ControllerBase
    {
        private StoreContext _storeContext;
        private IValidator<BeerInsertDto> _beerInsertValidator;

        public BeerApiController(StoreContext storeContext, IValidator<BeerInsertDto> beerInsertValidator) 
        {
            _storeContext = storeContext;
            _beerInsertValidator = beerInsertValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get()
        {
            return await _storeContext.Beers.Select(x => new BeerDto
            {
                Id = x.BeerId,
                Name = x.Name,
                Alcohol = x.Alcohol,
                BrandId = x.BrandId,
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetId(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId,
            };

            return Ok(beerDto);

        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var berr = new Beer()
            {
                Name = beerInsertDto?.Name,
                BrandId = beerInsertDto.BrandId,
                Alcohol = beerInsertDto.Alcohol
            };

            await _storeContext.AddAsync(berr);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = berr.BeerId,
                Name = berr.Name,
                Alcohol = berr.Alcohol,
                BrandId = berr.BrandId,
            };

            return CreatedAtAction(nameof(GetId), new { id = berr.BeerId }, beerDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandId = beerUpdateDto.BrandId;

            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId,
            };

            return Ok(beerDto);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _storeContext.Beers.Remove(beer);
            await _storeContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
