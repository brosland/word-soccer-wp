using System;
using System.Collections.Generic;

namespace WordSoccer.Game
{
	public class LetterGenerator
	{
		private readonly Dictionary<Char, Double> letterFrequency;
		private readonly Random generator;

		public LetterGenerator(Dictionary<Char, Double> letterFrequency)
		{
			this.letterFrequency = letterFrequency;
			this.generator = new Random();
		}

		public char NextLetter()
		{
			double x = generator.NextDouble();
			double minBoundary = 0f, maxBoundary;

			foreach (KeyValuePair<char, double> letter in letterFrequency)
			{
				maxBoundary = minBoundary + letter.Value;

				if (minBoundary <= x && x < maxBoundary)
				{
					return letter.Key;
				}

				minBoundary = maxBoundary;
			}

			throw new ArgumentOutOfRangeException();
		}
	}
}