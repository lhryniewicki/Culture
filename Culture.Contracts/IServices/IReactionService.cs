using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
	public interface IReactionService
	{
		Task<bool> SetReaction(SetReactionViewModel reactionViewModel, Guid userId);
		Task Commit();
	}
}
