using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordSoccer.Game
{
	public interface ISinglePlayerDictionary : IDictionary
	{
		Task<Dictionary<Char, Double>> GetSignFrequency();

		Task<List<String>> GetValidWordsFromLetters(char[] letters);
	}
}
