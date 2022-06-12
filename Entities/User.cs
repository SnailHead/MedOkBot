using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public UserRole RoleId { get; set; }
		public bool IsBlocked { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Middlename { get; set; }
		public DateTime? BirthDate { get; set; }
		public Departments DepartmentId { get; set; }
		public Bots BotId { get; set; }
		public string Email { get; set; }
		public long TelegramChattId { get; set; }
		public RegistrationState RegistrationStateId { get; set; }

		public User() { }
		public User(int id, string login, UserRole roleId, bool isBlocked, DateTime registrationDate, string firstname,
			string lastname, string middlename, DateTime? birthDate, Departments departmentId, Bots botId, string email,
			long telegramChattId, RegistrationState registrationStateId)
		{
			Id = id;
			Login = login;
			RoleId = roleId;
			IsBlocked = isBlocked;
			RegistrationDate = registrationDate;
			Firstname = firstname;
			Lastname = lastname;
			Middlename = middlename;
			BirthDate = birthDate;
			DepartmentId = departmentId;
			BotId = botId;
			Email = email;
			TelegramChattId = telegramChattId;
			RegistrationStateId = registrationStateId;
		}
	}
}
