using System;
using System.Collections.Generic;
using System.Linq;
using Diamond.Shared.Inventory;
using Diamond.Shared.Jobs;
using Diamond.Shared.JsonConverters;
using Newtonsoft.Json;

#if !USER_INTERFACE
using CitizenFX.Core;
using CitizenFX.Core.Native;
#endif

namespace Diamond.Shared
{
	using Items.Bases;

	[JsonConverter( typeof( CharacterJsonConverter ) )]
	public class Character
	{
#if !USER_INTERFACE
		public Player Player { get; set; }

		public Ped PlayerPed =>
			Entity.FromHandle( API.GetPlayerPed( this.Player.Handle ) ) as Ped;
#endif

		public bool Alive
		{
			get
			{
#if SERVER
                // todo this
                return true;
#elif CLIENT
				return this.PlayerPed.IsDead;
#else
	            // todo this
	            return true;
#endif
			}
		}

		private int _money;
		public int Money
		{
			get => this._money;
			set
			{
				this._money = value;
#if SERVER
                Player.TriggerEvent("MoneyUpdated", _money);
#endif
			}
		}

		private int _bank;
		public int Bank
		{
			get => this._bank;
			set
			{
				this._bank = value;
#if SERVER
                Player.TriggerEvent("BankUpdated", _bank);
#endif
			}
		}

		private int _dirtyMoney;
		public int DirtyMoney
		{
			get => this._money;
			set
			{
				this._dirtyMoney = value;
#if SERVER
                Player.TriggerEvent("DirtyMoneyUpdated", _dirtyMoney);
#endif
			}
		}

		private int _hunger;
		public int Hunger
		{
			get => this._hunger;
			set
			{
				this._hunger = Utility.Clamp( value, 0, 100 );

#if SERVER
                Player.TriggerEvent("HungerUpdated", _hunger);
#endif
			}
		}

		private int _thirst;
		public int Thirst
		{
			get => this._thirst;
			set
			{
				this._thirst = Utility.Clamp( value, 0, 100 );

#if SERVER
                Player.TriggerEvent("ThirstUpdated", _thirst);
#endif
			}
		}

		public ItemInventory ItemInventory;
		public VehicleInventory VehicleInventory;

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{this.FirstName} {this.LastName}";

		public bool Arrested { get; set; } = false;
		public bool Recovery { get; set; } = false;

		public int Age { get; set; }
		public float Health { get; set; }
		public float Drunkness { get; set; }
		public bool Crippled { get; set; }
		public TimeSpan LastHealTime { get; set; }

		public BaseJob Job { get; set; } = new UnemployedJob();
		public BaseJobGrade JobGrade { get; set; } = new UnemployedJob.Unemployed();

		public Character() { }

		public void DropItem( BaseItem item, int amount = 1 )
		{
			throw new NotImplementedException();
		}

		public void AddAmmo( string type, int amount )
		{
			throw new NotImplementedException();
		}

		public bool HasWeapon( string weaponClass )
		{
			throw new NotImplementedException();
		}

		public void AddWeapon( string weaponClass )
		{
			throw new NotImplementedException();
		}

		public void AddHunger( int restoreHunger )
		{
			throw new NotImplementedException();
		}

		public void Kill()
		{
			throw new NotImplementedException();
		}

		public bool KnowsBlueprint( BaseBlueprintItem baseBlueprintItem )
		{
			throw new NotImplementedException();
		}

		public void LearnBlueprint( BaseBlueprintItem baseBlueprintItem )
		{
			throw new NotImplementedException();
		}

		public bool CanAfford( int amount ) => this.Money - amount >= 0;
	}

	[Flags]
	public enum Gender
	{
		Male, Female
	}
}
