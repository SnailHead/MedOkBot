using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
	public enum RegistrationState
	{
		AskName = 0,
		AskFullname = 1,
		AskBirthDate = 2,
		AskEmail = 3,
		Completed = 4
	}
}