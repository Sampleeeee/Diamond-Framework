using System.Threading.Tasks;
using System.Timers;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared.Items.Bases
{
    public abstract class BaseAlcoholItem : BaseDrinkItem
    {
        public override int RestoreThirst => 0;
        public virtual float Drunkness => 1f;
        public virtual int Damage => 1;
        
#if CLIENT
        public override async Task OnUse(Character character)
        {
            await base.OnUse(character);
            // TODO this
        }
#endif
    }
}
