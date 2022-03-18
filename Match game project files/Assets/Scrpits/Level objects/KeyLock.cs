using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{

	[SerializeField] private string searchableTag;
	[SerializeField] private GameObject lockedObject;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(searchableTag))
		{

			Destroy(lockedObject);
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
