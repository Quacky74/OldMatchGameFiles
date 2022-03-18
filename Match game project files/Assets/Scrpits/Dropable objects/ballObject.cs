using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ballObject : MonoBehaviour
{

	[SerializeField] DropObject_so dropObjectData;
	public bool isLocked;
	public SpriteRenderer lockSprite;
	public dropObjectColor a_Color => dropObjectData.color;
	public int a_Points => dropObjectData.points;

	public DropObject_so a_ObjData
	{
		set => dropObjectData = value;
		get => dropObjectData;
	}
	
	private List<ballObject> AdjustentBalls;

	public List<ballObject> a_AdjustentBalls => AdjustentBalls;
	public event Action<ballObject> OnHitGround;
	//TODO: clean up

	private void OnEnable()
	{
		AdjustentBalls = new List<ballObject>();
	}

	private void Start()
	{
		SetSprite();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<ballObject>())
		{
			ballObject NewBall = other.GetComponent<ballObject>();

			if (!AdjustentBalls.Contains(NewBall) && NewBall.GetComponent<ballObject>().a_ObjData.category != objectCatorgry.Egg)
			{
				AdjustentBalls.Add(NewBall);
			}
		}


		if (other.CompareTag("Ground"))
		{
			if (dropObjectData.category == objectCatorgry.Egg)
			{
				OnHitGround?.Invoke(this);
			}
		}


	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<ballObject>())
		{
			ballObject NewBall = other.GetComponent<ballObject>();

			AdjustentBalls.Remove(NewBall);
		}
	}

	public void SetSprite()
	{
		GetComponent<SpriteRenderer>().sprite = dropObjectData.objSprite;
	}


	public void SetLockSprite(bool isSpriteActive)
	{
		isLocked = isSpriteActive;
		lockSprite.enabled = isSpriteActive;
	}
}
