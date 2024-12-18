﻿using Parser;

string taskJsonPath = "..\\..\\..\\Data\\task.json";
string fileJsonPath = "..\\..\\..\\Data\\file.json";
string rewardCsvPath = "..\\..\\..\\Data\\items.csv";
string resultJsonPath = "..\\..\\..\\Data\\result.json";
string resultCsvPath = "..\\..\\..\\Data\\result.csv";

try
{
    var dataLoader = new DataLoader();
    dataLoader.DataLoadedNotify += (message) => Console.WriteLine(message);

    // Достаем данные из task.json
    var tasks = dataLoader.LoadFromJson<Parser.Task>(taskJsonPath);

    // Достаем данные из file.json
    var taskContents = dataLoader.LoadFromJson<TaskContent>(fileJsonPath);

    // Достаем данные из items.csv
    var rewards = dataLoader.LoadFromCsv<Reward>(rewardCsvPath);

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
    var dataGenerator = new DataGenerator();
    dataGenerator.DataGeneratedNotify += (message) => Console.WriteLine(message);
    var jsonResult = dataGenerator.CreateRewardDictionary(filteredTaskContents, rewards);

    // Сохраняем в новый файл result.json
    var dataSaver = new DataSaver(new JsonSavingStrategy());
    dataSaver.DataSavedNotify += (message) => Console.WriteLine(message);

    dataSaver.Save(jsonResult, resultJsonPath);

    // Получаем все данные в формате list_name,object_name,reward_key,money,details,reputation,isUsed. 
    var csvResult = dataGenerator.CreateTaskRewardList(tasks, dataGenerator.CreateRewardDictionary(taskContents, rewards));
    csvResult.Sort((a, b) =>
    {
        return string.Compare(a.ListName, b.ListName, StringComparison.Ordinal);
    });

    // Сохраняем в новый файл result.csv
    dataSaver.SavingStrategy = new CsvSavingStrategy();
    dataSaver.Save(csvResult, resultCsvPath);
}
catch (Exception ex)
{
    Console.WriteLine("Что-то пошло не так:\n" + ex.Message);
}
