using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;
using System.Threading.Tasks;

namespace Culture.Contracts.Facades
{
    public interface ICommentsFacade
    {
        Task<CommentDto> CreateComment(CommentViewModel commentViewModel);
        Task<CommentsListViewModel> GetEventComments(int eventId, int page = 0, int take = 5);
        Task EditEventComment(EditCommentViewModel comment);
        Task DeleteEventComment(int commentId);
    }
}
