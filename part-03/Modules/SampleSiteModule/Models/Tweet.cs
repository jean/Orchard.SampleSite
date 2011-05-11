using System;

namespace SampleSiteModule.Models
{
	public class Tweet
	{
		public DateTime DateStamp { get; set; }
		public string Text { get; set; }

		public string FriendlyDate
		{
			get
			{
				TimeSpan span = DateTime.Now - DateStamp;
				if (span.TotalSeconds < 30)
					return "moments ago.";

				if (span.TotalSeconds < 60)
					return "Less than a minute ago.";

				if (span.TotalMinutes < 60)
					return String.Format("{0:0} minute{1} ago", span.TotalMinutes, span.TotalMinutes > 1 ? "s" : "");

				if (span.TotalHours < 24)
					return String.Format("{0:0} hour{1} ago", span.TotalHours, span.TotalHours > 1 ? "s" : "");

				return String.Format("{0:0} day{1} ago", span.TotalDays, span.TotalDays > 1 ? "s" : "");
			}
		}
	}
}