using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.Domain.DTOs;
using NZWalks_API.Models.DTOs;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    // https://localhost:1234/api/
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly NZ_Walks_DB_Context _dbContext;
        private readonly IRegionRepository _regionRepository;
        public RegionsController(NZ_Walks_DB_Context dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }
        /*GET ALL REGIONS*/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain models
            var regionsDomain = await _regionRepository.GetAllAsync();

            // Map Domain Models to DTO
            var regionsDto = new List<RegionDTO>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl

                });
            }
            return Ok(regionsDto);
        }

        /* GET SINGLE REGION (GET Region by Id*/
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Data From Database - Domain models
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            // var region = _dbContext.Regions.Find(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // Map/Convert RegionDomain to RegionDto
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return DTO back to client
            return Ok(regionDto);
        }

        // POST - To Create a new Region
        // POST: https://localhost:portNumber/api/regions
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionReqDto createRegion)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = createRegion.Code,
                Name = createRegion.Name,
                RegionImageUrl = createRegion.RegionImageUrl,
            };
            // Use Domain model ro create region
            await _dbContext.Regions.AddAsync(regionDomainModel);
            await _dbContext.SaveChangesAsync();

            // We need to the Mapping again
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

        }

        // Update Region
        // PUT: https://localhost:portNumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionReqDto updateRegionDto)
        {
            // Check if region exists from Database
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map DTO to Domain model
            regionDomainModel.Code = updateRegionDto.Code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            // NB -> We do not need to Add the context because, it's already tracked when we fetched it by id
            await _dbContext.SaveChangesAsync();

            // Convert domain model to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);

        }


        // DELETE https://localhost:portNumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete if it exists
            _dbContext.Regions.Remove(regionDomainModel);
            await _dbContext.SaveChangesAsync();

            // If returning the deleted region
            // Map Domain model to DTO

            return Ok();


        }
    }
}