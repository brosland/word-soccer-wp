using System;
using System.Collections.Generic;

namespace WordSoccer.Game.Dictionaries
{
	public abstract class SQLiteDictionary : IDictionary
	{
		public abstract string GetLangCode();

		public abstract bool IsWordValid(string word);

		public abstract Dictionary<Char, Double> GetLetterFrequency();

		public abstract List<String> GetValidWordsFromLetters(char[] letters);
	}
}