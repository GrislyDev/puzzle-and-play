using GrislyTools.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using static AESEncryptor;

namespace GrislyTools
{

	public class JSONDataStorageSystem<T> : IDataStorageSystem<T> where T : IData
	{
		private readonly string _key;

        public JSONDataStorageSystem()
        {
			_key = System.Convert.ToBase64String(KeyManager.GetOrCreateKey());
        }

        public bool Load(string key, out T data)
		{
			string savedData = PlayerPrefs.GetString(key, string.Empty);

			if (savedData == string.Empty)
			{
				data = default;
				return false;
			}
			else
			{
				AESEncryptedText encryptedText = JsonConvert.DeserializeObject<AESEncryptedText>(savedData);
				string jsonData = Decrypt(encryptedText, _key);
				data = JsonConvert.DeserializeObject<T>(jsonData);
				return true;
			}
		}

		public void Save(string key, T data)
		{
			string jsonData = JsonConvert.SerializeObject(data);
			AESEncryptedText closedData = Encrypt(jsonData, _key);
			string savedData = JsonConvert.SerializeObject(closedData);
			PlayerPrefs.SetString(key, savedData);
		}
	}
}