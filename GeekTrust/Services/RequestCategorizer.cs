using System;
using System.Collections.Generic;
using GeekTrust.Utils;
using GeekTrust.Repositories;
using Microsoft.Extensions.Logging;

namespace GeekTrust.Services
{
	class RequestCategorizer: IRequestCategorizer
	{
        private List<string[]> startDte, planDet, topupDet, reqRen;
        private readonly ILogger _logger;

		public RequestCategorizer(ILogger<RequestCategorizer> logger)
		{
            _logger = logger;
		}

        public void Categorize(string[] str)
        {
            // Loop through every line of input string
            foreach (var item in str)
            {
                // Split the input line to array with respect to spaces
                var itemArr = Utils.UtilityFunctions.TransformStringToArray(item);

                // Skip to next line if it's a blank line
                if (itemArr.Length == 0)
                    continue;

                // Take AXN based on 1st value of array
                switch (itemArr[0])
                {
                    case "START_SUBSCRIPTION":
                        // Initialize if NULL
                        if (startDte == null)
                            startDte = new();

                        // Add the Array to the List
                        startDte.Add(itemArr);
                        break;
                    case "ADD_SUBSCRIPTION":
                        // Initialize if NULL
                        if (planDet == null)
                            planDet = new();

                        // Add the Array to the List
                        planDet.Add(itemArr);
                        break;
                    case "ADD_TOPUP":
                        // Initialize if NULL
                        if (topupDet == null)
                            topupDet = new();

                        // Add the Array to the List
                        topupDet.Add(itemArr);
                        break;
                    case "PRINT_RENEWAL_DETAILS":
                        // Initialize if NULL
                        if (reqRen == null)
                            reqRen = new();

                        // Add the Array to the List
                        reqRen.Add(itemArr);
                        break;
                }
            }
        }

        public List<string[]> StartDate
        {
            get { return startDte; }
        }

        public List<string[]> PlanDetails
        {
            get { return planDet; }
        }

        public List<string[]> TopupDetails
        {
            get { return topupDet; }
        }

        public List<string[]> RequestRenewalDetails
        {
            get { return reqRen; }
        }
    }
}

