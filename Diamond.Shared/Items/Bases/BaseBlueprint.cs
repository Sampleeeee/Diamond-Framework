using System.Threading.Tasks;

namespace Diamond.Shared.Items.Bases
{
	public abstract class BaseBlueprintItem : BaseItem, IUseableItem
	{
		public abstract string StudyItem { get; set; }

		public string UseWord => "Study";

		public bool CanUse( Character character ) =>
			Utility.DefaultCanUse( this, character );

#if SERVER
        public void OnUse(Character character) =>
            Utility.DefaultOnUse(this, character);
#elif CLIENT
		public async Task OnUse( Character character ) =>
			Utility.DefaultOnUse( this, character );
#endif
	}
}
