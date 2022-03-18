using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Droppable object data", menuName = "ScriptableObject/Droppable Objects/Object Data")]

[System.Serializable]
public class DropObject_so : ScriptableObject
{
	public int points;
	public Sprite objSprite;
	public dropObjectColor color;
	public objectCatorgry category;
}

public enum dropObjectColor
{
	Red, Green, Blue, Black
}

public enum objectCatorgry
{
	Ball, Block, Egg, Balloon, Bomb//TODO: think of better name
}