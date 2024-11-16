using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text.Json;
using static Parser.DataSaver;

namespace Parser
{
    /// <summary>
    /// Дополнительная абстракция для извлечения файлов json и csv.
    /// </summary>
    internal class DataLoader
    {
        public delegate void DataLoadedHandler(string message);

        private JsonSerializerOptions _options;
        private CsvConfiguration _config;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DataLoader"/> с заданными параметрами для конфигурации JSON и CSV.
        /// </summary>
        /// <param name="options">
        /// Опции сериализации для JSON. Если не указаны, используется стандартная конфигурация с именами свойств в стиле camelCase.
        /// </param>
        /// <param name="config">
        /// Конфигурация для CSV. Если не указаны, используется стандартная конфигурация без заголовков.
        /// </param>
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

        public event DataLoadedHandler? DataLoadedNotify;

        /// <summary>
        /// Читает JSON-файл и десериализует его в словарь, где ключи — строки, а значения — объекты типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип объектов, которые будут десериализованы из значений JSON. Должен быть типом, совместимым с данными в файле.</typeparam>
        /// <param name="path">Путь к JSON-файлу, который будет прочитан и десериализован.</param>
        /// <returns>Словарь, где ключи — строки (имена объектов из JSON), а значения — объекты типа <typeparamref name="T"/>.</returns>
        public Dictionary<string, T> LoadFromJson<T>(string path)
        {
            // Чтение данных из JSON-файла
            string jsonFile = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonFile, _options);

            // Возвращаем десериализованный словарь или пустой словарь, если данные не были десериализованы
            var result = data ?? new Dictionary<string, T>();

            DataLoadedNotify?.Invoke($"Данные загружены: {path}");
            return result;
        }

        /// <summary>
        /// Читает CSV-файл и десериализует его в список объектов типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип объектов, которые будут десериализованы из строк CSV. Должен быть типом, совместимым с данными в файле.</typeparam>
        /// <param name="path">Путь к CSV-файлу, который будет прочитан и десериализован.</param>
        /// <returns>Список объектов типа <typeparamref name="T"/>, соответствующих строкам в CSV-файле.</returns>
        public List<T> LoadFromCsv<T>(string path)
        {
            // Чтение данных из CSV-файла
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, _config);

            // Возвращаем список объектов, соответствующих строкам CSV
            var result = csv.GetRecords<T>().ToList();
            DataLoadedNotify?.Invoke($"Данные загружены: {path}");
            return result;
        }
    }
}
