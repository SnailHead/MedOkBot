using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Enums;
using Common.Search;
using Dal.DbModels;

namespace Dal
{
	public class UsersDal : BaseDal<DefaultDbContext, User, Entities.User, int, UsersSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public UsersDal()
		{
		}

		protected internal UsersDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.User entity, User dbObject, bool exists)
		{
			dbObject.Login = entity.Login;
			dbObject.RoleId = (int)entity.RoleId;
			dbObject.IsBlocked = entity.IsBlocked;
			dbObject.RegistrationDate = entity.RegistrationDate;
			dbObject.Firstname = entity.Firstname;
			dbObject.Lastname = entity.Lastname;
			dbObject.Middlename = entity.Middlename;
			dbObject.BirthDate = entity.BirthDate;
			dbObject.DepartmentId = (int)entity.DepartmentId;
			dbObject.BotId = (int)entity.BotId;
			dbObject.Email = entity.Email;
			dbObject.TelegramChattId = entity.TelegramChattId;
			dbObject.RegistrationStateId = (int)entity.RegistrationStateId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<User>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<User> dbObjects, UsersSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.User>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<User> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<User, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.User, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.User ConvertDbObjectToEntity(User dbObject)
		{
			return dbObject == null ? null : new Entities.User(dbObject.Id, dbObject.Login, (UserRole)dbObject.RoleId,
				dbObject.IsBlocked, dbObject.RegistrationDate, dbObject.Firstname, dbObject.Lastname,
				dbObject.Middlename, dbObject.BirthDate, (Departments)dbObject.DepartmentId, (Bots)dbObject.BotId, dbObject.Email,
				dbObject.TelegramChattId, (RegistrationState)dbObject.RegistrationStateId);
		}
	}
}
