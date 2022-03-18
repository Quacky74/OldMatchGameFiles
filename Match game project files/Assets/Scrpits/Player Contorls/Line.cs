using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Line : MonoBehaviour
{
	public dropObjectColor currentDropObjectColor;
	public List<ballObject> balls;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<ballObject>())
		{
			balls.Add(other.GetComponent<ballObject>());

		}
	}

}
