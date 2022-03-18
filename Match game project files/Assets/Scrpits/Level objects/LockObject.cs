using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockObject : MonoBehaviour
{


	[SerializeField] private Text targetLockText;
	[SerializeField] private Image targetImage;
	[SerializeField] GameObject unlockableObject;
	[SerializeField] TargetRequirements lockRequirements;


	private void Awake()
	{
		targetLockText = GetComponentInChildren<Text>();
		targetImage.sprite = lockRequirements.target.objSprite;
		targetLockText.text = lockRequirements.counter.ToString();
	}

	private void Start()
	{
		EventsManager.Instance.onLineDrawn += CheckRequirements;
		EventsManager.Instance.OnProcessBomb += CheckRequirements;
	}


	void CheckRequirements(List<ballObject>  Objects)
	{
		foreach (var obj in Objects)
		{
			if (obj.a_Color == lockRequirements.target.color && obj.a_ObjData.category == lockRequirements.target.category)
			{
				lockRequirements.counter--;
				targetLockText.text = lockRequirements.counter.ToString();
			}
		}

		
		if (lockRequirements.counter < 1)
		{
			unlockableObject.SetActive(false);
			this.gameObject.SetActive(false);
		}
	}
}
