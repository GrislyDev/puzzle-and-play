namespace GrislyTools.Interfaces
{
	public interface IDataStorageSystem<T> where T : IData
	{
		void Save(string key, T data);

		bool Load(string key, out T data);
	}
}