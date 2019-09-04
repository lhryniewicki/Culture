using Culture.Models;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface IEventReactionRepository
	{
		void RemoveReaction(EventReaction eventReaction);
	}
}
