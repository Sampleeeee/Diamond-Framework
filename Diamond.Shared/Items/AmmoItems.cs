

using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.Items
{
	public class PistolAmmoItem : BaseAmmoItem
	{
		public override string Name { get; set; } = "Pistol Ammo";
		public override string Description { get; set; } = "Ammo for pistols (Contains 60 bullets)";

#if CLIENT
		public override int AmmoHash => Client.Utility.GetHashKey( "AMMO_PISTOL" );
#endif

		public override int Price { get; set; } = 100;
		public override int Amount => 60;
	}

	public class RifleAmmoItem : BaseAmmoItem
	{
		public override string Name { get; set; } = "Rifle Ammo";
		public override string Description { get; set; } = "Ammo for rifles (Contains 80 bullets)";

#if CLIENT
		public override int AmmoHash => Client.Utility.GetHashKey( "AMMO_RIFLE" );
#endif

		public override int Price { get; set; } = 500;
		public override int Amount => 80;
	}

	public class ShotgunAmmoItem : BaseAmmoItem
	{
		public override string Name { get; set; } = "Shotgun Ammo";
		public override string Description { get; set; } = "Ammo for shotguns (Contains 40 shells)";

#if CLIENT
		public override int AmmoHash => Client.Utility.GetHashKey( "AMMO_SHOTGUN" );
#endif

		public override int Price { get; set; } = 300;
		public override int Amount => 40;
	}

	public class SmgAmmoItem : BaseAmmoItem
	{
		public override string Name { get; set; } = "SMG Ammo";
		public override string Description { get; set; } = "Ammo for SMGs (Contains 100 bullets)";

#if CLIENT
		public override int AmmoHash => Client.Utility.GetHashKey( "AMMO_SMG" );
#endif

		public override int Price { get; set; } = 400;
		public override int Amount => 100;
	}

	public class SniperAmmoItem : BaseAmmoItem
	{
		public override string Name { get; set; } = "Sniper Ammo";
		public override string Description { get; set; } = "Ammo for snipers (Contains 30 rounds)";

#if CLIENT
		public override int AmmoHash => Client.Utility.GetHashKey( "AMMO_SNIPER" );
#endif

		public override int Price { get; set; } = 600;
		public override int Amount => 30;
	}

	/*
        TODO - Add these ammo types
        AMMO_MG
        AMMO_MOLOTOV
     */
}
