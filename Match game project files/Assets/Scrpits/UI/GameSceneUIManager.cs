using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUIManager: MonoBehaviour
{
	//UI Elements
	[SerializeField] private StartScreenUI startScreen;
	[SerializeField] private HudUi hudUi;
	[SerializeField] private DisplayLevelResulits displayResulits;

	//Game elements
	[SerializeField] private DropableObjectManager Spawner;
	[SerializeField] private GameManger GM;

	public event Action OnLoadUIStartScreen;

	private void OnEnable()
	{
		startScreen.OnStartGame += DisableStartScreen;
	}

	void Start()
	{
		EventsManager.Instance.OnOutOfMoves += DisplayResulits;
		EventsManager.Instance.OnTargetsCleared += DisplayResulits;
	}


	void EnableStartScreen()
	{
		startScreen.gameObject.SetActive(true);
	}

	void DisableStartScreen(bool p_isEnabled)
	{
		startScreen.gameObject.SetActive(p_isEnabled);
		EnableHudUi(true);
		OnLoadUIStartScreen?.Invoke();
		Spawner.enabled = true;//HACK
	}

	void EnableHudUi(bool p_isEnabled)
	{
		hudUi.gameObject.SetActive(p_isEnabled);
		hudUi.CallSetup();
	}

	void DisplayResulits(bool hasPlayerWon)
	{
		displayResulits.gameObject.SetActive(true);
		EnableHudUi(false);
		displayResulits.SetDisplayElements(hasPlayerWon);
	}

	void RestDisplay()
	{
		startScreen.gameObject.SetActive(true);
		displayResulits.gameObject.SetActive(false);
	}
}
