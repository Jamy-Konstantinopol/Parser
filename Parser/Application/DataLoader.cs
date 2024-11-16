using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text.Json;

namespace Parser
{
    /// <summary>
    /// Дополнительная абстракция для извлечения файлов json и csv
    /// </summary>
    internal class DataLoader
    {
        private JsonSerializerOptions _options;
        private CsvConfiguration _config;

        public DataLoader(JsonSerializerOptions? options = null, CsvConfiguration? config = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            _config = config ?? new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
        }

        /// <summary>
        /// Читает JSON-файл и десериализует его в словарь, где ключи — строки, а значения — объекты типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип объектов, которые будут десериализованы из значений JSON. Должен быть типом, совместимым с данными в файле.</typeparam>
        /// <param name="path">Путь к JSON-файлу, который будет прочитан и десериализован.</param>
        /// <returns>Словарь, где ключи — строки (имена объектов из JSON), а значения — объекты типа <typeparamref name="T"/>.</returns>
        public Dictionary<string, T> GetFromJson<T>(string path)
        {
            string jsonFile = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonFile, _options);
            return data ?? new Dictionary<string, T>();
        }

        /// <summary>
        /// Читает CSV-файл и десериализует его в список объектов типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип объектов, которые будут десериализованы из строк CSV. Должен быть типом, совместимым с данными в файле.</typeparam>
        /// <param name="path">Путь к CSV-файлу, который будет прочитан и десериализован.</param>
        /// <returns>Список объектов типа <typeparamref name="T"/>, соответствующих строкам в CSV-файле.</returns>
        public List<T> GetFromCsv<T>(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, _config);
            return csv.GetRecords<T>().ToList();
        }
    }
}
