using System.Threading.Tasks;
using CitizenFX.Core;

namespace Diamond.Shared.Items.Bases
{
    public interface IUseableItem
    {
        string UseWord { get; }
        bool CanUse(Character character);
        
#if SERVER
        void OnUse(Character character);
#else
        Task OnUse(Character character);
#endif
    }
}