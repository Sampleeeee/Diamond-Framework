namespace Diamond.Shared.Items.Bases
{
    public abstract class BaseDrugItem : BaseItem
    {
        public override bool Illegal { get; set; } = true;
    }
}