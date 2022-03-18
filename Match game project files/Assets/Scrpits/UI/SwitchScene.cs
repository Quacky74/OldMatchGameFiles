using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
	Button button;
	public string SceneName;

	private void OnEnable()
	{
		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(LoadNextScene);
	}

	void LoadNextScene()
	{
		SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
	}
}
