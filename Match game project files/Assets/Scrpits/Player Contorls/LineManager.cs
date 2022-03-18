using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
/*
 * Orginal line drawing code was write by:Nathan from info gamer
 * Link to youtube:https://www.youtube.com/watch?v=pa_U64G7gkE&t=2s
 */
public class LineManager : MonoBehaviour
{

	#region Line Vairbles
	[SerializeField] GameObject linePrefab;
	[SerializeField] LayerMask layerMask;
	GameObject currentLine;
	LineRenderer lineRenderer;
	EdgeCollider2D edgeCollider;
	List<Vector2> fingerPositions;
	bool isLegalTouch;

	#endregion

	#region Matching vairables
	[SerializeField] private int MinMatchs, CreateBombCount;
	[SerializeField] private List<ballObject> touchableObjects;

	#endregion

	private List<Line> ListOfLines;


	void Update()
	{
		#region Mouse Input
		
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, layerMask);

			if (hit.collider != null)
			{
				if (hit.collider.GetComponent<ballObject>())
				{
					ballObject startingball = hit.collider.GetComponent<ballObject>();
					if (ListOfLines == null)
					{
						ListOfLines = new List<Line>();
					}
					if (startingball.a_ObjData.category != objectCatorgry.Block && !startingball.isLocked)
					{
						isLegalTouch = true;
						CreateLine();
					}

					if (startingball.a_ObjData.category == objectCatorgry.Bomb)
					{
						GameManger.Inst.gameStats.moveCount -= 1;
						EventsManager.Instance.BombUsed(startingball);
						EventsManager.Instance.CheckRequirments();
						EventsManager.Instance.UpdateMoveCount();
					}


				}
			}


		}

		//If the player has is touching screen and touching a ball
		if (Input.GetMouseButton(0) && isLegalTouch)
		{

			Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
			{
				
				UpdateLine(tempFingerPos);
			}
		}


		if (Input.GetMouseButtonUp(0) && isLegalTouch)
		{
			isLegalTouch = false;
			fingerPositions.Clear();

			if (ValidateMatchList(currentLine.GetComponent<Line>().balls))
			{
				ClearTouchableObjects();
				
				GameManger.Inst.gameStats.moveCount -= 1;//HACK
				EventsManager.Instance.CheckRequirments();
				EventsManager.Instance.UpdateMoveCount();
			}

			ClearLine();

		}

		#endif
		#endregion


		#region Touch controls

#if UNITY_ANDROID

		if (Input.touches.Length > 0)
		{
			Touch currentTouch = Input.touches[0];
			if (currentTouch.phase == TouchPhase.Began)
			{
				Vector2 inputPosition = Camera.main.ScreenToWorldPoint(currentTouch.position);

				RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector3.zero, layerMask);

				if (hit.collider != null)
				{
					if (hit.collider.GetComponent<ballObject>())
					{
						ballObject startingball = hit.collider.GetComponent<ballObject>();
						if (startingball.a_ObjData.category != objectCatorgry.Block && !startingball.isLocked)
						{
							isLegalTouch = true;
							CreateLine();
						}

						if (startingball.a_ObjData.category == objectCatorgry.Bomb)
						{
							GameManger.Inst.gameStats.moveCount -= 1;
							EventsManager.Instance.BombUsed(startingball);
							EventsManager.Instance.CheckRequirments();
							EventsManager.Instance.UpdateMoveCount();
						}

					}
				}
			}


			if (currentTouch.phase == TouchPhase.Moved && isLegalTouch)
			{
				Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
				if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
				{
					UpdateLine(tempFingerPos);
				}
			}


			if ((currentTouch.phase == TouchPhase.Ended) && isLegalTouch)
			{
				isLegalTouch = false;
				fingerPositions.Clear();

				ValidateMatchList(currentLine.GetComponent<Line>().balls);
				if (touchableObjects.Count >= MinMatchs)
				{
					ClearTouchableObjects();
					GameManger.Inst.gameStats.moveCount -= 1;//HACK
					EventsManager.Instance.CheckRequirments();
					EventsManager.Instance.UpdateMoveCount();
					
				}

				ClearLine();
			}
		}

		#endif


		#endregion


	}



	#region Drawing lines
	void CreateLine()
	{

		fingerPositions = new List<Vector2>();
		currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);

		lineRenderer = currentLine.GetComponent<LineRenderer>();
		edgeCollider = currentLine.GetComponent<EdgeCollider2D>();


		fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		lineRenderer.SetPosition(0, fingerPositions[0]);
		lineRenderer.SetPosition(1, fingerPositions[1]);

		edgeCollider.points = fingerPositions.ToArray();

	}

	void UpdateLine(Vector2 newFingerPos)
	{
		fingerPositions.Add(newFingerPos);

		lineRenderer.positionCount++;
		lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

		edgeCollider.points = fingerPositions.ToArray();
	}

	void ClearLine()
	{
		Destroy(currentLine.gameObject);
	}

	#endregion

	#region Matching

	bool ValidateMatchList(List<ballObject> p_CurrentBalls)
	{
		touchableObjects = new List<ballObject>();
		dropObjectColor lineColor;

		//Check the length of the pass list and the line color
		if (p_CurrentBalls.Count > 0)
		{
			lineColor = p_CurrentBalls[0].a_Color;
		}
		else
		{
			return false;
		}

		//Check that each object in the list that is a ball is the right color
		foreach (var currentBall in p_CurrentBalls)
		{
			if (currentBall.a_Color != lineColor && currentBall.a_ObjData.category == objectCatorgry.Ball)
			{
				return false;
			}
		}

		foreach (var currentBall in p_CurrentBalls)
		{
			if (!touchableObjects.Contains(currentBall))
			{
				touchableObjects.Add(currentBall);

			}



			foreach (var adjustentBall in currentBall.a_AdjustentBalls)
			{


				switch (adjustentBall.a_ObjData.category)
				{
					case objectCatorgry.Ball:
						if (adjustentBall.isLocked)
						{
							if (!touchableObjects.Contains(adjustentBall))
							{
								touchableObjects.Add(adjustentBall);
							}
						}
						break;
					case objectCatorgry.Block:

						if (!touchableObjects.Contains(adjustentBall))
						{
							touchableObjects.Add(adjustentBall);
						}

						break;
				}
			}

		}


		if (touchableObjects.Count >= CreateBombCount)
		{
			EventsManager.Instance.CreateBomb(touchableObjects[0].transform.position);
		}


		return touchableObjects.Count >= MinMatchs;
	}

	void ClearTouchableObjects()
	{
		EventsManager.Instance.LineDrawn(touchableObjects);
		touchableObjects.Clear();
	}

	#endregion

}
