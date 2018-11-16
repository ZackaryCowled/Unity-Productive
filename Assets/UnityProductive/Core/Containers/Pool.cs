using System;
using System.Collections.Generic;

namespace UnityProductive
{
	public class Pool
	{
		const int INVALID_ID = -1;

		public delegate void Create(int id);
		public event Create OnCreate;

		List<int> freeIndexes;
		List<IPoolObject> objects;

		public Pool()
		{
			freeIndexes = new List<int>();
			objects = new List<IPoolObject>();
		}

		~Pool()
		{
			OnCreate = null;
		}

		public T CreateObject<T>() where T : IPoolObject, new()
		{
			int id = RecycleObject();

			if (id == INVALID_ID)
			{
				id = GenerateObject<T>();
			}

			OnCreate?.Invoke(id);

			return (T)objects[id];
		}

		public T1 CreateObject<T1, T2>(T2 factory, params object[] args) where T1 : IPoolObject, new() where T2 : IFactory<T1>
		{
			int id = RecycleObject();

			if(id == INVALID_ID)
			{
				id = GenerateObject<T1, T2>(factory, args);
			}

			OnCreate?.Invoke(id);

			return (T1)objects[id];
		}

		int RecycleObject()
		{
			int id = -1;

			if (freeIndexes.Count > 0)
			{
				id = freeIndexes[0];
				freeIndexes.RemoveAt(0);
				objects[id].Recycle();
			}

			return id;
		}

		int GenerateObject<T>() where T : IPoolObject, new()
		{
			int id = objects.Count;
			objects.Add(new T());
			objects[id].PoolObjectID = id;
			return id;
		}

		int GenerateObject<T1, T2>(T2 factory, params object[] args) where T1 : IPoolObject where T2 : IFactory<T1>
		{
			int id = objects.Count;
			objects.Add(factory.CreateInstance(args));
			objects[id].PoolObjectID = id;
			return id;
		}

		public void DestroyObject(int id)
		{
			objects[id].Destroy();
			freeIndexes.Add(id);
		}

		public T GetObject<T>(int id) where T : IPoolObject
		{
			return (T)objects[id];
		}

		public int AllocatedCount()
		{
			return objects.Count;
		}

		public int ObjectsCount()
		{
			return objects.Count - freeIndexes.Count;
		}

		public int ReadyCount()
		{
			return freeIndexes.Count;
		}

		public void ForEach<T>(Action<T> action) where T : IPoolObject
		{
			for(int i = 0; i < objects.Count; i++)
			{
				if (!freeIndexes.Contains(i))
				{
					action.Invoke((T)objects[i]);
				}
			}
		}
	}
}
