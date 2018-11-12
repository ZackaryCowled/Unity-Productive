namespace UnityProductive
{
	public interface IFactory<T>
	{
		T CreateInstance(params object[] args);
	}
}
