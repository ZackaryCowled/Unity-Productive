using UnityEngine;

namespace UnityProductive
{
	public class PoolObjectBehaviourFactory : IFactory<PoolObjectBehaviour>
	{
		public PoolObjectBehaviour CreateInstance(params object[] args)
		{
			return Object.Instantiate((GameObject)args[0]).GetComponent<PoolObjectBehaviour>();
		}
	}
}
