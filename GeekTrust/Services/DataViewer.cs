using System;
using Microsoft.Extensions.Logging;
using GeekTrust.Models;
using GeekTrust.Repositories;
using System.Runtime.InteropServices;
using System.Text;

namespace GeekTrust.Services
{
	class DataViewer : IDataViewer
	{
		private readonly IDbContext _context;
		private readonly ILogger _logger;

		public DataViewer(IDbContext context, ILogger<DataViewer> logger)
		{
			_context = context;
			_logger = logger;
		}

		public void ViewPlans()
		{
			StringBuilder plans = new("Displaying Plans\n");
			foreach (var item in _context.Plans)
			{
                plans.Append($"*** Guid={item.ID}, Name={item.Name}, Type={item.Type}, Price={item.Price}, Period={item.PeriodInMonths}\n");
			}
			_logger.LogInformation(plans.ToString());
		}

		public void ViewTopups()
		{
			StringBuilder topups = new("Displaying Topups\n");
			foreach (var item in _context.TopUps)
			{
                topups.Append($"*** Guid={item.ID}, Name={item.Name},  NumOfDevices={item.NumberOfDevicesSupported}, Price={item.Price}, Period={item.PeriodInMonths}\n");
			}
			_logger.LogInformation(topups.ToString());
		}
	}
}

