using System.Collections.Generic;
using GeekTrust.Models;
namespace GeekTrust.Repositories
{
	interface ISubscriptionCalculator
	{
		List<string> CalculatePlanRenewal(Request request);
		string CalculateTotalAmount(Request request);
	}
}

