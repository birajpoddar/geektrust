using System;
using System.Linq;
using GeekTrust.Repositories;
using GeekTrust.Models;
using System.Collections.Generic;

namespace GeekTrust.Services
{
	class SubscriptionCalculator : ISubscriptionCalculator
	{
        private readonly IDbContext _context;
        private readonly List<string> list;
        private decimal price;

		public SubscriptionCalculator(IDbContext context)
		{
            _context = context;
            list = new();
            price = 0;
		}

        // Calculate Plan Renewal Details and returns the List
        public List<string> CalculatePlanRenewal(Request request)
        {
            // Loop through requested plans
            foreach (var item in request.RequestedPlans)
            {
                // Retrieves the list of matching plan
                var requestedPlan = _context.Plans.Where(p => p.Name == item.Name && p.Type == item.Type);

                // If requested plan matches existing plan, get RenewalDate and add to list
                if (requestedPlan.Count() == 1)
                {
                    // Get the requested months form the existing plan
                    var requestedMonths = requestedPlan.Select(p => p.PeriodInMonths).First();

                    // Gets Renewal Date in string
                    var renewalDate = CalculateRenewalReminderDate(request.StartDate, requestedMonths);

                    // Adds Renewal Date for the particular plan to list
                    list.Add($"RENEWAL_REMINDER\t{item.Name}\t{renewalDate}");

                    // Adds the price of the Plan to price
                    price += requestedPlan.Select(p => p.Price).First();
                } 
            }

            // Returns the Renewal Details list
            return list;
        }

        // Calculates and returns the total amount payable for plans and topups
        public string CalculateTotalAmount(Request request)
        {
            // Add Topup Price to Total if Topup exists
            if (request.RequestedTopupPlan != null)
            {
                // Retrives the list of matching topup
                var requestedTopup = _context.TopUps.Where(t => t.Name == request.RequestedTopupPlan.Name);

                // If requested topup exists, add the price of it per month to price
                if (requestedTopup.Count() == 1)
                {
                    // Get the topup price per month, assuming all topups are charged on a monthly basis
                    var topupPricePerMonth = requestedTopup.Select(t => t.Price).First();

                    // Adds the topup price for requested number of months
                    price += topupPricePerMonth * request.RequestedTopupPlan.Months;
                }
            }

            // Returns the Renewal Amount
            return $"RENEWAL_AMOUNT\t{price}";
        }

        // Calculates Renewal Reminder Date for Plan
        private string CalculateRenewalReminderDate(DateTime startDate, int periodInMonths)
        {
            // Calculates Renewal Date
            var renewalDate = startDate.AddMonths(periodInMonths);

            // Gets the renewal reminder days in advance from EnvironmentValues and convert to int
            var reminderDays = Environment.GetEnvironmentVariable("RenewalReminderInDays") ?? "10";
            var reminderDaysInt = int.Parse(reminderDays);

            // Calculate Renewal Reminder Date
            var renewalReminderDate = renewalDate.AddDays(-reminderDaysInt);

            // Returns the Renew Reminder Date in string
            return renewalReminderDate.ToString("dd-MM-yyyy");
        }
    }
}

