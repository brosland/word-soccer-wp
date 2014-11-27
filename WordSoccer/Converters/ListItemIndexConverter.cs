using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace WordSoccer.Converters
{
	public class ListItemIndexConverter : IValueConverter
	{
		// Value should be ListBoxItem that contains the current record. RelativeSource={RelativeSource AncestorType=ListBoxItem}
		public object Convert(Object value, Type targetType, Object parameter, String language)
		{
			ListViewItem listViewItem = (ListViewItem)value;
			ListView listView = (ListView)listViewItem.Parent;

			return 0; //listView.ItemContainerGenerator.IndexFromContainer(listViewItem) + 1;
		}

		public object ConvertBack(Object value, Type targetType, Object parameter, String language)
		{
			throw new NotImplementedException();
		}
	}
}