using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Versioning.Models.Domain;

namespace WebAPI_Versioning
{
    public class CountriesData
    {
        public static List<Country> Get()
        {
            var countries = new[] {
                new {Id = 1, Name = "United States"},
                new {Id = 2, Name = "United Kingdom"},
                new {Id = 3, Name = "Germany"},
                new {Id = 4, Name = "Brazil"},
                new {Id = 5, Name = "Nigeria"},
                new {Id = 6, Name = "China"},
                new {Id = 7, Name = "Portugal"},
                new {Id = 8, Name = "France"},
                new {Id = 9, Name = "Poland"},
                new {Id = 10, Name = "Austria"},
            };
            return countries.Select(c => new Country { Id = c.Id, Name = c.Name }).ToList();
        }

    }
}