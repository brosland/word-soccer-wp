using System;

namespace WordSoccer.Game
{
	public class Word : IComparable<Word>
	{
		public readonly String word;
		private WordState state = WordState.PENDING;
		private IWordListener listener;

		public Word(String word)
		{
			this.word = word;
		}

		public WordState GetState()
		{
			return state;
		}

		public Word SetState(WordState state)
		{
			this.state = state;

			if (listener != null)
			{
				listener.OnStateChanged(state);
			}

			return this;
		}

		public Word SetListener(IWordListener listener)
		{
			this.listener = listener;
			return this;
		}

		public bool Equals(Word wordB)
		{
			return this.word.Equals(wordB.word);
		}

		public override int GetHashCode()
		{
			return (word != null ? word.GetHashCode() : 0);
		}

		public int CompareTo(Word wordB)
		{
			if (this.state != wordB.GetState())
			{
				return this.state < wordB.GetState() ? -1 : 1;
			}
			else if (this.word.Length != wordB.word.Length)
			{
				return this.word.Length < wordB.word.Length ? 1 : -1;
			}

			return String.Compare(this.word, wordB.word, StringComparison.Ordinal);
		}

		public enum WordState
		{
			PENDING = 1, VALID = 2, REMOVED = 3, INVALID = 4
		}

		public interface IWordListener
		{
			void OnStateChanged(WordState state);
		}
	}
}
