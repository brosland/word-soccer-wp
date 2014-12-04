using System;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordSoccer.Controls;
using WordSoccer.Game;
using WordSoccer.Game.Games;

namespace WordSoccer.UserControls
{
	public sealed partial class RoundUserControl : UserControl
	{
		private readonly InputLetterButton[] inputLetterButtons;
		private readonly LetterButton[] selectedLetterButtons;
		private readonly IGame game;

		public RoundUserControl(IGame game)
		{
			this.game = game;

			InitializeComponent();

			// input letter buttons
			inputLetterButtons = new InputLetterButton[BaseGame.LETTERS];

			Letter[] letters = game.GetPlayerA().GetLetters();

			for (int i = 0; i < inputLetterButtons.Length; i++)
			{
				InputLetterButton button = inputLetterButtons[i] = new InputLetterButton();
				button.SetLetter(letters[i]);
				button.Click += OnClickInputLetterButton;

				Grid.SetColumn(button, i);
				inputLettersGrid.Children.Add(button);
			}

			// selected letter buttons
			selectedLetterButtons = new LetterButton[BaseGame.LETTERS];

			for (int i = 0; i < selectedLetterButtons.Length; i++)
			{
				LetterButton button = selectedLetterButtons[i] = new LetterButton();
				button.Click += OnClickSelectedLetterButton;

				Grid.SetColumn(button, i);
				selecteLettersGrid.Children.Add(button);
			}

			// word lists
			wordListUserControl.SetPlayer(game.GetPlayerA());
			opponentWordListUserControl.SetPlayer(game.GetPlayerB());

			// submit button
			UpdateSubmitButton();
		}

		public void SetLettersBarVisibility(bool visible)
		{
			lettersGrid.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
		}

		public void ShowOpponentWordList(IPlayer opponent)
		{
			opponentWordListUserControl.Visibility = Visibility.Visible;
		}

		private void OnClickInputLetterButton(object sender, RoutedEventArgs routedEventArgs)
		{
			InputLetterButton button = (InputLetterButton) sender;
			button.IsEnabled = false;

			foreach (LetterButton selectedLetterButton in selectedLetterButtons)
			{
				if (!selectedLetterButton.HasLetter())
				{
					selectedLetterButton.SetLetter(button.GetLetter());
					break;
				}
			}

			UpdateSubmitButton();
		}

		private void OnClickSelectedLetterButton(object button, RoutedEventArgs routedEventArgs)
		{
			DeselectLetter((LetterButton) button);

			UpdateSubmitButton();
		}

		private void OnClickSubmitButton(object sender, RoutedEventArgs e)
		{
			Word word = new Word(GetSelectedWord());

			game.GetPlayerA().AddWord(word);

			foreach (LetterButton button in selectedLetterButtons)
			{
				if (button.HasLetter())
				{
					DeselectLetter(button);
				}
			}

			UpdateSubmitButton();
		}

		private String GetSelectedWord()
		{
			StringBuilder word = new StringBuilder();

			foreach (LetterButton letterButton in selectedLetterButtons)
			{
				word.Append(letterButton.HasLetter() ? letterButton.GetLetter().GetSign() : ' ');
			}

			return word.ToString().Trim();
		}

		private void DeselectLetter(LetterButton button)
		{
			Letter letter = button.RemoveLetter();
			inputLetterButtons[letter.GetNumber()].IsEnabled = true;
		}

		private void UpdateSubmitButton()
		{
			String currentWord = GetSelectedWord();

			if (currentWord.Length == 0|| currentWord.Contains(" "))
			{
				submitButton.IsEnabled = false;
				return;
			}

			foreach (Word word in game.GetPlayerA().GetWordList())
			{
				if (word.word.Equals(currentWord))
				{
					submitButton.IsEnabled = false;
					return;
				}
			}

			submitButton.IsEnabled = true;
		}
	}
}