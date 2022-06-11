using System;
using GeekTrust.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekTrust.Repositories
{
	interface IDbContext
	{
		DbSet<Plan> Plans { get; set; }
		DbSet<TopUp> TopUps { get; set; }
	}
}

