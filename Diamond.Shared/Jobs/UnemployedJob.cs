using System.Collections.Generic;

namespace Diamond.Shared.Jobs
{
	public class UnemployedJob : BaseJob
	{
		public override string Name => "Unemployed";
		public override string Description => "Lol you are poor.";

		public override List<BaseJobGrade> JobGrades { get; set; } = new List<BaseJobGrade>
		{
			new Unemployed()
		};

		public class Unemployed : BaseJobGrade
		{
			public override string Name => "Unemployed";
			public override int Salary => 50;
		}
	}
}
