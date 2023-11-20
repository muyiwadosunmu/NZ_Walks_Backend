using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.Domain.DTOs;
using NZWalks_API.Models.DTOs;
using NZWalks_API.Repositories;
using AutoMapper;
using NZWalks_API.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace NZWalks_API.Controllers
{
    // https://localhost:1234/api/
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;
        //////////////////////

        public RegionsController(IRegionRepository regionRepository,
        IMapper mapper, ILogger<RegionsController> logger)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        /*GET ALL REGIONS*/
        [HttpGet]
        // [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                // throw new Exception("This is a custom exception");
                // Get Data From Database - Domain models
                var regionsDomain = await _regionRepository.GetAllAsync();
                /**Map Domain Models to DTOs*/
                var regionsDto = _mapper.Map<List<RegionDTO>>(regionsDomain);
                //Return DTO
                _logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(regionsDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /* GET SINGLE REGION (GET Region by Id*/
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Data From Database - Domain models
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            // var region = _dbContext.Regions.Find(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // Map/Convert RegionDomain to RegionDto
            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            // Return DTO back to client
            return Ok(regionDto);
        }

        // POST - To Create a new Region
        // POST: https://localhost:portNumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] AddRegionReqDto createRegion)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(createRegion);
            // Use Domain model to create region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
            // We need to the Mapping again
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        // Update Region
        // PUT: https://localhost:portNumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionReqDto updateRegionDto)
        {
            // Map DTO to Domain model
            var regionDomainModel = _mapper.Map<Region>(updateRegionDto);
            // Fetch region from DB using id
            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            // Convert domain model to DTO
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);

        }


        // DELETE https://localhost:portNumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // If returning the deleted region
            // Map Domain model to DTO
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);


        }
    }
}