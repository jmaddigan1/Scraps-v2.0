using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance;

	[SerializeField] RectTransform dialoguePanel;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	void Start()
    {
        
    }

	public void SetText(string text) {
		dialoguePanel.GetComponentInChildren<Text>().text = text;
	}
}
