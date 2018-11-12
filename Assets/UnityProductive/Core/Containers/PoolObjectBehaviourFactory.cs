using UnityEngine;

namespace UnityProductive
{
	public class PoolObjectBehaviourFactory : IFactory<PoolObjectBehaviour>
	{
		public PoolObjectBehaviour CreateInstance(params object[] args)
		{
			GameObject gameObject = Object.Instantiate((GameObject)args[0]);
			return gameObject.GetComponent<PoolObjectBehaviour>();
		}
	}
}
