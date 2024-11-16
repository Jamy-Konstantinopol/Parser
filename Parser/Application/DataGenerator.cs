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

        /// <summary>
        /// Создаёт полный список наград для задач, объединяя данные из словарей задач, содержимого задач и списка вознаграждений.
        /// </summary>
        /// <param name="taskDictionary">Словарь задач, где ключ — идентификатор задачи, а значение — объект <see cref="Task"/>.</param>
        /// <param name="rewardDictionary">Словарь наград задач, где ключ — идентификатор задачи, а значение — объект <see cref="Reward"/>.</param>
        /// <returns>Список объектов <see cref="TaskReward"/>.</returns>
        static public List<TaskReward> CreateTaskRewardList(
            Dictionary<string, Task> taskDictionary,
            Dictionary<string, Reward> rewardDictionary)
        {
            var result = new List<TaskReward>();

            foreach (var reward in rewardDictionary)
            {
                // Находим задачи, которые содержат текущий ключ награды.
                var filteredTaskKeys = (from task in taskDictionary
                                        where task.Value.List != null && task.Value.List.Contains(reward.Key)
                                        select task.Key).ToList();

                // Если таких задач нет, добавляем объект с IsUsed = false.
                if (filteredTaskKeys == null || !filteredTaskKeys.Any())
                {
                    var taskReward = new TaskRewardBuilder()
                        .SetListName("").SetObjectName(reward.Key)
                        .SetMoney(reward.Value.Money).SetDetails(reward.Value.Details)
                        .SetReputation(reward.Value.Reputation).SetIsUsed(false).Build();
                    result.Add(taskReward);
                    continue;
                }

                // Добавляем все связанные задачи с текущей наградой.
                foreach (var filteredTaskKey in filteredTaskKeys)
                {
                    var taskReward = new TaskRewardBuilder()
                        .SetListName(filteredTaskKey).SetObjectName(reward.Key)
                        .SetMoney(reward.Value.Money).SetDetails(reward.Value.Details)
                        .SetReputation(reward.Value.Reputation).SetIsUsed(true).Build();
                    result.Add(taskReward);
                }
            }

            return result;
        }
    }
}
