
string? filePath = args.ElementAtOrDefault(0);
if (filePath == null)
{
    filePath = "new_beta.txt";
    if (!File.Exists(filePath))
    {
        throw new ArgumentException($"Incorrect usage, please provide a txt file in the CLI. \ndotnet run FiveWordsFiveLetters <filePathHere>");
    }
    Console.WriteLine("No filepath specified, using default");
}
try
{
    string[] lines = await File.ReadAllLinesAsync(filePath);
	var solver = FiveWordsFiveLettersLib.Solver.Create(lines);
	Console.WriteLine(lines.Length);

	var toTest = new FiveWordsFiveLettersLib.FiveLetterWord("bring");

	var elims = solver.GetEliminationsForWord(toTest);

    foreach (var elim in elims)
    {
        Console.WriteLine($"Eliminated {elim.Word} {elim.Hash:B}, AND = {(elim.Hash & toTest.Hash):B}");
    }

	solver.Solve();
}
catch
{
}
