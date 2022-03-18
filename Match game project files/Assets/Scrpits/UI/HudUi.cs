using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudUi : MonoBehaviour
{
	[SerializeField] Text scoreText, movesText, targetText;
	[SerializeField] Image targetImage;

	[SerializeField] private MatchObjectsLine matcherLine;
	[SerializeField] private BombManager bombManager;

	
	private int ScoreCounter, MovesCounter;
	private void OnEnable()
	{
		EventsManager.Instance.onLineDrawn += UpdateUi;
		EventsManager.Instance.onUpdateMoveCount += CountMoves;
		EventsManager.Instance.OnProcessBomb += UpdateUi;
		EventsManager.Instance.OnSetTarget += SetupHud;
		EventsManager.Instance.OnSetTarget += ResetHud;
	}

	public void CallSetup()
	{
		SetupHud(GameManger.Inst.currentRequirements);
	}

	void SetupHud(TargetRequirements newRequirements)
	{
		SetText(scoreText, 0);
		SetText(movesText, newRequirements.movesLimit);
		SetText(targetText, newRequirements.quantity);

		targetImage.sprite = newRequirements.target.objSprite;
	}

	void ResetHud(TargetRequirements newRequirements)
	{
		GameManger.Inst.gameStats.moveCount += MovesCounter;
		SetText(movesText, GameManger.Inst.gameStats.moveCount);
		SetText(targetText, newRequirements.quantity);

		targetImage.sprite = newRequirements.target.objSprite;
	}


	void UpdateUi(List<ballObject> objects)
	{

		foreach (var obj in objects)
		{
			CountPoints(obj.a_Points);

			if (obj.a_ObjData == GameManger.Inst.currentRequirements.target)
			{
				CheckTarget(obj.a_Color, obj.a_ObjData.category);
			}
		}
		
	}

	
	void CountPoints(int p_count)
	{
		ScoreCounter += p_count;
		SetText(scoreText, ScoreCounter);
	}

	void CountMoves()
	{
		SetText(movesText, GameManger.Inst.gameStats.moveCount);
	}

	void CheckTarget(dropObjectColor p_balltype, objectCatorgry p_ballcat)
	{
		if (p_balltype == GameManger.Inst.currentRequirements.target.color && p_ballcat == GameManger.Inst.currentRequirements.target.category)
		{
			CountTarget(1);
		}
	}

	void CountTarget(int p_count)
	{
		SetText(targetText, --GameManger.Inst.gameStats.targetCount);
	}

	void SetText(Text p_text, int Count)
	{
		p_text.text = Count.ToString();
	}



}
