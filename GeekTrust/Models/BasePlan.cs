using System;
namespace GeekTrust.Models
{
	class BasePlan
	{
			public Guid ID { get; set; }
			public string Name { get; set; }
			public int Price { get; set; }
			public int PeriodInMonths { get; set; }
	}
}

