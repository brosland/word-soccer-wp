using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.FileProperties;
using SQLite;

namespace WordSoccer.Model
{
	public class DatabaseHelper
	{
		private String databaseName, originalDatabasePath;
		private SQLiteConnection connection;
		public event EventHandler OnInitHandler;

		public DatabaseHelper(String databaseName, String originalDatabasePath)
		{
			this.databaseName = databaseName;
			this.originalDatabasePath = originalDatabasePath;
		}

		public SQLiteConnection GetSqLiteConnection()
		{
			if (connection == null)
			{
				throw new Exception("Connection is not initialized. Please call Init() at the first.");
			}

			return connection;
		}

		public async void Init()
		{
			bool exists;

			try
			{
				await ApplicationData.Current.LocalFolder.GetFileAsync(databaseName);
				exists = true;
			}
			catch (FileNotFoundException e)
			{
				exists = false;
			}

			//if (!exists)
			{
				await ReloadDatabase();
			}

			String databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName);

			connection = new SQLiteConnection("Data Source=" + databasePath + ";Version=3;");

			if (OnInitHandler != null)
			{
				OnInitHandler.Invoke(this, null);
			}
		}

		public async Task ReloadDatabase()
		{
			StorageFolder installedLocation = Package.Current.InstalledLocation;
			StorageFile file = await installedLocation.GetFileAsync(originalDatabasePath);
			StorageFolder localFolder = ApplicationData.Current.LocalFolder;

			await file.CopyAsync(localFolder, databaseName, NameCollisionOption.ReplaceExisting);

			StorageFile createdFile = await localFolder.GetFileAsync(databaseName);
			BasicProperties info = await createdFile.GetBasicPropertiesAsync();

			Debug.WriteLine(info.Size);
		}
	}
}