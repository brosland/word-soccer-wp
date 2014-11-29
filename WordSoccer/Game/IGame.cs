using System;

namespace WordSoccer.Game
{
	public interface IGame
	{
		void Init();

		void StartNewGame();

		void StartNewRound();

		void FinishRound();

		void EvaluateRound();

		void UpdateScore();

		void FinishGame();

		bool HasNextRound();

		IDictionary GetDictionary();

		IPlayer GetPlayerA();

		IPlayer GetPlayerB();

		int GetCurrentRoundNumber();

		String GetCurrentRoundLetters();

		void AddGameListener(IGameListener listener);

		void RemoveGameListener(IGameListener listener);
	}

	public interface IGameListener
	{
		void OnInit(IGame game);

		void OnStartGame(IGame game);

		void OnStartRound(IGame game);

		void OnFinishRound(IGame game);

		void OnOpponentWordsLoaded(IGame game);

		void OnEvaluateRound(IGame game);

		void OnUpdateScore(IGame game);

		void OnFinishGame(IGame game);
	}
}