// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WordSoccer.UserControls
{
	public sealed partial class RoundUserControl : UserControl
	{
		public RoundUserControl()
		{
			InitializeComponent();

			InitializeSelectedButtons();

			InitializeInputButtons();
		}

		private void InitializeInputButtons()
		{
			for (int i = 0; i < 11; i++)
			{
				Button button = new Button();
				button.Style = (Style) Application.Current.Resources["LetterButtonStyle"];
				button.Content = (char) ('A' + i);
				
				Grid.SetColumn(button, i);
				InputLettersGrid.Children.Add(button);
			}
		}

		private void InitializeSelectedButtons()
		{
			for (int i = 0; i < 11; i++)
			{
				Button button = new Button();
				button.Style = (Style) Application.Current.Resources["LetterButtonStyle"];
				button.Content = (char) ('A' + i);

				Grid.SetColumn(button, i);
				SelecteLettersGrid.Children.Add(button);
			}

			Button submitButton = new Button();
			submitButton.Style = (Style) Application.Current.Resources["SubmitButtonStyle"];
			submitButton.Content = "Submit";

			Grid.SetColumn(submitButton, 11);
			SelecteLettersGrid.Children.Add(submitButton);
		}
	}
}
