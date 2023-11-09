using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.CustomActionFilters;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTOs;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    // /api/walks
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {

            this._mapper = mapper;
            this._walkRepository = walkRepository;

        }

        [HttpGet]
        // Get Walks
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            // Get Data From Database - Domain models
            var walksDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
            /**Map Domain Models to DTOs*/
            //Return DTO
            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        // Create Walks
        public async Task<IActionResult> Create([FromBody] AddWalkReqDto addWalkReqDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = _mapper.Map<Walk>(addWalkReqDto);

            await _walkRepository.CreateAsync(walkDomainModel);
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
            // return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }


        // Get Walk By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);
            // var region = _dbContext.Regions.Find(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map/Convert walkDomainModel to WalkDto
            // Return DTO back to client
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkReqDto updateWalkReqDto)
        {
            // Map DTO to a Domain Model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkReqDto);
            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Convert domain model to DTO
            // return Ok to client
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));

        }

        // Delete a Walk By Id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalksDomainModel = await _walkRepository.DeleteAsync(id);

            if (deletedWalksDomainModel == null)
            {
                return NotFound();
            }
            // If returning the deleted region
            // Map Domain model to DTO
            // var regionDto = _mapper.Map<Walk>(deletedWalksDomainModel);
            return Ok(_mapper.Map<Walk>(deletedWalksDomainModel));

        }
    }
}