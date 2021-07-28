using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Model.DTO.Franchise;
using MovieCharactersAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Controller
{
    [Route("api/franchises")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseRepository _repository;
        private readonly IMapper _mapper;

        public FranchiseController(IMapper mapper, IFranchiseRepository franchiseRepository)
        {
            _repository = franchiseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FranchiseReadDTO>> GetAll()
        {
            var franchises = await _repository.GetAll();
            return _mapper.Map<List<FranchiseReadDTO>>(franchises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetById(int id)
        {
            var franchise = await _repository.GetById(id);
            if(franchise == null)
            {
                return NotFound();
            }
            return _mapper.Map<FranchiseReadDTO>(franchise);
        }





    }
}
