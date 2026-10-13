
string? filePath = args.ElementAtOrDefault(0);
if (filePath == null)
{
    filePath = "words_alpha.txt";
    if (!File.Exists(filePath))
    {
        throw new ArgumentException($"Incorrect usage, please provide a txt file in the CLI. \ndotnet ./FiveWordsFiveLetters/bin/Debug/net10.0/FiveWordsFiveLetters.dll <filePathHere> [wordLength] [wordCount]");
    }
    Console.WriteLine("No filepath specified, using default");
}
try
{
	if (!int.TryParse(args.ElementAtOrDefault(1), out int wordLength)) {
		wordLength = 5;
	}
	if (!int.TryParse(args.ElementAtOrDefault(2), out int wordCount)) {
		wordCount = 5;
	}
	
	System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
	stopWatch.Start();
    string[] lines = await File.ReadAllLinesAsync(filePath);
	long measurement1 = stopWatch.ElapsedMilliseconds ;
	Console.WriteLine($"✅ Read all {lines.Length} lines in {measurement1}ms");
	stopWatch.Restart();
	
    var solver = FiveWordsFiveLettersLib.Solver.Create(lines, wordLength, wordCount);
	long measurement2 = stopWatch.ElapsedMilliseconds;
	Console.WriteLine($"✅ Created Solver in {measurement2}ms");
	stopWatch.Restart();

    solver.Solve(false);
	stopWatch.Stop();
	long measurement3 = stopWatch.ElapsedMilliseconds;

	Console.WriteLine($"\n✅ Solved! Found {solver.Solutions.Count} solutions in {measurement3}ms");
    foreach (var solution in solver.Solutions)
    {
        Console.WriteLine($"Found solution {string.Join(',', solution.Words)}");
    }
}
catch (Exception e)
{
	Console.WriteLine(e);
}
