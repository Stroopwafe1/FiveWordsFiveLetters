namespace FiveWordsFiveLettersLib
{
    public class Solution : IEquatable<Solution>
    {
        public List<FiveLetterWord> Words { get; set; }
        public int Hash { get; set; }

        public Solution(List<FiveLetterWord> words)
        {
            Words = words;
            Hash = words.Aggregate(0, (acc, item) => acc + item.Word.GetHashCode());
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Solution) return false;
            return Equals((Solution)obj);
        }

        public bool Equals(Solution? other)
        {
            if (other is null) return false;
            return this.Hash == other.Hash;
        }
    }
}