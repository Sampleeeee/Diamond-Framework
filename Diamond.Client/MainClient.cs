using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared;
using Newtonsoft.Json;

namespace Diamond.Client
{
	public class MainClient : BaseScript
	{
		public static Character Character;

		public MainClient()
		{
			Debug.WriteLine( "Creating Instance" );
		}

		[EventHandler( "onClientResourceStart" )]
		private async void OnClientResourceStart()
		{
			while ( !API.NetworkIsPlayerActive( Game.Player.Handle ) )
				await Delay( 0 );

			TriggerServerEvent( "PlayerReady" );

			await Delay( 1000 );
			API.SendNuiMessage( JsonConvert.SerializeObject( new { name = "gay" } ) );
		}

		[Tick]
		private Task DrawUserInterface()
		{
			if ( Character == null ) return Task.FromResult( 0 );

			const float diff = 0.075f;
			var texts = new string[]
			{
				$"Name: {Character.FullName}\nJob: {Character.Job.Name}, {Character.JobGrade.Name}",
				$"Inventory Weight: {Character.ItemInventory.GetWeight()}\nMoney: {Character.Money:C}",
				$"Bank: {Character.Bank:C}\nDirty Money: {Character.DirtyMoney:C}",
				$"Hunger: {Character.Hunger}\nThirst: {Character.Thirst}"
			};

			var current = 0.1f;
			foreach ( string text in texts )
			{
				API.SetTextScale( 0.5f, 0.5f );
				API.SetTextOutline();

				API.BeginTextCommandDisplayText( "STRING" );
				API.AddTextComponentSubstringPlayerName( text );
				API.EndTextCommandDisplayText( 0f, 0f + current );

				current += diff;
			}

			return Task.FromResult( 0 );
		}
	}
}
