int[] inputList = [1, 2, 3, 4, 5];

var httpClient = new HttpClient();

{
    var tasks = inputList.Select(WaitAndPrintAsync).ToList();

    await Task.WhenAll(tasks);

    var outputList = tasks.ConvertAll(task => task.Result);

    Console.WriteLine("Order of output using Select Task (in order)");

    foreach (var output in outputList)
        Console.WriteLine(output);

    //OUTPUT:
    //Order of output using Select Task (in order)
    //1
    //2
    //3
    //4
    //5
}

Console.WriteLine();

{
    List<int> outputList = [];

    await Parallel.ForEachAsync(inputList, async (input, _) =>
    {
        var output = await WaitAndPrintAsync(input);
        outputList.Add(output);
    });

    Console.WriteLine("Order of output using Parallel.ForEachAsync (out of order)");

    foreach (var output in outputList)
        Console.WriteLine(output);

    //OUTPUT:
    //Order of output using Parallel.ForEachAsync (out of order)
    //5
    //1
    //4
    //2
    //3
}

async Task<int> WaitAndPrintAsync(int input)
{
    //Simulate a real asynchronous operation.
    await httpClient.GetAsync("https://google.com/");

    return input;
}