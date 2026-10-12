
string? filePath = args.ElementAtOrDefault(0);
if (filePath == null)
{
    filePath = "new_beta.txt";
    if (!File.Exists(filePath))
    {
        throw new ArgumentException($"Incorrect usage, please provide a txt file in the CLI. \ndotnet ./FiveWordsFiveLetters/bin/Debug/net10.0/FiveWordsFiveLetters.dll <filePathHere>");
    }
    Console.WriteLine("No filepath specified, using default");
}
try
{
    string[] lines = await File.ReadAllLinesAsync(filePath);
	var solver = FiveWordsFiveLettersLib.Solver.Create(lines);

	solver.Solve();
    foreach (var solution in solver.Solutions)
    {
        Console.WriteLine($"Found solution {string.Join(',', solution.Words)}");
    }
}
catch
{
}
