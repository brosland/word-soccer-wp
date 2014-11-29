using System;

namespace WordSoccer.Game
{
	public interface IDictionary
	{
		String GetLangCode();

		bool IsWordValid(String word);
	}
}