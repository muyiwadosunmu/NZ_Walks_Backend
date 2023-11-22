using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NZWalks_API.Models.DTOs
{
    public class RegisterReqDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string[]? Roles { get; set; }
    }
}