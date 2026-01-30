using System;

public class UpdateMaintenanceDTO
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? DescriptionOfWork { get; set; }
    public string? ReplacedParts { get; set; }
    public TimeSpan? Duration => EndDate - StartDate;
    public decimal? Cost { get; set; } 
    public string? MechanicName { get; set; }
}
