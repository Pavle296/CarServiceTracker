using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Account> Accounts { get; set; }
	public DbSet<Car> Cars { get; set; }
	public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
}
