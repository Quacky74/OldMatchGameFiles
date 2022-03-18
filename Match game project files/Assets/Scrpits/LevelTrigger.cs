using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
	public event Action<bool> OnMoveCam;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<ballObject>())
		{
			OnMoveCam?.Invoke(false);
		}
	}
}
