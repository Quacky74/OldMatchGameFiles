using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartScreenUI : MonoBehaviour
{
	[SerializeField] Text countText;
	[SerializeField] Image targetImage;
	[SerializeField] private TargetRequirements requirement;
	public TargetRequirements a_req
	{
		get { return requirement; }
		set { requirement = value; }

	}
	[SerializeField] private Button startButton;
	public event Action<bool> OnStartGame;

	private void OnEnable()
	{
		startButton.onClick.AddListener(StartGame);
		
	}

	private void Start()
	{
		SetUIElements(GameManger.Inst.currentRequirements);
	}

	void SetUIElements(TargetRequirements newRequirements)
	{
		countText.text = newRequirements.quantity + " X";
		targetImage.sprite = newRequirements.target.objSprite;
	}


	void StartGame()
	{

		OnStartGame?.Invoke(false);
	}
}
