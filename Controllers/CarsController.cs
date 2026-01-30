using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly Context context;
        public CarsController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await context.Cars.Select(c => new
            {
                c.Id,
                c.Brand,
                c.Model,
                c.YearOfManufacture,
                c.EngineCapacity,
                c.EnginePower,
                c.FuelType,
                c.RegistrationPlate,
                c.Status,
                c.Customer.FirstName,
                c.Customer.LastName

            }).ToListAsync();
            return Ok(cars);
        }

        [HttpPost("CreateCar")]
        public async Task<IActionResult> CreateCar(CarDTO carDTO)
        {
            var customer = await context.Customers.FindAsync(carDTO.CustomerId);
            if (customer == null)
                return NotFound(new { Message = "Customer not found" });

            var car = new Car
            {
                Brand = carDTO.Brand,
                Model = carDTO.Model,
                YearOfManufacture = carDTO.YearOfManufacture,
                EngineCapacity = carDTO.EngineCapacity,
                EnginePower = carDTO.EnginePower,
                FuelType = carDTO.FuelType,
                Status = carDTO.Status,
                RegistrationPlate = carDTO.RegistrationPlate,
                CustomerId = carDTO.CustomerId
            };
            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return Ok(new { Message = "Car created successfully" });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, UpdateCarDTO carDTO)
        {
            var car = await context.Cars.FindAsync(id);
            if (car == null)
                return NotFound(new { Message = "Car not found" });

            if (!string.IsNullOrWhiteSpace(carDTO.Brand))
                car.Brand = carDTO.Brand.Trim();

            if (!string.IsNullOrWhiteSpace(carDTO.Model))
                car.Model = carDTO.Model.Trim();

            if (carDTO.YearOfManufacture.HasValue)
                car.YearOfManufacture = carDTO.YearOfManufacture.Value;

            if (carDTO.EngineCapacity.HasValue)
                car.EngineCapacity = carDTO.EngineCapacity.Value;

            if (carDTO.EnginePower.HasValue)
                car.EnginePower = carDTO.EnginePower.Value;

            if (carDTO.FuelType.HasValue)
                car.FuelType = carDTO.FuelType.Value;

            if (carDTO.Status.HasValue)
                car.Status = carDTO.Status.Value;

            if (!string.IsNullOrWhiteSpace(carDTO.RegistrationPlate))
                car.RegistrationPlate = carDTO.RegistrationPlate.Trim();


            await context.SaveChangesAsync();
            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await context.Cars.FindAsync(id);
            if (car == null)
                return NotFound(new { Message = "Car not found" });

            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Car deleted successfully" });
        }

    }
}
