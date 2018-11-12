namespace UnityProductive
{
	public interface IPoolObject
	{
		int PoolObjectID { get; set; }

		void Initialize();

		void Destroy();

		void Recycle();
	}
}
