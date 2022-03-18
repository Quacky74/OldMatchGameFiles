using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{	
	public static EventsManager Instance;
	private void OnEnable()
	{
		if (Instance == null || Instance != this)
		{
			Instance = null;
			Instance = this;
		}
	}
	#region Match objects Done
		//Line drawn
		public event Action<List<ballObject>> onLineDrawn;//Update score and target UI, process a drawn line
		public void LineDrawn(List<ballObject> balls)
		{
			onLineDrawn?.Invoke(balls);
		}
		
		public event Action OnCheckRequirments;
		public void CheckRequirments()
		{
			OnCheckRequirments?.Invoke();
		}
		

		public event Action onUpdateMoveCount;

		public void UpdateMoveCount()
		{
			onUpdateMoveCount?.Invoke();
		}
		
		public event Action<Vector3> OnCreateBomb;
		public void CreateBomb(Vector3 BombPos)
		{
			OnCreateBomb?.Invoke(BombPos);
		}
		
		public event Action<ballObject> OnBombUsed;
		public void BombUsed(ballObject ball)
		{
			OnBombUsed?.Invoke(ball);
		}
	#endregion

	
	#region Bomb manager Done
	public event Action<List<ballObject>> OnProcessBomb;

	public void ProcessBomb(List<ballObject> BombsObjects)
	{
		OnProcessBomb?.Invoke(BombsObjects);
	}


	#endregion

	#region Game manager Done
	public event Action<bool> OnOutOfMoves;
	public void OutOfMoves(bool isOutofMoves)
	{
		OnOutOfMoves?.Invoke(false);
	}

	public event Action<bool> OnTargetsCleared;

	public void TargetsCleared()
	{
		OnTargetsCleared?.Invoke(true);
	}
	public event Action<TargetRequirements> OnSetTarget;

	public void SetTarget(TargetRequirements currentTarget)
	{
		OnSetTarget?.Invoke(currentTarget);
	}


	


	#endregion


	#region Ground Check

	public event Action<ballObject> OnGroundCheck;

	public void GroundCheck(ballObject CurrentObject)
	{
		OnGroundCheck?.Invoke(CurrentObject);
	}
	

	#endregion


	#region Camera Movement

	

	#endregion


	

}
