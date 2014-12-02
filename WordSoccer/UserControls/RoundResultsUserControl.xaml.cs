using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordSoccer.Game;
using WordSoccer.Game.Games;

namespace WordSoccer.UserControls
{
	public sealed partial class RoundResultsUserControl : UserControl
	{
		public RoundResultsUserControl(IGame game)
		{
			InitializeComponent();

			// round number
			if (game.GetCurrentRoundNumber() > BaseGame.ROUNDS)
			{
				headerTextBlock.Text = String.Format("Round +{0}", game.GetCurrentRoundNumber() - BaseGame.ROUNDS);
			}
			else
			{
				headerTextBlock.Text = String.Format("Round {0} / {1}", game.GetCurrentRoundNumber(), BaseGame.ROUNDS);
			}

			// player A - total letters
			playerATotalLettersTextBlock.Text = game.GetPlayerA().GetPoints().ToString();

			// player A - longest valid word
			playerALongestValidWordTextBlock.Text = game.GetPlayerA().GetCurrentLongestWord().ToString();

			// player A - usage of letters
			int playerAUsageOfLetters = (int) (100.0 * game.GetPlayerA().GetNumberOfUsedLetters() / BaseGame.LETTERS);
			playerAUsageOfLettersTextBlock.Text = String.Format("{0} %", playerAUsageOfLetters);

			// player B - total letters
			playerBTotalLettersTextBlock.Text = game.GetPlayerB().GetPoints().ToString();

			// player B - longest valid word
			playerBLongestValidWordTextBlock.Text = game.GetPlayerB().GetCurrentLongestWord().ToString();

			// player B - usage of letters
			int playerBUsageOfLetters = (int) (100.0 * game.GetPlayerB().GetNumberOfUsedLetters() / BaseGame.LETTERS);
			playerBUsageOfLettersTextBlock.Text = String.Format("{0} %", playerBUsageOfLetters);

			// total letters
			if (game.GetPlayerA().GetPoints() > game.GetPlayerB().GetPoints())
			{
				playerATotalLettersBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerATotalLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
			else if (game.GetPlayerA().GetPoints() < game.GetPlayerB().GetPoints())
			{
				playerBTotalLettersBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBTotalLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}

			// longest valid word
			if (game.GetPlayerA().GetCurrentLongestWord() > game.GetPlayerB().GetCurrentLongestWord())
			{
				playerALongestValidWordBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerALongestValidWordTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
				playerBYellowCardBorder.Visibility = Visibility.Visible;
			}
			else if (game.GetPlayerA().GetCurrentLongestWord() < game.GetPlayerB().GetCurrentLongestWord())
			{
				playerBLongestValidWordBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBLongestValidWordTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
				playerAYellowCardBorder.Visibility = Visibility.Visible;
			}

			// usage of letters
			if (game.GetPlayerA().GetNumberOfUsedLetters() > game.GetPlayerB().GetNumberOfUsedLetters())
			{
				playerAUsageOfLettersBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerAUsageOfLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
			else if (game.GetPlayerA().GetNumberOfUsedLetters() < game.GetPlayerB().GetNumberOfUsedLetters())
			{
				playerBUsageOfLettersBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBUsageOfLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}

			if (game.GetPlayerA().GetNumberOfUsedLetters() == BaseGame.LETTERS)
			{
				playerBRedCardBorder.Visibility = Visibility.Visible;
			}

			if (game.GetPlayerB().GetNumberOfUsedLetters() == BaseGame.LETTERS)
			{
				playerARedCardBorder.Visibility = Visibility.Visible;
			}
		}
	}
}