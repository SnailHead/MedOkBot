using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
	public enum TypeAction
	{
		[Display(Name = "Начать рабочий день")]
		ButtonStartOffice,
		[Display(Name = "Закончить рабочий день")]
		ButtonStopOffice,
		[Display(Name = "Статистика рабочих часов")]
		ButtonStaticticOffice,
		[Display(Name = "Коллеги")]
		ButtonUsers,
		[Display(Name = "Задачи")]
		ButtonTasks,
		[Display(Name = "Проекты")]
		ButtonProjects,
	}
}