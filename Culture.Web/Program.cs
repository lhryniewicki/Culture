using Culture.DataAccess.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Culture.Web
{
    public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().SeedData().GetAwaiter().GetResult().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
