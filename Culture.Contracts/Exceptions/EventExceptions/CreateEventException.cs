using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.Exceptions.EventExceptions
{
	public class CreateEventException:Exception
	{
		public CreateEventException(string message):base(message)
		{
			
		}
	}
}
