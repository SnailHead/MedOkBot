create database MisMedOk
alter database MisMedOk set recovery simple
go

use MisMedOk
go

create table Users
(
	Id int not null identity constraint PK_Users primary key,
	[Login] nvarchar(200) constraint Unique_Users_Login unique not null,
	RoleId int not null,
	IsBlocked bit not null,
	RegistrationDate datetime not null,
	Firstname nvarchar(max) not null,
	Lastname nvarchar(max) not null,
	Middlename nvarchar(max),
	BirthDate datetime,
	DepartmentId int not null,
	BotId int not null,
	Email nvarchar(max),
	TelegramChattId bigint not null,
	RegistrationStateId int not null
)