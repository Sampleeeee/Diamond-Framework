namespace Diamond.Shared.Inventory
{
    public class VehicleInventory : BaseInventory<BaseVehicle>
    {
        public VehicleInventory(Character owner) : base(owner)
        { }
    }
}