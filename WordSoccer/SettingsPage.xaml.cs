using System;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WordSoccer
{
	public sealed partial class SettingsPage : Page
	{
		private readonly Settings settings;

		public SettingsPage()
		{
			InitializeComponent();

			settings = new Settings(ApplicationData.Current.LocalSettings);
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
			StatusBar.GetForCurrentView().HideAsync();
			HardwareButtons.BackPressed += OnClickBackButton;

			PlayerNameTextBox.Text = (String) settings.Load(
				Settings.PLAYER_NAME_KEY, Settings.DEFAULT_PLAYER_NAME);

			AIPlayerNameTextBox.Text = (String) settings.Load(
				Settings.AIPLAYER_NAME_KEY, Settings.DEFAULT_AIPLAYER_NAME);

			PlayerNameTextBox.Text = (String) settings.Load(
				Settings.PLAYER_NAME_KEY, Settings.DEFAULT_PLAYER_NAME);

			int levelIndex = Int32.Parse((String) settings.Load(
				Settings.AIPLAYER_LEVEL_KEY, Settings.DEFAULT_AIPLAYER_LEVEL));

			AIPlayerLevelComboBox.SelectedIndex = levelIndex;
		}

		private void OnClickBackButton(object sender, BackPressedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				e.Handled = true;
				Frame.GoBack();
			}
		}

		private void OnLostFocusPlayerNameTextBox(object sender, RoutedEventArgs e)
		{
			settings.Save(Settings.PLAYER_NAME_KEY, PlayerNameTextBox.Text);
		}

		private void OnLostFocusAIPlayerNameTextBox(object sender, RoutedEventArgs e)
		{
			settings.Save(Settings.AIPLAYER_NAME_KEY, AIPlayerNameTextBox.Text);
		}

		private void OnLostFocusAIPlayerLevelComboBox(object sender, RoutedEventArgs e)
		{
			settings.Save(Settings.AIPLAYER_LEVEL_KEY, AIPlayerLevelComboBox.SelectedIndex.ToString());
		}
	}
}