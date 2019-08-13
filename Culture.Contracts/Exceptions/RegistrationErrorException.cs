using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.Exceptions
{
	public class RegistrationErrorException:Exception
	{
		public RegistrationErrorException(string message):base(message)
		{

		}
	}
}
