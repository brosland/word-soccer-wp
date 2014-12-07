using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordSoccer.Game.Games;

namespace WordSoccer.Game.Players
{
	public class Player : IPlayer
	{
		private readonly String name;
		private readonly List<Word> words;
		private readonly List<Card> cards;
		private readonly Letter[] letters;
		private readonly Word.IWordListener wordListener;
		private int points, totalPoints, score, usedLetters, redCards, yellowCards;
		private IGame game;
		private IPlayerListener listener;

		public Player(String name)
		{
			this.name = name;
			words = new List<Word>();
			cards = new List<Card>();
			letters = new Letter[BaseGame.LETTERS];

			for (int i = 0; i < letters.Length; i++)
			{
				letters[i] = new Letter(i);
			}

			wordListener = new WordListener(this);
		}

		public String GetName()
		{
			return name;
		}

		public int GetScore()
		{
			return score;
		}

		public void SetScore(int score)
		{
			this.score = score;
		}

		public async void AddWord(Word word)
		{
			if (game == null)
			{
				throw new Exception("Game is not started properly.");
			}

			word.SetListener(wordListener);

			words.Add(word);
			WordListChanged();

			bool valid = await Task.Run(() => game.GetDictionary().IsWordValid(word.word));

			if (valid)
			{
				word.SetState(Word.WordState.VALID);

				AddUsedLetters(word);
				points += word.word.Length;
				totalPoints += word.word.Length;
			}
			else
			{
				word.SetState(Word.WordState.INVALID);
			}
		}

		public List<Word> GetWords()
		{
			return new List<Word>(words);
		}

		public int GetCurrentLongestWord()
		{
			int length = 0;

			foreach (Word word in words)
			{
				if (word.GetState() == Word.WordState.VALID && word.word.Length > length)
				{
					length = word.word.Length;
				}
			}

			return length;
		}

		public int GetPoints()
		{
			return points;
		}

		public void ResetPoints()
		{
			points = 0;
		}

		public int GetTotalPoints()
		{
			return totalPoints;
		}

		public Letter[] GetLetters()
		{
			return letters;
		}

		public bool HasUsedAllLetters()
		{
			return usedLetters == BaseGame.LETTERS - GetNumberOfCards(Card.RED);
		}

		public int GetNumberOfUsedLetters()
		{
			return usedLetters;
		}

		public void AddCard(Card card)
		{
			if (GetNumberOfCards(Card.RED) + 1 > BaseGame.MAX_RED_CARDS)
			{
				return;
			}

			int indexOfLastEnableLetter = letters.Length - 1 - GetNumberOfCards(Card.RED);
			Letter letter = letters[indexOfLastEnableLetter];

			if (card == Card.YELLOW)
			{
				letter.SetCard(letter.GetCard() == Card.YELLOW ? Card.RED : Card.YELLOW);
				yellowCards++;
			}
			else
			{
				if (letter.GetCard() == Card.YELLOW)
				{
					Letter prevLetter = letters[indexOfLastEnableLetter - 1];
					prevLetter.SetCard(Card.YELLOW);
				}

				letter.SetCard(Card.RED);
				redCards++;
			}

			cards.Add(card);
		}

		public List<Card> GetCards()
		{
			return new List<Card>(cards);
		}

		public int GetNumberOfCards(Card card)
		{
			return card == Card.YELLOW
				? yellowCards % 2
				: yellowCards / 2 + redCards;
		}

		public virtual void OnStartGame(IGame game)
		{
			this.game = game;
			score = points = totalPoints = usedLetters = redCards = yellowCards = 0;

			cards.Clear();
		}

		public virtual void OnStartRound(IGame game)
		{
			// reset used letters
			for (int i = 0; i < letters.Length; i++)
			{
				letters[i].SetSign(game.GetCurrentRoundLetters()[i])
					.SetUsed(false);
			}

			usedLetters = 0;

			// reset found words
			words.Clear();
			WordListChanged();
		}

		public void SetListener(IPlayerListener listener)
		{
			this.listener = listener;
		}

		private void WordListChanged()
		{
			words.Sort();

			if (listener != null)
			{
				listener.OnWordListChange();
			}
		}

		private void AddUsedLetters(Word word)
		{
			for (int i = 0; i < word.word.Length; i++)
			{
				foreach (Letter letter in letters)
				{
					if (!letter.IsUsed() && !letter.IsDisabled() && word.word[i] == letter.GetSign())
					{
						letter.SetUsed(true);
						usedLetters++;
						break;
					}
				}
			}
		}

		private class WordListener : Word.IWordListener
		{
			private readonly Player player;

			public WordListener(Player player)
			{
				this.player = player;
			}

			public void OnStateChanged(Word word)
			{
				player.WordListChanged();
			}
		}
	}
}