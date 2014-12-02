using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordSoccer.Game;

namespace WordSoccer.Controls
{
	public class LetterButton : Button
	{
		protected Letter letter;

		public LetterButton()
		{
			IsEnabled = false;
			Content = "_";

			UpdateStyle();
		}

		public bool HasLetter()
		{
			return letter != null;
		}

		public Letter GetLetter()
		{
			return letter;
		}

		public void SetLetter(Letter letter)
		{
			this.letter = letter;

			Content = letter.GetSign().ToString().ToUpper();
			IsEnabled = letter.GetCardType() != Card.CardType.RED;

			UpdateStyle();
		}

		public Letter RemoveLetter()
		{
			Letter letter = this.letter;
			this.letter = null;

			Content = "_";
			IsEnabled = false;

			UpdateStyle();

			return letter;
		}

		protected virtual void UpdateStyle()
		{
			String styleName = HasLetter() ? "LetterButtonStyle" : "DisabledLetterButtonWithoutTextStyle";
			Style = (Style) Application.Current.Resources[styleName];
		}
	}
}