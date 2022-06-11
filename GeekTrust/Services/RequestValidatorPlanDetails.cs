using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using GeekTrust.Repositories;
using GeekTrust.Utils;
using Microsoft.Extensions.Logging;

namespace GeekTrust.Services
{
	// RequestValidator which assumes that the Program will exit once an error is encountered
	partial class RequestValidator : IRequestValidator
	{
		#region Plan Validations
		public void PlanDetailsValidations()
		{
			// If Request contains Plan, validate Plan details
			if (RequestContainsPlan())
			{
				RequestContainsValidPlanArgumentsAndSamePlanOnce();
			}
			else
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("SUBSCRIPTIONS_NOT_FOUND");
			}
		}

		public bool RequestContainsPlan()
		{
			// If PlanDetails doesn't exists in request
			if (_catRequest.PlanDetails == null)
			{
				// Returns false if PlanDetails doesn't exists
				return false;
			}

			// Return true if PlanDetails exists
			return true;
		}

		public void RequestContainsValidPlanArgumentsAndSamePlanOnce()
		{
			foreach (var item in _catRequest.PlanDetails)
			{
				// Validate Number of Arguments
				if (item.Length != 3)
				{
					// Add ErrorMessage and trigger event
					GenerateErrorListCallback("Invalid Plan Details for ADD_SUBSCRIPTION");
				}

				// Validate Duplicate Category
				if (_catRequest.PlanDetails.Where(m => m[1] == item[1]).Count() > 1)
				{
					// Add ErrorMessage and trigger event
					GenerateErrorListCallback("ADD_SUBSCRIPTION_FAILED\tDUPLICATE_CATEGORY");
				}
			}
		}
		#endregion
	}
}

