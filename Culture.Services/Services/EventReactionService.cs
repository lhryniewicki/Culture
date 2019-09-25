using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class EventReactionService:IEventReactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventReactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EventReactionsDto> GetReactions(int eventId,Guid userId)
        {
            var reactions = await _unitOfWork.EventReactionRepository.GetEventReactions(eventId);
            var currentReaction = await _unitOfWork.EventReactionRepository.GetUserReactionAsync(userId,eventId);
            var groupedReactions = reactions.GroupBy(x => x.Type)
            .Select(x => new EventReactionDto()
            {
                Count = x.Count(),
                ReactionType = x.Key.ToString().ToLower()
            })
            .OrderByDescending(x => x.Count);

            return new EventReactionsDto()
            {
                EventReactions = groupedReactions,
                TotalCount = reactions.Count(),
                CurrentReaction = currentReaction.Type.ToString().ToLower()
            };
        }
    }
}
