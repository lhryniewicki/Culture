using Culture.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
	public class SetReactionViewModel
	{
		public int EventId { get; set; }
		public ReactionType	ReactionType { get; set; }

	}
}
