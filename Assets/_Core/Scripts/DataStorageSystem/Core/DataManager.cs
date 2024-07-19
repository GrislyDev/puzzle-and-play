using GrislyTools.Interfaces;
using UnityEngine;

namespace GrislyTools
{
	public static class DataManager
	{
		public static StorageData Data => _gameData;


		private static StorageData _gameData;
		private static string _dataStorageKey = "GameData";
		private static IDataStorageSystem<StorageData> _storageSystem;

		static DataManager()
		{
			_storageSystem = new JSONDataStorageSystem<StorageData>();

			if (!_storageSystem.Load(_dataStorageKey, out _gameData))
			{
				_gameData = new StorageData();
			}

			_gameData.InitializeStorageData(_dataStorageKey, _storageSystem);

			Debug.Log("DataManager created");
		}
	}
}