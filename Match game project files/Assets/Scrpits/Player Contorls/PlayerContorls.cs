using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorls : MonoBehaviour
{
	public event Action<ballObject> OnFoundBall;

	// Update is called once per frame
	void Update()
	{

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

			if (hit.collider != null)
			{
				if (hit.collider.GetComponent<ballObject>())
				{
					ValidTouch(hit.collider.GetComponent<ballObject>());
				}
			}
		}
#endif

#if UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
			RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector3.zero);

			if (hit.collider != null)
			{
				if (hit.collider.GetComponent<ballObject>())
				{
					ValidTouch(hit.collider.GetComponent<ballObject>());
				}
			}
		}
#endif

	}


	void ValidTouch(ballObject p_currentBall)
	{
		OnFoundBall?.Invoke(p_currentBall);
	}
}
