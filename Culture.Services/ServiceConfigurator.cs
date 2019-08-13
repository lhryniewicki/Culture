using System;
using System.Collections.Generic;
using System.Text;
using Culture.Contracts.IRepositories;
using Culture.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;



namespace Culture.Services
{
	public static class ServiceConfigurator
	{
		public static void ConfigureServices(this IServiceCollection services)
		{
			services.AddScoped<IEventRepository, EventRepository>();
		}
	}
}
