namespace Parser
{
    /// <summary>
    /// Класс для генерации данных, включая создание словаря вознаграждений, соответствующих задачам.
    /// </summary>
    internal static class DataGenerator
    {
        /// <summary>
        /// Создаёт словарь, в котором ключами являются идентификаторы задач, а значениями — соответствующие вознаграждения.
        /// </summary>
        /// <param name="taskContents">Словарь с содержимым задач, где ключ — идентификатор задачи, а значение — объект типа <see cref="TaskContent"/>.</param>
        /// <param name="rewards">Список вознаграждений, из которых будет выбрано соответствующее для каждой задачи.</param>
        /// <returns>
        /// Словарь, где ключами являются идентификаторы задач, а значениями — соответствующие вознаграждения. 
        /// </returns>
        static public Dictionary<string, Reward> CreateRewardDictionary(Dictionary<string, TaskContent> taskContents, List<Reward> rewards)
        {
            // Проверка на null, чтобы избежать неопределенного поведения
            if (taskContents == null || rewards == null)
                return new Dictionary<string, Reward>();

            var result = new Dictionary<string, Reward>();

            // Поиск соответствующего вознаграждения для каждой задачи
            foreach (var taskContent in taskContents)
            {
                // Находим вознаграждение, которое соответствует свойству "Reward" из TaskContent
                var matchedReward = (from reward in rewards
                                     where reward.Name == taskContent.Value.Reward
                                     select reward).FirstOrDefault();

                // Если найдено соответствующее вознаграждение, добавляем его в результат
                if (matchedReward != null)
                    result.Add(taskContent.Key, matchedReward);
            }

            return result;
        }
    }
}
