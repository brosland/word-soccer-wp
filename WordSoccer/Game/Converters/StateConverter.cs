using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WordSoccer.Game.Converters
{
	class StateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			Word.WordState state = (Word.WordState) value;

			Debug.WriteLine("aaaa: " + state.ToString());

			switch (state)
			{
				case Word.WordState.VALID:
					return Application.Current.Resources["WordListValidWordStyle"];

				case Word.WordState.INVALID:
					return Application.Current.Resources["WordListInvalidWordStyle"];

				case Word.WordState.REMOVED:
					return Application.Current.Resources["WordListRemovedWordStyle"];

				default:
					return Application.Current.Resources["WordListPendingWordStyle"];
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}