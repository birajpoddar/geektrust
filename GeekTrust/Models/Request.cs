using System;
using System.Collections.Generic;

namespace GeekTrust.Models
{
	class Request
	{
		public DateTime StartDate { get; set; }
		public List<RequestedPlan> RequestedPlans { get; set; }
		public RequestedTopup RequestedTopupPlan { get; set; }
		public bool PrintRenewalDetails { get; set; }
	}

	class RequestedPlan
    {
		public string Name;
		public string Type;
    }

	class RequestedTopup
    {
		public string Name;
		public int Months;
    }
}

