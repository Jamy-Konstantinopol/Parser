using System.Reflection;
using System.Text.Json;

namespace Parser
{
    /// <summary>
    /// Интерфейс для стратегий сохранения данных.
    /// </summary>
    public interface IDataSavingStrategy
    {
        /// <summary>
        /// Сохраняет данные в файл.
        /// </summary>
        /// <typeparam name="T">Тип данных, которые нужно сохранить.</typeparam>
        /// <param name="data">Данные, которые нужно сохранить.</param>
        /// <param name="path">Путь, по которому будет сохранён файл.</param>
        void Save<T>(T data, string path);
    }

    /// <summary>
    /// Стратегия для сохранения данных в JSON.
    /// </summary>
    public class JsonSavingStrategy : IDataSavingStrategy
    {
        private JsonSerializerOptions _options;

        public JsonSavingStrategy(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public void Save<T>(T data, string path)
        {
            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(path, json);
        }
    }

    /// <summary>
    /// Стратегия для сохранения данных в CSV.
    /// </summary>
    public class CsvSavingStrategy : IDataSavingStrategy
    {
        public void Save<T>(T data, string path)
        {
            if (data is IEnumerable<T> enumerableData)
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (properties == null || properties.Length == 0)
                    return;

                using (var writer = new StreamWriter(path))
                {
                    foreach (var item in enumerableData)
                    {
                        var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "");
                        writer.WriteLine(string.Join(",", values));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Класс для сохранения данных, использующий стратегию для конкретного формата.
    /// </summary>
    internal class DataSaver
    {
        public IDataSavingStrategy SavingStrategy { private get; set; }

        /// <summary>
        /// Конструктор класса <see cref="DataSaver"/>. 
        /// Инициализирует стратегию сохранения данных.
        /// </summary>
        /// <param name="strategy">Стратегия сохранения данных (например, JSON или CSV).</param>
        public DataSaver(IDataSavingStrategy strategy)
        {
            SavingStrategy = strategy;
        }

        /// <summary>
        /// Сохраняет данные в файл с использованием выбранной стратегии.
        /// </summary>
        /// <typeparam name="T">Тип данных, который будет сохранён.</typeparam>
        /// <param name="data">Данные, которые нужно сохранить.</param>
        /// <param name="path">Путь, по которому будет сохранён файл.</param>
        public void Save<T>(T data, string path)
        {
            SavingStrategy.Save(data, path);
        }
    }
}
