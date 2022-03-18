using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<ballObject>())
		{
			if (other.GetComponent<ballObject>().a_ObjData.category == objectCatorgry.Egg)
			{
				EventsManager.Instance.GroundCheck(other.GetComponent<ballObject>());
			}
		}
	}
}
