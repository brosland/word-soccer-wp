using System;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WordSoccer
{
	public sealed partial class SettingsPage : Page
	{
		private readonly ApplicationSettings settings;

		public SettingsPage()
		{
			InitializeComponent();

			DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

			settings = new ApplicationSettings(ApplicationData.Current.LocalSettings);

			PlayerNameTextBox.Text = (String) settings.Load(
				ApplicationSettings.PLAYER_NAME_KEY, ApplicationSettings.DEFAULT_PLAYER_NAME);

			AIPlayerNameTextBox.Text = (String) settings.Load(
				ApplicationSettings.AIPLAYER_NAME_KEY, ApplicationSettings.DEFAULT_AIPLAYER_NAME);

			PlayerNameTextBox.Text = (String) settings.Load(
				ApplicationSettings.PLAYER_NAME_KEY, ApplicationSettings.DEFAULT_PLAYER_NAME);

			int levelIndex = Int32.Parse((String) settings.Load(
				ApplicationSettings.AIPLAYER_LEVEL_KEY, ApplicationSettings.DEFAULT_AIPLAYER_LEVEL));

			AIPlayerLevelComboBox.SelectedIndex = levelIndex;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void OnLostFocusPlayerNameTextBox(object sender, RoutedEventArgs e)
		{
			settings.Save(ApplicationSettings.PLAYER_NAME_KEY, PlayerNameTextBox.Text);
		}

		private void OnLostFocusAIPlayerNameTextBox(object sender, RoutedEventArgs e)
		{
			settings.Save(ApplicationSettings.AIPLAYER_NAME_KEY, AIPlayerNameTextBox.Text);
		}

		private void OnLostFocusAIPlayerLevelComboBox(object sender, RoutedEventArgs e)
		{
			settings.Save(ApplicationSettings.AIPLAYER_LEVEL_KEY, AIPlayerLevelComboBox.SelectedIndex.ToString());
		}
	}
}