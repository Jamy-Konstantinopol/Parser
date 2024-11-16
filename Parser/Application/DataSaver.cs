using System.Text.Json;

namespace Parser
{
    internal class DataSaver
    {
        JsonSerializerOptions _options;

        public DataSaver(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public void SaveJson<T>(T data, string path)
        {
            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(path, json);
        }
    }
}
