using CitizenFX.Core;
using Diamond.Shared;
using Diamond.Shared.Items;
using Newtonsoft.Json;

namespace Diamond.Client.Handlers
{
    public class CharacterHandler : BaseScript
    {
        [EventHandler("SetupCharacter")]
        public void SetupCharacter(string json)
        {
            MainClient.Character = JsonConvert.DeserializeObject<Character>(json, new CharacterJsonConverter());
        }

        [EventHandler("ThirstUpdated")]
        private void OnThirstUpdated(int val)
        {
            if (MainClient.Character != null)
                MainClient.Character.Thirst = val;
        }
        
        [EventHandler("HungerUpdated")]
        private void OnHungerUpdated(int val)
        {
            if (MainClient.Character != null)
                MainClient.Character.Hunger = val;
        }
    }
}