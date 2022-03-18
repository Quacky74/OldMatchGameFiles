using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private List<Vector3> cameraPostions;
	[SerializeField] private int cameraPosCounter;
	[SerializeField] private LevelTrigger lTrigger;
	[SerializeField] private StartScreenUI startUI;
	[SerializeField] bool isAtPositon;

	private void OnEnable()
	{
		lTrigger.OnMoveCam += TriggerCameraMovement;
		startUI.OnStartGame += TriggerCameraMovement;
	}


	private void LateUpdate()
	{
		if (!isAtPositon && cameraPosCounter < cameraPostions.Count)
		{
			MoveCamera(cameraPosCounter);
		}
	}

	void MoveCamera(int positionCount)
	{

		transform.Translate(cameraPostions[positionCount].y > transform.position.y
			? new Vector3(0, 1, 0)
			: new Vector3(0, -1, 0));

		if (transform.position == cameraPostions[positionCount])
		{
			isAtPositon = true;
			cameraPosCounter++;
		}
	}

	void TriggerCameraMovement(bool pBool)
	{
		isAtPositon = false;
	}
}
