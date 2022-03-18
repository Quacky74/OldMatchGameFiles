using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Target Requirements", menuName = "ScriptableObject/Game manager data/Target Requirements")]
public class TargetRequirements : ScriptableObject
{
	public string Name;
	public DropObject_so target;
	public int quantity;
	public int counter;
	public int movesLimit;

	private void OnEnable()
	{
		counter = quantity;
	}
}
