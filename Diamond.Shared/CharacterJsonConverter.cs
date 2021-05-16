using System;
using System.Collections.Generic;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.Jobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Diamond.Shared
{
    public class CharacterJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Character character)) return;
            
            writer.WriteStartObject();
                writer.WritePropertyName("FirstName");
                writer.WriteValue(character.FirstName);
                        
                writer.WritePropertyName("LastName");
                writer.WriteValue(character.LastName);
                
                writer.WritePropertyName("Age");
                writer.WriteValue(character.Age);
                        
                writer.WritePropertyName("Inventory");
                writer.WriteStartObject();
                    foreach (var kvp in character.ItemInventory)
                    {
                        if (kvp.Value <= 0) continue;
                        
                        writer.WritePropertyName(kvp.Key.UniqueId);
                        writer.WriteValue(kvp.Value);
                    }
                writer.WriteEndObject();
                
                writer.WritePropertyName("Money");
                writer.WriteValue(character.Money);
                
                writer.WritePropertyName("Bank");
                writer.WriteValue(character.Bank);
                
                writer.WritePropertyName("DirtyMoney");
                writer.WriteValue(character.DirtyMoney);
                
                writer.WritePropertyName("Job");
                writer.WriteValue(character.Job.UniqueId);
                
                writer.WritePropertyName("Hunger");
                writer.WriteValue(character.Hunger);
                
                writer.WritePropertyName("Thirst");
                writer.WriteValue(character.Thirst);

                writer.WritePropertyName("JobGrade");
                writer.WriteValue(character.JobGrade.UniqueId);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType != typeof(Character)) return existingValue;

            var item = JObject.Load(reader);
            var items = item["Inventory"]?.ToObject<Dictionary<string, int>>();

            var job = item["Job"]?.ToObject<string>();
            var jobGrade = item["JobGrade"]?.ToObject<string>();
            
            var character = new Character
            {
                FirstName = item["FirstName"]?.ToObject<string>() ?? string.Empty,
                LastName = item["LastName"]?.ToObject<string>() ?? string.Empty,
                Age = item["Age"]?.ToObject<int>() ?? 0,
                Money = item["Money"].ToObject<int>(),
                Bank = item["Bank"].ToObject<int>(),
                DirtyMoney = item["DirtyMoney"].ToObject<int>(),
                Hunger = item["Hunger"].ToObject<int>(),
                Thirst = item["Thirst"].ToObject<int>(),
                Job = Activator.CreateInstance(Type.GetType(job)) as BaseJob,
                JobGrade = Activator.CreateInstance(Type.GetType(jobGrade)) as BaseJobGrade
            };

            character.ItemInventory = new ItemInventory(character, items);
            return character;
        }

        public override bool CanConvert(Type objectType) =>
            objectType == typeof(Character);
    }
}