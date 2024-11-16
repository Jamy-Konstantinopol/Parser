namespace Parser
{
    internal static class DataGenerator
    {
        static public Dictionary<string, T> CreateRewardDictionary<T>(List<string> taskContentKey, List<T> values)
        {
            if (taskContentKey == null || values == null)
                return new Dictionary<string, T>();

            var result = new Dictionary<string, T>();
            int count = Math.Min(taskContentKey.Count, values.Count);

            for (int i = 0; i < count; i++)
                result[taskContentKey[i]] = values[i];

            return result;
        }
    }
}
