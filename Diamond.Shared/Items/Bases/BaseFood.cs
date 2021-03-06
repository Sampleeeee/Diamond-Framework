using System.Collections.Generic;
using System.Threading.Tasks;

#if !USER_INTERFACE
using CitizenFX.Core;
using CitizenFX.Core.Native;
#endif

namespace Diamond.Shared.Items.Bases
{
	public abstract class BaseFoodItem : BaseItem, IUseableItem
	{
		public virtual int RestoreHunger => 10;
		public virtual string Model => "prop_cs_burger_01";
#if !USER_INTERFACE
		public virtual Vector3 Offset => new Vector3( 0.15f, 0f, 0.05f );
		public virtual Vector3 Rotation => new Vector3( 0f, 120f, 0f );
#endif

		public virtual KeyValuePair<string, string> Animation =>
			new KeyValuePair<string, string>( "mp_player_inteat@burger", "mp_player_int_eat_burger_fp" );

		public string UseWord => "Consume";

		public bool CanUse( Character character )
		{
			// TODO will eventaully want to add more of these
			return !character.Arrested;
		}

#if SERVER
        public void OnUse(Character character) =>
            character.Hunger += RestoreHunger;
#elif CLIENT
		public async Task OnUse( Character character )
		{
			var prop = await World.CreateProp( new Model( this.Model ), Game.PlayerPed.Position, Vector3.Zero, false, false );
			prop.AttachTo( Game.PlayerPed.Bones[Bone.SKEL_L_Hand], this.Offset, this.Rotation );

			float duration = API.GetAnimDuration( this.Animation.Key, this.Animation.Value );

			Game.PlayerPed.Task.PlayAnimation( this.Animation.Key, this.Animation.Value, 8f, -1,
				( AnimationFlags )49 );

			await BaseScript.Delay( ( int )( duration * 1000f ) );

			prop.Delete();
			Game.PlayerPed.Task.ClearSecondary();
		}
#endif
	}
}
