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
		List<PoolObject> objects;

		public Pool()
		{
			freeIndexes = new List<int>();
			objects = new List<PoolObject>();
		}

		~Pool()
		{
			OnCreate = null;
		}

		public T CreateObject<T>() where T : PoolObject, new()
		{
			int id = RecycleObject();

			if (id == INVALID_ID)
			{
				id = GenerateObject<T>();
			}

			OnCreate(id);

			return (T)objects[id];
		}

		int RecycleObject()
		{
			int id = -1;

			if (freeIndexes.Count > 0)
			{
				id = freeIndexes[0];
				freeIndexes.RemoveAt(0);
				objects[id].OnRecycle();
			}

			return id;
		}

		int GenerateObject<T>() where T : PoolObject, new()
		{
			int id = objects.Count;
			objects.Add(new T());
			objects[id].OnInitialize();
			return id;
		}

		public void DestroyObject(int id)
		{
			objects[id].OnDestroy();
			freeIndexes.Add(id);
		}

		public T GetObject<T>(int id) where T : PoolObject
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

		public void ForEach<T>(Action<T> action) where T : PoolObject
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
