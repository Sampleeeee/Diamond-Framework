using System.Collections.Generic;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared.Items
{
	public class CombatPistolItem : BaseWeaponItem
	{
		public override string Name { get; set; } = "Combat Pistol";
		public override string Description { get; set; } = "gun";

		public override string ImageUrl => "./images/items/WEAPON_COMBATPISTOL.png";
#if CLIENT
		public override uint WeaponHash => ( uint )Client.Utility.GetHashKey( "WEAPON_COMBATPISTOL" );
#endif
	}
}
