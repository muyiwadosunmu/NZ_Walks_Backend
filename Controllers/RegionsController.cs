using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Controllers
{
    // https://localhost:1234/api/
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region> {
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://www.pexels.com/photo/the-lightpath-in-the-middle-of-the-freeway-in-auckland-5342974/"
                },
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://www.pexels.com/photo/the-sun-is-setting-over-a-city-and-mountains-18062808/"
                }
            };

            return Ok(regions);
        }
    }
}