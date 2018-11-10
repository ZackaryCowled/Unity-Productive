namespace UnityProductive
{
	public interface IPoolObject
	{
		void Instantiate();

		void Destroy();

		void Recycle();
	}
}
