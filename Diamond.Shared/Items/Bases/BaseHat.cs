#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared.Items.Bases
{
	public abstract class BaseHatItem : BaseItem
	{
		public virtual float Scale { get; set; } = 1f;
#if !USER_INTERFACE
		public virtual Vector3 Offset { get; set; } = Vector3.Zero;
		public virtual Vector3 Rotation { get; set; } = Vector3.Zero;
#endif
	}
}
