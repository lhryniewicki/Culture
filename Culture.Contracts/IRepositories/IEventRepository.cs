using Culture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface IEventRepository
	{
		Task<Event> GetEventDetailsAsync(int id);
		Task AddEventAsync(Event eventt);
	}
}
