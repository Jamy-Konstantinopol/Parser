using System.Reflection;
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

        public void SaveCsv<T>(IEnumerable<T> data, string path)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (properties == null || properties.Length == 0)
                return;

            using (var writer = new StreamWriter(path))
            {
                foreach (var item in data)
                {
                    var values = properties.Select(p => p.GetValue(item, null)?.ToString() ?? "");

                    if (values != null)
                        writer.WriteLine(string.Join(",", values));
                }
            }
        }
    }
}
