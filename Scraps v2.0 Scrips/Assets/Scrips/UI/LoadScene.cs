using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class LoadScene : MonoBehaviour
{
	public SceneIndex ToLoad;

	private void Awake()
	{
		Button button = GetComponent<Button>();

		button.onClick.AddListener(Load);
	}

	private void Load()
	{
		LevelManager.Instance.LoadLevel(ToLoad);
	}
}
