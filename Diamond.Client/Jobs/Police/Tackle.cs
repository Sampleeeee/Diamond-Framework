using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared;
using Diamond.Shared.Items;
using Gender = CitizenFX.Core.Gender;

namespace Diamond.Client.Jobs.Police
{
    // TODO make this an animation
    // TODO do less stuff to find the ped, this can make ms spike up to .40 from .16
    public class Tackle : BaseScript
    {
        public Tackle()
        {
            API.RegisterKeyMapping("tackle", "Tackle", "keyboard", "y");
            Debug.WriteLine(API.NetworkGetPlayerIndexFromPed(932) + "");
        }

        [Command("tackle")]
        private void TackleCommand()
        {
            Debug.WriteLine("Tackle command");
            if (!MainClient.Character.Job.IsPolice) return;


            var player = GetPlayerToTackle();
            if (player == null)
            {
                // TODO check distance
                var ped = GetClosestPedToTackle();
                ped.Ragdoll();
                DoTackle();
                return;
            }
            
            TriggerServerEvent("TacklePlayer", player.ServerId);
        }

        // ReSharper disable twice AssignNullToNotNullAttribute
        private Player GetPlayerToTackle()
        {
            return Players.OrderBy(x => Vector3.DistanceSquared(x.Character.Position, Game.PlayerPed.Position))
                .FirstOrDefault(x => x != Game.Player);
        }

        private Ped GetClosestPedToTackle()
        {
            return World.GetAllPeds().OrderBy(x => Vector3.DistanceSquared(x.Position, Game.PlayerPed.Position))
                .FirstOrDefault(x => x != Game.PlayerPed);
        }

        [EventHandler("DoTackle")]
        private void DoTackle()
        {
            Game.PlayerPed.Ragdoll(1000);
        }
    }
}