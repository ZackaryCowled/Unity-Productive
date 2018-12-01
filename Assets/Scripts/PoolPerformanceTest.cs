using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

public class PoolPerformanceTest : MonoBehaviour
{
	public bool usePoolObjects;
	public GameObject Prefab;
	public int MaxAlive = 1000;
	public float Spread = 50.0f;
	public float SpawnHeight = 10.0f;

	Pool pool;
	PoolObjectBehaviourFactory factory;
	List<PoolObjectBehaviour> poolObjectBehaviours;

	void Awake()
	{
		pool = new Pool();
		factory = new PoolObjectBehaviourFactory();
		poolObjectBehaviours = new List<PoolObjectBehaviour>();
	}

	void Update()
	{
		if (usePoolObjects)
		{
			while (pool.ObjectsCount() < MaxAlive)
			{
				Vector3 randomSphere = Random.insideUnitSphere;
				Vector3 spawnPosition = new Vector3(randomSphere.x * Mathf.Abs(Spread), SpawnHeight, randomSphere.z * Mathf.Abs(Spread));

				PoolObjectBehaviour poolObjectBehaviour = pool.CreateObject<PoolObjectBehaviour, PoolObjectBehaviourFactory>(factory, Prefab);
				poolObjectBehaviour.transform.position = spawnPosition;
			}
		}
		else
		{
			while (poolObjectBehaviours.Count < MaxAlive)
			{
				Vector3 randomSphere = Random.insideUnitSphere;
				Vector3 spawnPosition = new Vector3(randomSphere.x * Spread, SpawnHeight, randomSphere.z * Spread);

				poolObjectBehaviours.Add(Instantiate(Prefab, spawnPosition, Quaternion.identity).GetComponent<PoolObjectBehaviour>());
			}
		}

		pool.ForEach((PoolObjectBehaviour poolObject) =>
		{
			if (poolObject.Health <= 0 || pool.ObjectsCount() > MaxAlive)
			{
				pool.DestroyObject(poolObject.PoolObjectID);
			}
		});

		for (int i = 0; i < poolObjectBehaviours.Count; i++)
		{
			if (poolObjectBehaviours[i].Health <= 0 || poolObjectBehaviours.Count > MaxAlive)
			{
				Destroy(poolObjectBehaviours[i].gameObject);
				poolObjectBehaviours.RemoveAt(i);
			}
		}
	}
}
