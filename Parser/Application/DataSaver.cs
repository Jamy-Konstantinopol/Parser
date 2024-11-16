using System.Reflection;
using System.Text.Json;

namespace Parser
{
    /// <summary>
    /// Класс для сохранения данных в форматы JSON и CSV.
    /// </summary>
    internal class DataSaver
    {
        private JsonSerializerOptions _options;

        /// <summary>
        /// Конструктор класса <see cref="DataSaver"/>. 
        /// Инициализирует настройки сериализации JSON.
        /// </summary>
        /// <param name="options">Настройки сериализации JSON. Если <c>null</c>, используются значения по умолчанию.</param>
        public DataSaver(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        /// <summary>
        /// Сохраняет объект в JSON-файл.
        /// </summary>
        /// <typeparam name="T">Тип данных, который будет сериализован в JSON.</typeparam>
        /// <param name="data">Данные, которые нужно сохранить в JSON.</param>
        /// <param name="path">Путь, по которому будет сохранён файл JSON.</param>
        public void SaveJson<T>(T data, string path)
        {
            // Сериализуем данные в JSON
            string json = JsonSerializer.Serialize(data, _options);
            // Записываем JSON в файл
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Сохраняет данные в CSV-файл.
        /// </summary>
        /// <typeparam name="T">Тип данных, который будет сохранён в CSV.</typeparam>
        /// <param name="data">Коллекция данных, которые нужно сохранить в CSV.</param>
        /// <param name="path">Путь, по которому будет сохранён файл CSV.</param>
        public void SaveCsv<T>(IEnumerable<T> data, string path)
        {
            // Получаем все публичные свойства типа T
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Если свойств нет, выходим из метода
            if (properties == null || properties.Length == 0)
                return;

            // Открываем поток для записи в файл
            using (var writer = new StreamWriter(path))
            {
                foreach (var item in data)
                {
                    // Получаем значения всех свойств элемента
                    var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "");

                    // Если значения найдены, записываем их в файл, разделённые запятыми
                    if (values != null)
                        writer.WriteLine(string.Join(",", values));
                }
            }
        }
    }
}
