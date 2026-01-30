using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
    public enum FuelType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }
    public enum CurrentStatus
    {
        Waiting,      
        UnderRepair, 
        Done          
    }
public class Car
{
    public int Id { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required int YearOfManufacture { get; set; }
    public required int EngineCapacity { get; set; } // in cc
    public required int EnginePower { get; set; } // in HP
    public required FuelType FuelType { get; set; }
    public required CurrentStatus Status { get; set; }
    public string? RegistrationPlate { get; set; } = null;

    public int CustomerId { get; set; }  
    public Customer Customer { get; set; } = null!; 
    public List<Maintenance> Maintenances { get; set; } = new List<Maintenance>(); 
}
