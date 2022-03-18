using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (fileName = "Game Manager droppable prefabs", menuName = "ScriptableObject/Game manager data/Droppable prefabs")]
public class DorpablePrefabs_so : ScriptableObject
{
	public GameObject ballPrefab;
	public GameObject blockPrefab;
	public GameObject bombPrefab;
	public GameObject balloonPrefab;
	public GameObject eggPrefab;
}
