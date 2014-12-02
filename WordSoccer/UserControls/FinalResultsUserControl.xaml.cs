using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordSoccer.Game;

namespace WordSoccer.UserControls
{
	public sealed partial class FinalResultsUserControl : UserControl
	{
		public FinalResultsUserControl(IGame game)
		{
			InitializeComponent();

			// player A - total letters
			playerATotalLettersTextBlock.Text = game.GetPlayerA().GetTotalPoints().ToString();

			// player A - longest valid word
			playerAScoreTextBlock.Text = game.GetPlayerA().GetScore().ToString();

			// player B - total letters
			playerBTotalLettersTextBlock.Text = game.GetPlayerB().GetTotalPoints().ToString();

			// player B - longest valid word
			playerBScoreTextBlock.Text = game.GetPlayerB().GetScore().ToString();

			// total letters
			if (game.GetPlayerA().GetTotalPoints() > game.GetPlayerB().GetTotalPoints())
			{
				playerATotalLettersBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerATotalLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
			else if (game.GetPlayerA().GetTotalPoints() < game.GetPlayerB().GetTotalPoints())
			{
				playerBTotalLettersBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBTotalLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}

			// score
			if (game.GetPlayerA().GetScore() > game.GetPlayerB().GetScore())
			{
				playerAScoreBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerAScoreTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
			else
			{
				playerBScoreBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBScoreTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
		}
	}
}