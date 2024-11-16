using Parser;

string taskJsonPath = "..\\..\\..\\Data\\task.json";
string fileJsonPath = "..\\..\\..\\Data\\file.json";
string rewardCsvPath = "..\\..\\..\\Data\\items.csv";
string resultJsonPath = "..\\..\\..\\Data\\result.json";
string resultCsvPath = "..\\..\\..\\Data\\result.csv";

try
{
    var dataLoader = new DataLoader();
    var tasks = dataLoader.GetFromJson<Parser.Task>(taskJsonPath);
    var taskContents = dataLoader.GetFromJson<TaskContent>(fileJsonPath);
    var rewards = dataLoader.GetFromCsv<Reward>(rewardCsvPath);
    var filteredTaskContents = new TasksFilter().FilterTaskContents(taskContents, tasks);
    var jsonResult = DataGenerator.CreateRewardDictionary(filteredTaskContents.Keys.ToList(), rewards);
    new DataSaver().SaveJson(jsonResult, resultJsonPath);
}
catch (Exception ex)
{
    Console.WriteLine("Что-то пошло не так:\n" + ex.Message);
}
