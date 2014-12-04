using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using WordSoccer.Game;

namespace WordSoccer.Converters
{
	public class WordListTextStyleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			Word.WordState state = (Word.WordState) value;

			switch (state)
			{
				case Word.WordState.VALID:
					return Application.Current.Resources["WordListBaseTextStyle"];

				case Word.WordState.INVALID:
					return Application.Current.Resources["WordListInvalidWordTextStyle"];

				case Word.WordState.REMOVED:
					return Application.Current.Resources["WordListRemovedWordTextStyle"];

				default:
					return Application.Current.Resources["WordListPendingWordTextStyle"];
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}