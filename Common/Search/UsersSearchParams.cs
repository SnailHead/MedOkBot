using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class UsersSearchParams : BaseSearchParams
	{
		public long? TelegramChatId { get; set; }
		public UsersSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}
