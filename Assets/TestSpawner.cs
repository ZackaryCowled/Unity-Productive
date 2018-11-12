using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

public class TestSpawner : MonoBehaviour
{
	public GameObject Prefab;
	public int MaxAlive = 1000;
	public float SpawnsPerSecond = 10.0f;

	Pool pool;
	PoolObjectBehaviourFactory factory;
	float timer = 0.0f;

	void Awake()
	{
		pool = new Pool();
		factory = new PoolObjectBehaviourFactory();
	}

	void Update()
	{
		timer += SpawnsPerSecond * Time.deltaTime;

		while (timer >= 1.0f)
		{
			timer -= 1.0f;

			if (pool.ObjectsCount() < MaxAlive)
			{
				PoolObjectBehaviour poolObjectBehaviour = pool.CreateObject<PoolObjectBehaviour, PoolObjectBehaviourFactory>(factory, Prefab);
				poolObjectBehaviour.transform.position = new Vector3(Random.Range(-25.0f, 25.0f), 10.0f, Random.Range(-25.0f, 25.0f));
			}
		}

		pool.ForEach((PoolObjectBehaviour poolObject) =>
		{
			if(poolObject.Health <= 0 || poolObject.transform.position.y < -10.0f)
			{
				pool.DestroyObject(poolObject.PoolObjectID);
			}
		});
	}
}
