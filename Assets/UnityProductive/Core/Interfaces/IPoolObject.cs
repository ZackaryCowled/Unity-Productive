namespace UnityProductive
{
	public interface IPoolObject
	{
		int PoolObjectID { get; set; }

		void Destroy();

		void Recycle();
	}
}
