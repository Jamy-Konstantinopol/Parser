namespace Parser
{
    internal static class DataGenerator
    {
        static public Dictionary<string, Reward> CreateRewardDictionary(Dictionary<string, TaskContent> taskContents, List<Reward> rewards)
        {
            // Делаем проверку на null, ведь если что-то равно null,
            // то смысла идти дальше нет, плюс может быть неопределённое поведение
            if (taskContents == null || rewards == null)
                return new Dictionary<string, Reward>();

            var result = new Dictionary<string, Reward>();

            // Нахожу все значения, где reward.Name == taskContent.Value.Reward,
            // то есть первый столбец из items.csv равен равен свойству "reward" из файла file.json
            foreach (var taskContent in taskContents)
            {
                var matchedReward = (from reward in rewards
                                     where reward.Name == taskContent.Value.Reward
                                     select reward).FirstOrDefault();

                // Тоже делаю проверку на null
                if (matchedReward != null)
                    result.Add(taskContent.Key, matchedReward);
            }

            return result;
        }
    }
}
