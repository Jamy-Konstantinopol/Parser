namespace Parser
{
    internal static class DataGenerator
    {
        static public Dictionary<string, Reward> CreateRewardDictionary(Dictionary<string, TaskContent> taskContents, List<Reward> rewards)
        {
            if (taskContents == null || rewards == null)
                return new Dictionary<string, Reward>();

            var result = new Dictionary<string, Reward>();

            foreach (var taskContent in taskContents)
            {
                var matchedReward = (from reward in rewards
                                     where reward.Name == taskContent.Value.Reward
                                     select reward).FirstOrDefault();

                if (matchedReward != null)
                    result.Add(taskContent.Key, matchedReward);
            }

            return result;
        }
    }
}
