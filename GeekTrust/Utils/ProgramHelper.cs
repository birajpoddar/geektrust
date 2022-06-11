    using System.IO;
using GeekTrust.Repositories;
using GeekTrust.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace GeekTrust.Utils
{
	class ProgramHelper
	{
        private readonly IRequestCategorizer _categorizeRequest;
        private readonly IRequestValidator _requestValidator;
        private readonly IRequestDeserializer _requestDeserializer;
        private readonly IDataSeeder _seeder;
        private readonly IDataViewer _viewer;
        private readonly ISubscriptionCalculator _calculateSubscriptions;
        private readonly ILogger _logger;
        internal static List<string> responseMessages = new();

        public ProgramHelper(
            IRequestCategorizer categorizeRequest,
            IRequestValidator requestValidator,
            IRequestDeserializer requestDeserializer,
            IDataSeeder seeder,
            IDataViewer viewer,
            ISubscriptionCalculator calculateSubscriptions,
            ILogger<ProgramHelper> logger)
		{
            _categorizeRequest = categorizeRequest;
            _requestValidator = requestValidator;
            _requestDeserializer = requestDeserializer;
            _seeder = seeder;
            _viewer = viewer;
            _calculateSubscriptions = calculateSubscriptions;
            _logger = logger;
        }

        // Seed Data if there is no data present
        public void SeedData()
        {
            // Create the Plans
            _seeder.SeedPlans();
            _seeder.SeedTopups();

            // View All Plans
            _viewer.ViewPlans();
            _viewer.ViewTopups();
        }

        // Deserialize the input file contents into an object for consumption
        public Request DeserializeInput(string inputFilePath)
        {
            // Add EventHandler for ErrorEcountered event
            _requestValidator.ErrorEncountered += delegate
            {
                // Print Error Messages
                UtilityFunctions.PrintStringList(Program.ErrorMessages);

                // Throw an exception to be caught by Main()
                throw new Exception("");
            };

            // Validate Request File Exists
            _requestValidator.RequestFileExists(inputFilePath);

            // Read all lines from the File
            var inputData = File.ReadAllLines(inputFilePath);

            // Categorize Request Strings
            _categorizeRequest.Categorize(inputData);

            // Validate Request
            _requestValidator.RequestNotEmpty();
            _requestValidator.StartDateValidations();
            _requestValidator.TopupDetailValidations();
            _requestValidator.PlanDetailsValidations();
            _requestValidator.RenewalDetailsValidations();

            // Deserialize the request and return the same
            return _requestDeserializer.Deserialize(inputData);
        }

        // Calculate Subscriptions
        public List<string> CalculateSubscriptionRenewalDateAmount(Request request)
        {
            // Gets the Plan Renewal Details
            responseMessages = _calculateSubscriptions.CalculatePlanRenewal(request);

            // Add the Total Amount to list
            responseMessages.Add(_calculateSubscriptions.CalculateTotalAmount(request));

            // Returns the list if Renewal Details is requested else return NULL
            if (request.PrintRenewalDetails)
                return responseMessages;
            else
                return new List<string>();
        }
    }
}

