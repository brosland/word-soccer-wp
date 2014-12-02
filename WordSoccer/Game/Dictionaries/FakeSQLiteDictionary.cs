using System;
using System.Collections.Generic;

namespace WordSoccer.Game.Dictionaries
{
	public class FakeSQLiteDictionary : SQLiteDictionary
	{
		private readonly String langCode;
		private readonly Dictionary<Char, Double> letterFrequency;
		private readonly Random validationGenerator;

		public FakeSQLiteDictionary(String langCode)
		{
			this.langCode = langCode;
			validationGenerator = new Random();
			letterFrequency = new Dictionary<char, double>();

			for (int i = 'a'; i <= 'z'; i++)
			{
				letterFrequency.Add((char) i, 1.0 / ('z' - 'a' + 1));
			}
		}

		public override string GetLangCode()
		{
			return langCode;
		}

		public override Dictionary<Char, Double> GetLetterFrequency()
		{
			return letterFrequency;
		}

		public override bool IsWordValid(string word)
		{
			return true; //validationGenerator.Next() % 2 == 0;
		}

		public override List<String> GetValidWordsFromLetters(char[] letters)
		{
			return new List<String>();
		}
	}
}