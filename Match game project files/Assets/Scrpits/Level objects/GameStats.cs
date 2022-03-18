using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Game play Stats", menuName = "ScriptableObject/Game manager data/Game play Stats")]
public class GameStats : ScriptableObject
{
	public int moveCount;
	public int targetCount;
}
