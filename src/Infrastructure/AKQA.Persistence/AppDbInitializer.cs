using System.Linq;

namespace AKQA.Persistence
{
    public class AppDbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Customers.Any())
            {
                return; // Db has been seeded
            }
        }
    }
}
