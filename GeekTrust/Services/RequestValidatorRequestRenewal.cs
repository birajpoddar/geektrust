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
		#region Request Renewal Validations
		public void RenewalDetailsValidations()
        {
			// If Request contains RequestRenewal, validate RequestRenewal details
			if (RequestContainsRenewalDetails())
			{
				RequestContainsRenewalDetailsOnce();
				RequestContainsValidRenewalDetailsArguments();
			}
		}

		public bool RequestContainsRenewalDetails()
		{
			// If RequestRenewal doesn't exists in request
			if (_catRequest.RequestRenewalDetails == null)
			{
				// Log details
				_logger.LogInformation("Renewal Details not requested");
				return false;
			}

			// Return true if RequestRenewal exists
			return true;
		}

		public void RequestContainsRenewalDetailsOnce()
		{
			if (_catRequest.RequestRenewalDetails.Count > 1)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("Duplicate Request Renewal Details Request");
			}
		}

		public void RequestContainsValidRenewalDetailsArguments()
		{
			if (_catRequest.RequestRenewalDetails.First().Length != 1)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("Invalid Request Renewal Details for PRINT_RENEWAL_DETAILS");
			}
		}
		#endregion
	}
}

