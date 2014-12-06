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
			generator = new Random();
		}

		public char NextLetter()
		{
			double x = generator.NextDouble();
			double boundary = 0f;

			foreach (KeyValuePair<char, double> letter in letterFrequency)
			{
				boundary += letter.Value;

				if (x <= boundary)
				{
					return letter.Key;
				}
			}

			throw new ArgumentOutOfRangeException();
		}
	}
}