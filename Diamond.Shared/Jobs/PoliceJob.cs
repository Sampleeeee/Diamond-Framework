using System.Collections.Generic;

namespace Diamond.Shared.Jobs
{
    public class TrooperJob : BaseJob
    {
        public override string Name => "San Andreas State Troopers";
        public override string UniqueId => GetType().FullName;
        public override string Description => "Protect and serve for the state of San Andreas.";

        public override bool IsPolice => true;

        public override List<BaseJobGrade> JobGrades { get; set; } = new List<BaseJobGrade>
        {
            new Colonel(),
            new LtColonel(),
            new Major(),
            new Captain(),
            new Lieutenant(),
            new Sergeant(),
            new Corporal(),
            new TrooperII(),
            new TrooperI(),
            new Cadet()
        };

        public class Colonel : BaseJobGrade
        {
            public override string Name => "Colonel";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 1_000;
        }

        public class LtColonel : BaseJobGrade
        {
            public override string Name => "Lt. Colonel";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 900;
        }

        public class Major : BaseJobGrade
        {
            public override string Name => "Major";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 800;
        }

        public class Captain : BaseJobGrade
        {
            public override string Name => "Captain";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 700;
        }

        public class Lieutenant : BaseJobGrade
        {
            public override string Name => "Lieutenant";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 600;
        }

        public class Corporal : BaseJobGrade
        {
            public override string Name => "Corporal";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 500;
        }
        
        public class Sergeant : BaseJobGrade
        {
            public override string Name => "Sergeant";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 400;
        }

        public class TrooperII : BaseJobGrade
        {
            public override string Name => "Trooper II";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 300;
        }
        
        public class TrooperI : BaseJobGrade
        {
            public override string Name => "Trooper I";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 200;
        }

        public class Cadet : BaseJobGrade
        {
            public override string Name => "Cadet";
            public override string UniqueId => GetType().FullName;
            public override int Salary => 100;
        }
    }
}