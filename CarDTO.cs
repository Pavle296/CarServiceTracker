public class CarDTO
{
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required int YearOfManufacture { get; set; }
    public required int EngineCapacity { get; set; } // in cc
    public required int EnginePower { get; set; } // in HP
    public required FuelType FuelType { get; set; }
    public required CurrentStatus Status { get; set; }
    public string? RegistrationPlate { get; set; }
    public required int CustomerId { get; set; }

}
