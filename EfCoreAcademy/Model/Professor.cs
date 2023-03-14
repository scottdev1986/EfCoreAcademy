namespace EfCoreAcademy.Model;

public class Professor : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public IEnumerable<Class> Classes { get; set; } = default!;
    public Address Address { get; set; } = default!;
}