using System.Collections.Generic;
using System.Threading.Tasks;

#if !USER_INTERFACE
using CitizenFX.Core;
using CitizenFX.Core.Native;
#endif

namespace Diamond.Shared.Items.Bases
{
	public abstract class BaseDrinkItem : BaseItem, IUseableItem
	{
		public virtual int RestoreThirst => 10;
		public string UseWord => "Drink";

		public virtual string Model => "prop_ld_flow_bottle";

#if !USER_INTERFACE
		public virtual Vector3 Offset => new Vector3( 0.15f, 0f, 0.05f );
		public virtual Vector3 Rotation => new Vector3( 0f, 120f, 0f );
#endif

		public virtual KeyValuePair<string, string> Animation =>
			new KeyValuePair<string, string>( "mp_player_intdrink", "loop_bottle" );

		public virtual int TimeOverride => -1;

		public virtual bool CanUse( Character character ) =>
			Utility.DefaultCanUse( this, character );

#if SERVER
        public void OnUse(Character character) =>
            character.Thirst += RestoreThirst;
#elif CLIENT
		public virtual async Task OnUse( Character character )
		{
			Debug.WriteLine( "Drinking drink!" );
			var prop = await World.CreateProp( new Model( Model ), Game.PlayerPed.Position, Vector3.Zero, false, false );
			prop.AttachTo( Game.PlayerPed.Bones[Bone.SKEL_L_Hand], Offset, Rotation );

			float duration = API.GetAnimDuration( Animation.Key, Animation.Value );

			Game.PlayerPed.Task.PlayAnimation( Animation.Key, Animation.Value, 8f, -1,
				( AnimationFlags )49 );

			if ( TimeOverride != -1 )
				await BaseScript.Delay( TimeOverride );
			else
				await BaseScript.Delay( ( int )( duration * 1000f ) );

			prop.Delete();
			Game.PlayerPed.Task.ClearSecondary();
		}
#endif
	}
}
