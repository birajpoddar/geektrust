using System;
using System.Collections.Generic;
using GeekTrust.Models;

namespace GeekTrust.Repositories
{
	interface IRequestDeserializer
	{
		// Deserialize Request as object
		Request Deserialize(string[] str);
	}
}

