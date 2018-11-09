namespace UnityProductive
{
	public class PoolObject
	{
		public virtual void OnInitialize() { }

		public virtual void OnRecycle() { }

		public virtual void OnDestroy() { }
	}
}
