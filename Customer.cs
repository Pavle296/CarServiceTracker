

using System.Text.Json.Serialization;

public class Customer
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; } = null;
    public string? Address { get; set; } = null;

    public List<Car> Cars { get; set; } = new List<Car>();
}
