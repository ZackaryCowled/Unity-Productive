using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public class PoolObjectBehaviour : IPoolObject
	{
		public GameObject PoolObject { get; set; }
		public float Health { get; set; }
		public float Bounciness = 10.0f;
		public float MaxHealth = 100.0f;
		public float HitDamage = 10.0f;

		public void Instantiate()
		{
			Debug.Log("Instantiate");
			PoolObject = null;
			Health = MaxHealth;
	}

		public void Destroy()
		{
			Debug.Log("Destroy");
			PoolObject.SetActive(false);
		}

		public void Recycle()
		{
			Debug.Log("Recycle");
			PoolObject.SetActive(true);
			PoolObject.transform.position = Vector3.zero;
			PoolObject.transform.rotation = Quaternion.identity;
			Health = MaxHealth;
		}

		void OnCollisionEnter(Collision collision)
		{
			PoolObject.transform.position += collision.impulse * Bounciness;
			Health -= HitDamage;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.Lerp(Color.red, Color.green, MaxHealth / Health);
			Gizmos.DrawCube(PoolObject.transform.position, Vector3.one);
		}
	}
}
