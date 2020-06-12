using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;

	private CanvasGroup loadingScreen;

	public float FadeSpeed = 5;
	private bool loading;
	private int current;

	// Setup Singleton
	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		// Find the loading screen
		// BUG:(Nathen) For some reason (In Editor) If you have the persistent scene selected this wont find the Loading screen
		// NOTE:(Nathen) This works fine if the selected scene in the one with the loading screen
		loadingScreen = GameObject.FindGameObjectWithTag("Loading").GetComponent<CanvasGroup>();

		// BUILD:(Nathen) This is for when we build the game
		// SceneManager.LoadSceneAsync((int)SceneIndex.MAIN_MENU, LoadSceneMode.Additive);
		// current = (int)SceneIndex.MAIN_MENU;

		// Get the current scene
		// NOTE:(Nathen) This checks if we started the game from a scene that wassent the persistent scene
		// BUILD:(Nathen) Remove this when we build
		if (SceneManager.GetActiveScene().buildIndex != (int)SceneIndex.MANAGER)
		{
			// We set the current index to the current scene so we can unload it later
			current = SceneManager.GetActiveScene().buildIndex;
		}
	}

	private void Update()
	{
		// Fade IN and OUT the loading screen
		if (loading)
		{
			loadingScreen.alpha += Time.deltaTime * FadeSpeed;

			if (loadingScreen.alpha == 1) {
				loadingScreen.GetComponent<Image>().raycastTarget = true;
			}
		}
		else
		{
			loadingScreen.alpha -= Time.deltaTime * FadeSpeed / 5;

			if (loadingScreen.alpha == 0) {
				loadingScreen.GetComponent<Image>().raycastTarget = false;
			}
		}
	}

	public void LoadLevel(SceneIndex toLoad)
	{
		StartCoroutine(coLoadLevel(toLoad));
	}

	IEnumerator coLoadLevel(SceneIndex toLoad)
	{
		loading = true;

		// Wait for the loading screen to fade in
		yield return new WaitForSecondsRealtime(0.5f);

		// If we are not the Manager scene
		if (current != (int)SceneIndex.MANAGER) {
			SceneManager.UnloadSceneAsync(current);
		}

		SceneManager.LoadSceneAsync((int)toLoad, LoadSceneMode.Additive);
		current = (int)toLoad;

		loading = false;
	}
}
