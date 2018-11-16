using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public class PoolObjectBehaviour : MonoBehaviour, IPoolObject
	{
		public int PoolObjectID { get; set; }
		public float Health { get; set; }

		public float MaxHealth = 100.0f;
		public float HitDamage = 10.0f;

		Material poolObjectMaterial;

		void Awake()
		{			
			Health = MaxHealth;
			poolObjectMaterial = GetComponent<Renderer>().material;
			UpdateColor();
		}

		public void Destroy()
		{
			gameObject.SetActive(false);
		}

		public void Recycle()
		{
			gameObject.SetActive(true);
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			Health = MaxHealth;
			UpdateColor();
		}

		void OnCollisionEnter(Collision collision)
		{
			Health -= HitDamage;
			UpdateColor();
		}

		void UpdateColor()
		{
			poolObjectMaterial.color = Color.Lerp(Color.red, Color.green, Health / MaxHealth);
		}
	}
}
