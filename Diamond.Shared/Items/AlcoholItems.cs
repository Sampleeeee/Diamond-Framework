using System.Management.Instrumentation;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared.Items
{
    public class BeerItem : BaseAlcoholItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Beer";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A large glass bottle of beer.";

        public override string Model => "prop_beer_bottle";
        public override Vector3 Rotation => new Vector3(90f, 120f, 40f);
        public override Vector3 Offset => new Vector3(0.05f, -0.15f, 0f);

        public override float Drunkness => 0.3f;
        public override int Damage => 5;
        
        public int Price { get; set; } = 60;
        public int BulkAmount { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class BrandyItem : BaseAlcoholItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Brandy";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A bottle of brandy.";

        public override float Drunkness => 0.25f;
        public override int Damage => 8;
        
        public int Price { get; set; } = 60;
        public int BulkAmount { get; set; } = 1;

        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class ChampagneItem : BaseAlcoholItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Champagne";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A bottle of sparkling champagne.";

        public override float Drunkness => 0.5f;
        public override int Damage => 5;
        
        public int Price { get; set; } = 200;
        public int BulkAmount { get; set; }
        
        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class RumItem : BaseAlcoholItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Rum";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A clear jug of rum.";

        public override float Drunkness => 0.3f;
        public override int Damage => 8;
        
        public int Price { get; set; } = 80;
        public int BulkAmount { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class VodkaItem : BaseAlcoholItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Vodka";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A bottle of premium distilled vodka.";

        public override float Drunkness => 0.45f;
        public override int Damage => 15;
        
        public int Price { get; set; } = 120;
        public int BulkAmount { get; set; } = 1;

        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class WineItem : BaseAlcoholItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Wine";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A bottle of red wine.";

        public override float Drunkness => 0.4f;
        public override int Damage => 10;

        public int BulkAmount { get; set; } = 1;
        public int Price { get; set; } = 70;
        
        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
}