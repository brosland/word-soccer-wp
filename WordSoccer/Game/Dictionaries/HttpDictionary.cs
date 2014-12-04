using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace WordSoccer.Game.Dictionaries
{
	class HttpDictionary : ISinglePlayerDictionary
	{
		private const String SIGN_FREQUENCY_URI = "http://brosland.info/word-soccer/dictionary/sign-frequency?langCode={0}";
		private const String WORD_VALIDATION_URI = "http://brosland.info/word-soccer/dictionary/validate?langCode={0}&word={1}";
		private const String VALID_WORDS_URI = "http://brosland.info/word-soccer/dictionary/valid-words?langCode={0}&letters={1}";

		private readonly String langCode;
		private readonly HttpClient httpClient;

		public HttpDictionary(String langCode)
		{
			this.langCode = langCode;
			httpClient = new HttpClient();
		}

		public string GetLangCode()
		{
			return langCode;
		}

		public async Task<Dictionary<Char, Double>> GetSignFrequency()
		{
			try
			{
				Uri uri = new Uri(String.Format(SIGN_FREQUENCY_URI, langCode));
				String result = await httpClient.GetStringAsync(uri);

				JsonObject json = JsonObject.Parse(result);
				Dictionary<char, double> signFrequency = new Dictionary<char, double>();

				foreach (String key in json.Keys)
				{
					signFrequency.Add(key[0], json.GetNamedNumber(key));
				}

				return signFrequency;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);

				throw e;
			}
		}

		public async Task<bool> IsWordValid(String word)
		{
			try
			{
				Uri uri = new Uri(String.Format(WORD_VALIDATION_URI, langCode, word));
				String result = await httpClient.GetStringAsync(uri);

				return JsonObject.Parse(result).GetNamedBoolean("valid");
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);

				throw e;
			}
		}

		public async Task<List<String>> GetValidWordsFromLetters(char[] letters)
		{
			try
			{
				Uri uri = new Uri(String.Format(VALID_WORDS_URI, langCode, new String(letters)));
				String result = await httpClient.GetStringAsync(uri);

				JsonArray json = JsonArray.Parse(result);
				List<String> words = new List<String>(json.Count);

				for (uint i = 0; i < json.Count; i++)
				{
					words.Add(json.GetStringAt(i));
				}

				return words;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);

				throw e;
			}
		}
	}
}