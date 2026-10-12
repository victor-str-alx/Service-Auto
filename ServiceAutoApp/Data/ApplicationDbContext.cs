using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Models;

namespace ServiceAutoApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentService> AppointmentServices => Set<AppointmentService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppointmentService>()
            .HasKey(x => new { x.AppointmentId, x.ServiceTypeId });

        modelBuilder.Entity<AppointmentService>()
            .HasOne(x => x.Appointment)
            .WithMany(x => x.AppointmentServices)
            .HasForeignKey(x => x.AppointmentId);

        modelBuilder.Entity<AppointmentService>()
            .HasOne(x => x.ServiceType)
            .WithMany(x => x.AppointmentServices)
            .HasForeignKey(x => x.ServiceTypeId);
    }
}
