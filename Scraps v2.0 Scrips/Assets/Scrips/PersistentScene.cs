using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class PersistentScene
{
	static PersistentScene() {
		EditorSceneManager.sceneOpened += OnSceneOpenCallback;
	}

	static void OnSceneOpenCallback(Scene scene, OpenSceneMode sceneMode)
	{
		if (EditorSceneManager.GetSceneByBuildIndex((int)SceneIndex.MANAGER).isLoaded == false) {
			EditorSceneManager.OpenScene("Assets/Scenes/Persistent_Scene.unity", OpenSceneMode.Additive);
		}
	}
}
