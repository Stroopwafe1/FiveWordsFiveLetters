namespace FiveWordsFiveLettersLib
{
	public class FiveLetterWord : IEquatable<FiveLetterWord> {
		public string Word { get; set; }
		public int Hash { get; set; }

		public FiveLetterWord(string word) {
			Word = word;
			Hash = 0;
			foreach (char c in word.ToLower())
			{
				Hash |= 1 << ((int)c - (int)'a'); // Convert c to a bit in a 32-bit int
			}
		}

		public override int GetHashCode() {
			return Hash;
		}

		public override bool Equals(object obj) {
			return obj is FiveLetterWord && Equals((FiveLetterWord) obj);
		}

		/**
		* Compares the hashes of this word and the provided word
		* 
		* Because the hash is a uint32 where every bit is a letter of the alphabet
		* the words contain the same letter if the bitwise AND between them != 0. 
		* The bitwise AND will actually show which letter is overlapping
		* 0000_00zy_xwvu_tsrq_ponm_lkji_hgfe_dcba
		* 
		* Example:
		* 0000_0000_0000_0010_0010_0001_0100_0010 (bring)
		* 0000_0010_0000_0100_0000_0000_0101_0001 (gazes)
		* 0000_0000_0000_0000_0000_0000_0100_0000 BIT AND (g)
		*/
		public bool Equals(FiveLetterWord other) {
			return (other.Hash & this.Hash) != 0;
		}

		public static bool operator !=(FiveLetterWord word1, FiveLetterWord word2) {
			if (word1 is null) {
				return word2 is not null;
			}

			return !word1.Equals(word2);
		}

		public static bool operator ==(FiveLetterWord word1, FiveLetterWord word2) {
			if (word1 is null) {
				return word2 is null;
			}

			return word1.Equals(word2);
		}

		public override string ToString() {
			return Word;
		}
	}
}