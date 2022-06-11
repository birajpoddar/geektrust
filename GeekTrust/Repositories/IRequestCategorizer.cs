using System;
using System.Collections.Generic;

namespace GeekTrust.Repositories
{
	interface IRequestCategorizer
	{
		// Categorize Request Types
		void Categorize(string[] str);

		// Return Request Types
		List<string[]> StartDate { get; }
		List<string[]> PlanDetails { get; }
		List<string[]> TopupDetails { get; }
		List<string[]> RequestRenewalDetails { get; }
	}
}

