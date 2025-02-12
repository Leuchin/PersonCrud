using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
        }

        public DbSet<PersonModel> Persons => Set<PersonModel>();
    }
}
