using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Entities;
using on_time_be.Infrastructure.Identity;

namespace on_time_be.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<User> UserList => Set<User>();
    
    public DbSet<Appointment> AppointmentList => Set<Appointment>();
    
    public DbSet<Service> ServiceList => Set<Service>();
    
    public DbSet<Salon> SalonList => Set<Salon>();
    
    public DbSet<Payment> PaymentList => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("User");
        builder.Entity<Appointment>().ToTable("Appointment");
        builder.Entity<Service>().ToTable("Service");
        builder.Entity<Salon>().ToTable("Salon");
        builder.Entity<Payment>().ToTable("Payment");

        builder.Entity<Appointment>().Ignore(a => a.DomainEvents);
        builder.Entity<Payment>().Ignore(p => p.DomainEvents);
        builder.Entity<Salon>().Ignore(p => p.DomainEvents);
        builder.Entity<User>().Ignore(p => p.DomainEvents);
        builder.Entity<Service>().Ignore(p => p.DomainEvents);

        builder.Entity<Salon>().Property(s => s.DepositValue).HasPrecision(18, 4);
        builder.Entity<Salon>().Property(s => s.PriceWithDeposit).HasPrecision(18, 4);
        builder.Entity<Salon>().Property(s => s.PriceWithoutDeposit).HasPrecision(18, 4);
        builder.Entity<Service>().Property(s => s.Price).HasPrecision(18, 4);

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
