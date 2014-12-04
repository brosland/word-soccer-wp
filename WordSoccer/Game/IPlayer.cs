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

		WordList GetWordList();

		int GetCurrentLongestWord();

		int GetPoints();

		void ResetPoints();

		int GetTotalPoints();

		Letter[] GetLetters();

		bool HasUsedAllLetters();

		int GetNumberOfUsedLetters();

		void AddCard(Card card);

		List<Card> GetCards();

		int GetNumberOfCards(Card.CardType cardType);

		void OnStartGame(IGame game);

		void OnStartRound(IGame game);
	}
}