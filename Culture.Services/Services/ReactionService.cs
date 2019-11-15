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

		public async Task<bool> SetReaction(SetReactionViewModel reactionViewModel, Guid userId)
		{

            var eventModel = await _unitOfWork.EventRepository.GetEventWithReactions(reactionViewModel.EventId);
            var userReacted =  await _unitOfWork.EventReactionRepository.GetUserReactionAsync(userId, reactionViewModel.EventId);

            if (eventModel == null)
            {
                throw new Exception("Reaction set on non existent event");
            }


            if (userReacted != null)
            {
                var isOwner = await _unitOfWork.UserRepository.IsUserReactionOwner(userId, reactionViewModel.EventId);

                if(isOwner) await UpdateReaction(reactionViewModel, userReacted, eventModel);

                return true;
			}

             await CreateReaction(reactionViewModel, eventModel,userId);
            return false;

        }

        private async Task<EventReaction> CreateReaction(SetReactionViewModel reactionViewModel,Event _event,Guid userId)
		{
			var reaction = new EventReaction()
			{
				EventId = reactionViewModel.EventId,
				Type = reactionViewModel.ReactionType,
				UserId = userId
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
