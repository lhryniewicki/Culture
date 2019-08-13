using Culture.DataAccess.Context;
using Culture.Models;
using Culture.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly CultureDbContext _cultureDbContext;

		public UserRepository(CultureDbContext cultureDbContext)
		{
			_cultureDbContext = cultureDbContext;
		}

	}
}
