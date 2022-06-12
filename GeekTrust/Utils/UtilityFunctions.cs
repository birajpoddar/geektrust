using System;
using System.Collections.Generic;
using System.Globalization;

namespace GeekTrust.Utils
{
	class UtilityFunctions
	{
		// Transforms String to DateOnly
		public static DateTime? TransformStringToDate(string str)
        {
			// Try to parse the Date to valid format
			var flag = DateTime.TryParse(
				str,
				new CultureInfo("en-IN"),
				DateTimeStyles.None,
				out var dte);

			// If parsing is successful, return the DateOnly variable
			if (flag)
				return dte;

			// If parsing fails, return NULL
			return null;
        }

        // Transforms String to Array for passed STRING
		public static string[] TransformStringToArray(string str)
        {
			// If string is not null, split and return array
			if (str != null)
			{
				return str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			}

			// If string is null, return null
			return null;
        }

		// Print all lines from List
		public static void PrintStringList(List<string> list)
		{
			// If list is not NULL, print all lines
			if (list != null)
			{
				// Loop through all elements
				foreach (var item in list)
				{
					// Print each line from list
					Console.WriteLine(item);
				}
			}
		}
	}
}

