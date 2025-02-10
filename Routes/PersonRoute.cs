using Microsoft.EntityFrameworkCore;
using Person.data;
using Person.Models;
namespace Person.Routes;

/*Person Routes e uma classe criada para
Não pesar os dados no program.cs, além disso ele e uma rota
onde recebe os dados de pessoas(person)*/

public static class PersonRoute{
    public static void PersonRoutes(this WebApplication app){
        var routes = app.MapGroup("person");
        //Post da pessoa
        routes.MapPost("", async (PersonRequest req, PersonContext context) => 
            {
                var person = new PersonModel(req.Name);
                await context.AddAsync(person);
                await context.SaveChangesAsync();
            });
            //Get da pessoa
       routes.MapGet("", async (PersonContext context)=> {
            var Persons = await context.Persons.ToListAsync();
            return Results.Ok(Persons);
       });
         //Get da pessoa por Id
        routes.MapPut("{id:guid}", 
        async (Guid id, PersonRequest req, PersonContext context) =>
        {
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return Results.NotFound();
            }
            person.ChangeName(req.Name);
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });
    }
} 