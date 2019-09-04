using Culture.Contracts;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
	public class ReactionService:IReactionService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ReactionService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<EventReaction> SetReaction(SetReactionViewModel reactionViewModel)
		{
			var _event = await _unitOfWork.EventRepository.GetEventWithReactions(reactionViewModel.EventId);

			if(_event==null)
			{
				throw new Exception("Reaction set on non existent event");
			}

			var userReaction = _event.Reactions.FirstOrDefault(x => x.UserId == reactionViewModel.UserId);

			if (userReaction != null)
			{
				return await UpdateReaction(reactionViewModel,userReaction,_event);
			}

			return await CreateReaction(reactionViewModel,_event);

			
		}

		private async Task<EventReaction> CreateReaction(SetReactionViewModel reactionViewModel,Event _event)
		{
			var reaction = new EventReaction()
			{
				EventId = reactionViewModel.EventId,
				Type = reactionViewModel.ReactionType,
				UserId = reactionViewModel.UserId
			};

			_event.Reactions.Add(reaction);
			return reaction;
		}

		private async Task<EventReaction> UpdateReaction(SetReactionViewModel reactionViewModel, EventReaction userReaction, Event _event)
		{
			if(userReaction.Type == reactionViewModel.ReactionType)
			{
				_unitOfWork.EventReactionRepository.RemoveReaction(userReaction);
				return null;
			}

			 userReaction.Type = reactionViewModel.ReactionType;
			return userReaction;

		}
		public Task Commit()
		{
			return _unitOfWork.Commit();
		}

	}
}
