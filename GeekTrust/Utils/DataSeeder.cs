using System;
using GeekTrust.Models;
using GeekTrust.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace GeekTrust.Utils
{
    class DataSeeder : IDataSeeder
    {
        private DoReMiContext _context;
        private ILogger _logger;

        public DataSeeder(DoReMiContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void SeedPlans()
        {
            if (!_context.Plans.Any())
            {
                // Music Plans added
                _context.Plans.AddRange(
                    new Plan() { Name = "MUSIC", Type = "FREE", Price = 0, PeriodInMonths = 1 },
                    new Plan() { Name = "MUSIC", Type = "PERSONAL", Price = 100, PeriodInMonths = 1 },
                    new Plan() { Name = "MUSIC", Type = "PREMIUM", Price = 250, PeriodInMonths = 3 }
                    );

                // Video Plans added
                _context.Plans.AddRange(
                    new Plan() { Name = "VIDEO", Type = "FREE", Price = 0, PeriodInMonths = 1 },
                    new Plan() { Name = "VIDEO", Type = "PERSONAL", Price = 200, PeriodInMonths = 1 },
                    new Plan() { Name = "VIDEO", Type = "PREMIUM", Price = 500, PeriodInMonths = 3 }
                    );

                // Podcast Plans added
                _context.Plans.AddRange(
                    new Plan() { Name = "PODCAST", Type = "FREE", Price = 0, PeriodInMonths = 1 },
                    new Plan() { Name = "PODCAST", Type = "PERSONAL", Price = 100, PeriodInMonths = 1 },
                    new Plan() { Name = "PODCAST", Type = "PREMIUM", Price = 300, PeriodInMonths = 3 }
                    );

                // Save all Changes
                _context.SaveChanges();
                _logger.LogInformation("*** All Plans loaded in Memory");
            }
        }

        public void SeedTopups()
        {
            if (!_context.TopUps.Any())
            {
                // Topup Plans added
                _context.TopUps.AddRange(
                    new TopUp() { Name = "FOUR_DEVICE", NumberOfDevicesSupported = 4, Price = 50, PeriodInMonths = 1 },
                    new TopUp() { Name = "TEN_DEVICE", NumberOfDevicesSupported = 10, Price = 100, PeriodInMonths = 1 }
                    );

                // Save all Changes
                _context.SaveChanges();
                _logger.LogInformation("*** All Topup Plans loaded in Memory");
            }
        }
    }
}

