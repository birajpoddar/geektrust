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
		// Event handler and dependent objects
		public event EventHandler ErrorEncountered;
		private readonly IRequestCategorizer _catRequest;
		private readonly ILogger _logger;

		// ErrorMessage List
		internal static List<string> validationErrors;

		public RequestValidator(IRequestCategorizer catRequest, ILogger<RequestValidator> logger)
		{
			_catRequest = catRequest;
			_logger = logger;
			validationErrors = new();
		}

		#region Request File Validations
		public void RequestFileExists(string filePath)
        {
			// Publish the event of File doesn't exists
			if(!File.Exists(filePath))
			{
				// Log Error and add the error to validationError list
				GenerateErrorListCallback("Request File does not exists");
            }
        }
		#endregion

		#region Request Validations
		public void RequestNotEmpty()
		{
			// Validate None of the Request Values are NULL
			if (_catRequest.StartDate == null &&
				_catRequest.PlanDetails == null &&
				_catRequest.TopupDetails == null &&
				_catRequest.RequestRenewalDetails == null)
			{
				// Log Error and add the error to validationError list
				GenerateErrorListCallback("SUBSCRIPTIONS_NOT_FOUND");
			}

		}
		#endregion

		#region Generate the ErrorList and Publish the event
		private void GenerateErrorListCallback(params string[] errMsgs)
		{
			foreach (var msg in errMsgs)
			{
				// Add the error to list and log error
				validationErrors.Add(msg);
				_logger.LogError(msg);
			}

			// Publish event if subscriber exists
			ErrorEncountered?.Invoke(this, EventArgs.Empty);
		}
		#endregion
	}
}

