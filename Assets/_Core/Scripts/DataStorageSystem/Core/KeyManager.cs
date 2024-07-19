using System.Security.Cryptography;
using UnityEngine;

public static class KeyManager
{
	private static readonly string encryptionKey = "encryption_key";

	public static byte[] GetOrCreateKey()
	{
		if (PlayerPrefs.HasKey(encryptionKey))
		{
			string savedKey = PlayerPrefs.GetString(encryptionKey);
			return System.Convert.FromBase64String(savedKey);
		}
		else
		{
			byte[] key = new byte[32];
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(key);
			}
			string keyString = System.Convert.ToBase64String(key);
			PlayerPrefs.SetString(encryptionKey, keyString);
			return key;
		}
	}
}
