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

        static public List<TaskReward> CreateTaskRewardList(
            Dictionary<string, Task> taskDictionary,
            Dictionary<string, Reward> rewardDictionary)
        {
            var result = new List<TaskReward>();

            foreach (var rewardPair in rewardDictionary)
            {
                

                var filteredTaskKeys = (from task in taskDictionary
                                              where task.Value.List != null && task.Value.List.Contains(rewardPair.Key)
                                              select task.Key).ToList();

                if (filteredTaskKeys == null || filteredTaskKeys.Count == 0)
                {
                    var taskReward = new TaskReward();
                    taskReward.ListName = "";
                    taskReward.ObjectName = rewardPair.Key;
                    taskReward.Money = rewardPair.Value.Money;
                    taskReward.Details = rewardPair.Value.Details;
                    taskReward.Reputation = rewardPair.Value.Reputation;
                    taskReward.IsUsed = false;
                    continue;
                }

                foreach (var task in filteredTaskKeys)
                {
                    var taskReward = new TaskReward();
                    taskReward.ListName = task;
                    taskReward.ObjectName = rewardPair.Key;
                    taskReward.Money = rewardPair.Value.Money;
                    taskReward.Details = rewardPair.Value.Details;
                    taskReward.Reputation = rewardPair.Value.Reputation;
                    taskReward.IsUsed = true;
                    result.Add(taskReward);
                }
            }

            return result;
        }
    }
}
