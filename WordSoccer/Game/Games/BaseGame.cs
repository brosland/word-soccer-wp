namespace WordSoccer.Game.Games
{
	public abstract class BaseGame : IGame
	{
		public static readonly int LETTERS = 11;
		public static readonly int ROUNDS = 6;
		public static readonly int MAX_RED_CARDS = 6;
		public static readonly int ROUND_DURATION = 30000; // 90s = 1:30
		public static readonly int MIN_GOAL_LETTERS = 21;

		public abstract void Init();

		public abstract void StartNewGame();

		public abstract void StartNewRound();

		public abstract void FinishRound();

		public abstract void EvaluateRound();

		public abstract void UpdateScore();

		public abstract void FinishGame();

		public abstract bool HasNextRound();

		public abstract IDictionary GetDictionary();

		public abstract IPlayer GetPlayerA();

		public abstract IPlayer GetPlayerB();

		public abstract int GetCurrentRoundNumber();

		public abstract string GetCurrentRoundLetters();

		public abstract void AddGameListener(IGameListener listener);

		public abstract void RemoveGameListener(IGameListener listener);
	}
}