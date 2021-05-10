

using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.Items
{
    public class CocaineBagItem : BaseDrugItem
    {
        public override string Name { get; set; } = "Cocaine Bag";
        public override string Description { get; set; } = "A bag of cocaine";
    }
    
    public class DrugsItem : BaseDrugItem
    {
        public override string Name { get; set; } = "Drugs";
        public override string Description { get; set; } = "Toxic drugs";
    }

    public class WeedBagItem : BaseDrugItem
    {
        public override string Name { get; set; } = "Weed Bag";
        public override string Description { get; set; } = "A bag of weed";
    }
}