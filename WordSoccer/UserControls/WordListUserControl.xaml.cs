using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using WordSoccer.Game;

namespace WordSoccer.UserControls
{
	public sealed partial class WordListUserControl : UserControl, IPlayerListener
	{
		public ObservableCollection<WordListItem> items { get; set; }
		private IPlayer player;

		public WordListUserControl()
		{
			InitializeComponent();

			items = new ObservableCollection<WordListItem>();
		}

		public void SetPlayer(IPlayer player)
		{
			this.player = player;
			player.SetListener(this);
		}

		public void OnWordListChange()
		{
			items.Clear();

			List<Word> words = player.GetWords();

			for (int i = 0; i < words.Count; i++)
			{
				items.Add(new WordListItem(words[i], i + 1));
			}
		}

		public class WordListItem
		{
			private readonly Word word;
			private int index;

			public WordListItem(Word word, int index)
			{
				this.word = word;
				this.index = index;
			}

			public Word Word
			{
				get { return word; }
			}

			public int Index
			{
				get { return index; }
			}
		}
	}
}