using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSoccer.Game.Dictionaries
{
	public class SQLiteDictionary : IDictionary
	{
		public string GetLangCode()
		{
			throw new NotImplementedException();
		}

		public Dictionary<Char, Double> GetLetterFrequency()
		{
			throw new NotImplementedException();
		}

		public bool IsWordValid(string word)
		{
			throw new NotImplementedException();
		}

		public List<String> GetValidWordsFromLetters(char[] letters)
		{
			throw new NotImplementedException();
		}
	}
}