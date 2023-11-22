using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks_API.Data
{
    public class NZ_Walks_AuthDB_Context : IdentityDbContext
    {
        public NZ_Walks_AuthDB_Context(DbContextOptions<NZ_Walks_AuthDB_Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "dacd5c5a-5d3d-4264-8aad-3f4ad9d25eb9";
            var writerRoleId = "eced31ae-1b53-4a77-8c45-c26039adeae3";

            var roles = new List<IdentityRole> {
                new IdentityRole {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName  = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}