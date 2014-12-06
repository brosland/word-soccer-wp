using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordSoccer.Game.Games;

namespace WordSoccer.Game.Players
{
	class AIPlayer : Player
	{
		private static readonly int MAX_WORDS = 100;
		private static double[] EASY_LEVEL_PERCENTAGE = { 0.05, 0.1 };
		private static double[] MEDIUM_LEVEL_PERCENTAGE = { 0.1, 0.2 };
		private static double[] HARD_LEVEL_PERCENTAGE = { 0.15, 0.3 };
		private static double[] EXPERT_LEVEL_PERCENTAGE = { 0.3, 0.5 };
		private static double[] IMPOSSIBLE_LEVEL_PERCENTAGE = { 0.5, 0.8 };

		private readonly Random percentageGenerator, selectiveGenerator;
		private readonly double minPercentage, maxPercentage;

		public AIPlayer(String name, Level level)
			: base(name)
		{
			this.percentageGenerator = new Random();
			this.selectiveGenerator = new Random();

			double[] percentage = GetLevelPercentage(level);
			this.minPercentage = percentage[0];
			this.maxPercentage = percentage[1];
		}

		public override async void OnStartRound(IGame game)
		{
			base.OnStartRound(game);

			int letterCount = BaseGame.LETTERS - GetNumberOfCards(Card.RED);
			char[] letters = game.GetCurrentRoundLetters().Substring(0, letterCount).ToCharArray();

			ISinglePlayerDictionary dictionary = (ISinglePlayerDictionary) game.GetDictionary();
			List<String> strings = await Task.Run(() => dictionary.GetValidWordsFromLetters(letters));

			double percentage = minPercentage + (maxPercentage - minPercentage) * percentageGenerator.NextDouble();
			int count = (int) ((strings.Count > MAX_WORDS ? MAX_WORDS : strings.Count) * percentage);

			for (int i = 0; i < count; i++)
			{
				int index = selectiveGenerator.Next(strings.Count);

				AddWord(new Word(strings[index]));

				strings.RemoveAt(index);
			}
		}

		private double[] GetLevelPercentage(Level level)
		{
			switch (level)
			{
				case Level.EASY:
					return EASY_LEVEL_PERCENTAGE;

				case Level.MEDIUM:
					return MEDIUM_LEVEL_PERCENTAGE;

				case Level.HARD:
					return HARD_LEVEL_PERCENTAGE;

				case Level.EXPERT:
					return EXPERT_LEVEL_PERCENTAGE;

				case Level.IMPOSSIBLE:
					return IMPOSSIBLE_LEVEL_PERCENTAGE;

				default:
					return new double[] { 0, 100.0 };
			}
		}

		public enum Level
		{
			EASY, MEDIUM, HARD, EXPERT, IMPOSSIBLE
		}
	}
}