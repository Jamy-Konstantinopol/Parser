namespace Parser
{
    // Класс, объекты которого будут хранить значения из файла task.json
    internal class Task
    {
        public int Id { get; set; }
        public List<string>? List { get; set; }
    }
}
