using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using WordSoccer.Game;

namespace WordSoccer.UserControls
{
	public sealed partial class WordListUserControl : UserControl, IPlayerListener, Word.IWordListener
	{
		private IPlayer player;

		public WordListUserControl()
		{
			InitializeComponent();
		}

		public void SetPlayer(IPlayer player)
		{
			player.SetListener(this);
		}

		public void OnWordAdded(Word word)
		{
			word.SetListener(this);

			if (wordListView != null && wordListView.Items != null)
			{
				wordListView.Items.Add(word);
			}
		}

		public void OnStateChanged(Word.WordState state)
		{
			if (wordListView != null && wordListView.Items != null && player != null)
			{
				wordListView.Items.Clear();
			
				foreach (Word word in player.GetWords())
				{
					wordListView.Items.Add(word);
				}
			}
		}
	}
}