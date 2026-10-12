namespace FiveWordsFiveLettersLib
{
    public class Solver
    {
		private List<FiveLetterWord> _words;
		public HashSet<Solution> Solutions { get; set; }
		

        private Solver(List<FiveLetterWord> words)
        {
			this._words = words;
			Solutions = new HashSet<Solution>();
        }

        public static Solver Create(string[] words)
        {
            List<FiveLetterWord> result = new List<FiveLetterWord>();
            foreach (string word in words)
            {
                if (word.Length != 5) continue;
				FiveLetterWord parsed = new FiveLetterWord(word);
				result.Add(parsed);
            }
			return new Solver(result);
        }

		public void Solve() {
			List<FiveLetterWord> words = new List<FiveLetterWord>();
			
			// Backtracking algorithm seems decent for this task. I also used one for my Sudoku solver

			foreach (var word in _words) {
				words.Add(word);
				int hash = word.Hash;
				Solve(words, hash);
				words.Clear();
			}
		}

		public void Solve(List<FiveLetterWord> currentSet, int currentHash) {
			if (currentSet.Count == 5) {
				// Copy our set to a new solution that deduplicates the same words in different order
				Solutions.Add(new Solution(currentSet.ToList()));
				return;
			}

			List<FiveLetterWord> options = GetRemainingWords(currentHash);
			foreach (FiveLetterWord word in options) {
				currentSet.Add(word);
				int nextHash = currentHash | word.Hash;
				Solve(currentSet, nextHash);
				currentSet.RemoveAt(currentSet.Count - 1);
			}
		}

		public List<FiveLetterWord> GetEliminationsForWord(FiveLetterWord word) {
			return _words.Where(w => w == word).ToList();
		}

		public List<FiveLetterWord> GetEliminationsForWord(string wordString) {
			var word = new FiveLetterWord(wordString);
			return GetEliminationsForWord(word);
		}

		public List<FiveLetterWord> GetRemainingWords(int hash) {
			return _words.Where(w => (w.Hash & hash) == 0).ToList();
		}
    }
}
