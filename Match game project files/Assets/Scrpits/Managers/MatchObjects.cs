using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*OLD SCRPIT
 SEE MatchObjectsLine FOR UP TO DATE SCRPIT
 */
public class MatchObjects : MonoBehaviour
{
	private List<ballObject> FoundBalls;
	[SerializeField] private int MinMatchs;

	[SerializeField] PlayerContorls contorls;
	[SerializeField] private TargetRequirements requirements;
	[SerializeField] private GameStats stats;
	

	public event Action<int> OnScoredPoints;//TODO:Rename this 
	public event Action<dropObjectColor> OnBallCleared;
	public event Action<int> OnSpawnBall;
	public event Action<int> OnLegalPlayerMove;

	void OnEnable()
	{
		stats.moveCount = requirements.movesLimit;
		OnLegalPlayerMove?.Invoke(stats.moveCount);
		contorls = GetComponent<PlayerContorls>();
		contorls.OnFoundBall += CheckObjects;
		FoundBalls = new List<ballObject>();
	}


	private void Start()
	{
		OnScoredPoints?.Invoke(0);
	}


	void CheckObjects(ballObject NewBall)
	{

		if (!FoundBalls.Contains(NewBall))
		{
			sortList(NewBall);
		}

		if (FoundBalls.Count > MinMatchs && stats.moveCount > 0)
		{
			stats.moveCount--;
			OnLegalPlayerMove?.Invoke(stats.moveCount);
			DestroyObjectsInList();
		}

		FoundBalls.Clear();
	}


	void sortList(ballObject p_ball)
	{
		foreach (var adjustentObject in p_ball.a_AdjustentBalls)
		{
			if (!FoundBalls.Contains(adjustentObject))
			{
				FoundBalls.Add(adjustentObject);
				sortList(adjustentObject);
			}
		}
	}

	void DestroyObjectsInList()//TODO: Rewok this to work with animations, audio quies and other specal effects.
	{
		OnSpawnBall?.Invoke(FoundBalls.Count);

		foreach (var foundBall in FoundBalls)
		{
			OnScoredPoints?.Invoke(foundBall.a_Points);
			OnBallCleared?.Invoke(foundBall.a_Color);
		
			Destroy(foundBall.gameObject);
		}

		FoundBalls.Clear();
	}

}
