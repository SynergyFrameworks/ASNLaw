using System;
using Infrastructure.Common;

namespace Infrastructure.PushNotifications
{
	public class PushNotificationSearchCriteria : SearchCriteriaBase
	{
		public string[] Ids { get; set; }
		public bool OnlyNew { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		
	}
}
