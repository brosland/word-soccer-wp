using System.Collections.ObjectModel;
using Windows.Foundation.Diagnostics;
using Windows.UI.Xaml.Controls;
using WordSoccer.Game;

namespace WordSoccer.UserControls
{
	public sealed partial class WordListUserControl : UserControl, WordList.IWordListListener
	{
		public ObservableCollection<WordListItem> Words { get; set; }
		private IPlayer player;

		public WordListUserControl()
		{
			InitializeComponent();

			Words = new ObservableCollection<WordListItem>();
		}

		public void SetPlayer(IPlayer player)
		{
			this.player = player;
			player.GetWordList().SetListener(this);
		}

		public void OnWordListChanged()
		{
			Words.Clear();

			for (int i = 0; i < player.GetWordList().Count; i++)
			{
				Words.Add(new WordListItem(player.GetWordList()[i], i + 1));
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