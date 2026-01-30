public class UpdateCarDTO
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? YearOfManufacture { get; set; }
    public int? EngineCapacity { get; set; } // in cc
    public int? EnginePower { get; set; } // in HP
    public FuelType? FuelType { get; set; }
    public CurrentStatus? Status { get; set; }
    public string? RegistrationPlate { get; set; }
}
