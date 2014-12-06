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

			IPlayer playerA = game.GetPlayerA();
			IPlayer playerB = game.GetPlayerB();

			// player A - total letters
			playerATotalLettersTextBlock.Text = playerA.GetPoints().ToString();

			// player A - longest valid word
			playerALongestValidWordTextBlock.Text = playerA.GetCurrentLongestWord().ToString();

			// player A - usage of letters
			int playerAUsageOfLetters = (int) (100.0 * playerA.GetNumberOfUsedLetters()
				/ (BaseGame.LETTERS - playerA.GetNumberOfCards(Card.RED)));

			playerAUsageOfLettersTextBlock.Text = String.Format("{0} %", playerAUsageOfLetters);

			// player B - total letters
			playerBTotalLettersTextBlock.Text = playerB.GetPoints().ToString();

			// player B - longest valid word
			playerBLongestValidWordTextBlock.Text = playerB.GetCurrentLongestWord().ToString();

			// player B - usage of letters
			int playerBUsageOfLetters = (int) (100.0 * playerB.GetNumberOfUsedLetters()
				/ (BaseGame.LETTERS - playerB.GetNumberOfCards(Card.RED)));

			playerBUsageOfLettersTextBlock.Text = String.Format("{0} %", playerBUsageOfLetters);

			// total letters
			if (playerA.GetPoints() > playerB.GetPoints())
			{
				playerATotalLettersBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerATotalLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
			else if (playerA.GetPoints() < playerB.GetPoints())
			{
				playerBTotalLettersBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBTotalLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}

			// longest valid word
			if (playerA.GetCurrentLongestWord() > playerB.GetCurrentLongestWord())
			{
				playerALongestValidWordBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerALongestValidWordTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
				playerBYellowCardBorder.Visibility = Visibility.Visible;
			}
			else if (playerA.GetCurrentLongestWord() < playerB.GetCurrentLongestWord())
			{
				playerBLongestValidWordBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBLongestValidWordTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
				playerAYellowCardBorder.Visibility = Visibility.Visible;
			}

			// usage of letters
			if (playerAUsageOfLetters > playerBUsageOfLetters)
			{
				playerAUsageOfLettersBorder.Style = (Style) Application.Current.Resources["PlayerAHighlightedValueCellStyle"];
				playerAUsageOfLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}
			else if (playerAUsageOfLetters < playerBUsageOfLetters)
			{
				playerBUsageOfLettersBorder.Style = (Style) Application.Current.Resources["PlayerBHighlightedValueCellStyle"];
				playerBUsageOfLettersTextBlock.Style = (Style) Application.Current.Resources["HighlightedValueCellTextStyle"];
			}

			if (playerA.HasUsedAllLetters())
			{
				playerBRedCardBorder.Visibility = Visibility.Visible;
			}

			if (playerB.HasUsedAllLetters())
			{
				playerARedCardBorder.Visibility = Visibility.Visible;
			}
		}
	}
}