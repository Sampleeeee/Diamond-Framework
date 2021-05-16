using System.Collections.Generic;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared.Items
{
	public class CombatPistolItem : BaseWeaponItem
	{
		public override string Name { get; set; } = "Combat Pistol";
		public override string Description { get; set; } = "gun";

#if CLIENT
		public override uint WeaponHash => ( uint )Client.Utility.GetHashKey( "WEAPON_COMBATPISTOL" );
#endif
	}
}
