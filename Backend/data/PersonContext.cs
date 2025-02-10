using Microsoft.EntityFrameworkCore;
using Person.Models;
namespace Person.data;

public class PersonContext : DbContext
{
    //Criação de uma tabela chamada Persons
    public DbSet<PersonModel> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=person.sqlite");
        base.OnConfiguring(optionsBuilder);
    }  
}
