using Microsoft.EntityFrameworkCore;
using Person.Models;
namespace Person.data;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options)
    {
    }
    //Criação de uma tabela chamada Persons
    public DbSet<PersonModel> Persons { get; set; }
}
