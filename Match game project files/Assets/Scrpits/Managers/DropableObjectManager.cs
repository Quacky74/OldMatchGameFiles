using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class DropableObjectManager : MonoBehaviour
{
	[SerializeField] private bool isObjectsLocked;
	[SerializeField] private bool isSpwanBlocks;
	[SerializeField] private DorpablePrefabs_so dropablePrefabs;
	[SerializeField] private List<DropObject_so> ballDatasets;
	[SerializeField] private List<DropObject_so> blockDatasets;
	[SerializeField] private List<DropObject_so> eggDatasets;

	[SerializeField] private float spawnTime, delayTime;
	[SerializeField] private int spawnRate, maxNumberSpawned, spawnHeight, spawnMinX, spawnMaxX;


	public List<ballObject> dropedObjects;


	private void OnEnable()
	{
		dropedObjects = new List<ballObject>();
		InvokeRepeating(nameof(CallSpawn), spawnTime, delayTime);

		EventsManager.Instance.onLineDrawn += ProcessLine;
		EventsManager.Instance.OnProcessBomb += ProcessLine;
	}

	void CallSpawn()
	{
		SortList();
		if (dropedObjects.Count >= maxNumberSpawned)
		{
			CancelInvoke(nameof(CallSpawn));
		}
		else
		{
			CycleSpawning(spawnRate);
		}

	}

	void SortList()
	{
		for (int i = 0; i < dropedObjects.Count; i++)
		{
			if (dropedObjects[i] == null)
			{
				dropedObjects.RemoveAt(i);
			}
		}
	}

	void CycleSpawning(int p_spawnRate)
	{
		//TODO: Look at making spawn rates
		for (int i = 0; i < p_spawnRate; i++)
		{
			if (i/2 == 2)
			{
				if (blockDatasets.Count > 0)
				{
					SpawnBall(GetSpawnLocation(), GetBlockType());
				}
				
			}
			else
			{
				if (ballDatasets.Count > 0)
				{
					SpawnBall(GetSpawnLocation(), GetBallType());
				}

				
			}
		}
	}

	void SpawnBall(Vector3 p_spawnPoint, DropObject_so pDropObjectData)
	{
		switch (pDropObjectData.category)
		{
			case objectCatorgry.Ball:
				GameObject NewBall = Instantiate(dropablePrefabs.ballPrefab, p_spawnPoint, Quaternion.identity);
				NewBall.GetComponent<ballObject>().a_ObjData = pDropObjectData;

				if (NewBall.GetComponent<ballObject>().a_ObjData == GameManger.Inst.GeTargetRequirement().target &&
					isObjectsLocked)
				{
					NewBall.GetComponent<ballObject>().SetLockSprite(isObjectsLocked);
				}
			


				dropedObjects.Add(NewBall.GetComponent<ballObject>());
				break;
			case objectCatorgry.Block:
				GameObject NewBlock = Instantiate(dropablePrefabs.blockPrefab, p_spawnPoint, Quaternion.identity);
				NewBlock.GetComponent<ballObject>().a_ObjData = pDropObjectData;

				if (NewBlock.GetComponent<ballObject>().a_ObjData == GameManger.Inst.GeTargetRequirement().target &&
					isObjectsLocked)
				{
					NewBlock.GetComponent<ballObject>().SetLockSprite(isObjectsLocked);
				}
			


				dropedObjects.Add(NewBlock.GetComponent<ballObject>());
				break;
		}

	}


	void ProcessLine(List<ballObject> currentLineList)
	{

		for (int i =0; i < currentLineList.Count; i++)
		{
			switch (currentLineList[i].a_ObjData.category)
			{
				case objectCatorgry.Ball:

					if (currentLineList[i].isLocked)
					{
						currentLineList[i].SetLockSprite(false);
					}
					else
					{
						RestBallLocation(currentLineList[i]);
					}

					break;
				case objectCatorgry.Block:

					if (currentLineList[i].a_ObjData.color == dropObjectColor.Black || !isSpwanBlocks)
					{
						Destroy(currentLineList[i].gameObject);
					}
					else
					{
						currentLineList[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
						currentLineList[i].transform.position = GetSpawnLocation();

					}

					break;
				case objectCatorgry.Balloon:
					if (currentLineList[i].isLocked)
					{
						for (int b = 0; b < 4; b++)
						{
							Vector3 pos = dropedObjects[Random.Range(0, dropedObjects.Count)].transform.position;

							GameObject newBalloon =
								Instantiate(dropablePrefabs.balloonPrefab, pos, Quaternion.identity);
							
							newBalloon.GetComponent<ballObject>().SetSprite();
							newBalloon.GetComponent<ballObject>().SetLockSprite(false);

							dropedObjects.Add(newBalloon.GetComponent<ballObject>());

							currentLineList[i].SetLockSprite(false);
						}
						

					}
					
					dropedObjects.Remove(currentLineList[i]);
					Destroy(currentLineList[i].gameObject);

					break;
			}
		}
	}


	void RestBallLocation(ballObject pCurrentBall)
	{


		switch (pCurrentBall.a_ObjData.category)
		{
			case objectCatorgry.Ball:
				pCurrentBall.a_ObjData = GetBallType();
				break;
			case objectCatorgry.Block:
				pCurrentBall.a_ObjData = GetBlockType();
				break;
			case objectCatorgry.Egg:
				pCurrentBall.a_ObjData = GetEggType();
				break;
		}




		
		pCurrentBall.gameObject.transform.position = GetSpawnLocation();
		pCurrentBall.SetSprite();
	}

	Vector3 GetSpawnLocation()
	{
		return new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnHeight, 1);
	}

	DropObject_so GetBallType()
	{
		return ballDatasets[Random.Range(0, ballDatasets.Count)];
	}


	DropObject_so GetBlockType()
	{
		return blockDatasets[Random.Range(0, blockDatasets.Count)];
	}

	DropObject_so GetEggType()
	{
		return eggDatasets[Random.Range(0, eggDatasets.Count)];
	}
}
