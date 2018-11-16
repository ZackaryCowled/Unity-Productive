using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

public class PoolPerformanceTest : MonoBehaviour
{
	public bool usePoolObjects;
	public GameObject Prefab;
	public int MaxAlive = 1000;
	public float SpawnsPerSecond = 10.0f;

	Pool pool;
	PoolObjectBehaviourFactory factory;
	List<PoolObjectBehaviour> gameObjects;
	float timer = 0.0f;

	void Awake()
	{
		if (usePoolObjects)
		{
			pool = new Pool();
			factory = new PoolObjectBehaviourFactory();
		}
		else
		{
			gameObjects = new List<PoolObjectBehaviour>();
		}
	}

	void Update()
	{
		timer += SpawnsPerSecond * Time.deltaTime;

		while (timer >= 1.0f)
		{
			timer -= 1.0f;

			if (usePoolObjects)
			{
				if (pool.ObjectsCount() < MaxAlive)
				{
					PoolObjectBehaviour poolObjectBehaviour = pool.CreateObject<PoolObjectBehaviour, PoolObjectBehaviourFactory>(factory, Prefab);
					poolObjectBehaviour.transform.position = new Vector3(Random.Range(-25.0f, 25.0f), 10.0f, Random.Range(-25.0f, 25.0f));
				}
			}
			else
			{
				if(gameObjects.Count < MaxAlive)
				{
					gameObjects.Add(Instantiate(Prefab, new Vector3(Random.Range(-25.0f, 25.0f), 10.0f, Random.Range(-25.0f, 25.0f)), Quaternion.identity).GetComponent<PoolObjectBehaviour>());
				}
			}
		}

		if (usePoolObjects)
		{
			pool.ForEach((PoolObjectBehaviour poolObject) =>
			{
				if (poolObject.Health <= 0 || poolObject.transform.position.y < -10.0f)
				{
					pool.DestroyObject(poolObject.PoolObjectID);
				}
			});
		}
		else
		{
			for(int i = 0; i < gameObjects.Count; i++)
			{
				if(gameObjects[i].Health <= 0 || gameObjects[i].transform.position.y < -10.0f)
				{
					Destroy(gameObjects[i]);
					gameObjects.RemoveAt(i);
				}
			}
		}
	}
}
