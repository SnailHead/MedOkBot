using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using User = Entities.User;

namespace BL
{
	public class UsersBL
	{
		public async Task<int> AddOrUpdateAsync(User entity)
		{
			entity.Id = await new UsersDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new UsersDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(UsersSearchParams searchParams)
		{
			return new UsersDal().ExistsAsync(searchParams);
		}

		public Task<User> GetAsync(int id)
		{
			return new UsersDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new UsersDal().DeleteAsync(id);
		}

		public Task<SearchResult<User>> GetAsync(UsersSearchParams searchParams)
		{
			return new UsersDal().GetAsync(searchParams);
		}
	}
}

