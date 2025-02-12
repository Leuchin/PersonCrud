namespace Person.Models;

public class PersonModel
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public string Biography { get; set; } = string.Empty;
}