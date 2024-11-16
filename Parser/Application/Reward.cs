namespace Parser
{
    // Класс, объекты которого будут хранить значения из файла items.csv
    internal class Reward
    {
        public string? Name { get; set; }
        public int Money { get; set; }
        public int Details { get; set; }
        public int Reputation { get; set; }
    }
}
