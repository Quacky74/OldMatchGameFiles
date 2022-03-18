using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
BombManager : MonoBehaviour
{

	public MatchObjectsLine lineMatcher;

	[SerializeField] private DorpablePrefabs_so DorpablePrefabs;
	public List<ballObject> bombList;


	private void Start()
	{
		EventsManager.Instance.OnCreateBomb += CreateBomb;
		EventsManager.Instance.OnBombUsed += BombEffect;
	}

	public void CreateBomb(Vector3 pos)
	{
		GameObject newBomb = Instantiate(DorpablePrefabs.bombPrefab, pos, Quaternion.identity);
		bombList.Add(newBomb.GetComponent<ballObject>());
	}

	public void BombEffect(ballObject currentBomb)
	{
		EventsManager.Instance.ProcessBomb(currentBomb.a_AdjustentBalls);
		bombList.Remove(currentBomb);
		Destroy(currentBomb.gameObject);
	}

}
