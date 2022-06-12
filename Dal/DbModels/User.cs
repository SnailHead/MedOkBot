using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int RoleId { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public DateTime? BirthDate { get; set; }
        public int DepartmentId { get; set; }
        public int BotId { get; set; }
        public string Email { get; set; }
        public long TelegramChattId { get; set; }
        public int RegistrationStateId { get; set; }
    }
}
