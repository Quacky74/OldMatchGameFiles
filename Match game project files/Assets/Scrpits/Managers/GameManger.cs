using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour
{

	[SerializeField] private bool isGameModeEndless;
	[SerializeField] private List<TargetRequirements> Requirements;
	[SerializeField] private GroundTrigger groundTrigger;
	[HideInInspector] public TargetRequirements currentRequirements;


	public GameStats gameStats;
	public static GameManger Inst; //HACK, shortcut for now, look for another way to do this.



	private void OnEnable()
	{
		if (Inst != this || Inst == null)
		{
			Inst = null;
			Inst = this;
		}

		currentRequirements = isGameModeEndless ? GetNewRequirements() : Requirements[0];
		
		SetPlayerStats();
	}

	void Start()
	{
		EventsManager.Instance.OnCheckRequirments += CheckTargetsCleared;
		EventsManager.Instance.onUpdateMoveCount += CheckMoves;
		EventsManager.Instance.OnGroundCheck += EggHitGroundFunction;
		EventsManager.Instance.SetTarget(currentRequirements);
	}


	void SetPlayerStats()
	{
		gameStats.moveCount = currentRequirements.movesLimit;
		gameStats.targetCount = currentRequirements.quantity;

	}


	
	public void CheckMoves()
	{
		if(gameStats.moveCount < 1)
		{
			EventsManager.Instance.OutOfMoves(false);
		}

	}

	//check if number of cleared targets is blew zero
	public void CheckTargetsCleared()
	{
		if (gameStats.targetCount < 1)
		{
			if (isGameModeEndless)
			{
				currentRequirements = GetNewRequirements();

				gameStats.moveCount += currentRequirements.movesLimit;
				gameStats.targetCount = currentRequirements.quantity;
				EventsManager.Instance.SetTarget(currentRequirements);
				
			}
			else
			{
				EventsManager.Instance.TargetsCleared();
			}

		}
	}


	TargetRequirements GetNewRequirements()
	{
		return Requirements[Random.Range(0, Requirements.Count)];
	}



	void EggHitGroundFunction(ballObject currentObject)
	{
		
		if (currentRequirements.target.category == objectCatorgry.Egg && 
			currentObject.a_ObjData.category == objectCatorgry.Egg)
		{
			gameStats.targetCount--;
			CheckTargetsCleared();
		}
	}

	public TargetRequirements GeTargetRequirement()
	{
		return currentRequirements;
	}


}
