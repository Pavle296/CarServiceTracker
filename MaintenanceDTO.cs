using System;

public class MaintenanceDTO
{
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string DescriptionOfWork { get; set; } 
    public string? ReplacedParts { get; set; } = null;
    public TimeSpan Duration => EndDate - StartDate; 
    public decimal Cost { get; set; } = 0; 
    public required string MechanicName { get; set; } 
    public int CarId { get; set; } 
}
