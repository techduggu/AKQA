using AKQA.Persistence;
using System;

namespace AKQA.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly AppDbContext _context;

        public CommandTestBase()
        {
            _context = AKQAContextFactory.Create();
        }

        public void Dispose()
        {
            AKQAContextFactory.Destroy(_context);
        }
    }
}
