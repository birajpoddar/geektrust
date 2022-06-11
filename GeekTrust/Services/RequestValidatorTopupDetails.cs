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
		#region Topup Validations
		public void TopupDetailValidations()
		{
			if (RequestContainsTopup())
			{
				// If Request contains Topup and Plan(s), validate Topup details
				if (RequestContainsPlan())
				{
					RequestContainsTopUpOnce();
					RequestContainsValidTopupArguments();
					RequestContainsValidTopupMonth();
				}
				else
				{
					// If Request contains Topup but no Plan(s), add ErrorMessage and trigger event
					GenerateErrorListCallback("ADD_TOPUP_FAILED\tSUBSCRIPTIONS_NOT_FOUND", "SUBSCRIPTIONS_NOT_FOUND");
				}
			}
		}


		public bool RequestContainsTopup()
		{
			// Validate TopupDetails exists in request
			if (_catRequest.TopupDetails == null)
			{
				// Log details
				_logger.LogInformation("No Topup requested");
				return false;
			}

			// Return true if Topup details found
			return true;
		}


		public void RequestContainsTopUpOnce()
		{
			if (_catRequest.TopupDetails.Count > 1)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("ADD_TOPUP_FAILED\tDUPLICATE_TOPUP");
			}
		}

		public void RequestContainsValidTopupArguments()
		{
			if (_catRequest.TopupDetails.First().Length != 3)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("Invalid Topup Details for ADD_TOPUP");
			}
		}

		public void RequestContainsValidTopupMonth()
        {
			// Save the Parsing result for Topup Month in res
			var res = int.TryParse(_catRequest.TopupDetails.First().Last(), out int i);

			// If Topup Month is not Integer, exit application
			if(!res)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("Invalid Topup Month");
			}
        }
		#endregion
	}
}

