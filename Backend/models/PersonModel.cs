namespace Person.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public string Biography { get; set; } = default!;
    }
}