using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarServiceTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenancesController : ControllerBase
    {
        private readonly Context context;
        public MaintenancesController(Context context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetMaintenances()
        {
            var maintenances = await context.Maintenances.Select(m => new
            {
                m.Id,
                m.StartDate,
                m.EndDate,
                m.DescriptionOfWork,
                m.ReplacedParts,
                m.Duration,
                m.Cost,
                m.MechanicName,
            }).ToListAsync();
            return Ok(maintenances);
        }


        [HttpPost("CreateMaintenance")]
        public async Task<IActionResult> PostMaintenance(MaintenanceDTO maintenanceDTO)
        {
            var maintenance = new Maintenance
            {
                StartDate = maintenanceDTO.StartDate,
                EndDate = maintenanceDTO.EndDate,
                DescriptionOfWork = maintenanceDTO.DescriptionOfWork,
                ReplacedParts = maintenanceDTO.ReplacedParts,
                Cost = maintenanceDTO.Cost,
                MechanicName = maintenanceDTO.MechanicName,
                CarId = maintenanceDTO.CarId
            };
            context.Maintenances.Add(maintenance);
            await context.SaveChangesAsync();
            return Ok(new { Message = "Successfully executed" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenance(int id, UpdateMaintenanceDTO maintenanceDTO)
        {
            var maintenance = await context.Maintenances.FindAsync(id);

            if (maintenance == null)
                return NotFound(new { Message = "Maintenance record not found" });

            if (maintenanceDTO.StartDate.HasValue)
                maintenance.StartDate = maintenanceDTO.StartDate.Value;

            if (maintenanceDTO.EndDate.HasValue)
                maintenance.EndDate = maintenanceDTO.EndDate.Value;

            if (!string.IsNullOrEmpty(maintenanceDTO.DescriptionOfWork))
                maintenance.DescriptionOfWork = maintenanceDTO.DescriptionOfWork;

            if (!string.IsNullOrEmpty(maintenanceDTO.ReplacedParts))
                maintenance.ReplacedParts = maintenanceDTO.ReplacedParts;

            if (maintenanceDTO.Cost.HasValue)
                maintenance.Cost = maintenanceDTO.Cost.Value;

            if (!string.IsNullOrEmpty(maintenanceDTO.MechanicName))
                maintenance.MechanicName = maintenanceDTO.MechanicName;


            await context.SaveChangesAsync();
            return Ok(maintenance);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(int id)
        {
            var maintenance = await context.Maintenances.FindAsync(id);
            if (maintenance == null)
                return NotFound(new { Message = "Maintenance record not found" });

            context.Maintenances.Remove(maintenance);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Maintenance record deleted successfully" });
        }
    }
}
