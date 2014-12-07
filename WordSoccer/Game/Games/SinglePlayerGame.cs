using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordSoccer.Game.Games
{
	public class SinglePlayerGame : BaseGame
	{
		private readonly ISinglePlayerDictionary dictionary;
		private readonly IPlayer playerA, playerB;
		private readonly List<IGameListener> listeners;
		private LetterGenerator generator;
		private int currentRoundNumber;
		private String currentRoundLetters;

		public SinglePlayerGame(ISinglePlayerDictionary dictionary, IPlayer playerA, IPlayer playerB)
		{
			this.dictionary = dictionary;
			this.playerA = playerA;
			this.playerB = playerB;
			this.listeners = new List<IGameListener>();
		}

		public async override void Init()
		{
			Dictionary<Char, Double> letterFrequency = await Task.Run(() => dictionary.GetSignFrequency());
			generator = new LetterGenerator(letterFrequency);

			foreach (IGameListener listener in listeners)
			{
				listener.OnInit(this);
			}
		}

		public override void StartNewGame()
		{
			currentRoundNumber = 0;

			playerA.OnStartGame(this);
			playerB.OnStartGame(this);

			foreach (IGameListener listener in listeners)
			{
				listener.OnStartGame(this);
			}
		}

		public override void StartNewRound()
		{
			if (!HasNextRound())
			{
				FinishGame();
				return;
			}

			currentRoundNumber++;
			currentRoundLetters = "";

			for (int i = 0; i < LETTERS; i++)
			{
				currentRoundLetters += generator.NextLetter();
			}

			playerA.OnStartRound(this);
			playerB.OnStartRound(this);

			foreach (IGameListener listener in listeners)
			{
				listener.OnStartRound(this);
			}
		}

		public override void FinishRound()
		{
			foreach (IGameListener listener in listeners)
			{
				listener.OnFinishRound(this);
			}

			foreach (IGameListener listener in listeners)
			{
				listener.OnOpponentWordsLoaded(this);
			}
		}

		public override void EvaluateRound()
		{
			foreach (Word wordA in playerA.GetWords())
			{
				if (wordA.GetState() != Word.WordState.VALID)
				{
					continue;
				}

				foreach (Word wordB in playerB.GetWords())
				{
					if (wordB.GetState() == Word.WordState.VALID && wordA.Equals(wordB))
					{
						wordA.SetState(Word.WordState.REMOVED);
						wordB.SetState(Word.WordState.REMOVED);
					}
				}
			}

			foreach (IGameListener listener in listeners)
			{
				listener.OnEvaluateRound(this);
			}
		}

		public override void UpdateScore()
		{
			List<Card> playerACards = new List<Card>(2);
			List<Card> playerBCards = new List<Card>(2);
			
			// yellow card
			if (playerA.GetCurrentLongestWord() < playerB.GetCurrentLongestWord())
			{
				playerACards.Add(Card.YELLOW);
			}
			else if (playerA.GetCurrentLongestWord() > playerB.GetCurrentLongestWord())
			{
				playerBCards.Add(Card.YELLOW);
			}

			// red cards
			if (playerA.HasUsedAllLetters())
			{
				playerBCards.Add(Card.RED);
			}

			if (playerB.HasUsedAllLetters())
			{
				playerACards.Add(Card.RED);
			}

			// adding cards to players
			foreach (Card card in playerACards)
			{
				playerA.AddCard(card);
			}

			foreach (Card card in playerBCards)
			{
				playerB.AddCard(card);
			}

			// goal
			bool goal = false;

			if (playerA.GetPoints() >= MIN_GOAL_LETTERS && playerA.GetPoints() >= GetPlayerB().GetPoints())
			{
				playerA.SetScore(playerA.GetScore() + 1);
				goal = true;
			}

			if (playerB.GetPoints() >= MIN_GOAL_LETTERS && playerB.GetPoints() >= GetPlayerA().GetPoints())
			{
				playerB.SetScore(playerB.GetScore() + 1);
				goal = true;
			}

			if (goal)
			{
				playerA.ResetPoints();
				playerB.ResetPoints();
			}

			foreach (IGameListener listener in listeners)
			{
				listener.OnUpdateScore(this);
			}
		}

		public override void FinishGame()
		{
			foreach (IGameListener listener in listeners)
			{
				listener.OnFinishGame(this);
			}
		}

		public override bool HasNextRound()
		{
			return currentRoundNumber < ROUNDS || playerA.GetScore() == playerB.GetScore();
		}

		public override IDictionary GetDictionary()
		{
			return dictionary;
		}

		public override IPlayer GetPlayerA()
		{
			return playerA;
		}

		public override IPlayer GetPlayerB()
		{
			return playerB;
		}

		public override int GetCurrentRoundNumber()
		{
			return currentRoundNumber;
		}

		public override String GetCurrentRoundLetters()
		{
			return currentRoundLetters;
		}

		public override void AddGameListener(IGameListener listener)
		{
			listeners.Add(listener);
		}

		public override void RemoveGameListener(IGameListener listener)
		{
			listeners.Remove(listener);
		}
	}
}