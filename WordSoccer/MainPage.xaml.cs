using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WordSoccer
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
			StatusBar.GetForCurrentView().HideAsync();
		}

		private void OnClickPlayGameButton(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(GamePage));
		}

		private void OnClickSettingsButton(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(SettingsPage));
		}

		private void OnClickExitButton(object sender, RoutedEventArgs e)
		{
			Application.Current.Exit();
		}
	}
}