using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayLevelResulits : MonoBehaviour
{
	[SerializeField] private Text titleText;
	[SerializeField] private Image targetImage, feedbackImage;
	[SerializeField] private Sprite postiveFeedback, negtiveFeedback;
	[SerializeField] private TargetRequirements requirements;

	public GameSceneUIManager manager;

	private void OnEnable()
	{
		requirements = GameManger.Inst.currentRequirements;
	}

	private void Start()
	{
		targetImage.sprite = requirements.target.objSprite;
	}

	public void SetDisplayElements(bool hasPlayerWon)
	{
		titleText.text = hasPlayerWon ? "Level Complete" : "Level failed";
		feedbackImage.sprite = hasPlayerWon ? postiveFeedback : negtiveFeedback;
	}
}
