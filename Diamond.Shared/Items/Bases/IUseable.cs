using System.Threading.Tasks;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared.Items.Bases
{
    public interface IUseableItem
    {
        string UseWord { get; }
        bool CanUse(Character character);
        
#if SERVER
        void OnUse(Character character);
#elif CLIENT
        Task OnUse(Character character);
#endif
    }
}
