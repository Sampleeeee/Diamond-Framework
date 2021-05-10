namespace Diamond.Shared.Items.Bases
{
    // TODO this probably should not be useable
    // If a weapon is in a player's inventory,
    // it should put it into their weapon wheel
    public abstract class BaseWeaponItem : BaseItem
    {
#if CLIENT
        public abstract uint WeaponHash { get; }
#endif
    }
}