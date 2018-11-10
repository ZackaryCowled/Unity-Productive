using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProductive;

public class TestSpawner : MonoBehaviour
{
	public PoolObject poolObject;
	public float spawnsPerSecond = 10.0f;

	Pool pool;
	float timer = 0.0f;

	void Awake()
	{
		pool = new Pool();
	}

	void Update()
	{
		timer += spawnsPerSecond * Time.deltaTime;

		while (timer >= 1.0f)
		{
			timer -= 1.0f;

			PoolObjectBehaviour poolObjectBehaviour = pool.CreateObject<PoolObjectBehaviour>();

			if (poolObjectBehaviour.PoolObject != null)
			{
				poolObjectBehaviour.PoolObject = Instantiate(poolObject.prefab);
			}

			poolObjectBehaviour.PoolObject.transform.position = new Vector3(Random.Range(-25.0f, 25.0f), 10.0f, Random.Range(-25.0f, 25.0f));
		}

		pool.ForEach((PoolObjectBehaviour poolObject) =>
		{
			if(poolObject.Health <= 0 || poolObject.PoolObject.transform.position.y < -10.0f)
			{
				pool.DestroyObject(poolObject.PoolObject.GetInstanceID());
			}
		});
	}
}
