using GrislyTools.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrislyTools
{
	[Serializable]
	public class StorageData : IData
	{
		[JsonProperty("Data")] private Dictionary<string, object> _data;

		private Dictionary<string, Action<object>> _onValueChanged;
		private static IDataStorageSystem<StorageData> _storageSystem;
		private string _storageKey;

		public void InitializeStorageData(string key, IDataStorageSystem<StorageData> storageSystem)
		{
			_storageSystem = storageSystem;
			_storageKey = key;

			if (_data == null)
				_data = new Dictionary<string, object>();

			_onValueChanged = new Dictionary<string, Action<object>>();
		}

		public bool GetValue<T>(string key, out T data, T defaultValue = default)
		{
			if (_data.TryGetValue(key, out var storedValue))
			{
				try
				{
					if (storedValue is T value)
					{
						data = value;
					}
					else
					{
						data = (T)Convert.ChangeType(storedValue, typeof(T));
					}
				}
				catch (InvalidCastException)
				{
					Debug.LogWarning($"Key {key} found, but value is not of type {typeof(T)}. Using default value.");
					data = defaultValue;
				}
				catch (FormatException)
				{
					Debug.LogWarning($"Key {key} found, but value cannot be converted to type {typeof(T)}. Using default value.");
					data = defaultValue;
				}
			}
			else
			{
				Debug.LogWarning($"No data was found with this key: {key}. A new one was created.");
				_data[key] = defaultValue;
				data = defaultValue;
			}

			return true;
		}
		public bool SetValue<T>(string key, T value)
		{
			if (_data.ContainsKey(key))
			{
				if (_data[key] is T)
				{
					_data[key] = value;
				}
				else
				{
					try
					{
						_data[key] = (T)Convert.ChangeType(value, typeof(T));
					}
					catch (InvalidCastException)
					{
						Debug.LogError($"Failed to set value for key {key}. Invalid cast from {value.GetType()} to {typeof(T)}.");
						return false;
					}
				}

				if (_onValueChanged.ContainsKey(key))
					_onValueChanged[key]?.Invoke(value);
			}
			else
			{
				_data.Add(key, value);
				Debug.LogWarning($"No data was found with this key: {key}. A new one was created");
			}

			_storageSystem.Save(_storageKey, this);
			return true;
		}
		public void SubcribeOnValueChange(string key, Action<object> callback)
		{
			if (_data.ContainsKey(key) && _onValueChanged.ContainsKey(key))
			{
				_onValueChanged[key] += callback;
			}
			else if (_data.ContainsKey(key))
			{
				_onValueChanged.Add(key, callback);
			}
			else
			{
				Debug.Log($"No data was found with this key: {key}");
			}
		}

		public void UnsubcribeAll()
		{
			_onValueChanged = new Dictionary<string, Action<object>>();
		}

		public void UnsubcribeOnValueChange(string key, Action<object> callback)
		{
			if (_onValueChanged.ContainsKey(key))
			{
				_onValueChanged[key] -= callback;
			}
		}

		public void Save()
		{
			_storageSystem.Save(_storageKey, this);
		}
	}
}
