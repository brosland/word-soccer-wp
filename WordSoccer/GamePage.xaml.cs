using System;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WordSoccer.Game;
using WordSoccer.Game.Dictionaries;
using WordSoccer.Game.Games;
using WordSoccer.Game.Players;
using WordSoccer.UserControls;

namespace WordSoccer
{
	public sealed partial class GamePage : Page, IGameListener
	{
		private IGame game;
		private DispatcherTimer timer;
		private UserControl currentUserControl;

		public GamePage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
			StatusBar.GetForCurrentView().HideAsync();

			HardwareButtons.BackPressed += OnClickBackButton;

			CreateGame();
		}

		private void OnClickBackButton(object sender, BackPressedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				e.Handled = true;
				Frame.GoBack();
			}
		}

		private void CreateGame()
		{
			UpdateStatusBar("Preparing game...", true);

			Settings settings = new Settings(ApplicationData.Current.LocalSettings);

			String playerAName = (String) settings.Load(
				Settings.PLAYER_NAME_KEY, Settings.DEFAULT_PLAYER_NAME);
			String playerBName = (String) settings.Load(
				Settings.AIPLAYER_NAME_KEY, Settings.DEFAULT_AIPLAYER_NAME);
			AIPlayer.Level level = (AIPlayer.Level) Int32.Parse((String) settings.Load(
				Settings.AIPLAYER_LEVEL_KEY, Settings.DEFAULT_AIPLAYER_LEVEL));

			ISinglePlayerDictionary dictionary = new HttpDictionary("en");

			Player playerA = new Player(playerAName);
			AIPlayer playerB = new AIPlayer(playerBName, level);

			game = new SinglePlayerGame(dictionary, playerA, playerB);
			game.AddGameListener(this);
			game.Init();
		}

		public void OnInit(IGame game)
		{
			playerANameTextBlock.Text = game.GetPlayerA().GetName();
			playerBNameTextBlock.Text = game.GetPlayerB().GetName();

			headerGrid.Visibility = Visibility.Visible;

			game.StartNewGame();
		}

		public void OnStartGame(IGame game)
		{
			UpdateStatusBar("Preparing new round...", true);

			game.StartNewRound();
		}

		public void OnStartRound(IGame game)
		{
			UpdateStatusBar("", false);

			if (game.GetCurrentRoundNumber() > BaseGame.ROUNDS)
			{
				roundTextBlock.Text = String.Format("+{0}.", game.GetCurrentRoundNumber() - BaseGame.ROUNDS);
			}
			else
			{
				roundTextBlock.Text = String.Format("{0}.", game.GetCurrentRoundNumber());
			}

			RoundUserControl roundUserControl = new RoundUserControl(game);

			ReplaceCurrentContent(roundUserControl);

			int remainingTime = BaseGame.ROUND_DURATION / 1000; // in seconds

			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Tick += delegate(object state, object e)
			{
				remainingTime--;

				String styleName = remainingTime < 10 ? "CriticalTimeStyle" : "TimeStyle";

				timeTextBlock.Text = String.Format("{0}:{1:D2}", remainingTime / 60, remainingTime % 60);
				timeTextBlock.Style = (Style) Application.Current.Resources[styleName];

				if (remainingTime <= 0)
				{
					timer.Stop();

					roundUserControl.SetLettersBarVisibility(false);

					game.FinishRound();
				}
			};
			timer.Start();
		}

		public void OnFinishRound(IGame game)
		{
			UpdateStatusBar("Loading opponent words...", true);
		}

		public void OnOpponentWordsLoaded(IGame game)
		{
			((RoundUserControl) currentUserControl).ShowOpponentWordList(game.GetPlayerB());

			UpdateStatusBar("Evaluating round...", true);

			game.EvaluateRound();
		}

		public void OnEvaluateRound(IGame game)
		{
			UpdateStatusBar("", true);

			RoutedEventHandler continueToRoundResultsDelagate = null;
			continueToRoundResultsDelagate = (sender, routedEvent) =>
			{
				continueButton.Click -= continueToRoundResultsDelagate;

				ReplaceCurrentContent(new RoundResultsUserControl(game));

				RoutedEventHandler continueToUpdateScoreDelagate = null;
				continueToUpdateScoreDelagate = (sender2, routedEvent2) =>
				{
					continueButton.Click -= continueToUpdateScoreDelagate;

					game.UpdateScore();
				};

				continueButton.Click += continueToUpdateScoreDelagate;
			};

			continueButton.Click += continueToRoundResultsDelagate;
			continueButton.Visibility = Visibility.Visible;
		}

		public void OnUpdateScore(IGame game)
		{
			// player A
			playerAScoreTextBlock.Text = game.GetPlayerA().GetScore().ToString();
			playerAYellowCardBorder.Visibility = game.GetPlayerA().GetNumberOfCards(Card.CardType.YELLOW) > 0
				? Visibility.Visible : Visibility.Collapsed;
			playerARedCardBorder.Visibility = game.GetPlayerA().GetNumberOfCards(Card.CardType.RED) > 0
				? Visibility.Visible : Visibility.Collapsed;
			playerARedCardTextBlock.Text = game.GetPlayerA().GetNumberOfCards(Card.CardType.RED) > 1
				? game.GetPlayerA().GetNumberOfCards(Card.CardType.RED).ToString() : "";

			// player B
			playerBScoreTextBlock.Text = game.GetPlayerB().GetScore().ToString();
			playerBYellowCardBorder.Visibility = game.GetPlayerB().GetNumberOfCards(Card.CardType.YELLOW) > 0
				? Visibility.Visible : Visibility.Collapsed;
			playerBRedCardBorder.Visibility = game.GetPlayerB().GetNumberOfCards(Card.CardType.RED) > 0
				? Visibility.Visible : Visibility.Collapsed;
			playerBRedCardTextBlock.Text = game.GetPlayerB().GetNumberOfCards(Card.CardType.RED) > 1
				? game.GetPlayerB().GetNumberOfCards(Card.CardType.RED).ToString() : "";

			if (game.HasNextRound())
			{
				UpdateStatusBar("Preparing new round...", true);

				game.StartNewRound();
			}
			else
			{
				game.FinishGame();
			}
		}

		public void OnFinishGame(IGame game)
		{
			UpdateStatusBar("", false);
			continueButton.Visibility = Visibility.Collapsed;

			ReplaceCurrentContent(new FinalResultsUserControl(game));
		}

		private void ReplaceCurrentContent(UserControl userControl)
		{
			if (currentUserControl != null)
			{
				contentGrid.Children.Remove(currentUserControl);
			}

			currentUserControl = userControl;

			contentGrid.Children.Add(currentUserControl);
		}

		private void UpdateStatusBar(String message, bool visible)
		{
			statusMessage.Text = message;
			footerBorder.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}