namespace Diamond.Shared.Inventory
{
	public abstract class BaseVehicle
	{
		public virtual string UniqueId => this.GetType().FullName;
		public abstract string Make { get; }
		public abstract string Model { get; }
		public virtual string Category => "Other";

		public abstract int Price { get; }
		public virtual bool Rentable => false;
		public virtual int RentPrice => 0;

#if CLIENT
		public abstract int VehicleHash { get; }
#endif
	}
}
