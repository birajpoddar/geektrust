using System;
using System.Collections.Generic;

namespace GeekTrust.Repositories
{
	interface IRequestValidator
	{
		// ErrorEncountered Event
		event EventHandler ErrorEncountered;

		// File Tests
		void RequestFileExists(string fileName);

		// Request Tests
		void RequestNotEmpty();

		// Start Date Tests
		void StartDateValidations();
		bool RequestContainsStartDate();
		void RequestContainsSingleStartDate();
		void RequestContainsValidStartDateArguments();
		void RequestContainsValidStartDate();

		// Plan Tests
		void PlanDetailsValidations();
		bool RequestContainsPlan();
		void RequestContainsValidPlanArgumentsAndSamePlanOnce();

		// Optional Topup Tests
		void TopupDetailValidations();
		bool RequestContainsTopup();
		void RequestContainsTopUpOnce();
		void RequestContainsValidTopupArguments();
		void RequestContainsValidTopupMonth();

		// Request Renewal Details Tests
		void RenewalDetailsValidations();
		bool RequestContainsRenewalDetails();
		void RequestContainsRenewalDetailsOnce();
		void RequestContainsValidRenewalDetailsArguments();
	}
}

