using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.Domain.DTOs;

namespace NZWalks_API.Models.DTOs
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        // public Guid DifficultyId { get; set; }
        // public Guid RegionId { get; set; }

        // Navigation Properties
        public DifficultyDto Difficulty { get; set; }
        public RegionDTO Region { get; set; }
    }
}

// We removed Id Properties because we have them already in the DTos itself