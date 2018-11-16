using System;
using System.Collections.Generic;

namespace UnityProductive
{
	public class HashPool<Hash>
	{
		Dictionary<Hash, int> indexMap;
		Pool pool;
		Hash nextHash;

		public HashPool()
		{
			indexMap = new Dictionary<Hash, int>();
			pool = new Pool();

			pool.OnCreate += CreateObject;
		}

		public T CreateObject<T>(Hash hash) where T : IPoolObject, new()
		{
			nextHash = hash;
			return pool.CreateObject<T>();
		}

		public T1 CreateObject<T1, T2>(Hash hash, T2 factory, params object[] args) where T1: IPoolObject, new() where T2 : IFactory<T1>
		{
			nextHash = hash;
			return pool.CreateObject<T1, T2>(factory, args);
		}

		void CreateObject(int id)
		{
			indexMap[nextHash] = id;
		}

		public void DestroyObject(Hash hash)
		{
			pool.DestroyObject(indexMap[hash]);
			indexMap.Remove(hash);
		}

		public T GetObject<T>(Hash hash) where T : IPoolObject
		{
			return pool.GetObject<T>(indexMap[hash]);
		}

		public void ForEach<T>(Action<T> action) where T : IPoolObject
		{
			pool.ForEach(action);
		}
	}
}
