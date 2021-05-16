using System.Collections.Generic;

namespace Diamond.Shared.Jobs
{
	public abstract class BaseJob
	{
		public abstract string Name { get; }
		public string UniqueId => GetType().FullName;
		public abstract string Description { get; }
		public virtual bool IsPolice { get; } = false;

		/// <summary>
		/// List of job grades. The lower the number, the most power.
		/// </summary>
		public virtual List<BaseJobGrade> JobGrades { get; set; } = new List<BaseJobGrade>();
	}

	public abstract class BaseJobGrade
	{
		public abstract string Name { get; }
		public string UniqueId => GetType().FullName;
		public abstract int Salary { get; }
	}
}
