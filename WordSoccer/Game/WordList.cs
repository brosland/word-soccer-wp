using System.Collections;
using System.Collections.Generic;

namespace WordSoccer.Game
{
	public class WordList : Word.IWordListener, IEnumerable<Word>
	{
		private readonly List<Word> words;
		private IWordListListener listener;
 
		public WordList()
		{
			words = new List<Word>();
		}

		public Word this[int i]
		{
			get
			{
				return words[i];
			}
		}

		public int Count
		{
			get { return words.Count; }
		}

		public void Add(Word word)
		{
			word.SetListener(this);

			words.Add(word);
			words.Sort();

			if (listener != null)
			{
				listener.OnWordListChanged();
			}
		}

		public List<Word> GetWords()
		{
			return words;
		}

		public int IndexOf(Word word)
		{
			return words.IndexOf(word);
		}

		public void Clear()
		{
			words.Clear();

			if (listener != null)
			{
				listener.OnWordListChanged();
			}
		}

		public void OnStateChanged(Word word)
		{
			words.Sort();

			if (listener != null)
			{
				listener.OnWordListChanged();
			}
		}

		public void SetListener(IWordListListener listener)
		{
			this.listener = listener;
		}

		public interface IWordListListener
		{
			void OnWordListChanged();
		}

		public IEnumerator<Word> GetEnumerator()
		{
			return words.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}