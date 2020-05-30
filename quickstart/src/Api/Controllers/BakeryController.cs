using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IndexDTO> GetCatalog()
        {
            var indexDTO = new IndexDTO()
            {
                AllBakeryTypes = await _repository.GetAllBakeryTypes(),
                Bakeries = await _repository.GetAll()
            };

            return indexDTO;
        }

        [HttpGet("listbakery")]
        public async Task<IEnumerable<Bakery>> GetListBakery(string tabtype)
        {
            var listBakery = await _repository.GetAll(x => x.IdtypeNavigation.Name == tabtype);
            return listBakery;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bakery>> GetBakery(int id)
        {
            var bakery = await _repository.GetBy(id);

            if (bakery == null)
            {
                return NotFound();
            }

            return bakery;
        }
    }
}