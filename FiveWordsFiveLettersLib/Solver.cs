using System.Collections.Concurrent;

namespace FiveWordsFiveLettersLib
{
    public class Solver
    {
        private List<FiveLetterWord> _words;
		private int _wordCount;
        public ConcurrentDictionary<Solution, byte> Solutions { get; set; }
		private ConcurrentDictionary<string, byte> _solved = new ConcurrentDictionary<string, byte>();

        private Solver(List<FiveLetterWord> words, int wordCount = 5)
        {
            this._words = words;
			this._wordCount = wordCount;
            Solutions = new ConcurrentDictionary<Solution, byte>();
        }

        public static Solver Create(string[] words, int wordLength = 5, int wordCount = 5)
        {
			if (wordLength * wordCount > 26) throw new ArgumentException("Word Length * Word Count needs to be <= 26 (The length of the alphabet)");

            List<FiveLetterWord> result = new List<FiveLetterWord>();
            foreach (string word in words)
            {
                if (word.Length != wordLength) continue;
				if (word.ToHashSet().Count != word.Length) continue;

                FiveLetterWord parsed = new FiveLetterWord(word);
                result.Add(parsed);
            }
            return new Solver(result, wordCount);
        }

        public void Solve(bool withFeedback = false)
        {
            // Backtracking algorithm seems decent for this task. I also used one for my Sudoku solver
			// When we get past half, we can be sure we already solved these
			Parallel.For(0, _words.Count / 2, i => {
				List<FiveLetterWord> words = new List<FiveLetterWord>();
				FiveLetterWord word = _words[i];
                words.Add(word);
                int hash = word.Hash;
				List<FiveLetterWord> options = GetRemainingWords(hash, _words);
                Solve(words, options, hash, withFeedback);
				_solved.TryAdd(word.Word, 1);
			});
        }

        public void Solve(List<FiveLetterWord> currentSet, List<FiveLetterWord> availableWords, int currentHash, bool withFeedback = false)
        {
            if (currentSet.Count == _wordCount)
            {
                // Copy our set to a new solution that deduplicates the same words in different order
                Solutions.TryAdd(new Solution(currentSet.ToList()), 1);
				if (withFeedback) {
					Console.WriteLine($"Found solution {string.Join(',', currentSet)}");
				}

                return;
            }

            List<FiveLetterWord> options = GetRemainingWords(currentHash, availableWords);
            foreach (FiveLetterWord word in options)
            {
				if (_solved.ContainsKey(word.Word)) continue;
                currentSet.Add(word);
                int nextHash = currentHash | word.Hash;
                Solve(currentSet, options, nextHash, withFeedback);
                currentSet.RemoveAt(currentSet.Count - 1);
            }
        }

        public List<FiveLetterWord> GetEliminationsForWord(FiveLetterWord word)
        {
            return _words.Where(w => w == word).ToList();
        }

        public List<FiveLetterWord> GetEliminationsForWord(string wordString)
        {
            var word = new FiveLetterWord(wordString);
            return GetEliminationsForWord(word);
        }

        public List<FiveLetterWord> GetRemainingWords(int hash, List<FiveLetterWord> availableWords)
        {
            return availableWords.Where(w => (w.Hash & hash) == 0).ToList();
        }
    }
}
