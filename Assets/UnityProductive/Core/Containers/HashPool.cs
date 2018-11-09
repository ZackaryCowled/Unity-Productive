using System;
using System.Collections;
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

		public T CreateObject<T>(Hash hash) where T : PoolObject, new()
		{
			nextHash = hash;
			return pool.CreateObject<T>();
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

		public T GetObject<T>(Hash hash) where T : PoolObject
		{
			return pool.GetObject<T>(indexMap[hash]);
		}

		public void ForEach<T>(Action<T> action) where T : PoolObject
		{
			pool.ForEach(action);
		}
	}
}
