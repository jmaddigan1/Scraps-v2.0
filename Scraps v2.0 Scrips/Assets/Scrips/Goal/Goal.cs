using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
	[SerializeField] GoalPoint robotPoint;
	[SerializeField] GoalPoint foxPoint;

	public SceneIndex ToLoad;

	private bool loadingNewLevel;

    // Update is called once per frame
    void Update()
    {
		if (foxPoint.occupied && robotPoint.occupied && loadingNewLevel == false) {
			LevelManager.Instance.LoadLevel(ToLoad);
			loadingNewLevel = true;
		}
    }
}
