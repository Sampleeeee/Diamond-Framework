using System.Collections.Generic;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared.Items
{

    public class CoalItem : BaseItem
    {
        public override string Name { get; set; } = "Coal";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "Ore containing traces of coal.";
        public override int Weight { get; set; } = 1;
    }

    public class MetalOreItem : BaseItem
    {
        public override string Name { get; set; } = "Metal Ore";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "Rock containing metal compounds. Metal can be extracted using heat.";
        public override int Weight { get; set; } = 1;
    }

    public class RockItem : BaseItem
    {
        public override string Name { get; set; } = "Rock";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A small piece of stone.";
        public override int Weight { get; set; } = 1;
    }

    public class SulfurItem : BaseItem
    {
        public override string Name { get; set; } = "Sulfur";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A mineral containing sulfur.";
        public override int Weight { get; set; } = 1;
    }

    public class PaintBucketItem : BaseItem
    {
        public override string Name { get; set; } = "Paint Bucket";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A bucket of paint.";
        public override int Weight { get; set; } = 1;
    }
}