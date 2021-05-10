using System.Linq;
using System.Timers;
using CitizenFX.Core;
using Diamond.Shared;

namespace Diamond.Server
{
    public class SalaryHandler : BaseScript
    {
        public SalaryHandler()
        {
            var timer = new Timer { Interval = 300 * 1000, AutoReset = true };
            timer.Elapsed += (_, args) =>
            {
                foreach (var character in MainServer.Instance.Characters.Select(kvp => kvp.Value))
                {
                    character.Money += character.JobGrade.Salary;
                    character.Player.TriggerEvent("SalaryGiven", character.JobGrade.Salary);
                }
            };
            
            timer.Start();
        }
    }
}