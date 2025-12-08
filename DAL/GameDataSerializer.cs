using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GameDataSerializer : IGameDataSerializer
    {
        public string SerializeList<T>(List<T> items)
        {
            return JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = false });
        }

        public List<string> DeserializeToListOfStrings(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return new List<string>();
            }
            var list = JsonSerializer.Deserialize<List<string>>(inputString);
            return list ?? new List<string>();
        }

        public List<EnumPlatforms> DeserializeToEnumOfPlatforms(string platforms)
        {
            var platformInts = JsonSerializer.Deserialize<List<int>>(platforms);
            if (platformInts == null)
                return new List<EnumPlatforms>();

            return platformInts
                .Where(i => Enum.IsDefined(typeof(EnumPlatforms), i))
                .Select(i => (EnumPlatforms)i)
                .ToList();
        }
    }
}
