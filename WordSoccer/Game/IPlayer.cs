using System;
using System.Collections.Generic;

namespace WordSoccer.Game
{
	public interface IPlayer
	{
		String GetName();

		int GetScore();

		void SetScore(int score);

		void AddWord(Word word);

		List<Word> GetWords();

		int GetCurrentLongestWord();

		int GetPoints();

		void ResetPoints();

		int GetTotalPoints();

		Letter[] GetLetters();

		bool HasUsedAllLetters();

		int GetNumberOfUsedLetters();

		void AddCard(Card card);

		List<Card> GetCards();

		int GetNumberOfCards(Card card);

		void OnStartGame(IGame game);

		void OnStartRound(IGame game);

		void SetListener(IPlayerListener listener);
	}

	public interface IPlayerListener
	{
		void OnWordListChange();
	}
}