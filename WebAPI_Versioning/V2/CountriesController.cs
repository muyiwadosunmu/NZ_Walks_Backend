using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Versioning.Models.DTOs;

namespace WebAPI_Versioning.V2
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var countriesDomainModel = CountriesData.Get();
            // Map Domain Model to DTO
            var response = new List<CountryDTO>();
            foreach (var countryDomain in countriesDomainModel)
            {
                response.Add(new CountryDTO
                {
                    Id = countryDomain.Id,
                    Name = countryDomain.Name
                });
            }
            return Ok(response);
        }

    }
}