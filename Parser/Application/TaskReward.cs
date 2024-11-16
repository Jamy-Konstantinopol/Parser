namespace Parser
{
    // Класс, объекты которого будут сохранены в файл result.csv
    internal class TaskReward
    {
        public string? ListName { get; set; }
        public string? ObjectName { get; set; }
        public int Money { get; set; }
        public int Details { get; set; }
        public int Reputation { get; set; }
        public bool IsUsed { get; set; }
    }

    // Builder для класса TaskReward.
    internal class TaskRewardBuilder
    {
        private string? _listName = "";
        private string? _objectName = "";
        private int _money = 0;
        private int _details = 0;
        private int _reputation = 0;
        private bool _isUsed = false;

        public TaskRewardBuilder SetListName(string? value)
        {
            _listName = value;
            return this;
        }

        public TaskRewardBuilder SetObjectName(string? value)
        {
            _objectName = value;
            return this;
        }

        public TaskRewardBuilder SetMoney(int value)
        {
            _money = value;
            return this;
        }

        public TaskRewardBuilder SetDetails(int value)
        {
            _details = value;
            return this;
        }

        public TaskRewardBuilder SetReputation(int value)
        {
            _reputation = value;
            return this;
        }

        public TaskRewardBuilder SetIsUsed(bool value)
        {
            _isUsed = value;
            return this;
        }

        public TaskReward Build()
        {
            return new TaskReward
            {
                ListName = _listName,
                ObjectName = _objectName,
                Money = _money,
                Details = _details,
                Reputation = _reputation,
                IsUsed = _isUsed,
            };
        }
    }
}
