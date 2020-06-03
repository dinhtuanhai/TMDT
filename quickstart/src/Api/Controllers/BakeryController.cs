using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/Bakery")]
    [ApiController]
    [Authorize]
    public class BakeryController : ControllerBase
    {
        private readonly TMDTContext  _context;
        private readonly IBakery _repository;

        public BakeryController(TMDTContext context, IBakery bakeryRepository)
        {
            _repository = bakeryRepository;
            _context = context;
        }

        [HttpGet("catalog")]
        [AllowAnonymous]
        public async Task<IndexDTO> GetCatalog(string bakeryType = "", string searchString = "")
        {
            var indexDTO = new IndexDTO()
            {
                TypeSearch = bakeryType,
                SearchString = searchString,
                AllBakeryTypes = await _repository.GetAllBakeryTypes(),
                Bakeries = await _repository.GetBakeries(bakeryType, searchString)
            };

            return indexDTO;
        }

        [HttpGet("types")]
        public async Task<IEnumerable<BakeryType>> GetTypes()
        {
            var allBakeryTypes = await _repository.GetAllBakeryTypes();

            return allBakeryTypes;
        }

        [HttpGet("listbakery")]
        public async Task<IEnumerable<Bakery>> GetListBakery(string tabtype)
        {
            var listBakery = await _repository.GetAll(x => x.IdtypeNavigation.Name == tabtype);
            return listBakery;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Bakery>> GetBakery(int id)
        {
            var bakery = await _repository.GetBy(id);

            if (bakery == null)
            {
                return NotFound();
            }

            return bakery;
        }

        [HttpGet("type/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BakeryType>> GetType(int id)
        {
            var type = await _context.BakeryType.FindAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        [HttpPost]
        public async Task<ActionResult<Bakery>> Create(Bakery bakery)
        {
            await _repository.Add(bakery);

            return CreatedAtAction(nameof(GetBakery), new { id = bakery.Id }, bakery);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Bakery bakery)
        {
            if (id != bakery.Id)
            {
                return BadRequest();
            }

            var bakerymodify = await _repository.GetBy(id);

            if (bakerymodify == null)
            {
                return NotFound();
            }

            try
            {
                //await _repository.Update(bakery);
                _context.Entry(await _context.Bakery.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(bakery);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("createtype")]
        public async Task<ActionResult<Bakery>> CreateType(BakeryType bakeryType)
        {
            _context.BakeryType.Add(bakeryType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("updatetype/{id}")]
        public async Task<IActionResult> UpdateType(int id, BakeryType bakeryType)
        {
            if (id != bakeryType.Id)
            {
                return BadRequest();
            }

            var typemodify = await _context.BakeryType.FindAsync(id);

            if (typemodify == null)
            {
                return NotFound();
            }

            try
            {
                //await _repository.Update(bakery);
                _context.Entry(await _context.BakeryType.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(bakeryType);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}