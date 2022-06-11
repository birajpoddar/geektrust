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
		#region Start Date Validations
		public void StartDateValidations()
		{
			// If Request contains StartDate, validate StartDate details
			if (RequestContainsStartDate())
			{
				RequestContainsSingleStartDate();
				RequestContainsValidStartDateArguments();
				RequestContainsValidStartDate();
			}
            else
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("START_SUBSCRIPTION date doesn't exist");
			}
		}


		public bool RequestContainsStartDate()
		{
			// If StartDate doesn't exists in request, return FALSE
			if (_catRequest.StartDate == null)
			{
				return false;
			}

			// Return true if StartDate exists
			return true;
		}

		public void RequestContainsSingleStartDate()
		{
			// Validate Duplicate Values
			if (_catRequest.StartDate.Count > 1)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("Duplicate values for START_SUBSCRIPTION");
			}
		}

		public void RequestContainsValidStartDateArguments()
		{
			// If StartDate line doesn't consists of 2 Arguments, log Error
			if (_catRequest.StartDate.First().Length != 2)
			{
				// Add ErrorMessage and trigger event
				GenerateErrorListCallback("Invalid Start Date Details for START_SUBSCRIPTION input");
			}
		}

		public void RequestContainsValidStartDate()
		{
			// Get the Start Date
			var reqStrtDte = _catRequest.StartDate.First()[1];

			// If requested StartDate is invalid, add the error to List
			if (Utils.UtilityFunctions.TransformStringToDate(reqStrtDte) == null)
			{
                // Add ErrorMessage and trigger event
                GenerateErrorListCallback("INVALID_DATE");
			}

		}
		#endregion
	}
}

