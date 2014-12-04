using System;
using System.Threading.Tasks;

namespace WordSoccer.Game
{
	public interface IDictionary
	{
		String GetLangCode();

		Task<bool> IsWordValid(String word);
	}
}