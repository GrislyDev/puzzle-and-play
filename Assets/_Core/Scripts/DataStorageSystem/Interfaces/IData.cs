using System;

namespace GrislyTools.Interfaces
{
	public interface IData
	{
		bool SetValue<T>(string key, T value);
		bool GetValue<T>(string key,out T data, T defaultValue = default);
		void SubcribeOnValueChange(string key, Action<object> callback);
		void UnsubcribeOnValueChange(string key, Action<object> callback);
		void UnsubcribeAll();
	}
}