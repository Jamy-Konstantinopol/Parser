namespace Parser
{
    // Класс, объекты которого будут хранить значения из файла file.json
    internal class TaskContent
    {
        public int Id { get; set; }
        public string? Reward { get; set; }
        public int Weight { get; set; }
    }
}
