using Parser;

string taskJsonPath = "..\\..\\..\\Data\\task.json";
string fileJsonPath = "..\\..\\..\\Data\\file.json";
string rewardCsvPath = "..\\..\\..\\Data\\items.csv";
string resultJsonPath = "..\\..\\..\\Data\\result.json";
string resultCsvPath = "..\\..\\..\\Data\\result.csv";

try
{
    var dataLoader = new DataLoader();

    // Достаем данные из task.json
    var tasks = dataLoader.GetFromJson<Parser.Task>(taskJsonPath);

    // Достаем данные из file.json
    var taskContents = dataLoader.GetFromJson<TaskContent>(fileJsonPath);

    // Достаем данные из items.csv
    var rewards = dataLoader.GetFromCsv<Reward>(rewardCsvPath);

    // Фильтруем данные в taskContents, которых нет в tasks
    var filteredTaskContents = new TasksFilter().FilterTaskContents(taskContents, tasks);

    // Создаем объекты подобного типа:
    //  {
    //      "object_6813": {
    //          "name": "explorer_cosmic",
    //          "money": 500,
    //          "details": 10,
    //          "reputation": 62
    //      }
    //  }
    //
    var jsonResult = DataGenerator.CreateRewardDictionary(filteredTaskContents, rewards);

    // Сохраняем в новый файл result.json
    var dataSaver = new DataSaver();
    dataSaver.SaveJson(jsonResult, resultJsonPath);


    dataSaver.SaveCsv(jsonResult, resultCsvPath);
}
catch (Exception ex)
{
    Console.WriteLine("Что-то пошло не так:\n" + ex.Message);
}
