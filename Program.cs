﻿using System.Collections.Concurrent;

int[] inputList = [1, 2, 3, 2, 1];

{
    var tasks = inputList.Select(WaitAndPrintAsync).ToList();

    await Task.WhenAll(tasks);

    var outputList = tasks.ConvertAll(task => task.Result);

    Console.WriteLine("Order of execution using Select Task (in order)");

    foreach (var output in outputList)
        Console.WriteLine(output);

    //OUTPUT:
    //Order of execution using Select Task
    //1
    //2
    //3
    //2
    //1
}

Console.WriteLine();

{
    ConcurrentBag<int> outputList = [];

    await Parallel.ForEachAsync(inputList, async (input, _) =>
    {
        var output = await WaitAndPrintAsync(input);
        outputList.Add(output);
    });

    Console.WriteLine("Order of execution using Parallel.ForEachAsync (out of order)");

    foreach (var output in outputList)
        Console.WriteLine(output);

    //OUTPUT:
    //Order of execution using Parallel.ForEachAsync
    //3
    //2
    //1
    //2
    //1
}

static async Task<int> WaitAndPrintAsync(int input)
{
    await Task.Delay(input * 100);
    return input;
}