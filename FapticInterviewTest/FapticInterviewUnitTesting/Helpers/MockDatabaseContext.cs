using FapticInterviewTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FapticInterviewUnitTesting.Helpers
{
    public class MockDatabaseContextMockDb : IDbContextFactory<PriceDbContext>
    {
        public PriceDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<PriceDbContext>()
            .UseInMemoryDatabase($"InMemoryPriceDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

            return new PriceDbContext(options);
        }
    }
}
