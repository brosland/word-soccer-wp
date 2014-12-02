using System;
using Windows.UI.Xaml;
using WordSoccer.Game;

namespace WordSoccer.Controls
{
	public class InputLetterButton : LetterButton
	{
		public bool IsEnable 
		{
			set
			{
				base.IsEnabled = value;

				UpdateStyle();
			}

			get { return base.IsEnabled; }
		}

		protected override void UpdateStyle()
		{
			if (!HasLetter())
			{
				base.UpdateStyle();
				return;
			}

			String styleName;

			if (letter.IsDisabled())
			{
				styleName = "DisabledLetterButtonWithRedCardStyle";
			}
			else
			{
				if (letter.IsUsed())
				{
					styleName = letter.GetCardType() == Card.CardType.YELLOW
						? "DisabledLetterButtonWithYellowCardStyle" : "DisabledLetterButtonStyle";
				}
				else
				{
					styleName = letter.GetCardType() == Card.CardType.YELLOW
						? "LetterButtonWithYellowCardStyle" : "LetterButtonStyle";
				}
			}

			Style = (Style) Application.Current.Resources[styleName];
		}
	}
}