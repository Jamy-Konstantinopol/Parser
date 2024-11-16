using Parser;

string taskJsonPath = "..\\..\\..\\Data\\task.json";
string fileJsonPath = "..\\..\\..\\Data\\file.json";
string rewardCsvPath = "..\\..\\..\\Data\\items.csv";
string resultJsonPath = "..\\..\\..\\Data\\retult.json";
string resultCsvPath = "..\\..\\..\\Data\\retult.json";

try
{
    var dataLoader = new DataLoader();
    var tasks = dataLoader.GetFromJson<Parser.Task>(taskJsonPath);
    var taskContents = dataLoader.GetFromJson<TaskContent>(fileJsonPath);
    var records = dataLoader.GetFromCsv<Reward>(rewardCsvPath);
    var filteredTaskContents = new TasksFilter().FilterTaskContents(taskContents, tasks);

    foreach (var task in filteredTaskContents)
    {
        Console.WriteLine(task.Key);
    }

    //foreach (var record in records)
    //{
    //    Console.WriteLine(record.Name + " " + record.Money);
    //}
}
catch (Exception ex)
{
    Console.WriteLine("Что-то пошло не так: " + ex.Message);
}
