using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarServiceTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Context context;

        public CustomersController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await context.Customers.Select(c => new
            {
                c.Id,
                c.FirstName,
                c.LastName,
                c.Phone,
                c.Email,
                c.Address
            }).ToListAsync();
            return Ok(customers);
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> PostCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer()
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Phone = customerDTO.Phone,
                Email = customerDTO.Email,
                Address = customerDTO.Address
            };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return Ok(new { Message = "Customer created successfully" });
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchData([FromQuery] SearchDTO searchDTO)
        {
            var query = context.Customers
                .Include(c => c.Cars)
                .ThenInclude(a => a.Maintenances)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchDTO.FirstName))
                query = query.Where(c => c.FirstName.Contains(searchDTO.FirstName));

            if (!string.IsNullOrEmpty(searchDTO.LastName))
                query = query.Where(c => c.LastName.Contains(searchDTO.LastName));

            if (!string.IsNullOrEmpty(searchDTO.Phone))
                query = query.Where(c => c.Phone.Contains(searchDTO.Phone));

            if (!string.IsNullOrEmpty(searchDTO.Email))
                query = query.Where(c => c.Email.Contains(searchDTO.Email));

            if (!string.IsNullOrEmpty(searchDTO.Brand))
                query = query.Where(c => c.Cars.Any(car => car.Brand.Contains(searchDTO.Brand)));

            if (!string.IsNullOrEmpty(searchDTO.Model))
                query = query.Where(c => c.Cars.Any(car => car.Model.Contains(searchDTO.Model)));

            if (!string.IsNullOrEmpty(searchDTO.RegistrationPlate))
                query = query.Where(c => c.Cars.Any(car => car.RegistrationPlate != null && car.RegistrationPlate.Contains(searchDTO.RegistrationPlate)));

            if (searchDTO.Status.HasValue)
                query = query.Where(c => c.Cars.Any(car => car.Status == searchDTO.Status.Value));

            var customers = await query.ToListAsync();

            var result = customers.Select(c => new
            {
                c.FirstName,
                c.LastName,
                c.Phone,
                c.Email,
                c.Address,
                Cars = c.Cars.Select(car => new
                {
                    car.Brand,
                    car.Model,
                    car.YearOfManufacture,
                    car.EngineCapacity,
                    car.EnginePower,
                    car.FuelType,
                    car.Status,
                    car.RegistrationPlate,
                    Maintenances = car.Maintenances.Select(m => new
                    {
                        m.StartDate,
                        m.EndDate,
                        m.DescriptionOfWork,
                        m.ReplacedParts,
                        m.Duration,
                        m.Cost,
                        m.MechanicName
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { Message = "Customer not found" });

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return Ok(new { Message = "Customer and related data deleted successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerDTO updateCustomerDTO)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { Message = "Customer not found" });

            if (!string.IsNullOrWhiteSpace(updateCustomerDTO.FirstName))
                customer.FirstName = updateCustomerDTO.FirstName.Trim();

            if (!string.IsNullOrWhiteSpace(updateCustomerDTO.LastName))
                customer.LastName = updateCustomerDTO.LastName.Trim();

            if (!string.IsNullOrWhiteSpace(updateCustomerDTO.Phone))
                customer.Phone = updateCustomerDTO.Phone.Trim();

            if (!string.IsNullOrWhiteSpace(updateCustomerDTO.Email))
                customer.Email = updateCustomerDTO.Email.Trim();

            if (!string.IsNullOrWhiteSpace(updateCustomerDTO.Address))
                customer.Address = updateCustomerDTO.Address.Trim();

            await context.SaveChangesAsync();
            return Ok(customer);
        }
    }
}
