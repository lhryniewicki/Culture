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
			var eventReq =  _unitOfWork.EventRepository.GetEventAsync(reactionViewModel.EventId);
            var userReactedReq =  _unitOfWork.EventReactionRepository.GetUserReactionAsync(reactionViewModel.UserId, reactionViewModel.EventId);

            await Task.WhenAll(new Task[] { eventReq, userReactedReq });

            var eventModel = await eventReq;
            var userReacted = await userReactedReq;

            if (eventModel == null)
            {
                throw new Exception("Reaction set on non existent event");
            }
            if (userReacted != null)
			{
				return await UpdateReaction(reactionViewModel, userReacted, eventModel);
			}

			return await CreateReaction(reactionViewModel, eventModel);

			
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
