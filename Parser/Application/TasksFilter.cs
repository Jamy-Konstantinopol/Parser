namespace Parser
{
    /// <summary>
    /// Класс для фильтрации содержимого задач в зависимости от наличия задач в коллекции <see cref="tasks"/>.
    /// Позволяет получить задачи, которые есть в списке задач, или наоборот — те, которых нет.
    /// </summary>
    internal class TasksFilter
    {
        /// <summary>
        /// Инициализирует новый экземпляр фильтра задач с заданным параметром для включения или исключения задач из фильтра.
        /// </summary>
        /// <param name="includeTasks">
        /// Булев параметр, который определяет, должны ли быть включены задачи из <see cref="tasks"/>:
        /// если <c>true</c>, фильтруются только те задачи, которые есть в <see cref="tasks"/>; 
        /// если <c>false</c>, фильтруются задачи, которых нет в <see cref="tasks"/>.
        /// Значение по умолчанию — <c>true</c>.
        /// </param>
        public TasksFilter(bool includeTasks = true)
        {
            IncludeTasks = includeTasks;
        }

        /// <summary>
        /// Получает или задает флаг, который определяет, должны ли быть включены задачи из <see cref="tasks"/>.
        /// Если значение равно <c>true</c>, возвращаются задачи, которые есть в списке <see cref="tasks"/>,
        /// если <c>false</c> — задачи, которых нет в списке <see cref="tasks"/>.
        /// </summary>
        public bool IncludeTasks { get; set; }

        /// <summary>
        /// Фильтрует задачи из <see cref="taskContents"/>, возвращая только те, которые соответствуют фильтру на основе <see cref="tasks"/>.
        /// Если <see cref="IncludeTasks"/> равно <c>true</c>, возвращаются только те задачи, которые есть в <see cref="tasks"/>,
        /// если <see cref="IncludeTasks"/> равно <c>false</c>, то возвращаются задачи, которых нет в <see cref="tasks"/>.
        /// </summary>
        /// <param name="taskContents">Словарь с содержимым задач, где ключом является идентификатор задачи, а значением — объект <see cref="TaskContent"/>.</param>
        /// <param name="tasks">Словарь с задачами, которые необходимо использовать для фильтрации.</param>
        /// <returns>Словарь с отфильтрованным содержимым задач в зависимости от значения <see cref="IncludeTasks"/>.</returns>
        public Dictionary<string, TaskContent> FilterTaskContents(
            Dictionary<string, TaskContent> taskContents,
            Dictionary<string, Parser.Task> tasks)
        {
            // Создаем HashSet, который содержит все задачи в tasks для удобства
            var tasksList = new HashSet<string>(tasks.Values.SelectMany(value => value.List ?? new List<string>()));

            // Тут мы фильтруем все задачи из taskContents, которых нет в tasksList (tasks) и возвращаем результат
            return (from content in taskContents
                    where IncludeTasks ? tasksList.Contains(content.Key) : !tasksList.Contains(content.Key)
                    select content).ToDictionary();
        }
    }
}
