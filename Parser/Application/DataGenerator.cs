using System.Text.Json;

namespace Parser
{
    internal class DataGenerator
    {
        JsonSerializerOptions _options;

        public DataGenerator(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        static public Dictionary<string, Reward> CreateRewardDictionary(List<string> taskContentKey, List<Reward> rewards)
        {
            if (taskContentKey == null || rewards == null)
                return new Dictionary<string, Reward>();

            var result = new Dictionary<string, Reward>();
            int count = Math.Min(taskContentKey.Count, rewards.Count);

            for (int i = 0; i < count; i++)
                result[taskContentKey[i]] = rewards[i];

            return result;
        }

        public void SaveJson<T>(T data, string path)
        {
            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(path, json);
        }
    }
}
