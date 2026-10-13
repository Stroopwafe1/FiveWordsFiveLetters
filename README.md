# Five words five letters

This project gives a solution to find all the possible combinations of 5 five-letter words.
The solution uses backtracking as the main solver.

## Algorithm

1. Map all words to their bitwise representation
2. For every word, find the remaining possible words
3. Add that word to the list
4. Recursively run step 2+3
5. If 5 words in the list -> Solution.
6. Backtrack for the remaining solutions

## Why backtracking

It's just an algorithm I'm most familiar with for recursive problem solving. I also used it for my Sudoku solver.

## Requirements

1. .NET Core 10 SDK

## Steps to run (Preview file)

```sh
$ dotnet build FiveWordsFiveLetters.slnx 
$ dotnet ./FiveWordsFiveLetters/bin/Debug/net10.0/FiveWordsFiveLetters.dll ./FiveWordsFiveLetters/new_beta.txt
```

If you're using NixOS or the Nix package manager, enter the shell before running those commands.

```sh
$ nix-shell
```

## Steps to run (All data)

```sh
$ dotnet build -c Release FiveWordsFiveLetters.slnx
$ dotnet ./FiveWordsFiveLetters/bin/Release/net10.0/FiveWordsFiveLetters.dll ./FiveWordsFiveLetters/words_alpha.txt
```

## Results

- Single Threaded: Running the solver on the complete data set takes around 4 minutes (250 seconds) on my machine.
- Multi Threaded: Running the solver on the complete data set takes around 40 seconds on my machine.
