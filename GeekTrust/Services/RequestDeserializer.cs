using System;
using System.Collections.Generic;
using GeekTrust.Utils;
using GeekTrust.Models;
using GeekTrust.Repositories;

namespace GeekTrust.Services
{
	class RequestDeserializer : IRequestDeserializer
	{
        public Request Deserialize(string[] str)
        {
            // Creating new Request object
            var request = new Request()
            {
                RequestedPlans = new List<RequestedPlan>(),
                RequestedTopupPlan = null,
                PrintRenewalDetails = false
            };

            foreach (var item in str)
            {
                // Splitting each line to string array based separated by spaces
                var req = Utils.UtilityFunctions.TransformStringToArray(item);

                // Create the request object based on the keywords
                switch (req[0].ToUpper())
                {
                    case "START_SUBSCRIPTION":
                        request.StartDate = (DateOnly)Utils.UtilityFunctions.TransformStringToDate(req[1]);
                        break;
                    case "ADD_SUBSCRIPTION":
                        request.RequestedPlans.Add(new RequestedPlan() { Name = req[1].ToUpper(), Type = req[2].ToUpper() });
                        break;
                    case "ADD_TOPUP":
                        request.RequestedTopupPlan = new RequestedTopup() { Name = req[1].ToUpper(), Months = Convert.ToInt32(req[2]) };
                        break;
                    case "PRINT_RENEWAL_DETAILS":
                        request.PrintRenewalDetails = true;
                        break;
                }
            }

            // Return the completed request
            return request;
        }
    }
}

