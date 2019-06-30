using AKQA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AKQA.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
