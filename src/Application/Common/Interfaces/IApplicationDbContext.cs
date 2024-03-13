using on_time_be.Domain.Entities;

namespace on_time_be.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> UserList { get; }
    DbSet<Appointment> AppointmentList { get; }
    
    DbSet<Service> ServiceList { get; }
    
    DbSet<Salon> SalonList { get; }
    
    DbSet<Payment> PaymentList { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
