using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.Exceptions
{
	public class LoginErrorException:Exception
	{
		public LoginErrorException(string message):base(message)
		{

		}
	}
}
